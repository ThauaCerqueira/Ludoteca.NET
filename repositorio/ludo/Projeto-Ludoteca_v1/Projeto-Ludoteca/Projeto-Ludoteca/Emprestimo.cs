namespace Projeto_Ludoteca;

using System;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static Utilitarios;

public class Emprestimo
{
    public static List<ListaJogosAlugados> RelatorioMembros = new();
    public static Membro EntradaCliente()
    {
        try
        {
            string cliente = Validacoes.ReceberEValidar<string>("Digite seu Nome de Cliente: ");
            cliente = Validacoes.FormatarEntrada(cliente);
            Membro membroEncontrado = Membro.Membros.FirstOrDefault(m => m.Nome == cliente);
            if (membroEncontrado != null)
            {
                return membroEncontrado;
            }
            else
            {
                throw new ArgumentException("\nEntrada Invalida. Voce nao esta cadastrado.Cadastre - se primeiro, e tente novamente.\n");
            }
        }
        catch (ArgumentException ex)
        {
            Print($"{ex.Message}");
            AvisoEntradaInvalida();
            return null;
        }
    }
    public static void EmprestarJogo()
    {
        Membro cliente = EntradaCliente();
        if (cliente == null)
            return;

        List<Jogo> Jogos = BibliotecaJogos.CriarEOuAcessarBiblioteca();

        while (true)
        {
            Clear();
            Print("EMPRESTIMO DE JOGO\n---------------------\n");
            Print("Veja abaixo o jogo que deseja alugar.\n");
            BibliotecaJogos.ListarJogos();

            string nomeJogo = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja alugar: ");
            nomeJogo = Validacoes.FormatarEntrada(nomeJogo);

            Jogo jogoEncontrado = Jogos.FirstOrDefault(j => j.Nome.Equals(nomeJogo));

            if (ValidarEExecutarEmprestimo(jogoEncontrado, cliente, Jogos))
                break;
        }
    }

    private static bool ValidarEExecutarEmprestimo(Jogo jogo, Membro cliente, List<Jogo> jogosDaBiblioteca)
    {
        if (jogo != null)
        {
            if (jogo.Status == "DISPONIVEL")
            {
                List<Jogo> ListaJogos = [];
                ListaJogos.Add(jogo);
                ListaJogosAlugados jogosAlugados = new(cliente.Id, cliente.Nome, ListaJogos);
                RelatorioMembros.Add(jogosAlugados);
                jogo.Status = "EMPRESTADO";
                BibliotecaJogos.SalvarBiblioteca(jogosDaBiblioteca);
                return true;
            }
            else
            {
                AvisoEntradaInvalida("\nEsse jogo ja esta emprestado. Tente outro.\n");
                return false;
            }
        }
        else
        {
            AvisoEntradaInvalida("\nEsse jogo nao existe na Biblioteca. Tente outro.\n");
            return false;
        }
    }

    public static void DevolverJogo()
    {
        Membro cliente = EntradaCliente();
        if (cliente == null)
            return;

        while (true)
        {
            Clear();
            Print("DEVOLUCAO DE JOGO\n---------------------\n");
            Print("Veja abaixo sua lista de jogos alugados. Qual deseja devolver?\n");
            ListaJogosAlugados dadosCliente = RelatorioMembros.FirstOrDefault(j => j.Nome.Equals(cliente.Nome));
            if (dadosCliente != null)
            {
                foreach (Jogo jogo in dadosCliente.ListaJogos)
                {
                    Print($"ID: {jogo.Id:D6} | NOME: {jogo.Nome} | Preco: {jogo.Preco:C} | Status: {jogo.Status}");
                }

                string nomeJogo = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja devolver: ");
                nomeJogo = Validacoes.FormatarEntrada(nomeJogo);

                Jogo jogoEncontrado = dadosCliente.ListaJogos.FirstOrDefault(j => j.Nome.Equals(nomeJogo));

                if (ValidarEExecutarDevolucao(jogoEncontrado, cliente, dadosCliente.ListaJogos))
                    break;
            }
            else
                AvisoEntradaInvalida("\nVoce nao tem jogos para devolver.\n");
        }
    }

    private static bool ValidarEExecutarDevolucao(Jogo jogo, Membro cliente, List<Jogo> listaJogos)
    {
        if (jogo != null)
        {
            List<Jogo> jogosDaBiblioteca = BibliotecaJogos.CriarEOuAcessarBiblioteca();
            Jogo jogoDaLib = jogosDaBiblioteca.FirstOrDefault(j => j.Nome.Equals(jogo.Nome));
            jogoDaLib.Status = "DISPONIVEL";

            listaJogos.Remove(jogo);
            
            BibliotecaJogos.SalvarBiblioteca(jogosDaBiblioteca);
            return true;
        }
        else
        {
            AvisoEntradaInvalida("\nEsse jogo nao existe na sua lista de jogos alugados. Tente outro.\n");
            return false;
        }
    }
}

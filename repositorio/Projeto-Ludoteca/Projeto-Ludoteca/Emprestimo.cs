namespace Projeto_Ludoteca;

using System;
using static Utilitarios;

public class Emprestimo
{
    public static List<ListaJogosAlugados> RelatorioMembros = new(); //Cria e instancia RelatorioMembros. Que eh uma lista de ListaJogosAlugados.

    // Tenta acessar um Cliente. Retorna o Cliente se ele estiver cadastrado,
    // caso contrario lanca um erro.
    public static Membro EntradaCliente()
    {
        // [AV1-5] --- inicio da serialização
        try
        {
            string cliente = Validacoes.ReceberEValidar<string>("Digite seu Nome de Cliente: "); //Explicacao comentada em: Validacoes
            cliente = Validacoes.FormatarEntrada(cliente); //Explicacao comentada em: Validacoes
            Membro membroEncontrado = Membro.Membros.FirstOrDefault(m => m.Nome == cliente);
            if (membroEncontrado != null)
            {
                AvisoEPressKey("\nCliente acessado"); //Explicacao comentada em: Utilitarios
                return membroEncontrado;
            }
            else
            {
                throw new ArgumentException("\nEntrada Invalida. Voce nao esta cadastrado.Cadastre - se primeiro, e tente novamente.\n");
            }
        }
        catch (ArgumentException ex)
        {
            Print($"{ex.Message}"); //Explicacao comentada em: Utilitarios
            AvisoEPressKey(); //Explicacao comentada em: Utilitarios
            return null;
        }
        // [AV1-5] --- fim
    }

    //Verifica o acesso do Cliente, acessa a lista de jogos contidos no arquivo da biblioteca,
    //mostra os jogos na lista, recebe o jogo a alugar, e chama uma funcao pra validar e realizar
    //o emprestimo
    public static void EmprestarJogo()
    {
        Membro cliente = EntradaCliente();
        if (cliente == null)
            return;

        List<Jogo> Jogos = BibliotecaJogos.CarregarBiblioteca();

        while (true)
        {
            Clear(); //Explicacao comentada em: Utilitarios
            Print("EMPRESTIMO DE JOGO\n---------------------\n"); //Explicacao comentada em: Utilitarios
            Print("Veja abaixo o jogo que deseja alugar.\n"); //Explicacao comentada em: Utilitarios
            BibliotecaJogos.ListarJogos();

            string nomeJogo = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja alugar ou 'sair' para sair: "); //Explicacao comentada em: Validacoes

            if (nomeJogo.ToUpper() == "SAIR")
                break;

            nomeJogo = Validacoes.FormatarEntrada(nomeJogo); //Explicacao comentada em: Validacoes

            Jogo jogoEncontrado = Jogos.FirstOrDefault(j => j.Nome.Equals(nomeJogo));   
            if (ValidarEExecutarEmprestimo(jogoEncontrado, cliente, Jogos))
                break;
        }
    }

    //Verifica se eh possivel executar o emprestimo, caso sim executa, caso nao da erro
    // [AV1-2] --- inicio da serialização
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
                AvisoEPressKey($"\nO Jogo: {jogo.Nome} foi alugado com sucesso!!!"); //Explicacao comentada em: Utilitarios
                return true;
            }
            else
            {
                AvisoEPressKey("\nEsse jogo ja esta emprestado. Tente outro.\n"); //Explicacao comentada em: Utilitarios
                return false;
            }
        }
        else
        {
            AvisoEPressKey("\nEsse jogo nao existe na Biblioteca. Tente outro.\n"); //Explicacao comentada em: Utilitarios
            return false;
        }
    }
    // [AV1-2] --- fim

    //Verifica o acesso do Cliente, tenta acessar a lista de jogos alugados pelo Cliente, se nao houver
    //jogos, da erro, se houver, mostra os jogos na lista, recebe o jogo a devolver, e chama uma funcao
    //pra validar e realizar a devolucao
    public static void DevolverJogo()
    {
        Membro cliente = EntradaCliente();
        if (cliente == null)
            return;

        while (true)
        {
            ListaJogosAlugados dadosCliente = RelatorioMembros.FirstOrDefault(j => j.Nome.Equals(cliente.Nome));
            if (dadosCliente != null)
            {
                Clear(); //Explicacao comentada em: Utilitarios
                Print("DEVOLUCAO DE JOGO\n---------------------\n"); //Explicacao comentada em: Utilitarios
                Print("Veja abaixo sua lista de jogos alugados. Qual deseja devolver?\n"); //Explicacao comentada em: Utilitarios
                foreach (Jogo jogo in dadosCliente.ListaJogos)
                {
                    Print($"ID: {jogo.Id:D6} | NOME: {jogo.Nome} | Preco: {jogo.Preco:C} | Status: {jogo.Status}"); //Explicacao comentada em: Utilitarios
                }
                string nomeJogo = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja devolver: "); //Explicacao comentada em: Validacoes
                nomeJogo = Validacoes.FormatarEntrada(nomeJogo); //Explicacao comentada em: Validacoes
                Jogo jogoEncontrado = dadosCliente.ListaJogos.FirstOrDefault(j => j.Nome.Equals(nomeJogo));
                if (ValidarEExecutarDevolucao(jogoEncontrado, cliente, dadosCliente.ListaJogos))
                    break;
            }
            else
            {
                AvisoEPressKey("\nVoce nao tem jogos para devolver.\n"); //Explicacao comentada em: Utilitarios
                break;
            }
        }
    }

    //Verifica se eh possivel executar a devolucao, caso sim executa, caso nao da erro
    // [AV1-2] --- inicio da serialização
    private static bool ValidarEExecutarDevolucao(Jogo jogo, Membro cliente, List<Jogo> listaJogos)
    {
        if (jogo != null)
        {
            List<Jogo> jogosDaBiblioteca = BibliotecaJogos.CarregarBiblioteca();
            Jogo jogoDaLib = jogosDaBiblioteca.FirstOrDefault(j => j.Nome.Equals(jogo.Nome));
            jogoDaLib.Status = "DISPONIVEL";

            listaJogos.Remove(jogo);
            
            BibliotecaJogos.SalvarBiblioteca(jogosDaBiblioteca);
            AvisoEPressKey($"\nO Jogo: {jogo.Nome} foi devolvido com sucesso!!!"); //Explicacao comentada em: Utilitarios
            return true;
        }
        else
        {
            AvisoEPressKey("\nEsse jogo nao existe na sua lista de jogos alugados. Tente outro.\n"); //Explicacao comentada em: Utilitarios
            return false;
        }
    }
    // [AV1-2] --- fim

    //Muda todos os jogos para "DISPONIVEL" quando o programa for fechar, para evitar erros
    //por ausencia de um arquivo externo de Membros e seus jogos alugados, e salva no arquivo
    public static void ZerarEmprestimos()
    {
        List<Jogo> jogosDaBiblioteca = BibliotecaJogos.CarregarBiblioteca();
        
        foreach(Jogo jogo in jogosDaBiblioteca)
        {
            if (jogo.Status == "EMPRESTADO")
                jogo.Status = "DISPONIVEL";
        }

        BibliotecaJogos.SalvarBiblioteca(jogosDaBiblioteca);
    }
}

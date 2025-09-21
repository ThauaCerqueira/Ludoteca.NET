using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

namespace Projeto_Ludoteca;

using static Utilitarios;

public static class BibliotecaJogos
{
    public static List<Jogo> Jogos = new();

    private static string bibliotecaJogos = "biblioteca.json"; 
    public static void ListarJogos()
    {
        Print("LISTAGEM DE JOGOS\n");
        Print("---------------------\n");

        if (File.Exists(bibliotecaJogos))
        {
            string jsonString = File.ReadAllText(bibliotecaJogos);
            Jogos = JsonSerializer.Deserialize<List<Jogo>>(jsonString);

            foreach (Jogo jogo in Jogos)
            {
                Print($"ID: {jogo.Id:D6} | NOME: {jogo.Nome} | Preco: {jogo.Preco:C} | Status: {jogo.Status}");
            }

            PrintInLine("\nPressione qualquer tecla...");
            Console.ReadKey();
        }
        else
        {
            AvisoEntradaInvalida("\nAinda nao ha jogos cadastrados. A biblioteca de jogos esta vazia.\n");
        }
    }

    public static void CadastrarJogo()
    {
        Print("CADASTRO DE JOGOS\n---------------------\n");
        Jogos = CriarEOuAcessarBiblioteca();
        Jogo jogo = new(Jogos.Count + 1);

        while (true)
        {
            string nome = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja cadastrar: ");
            jogo.Nome = Validacoes.FormatarEntrada(nome);

            if (!Jogos.Any(j => j.Nome.Equals(jogo.Nome)))
            {   
                do
                    jogo.Preco = Validacoes.ReceberEValidar<decimal>("Digite o preco do emprestimo do jogo: ");
                while (!Validacoes.ValidarEntradaZero<decimal>(jogo.Preco));

                Jogos.Add(jogo);
                SalvarBiblioteca(Jogos);
                break;
            }
            else
                AvisoEntradaInvalida("\nEsse jogo já existe na Biblioteca. Tente outro.\n");
        }
    }

    public static List<Jogo> CriarEOuAcessarBiblioteca()
    {
        if (!File.Exists(bibliotecaJogos))
        {
            Console.WriteLine("O arquivo não existe. Criando agora...");
            string conteudoJsonInicial = "[]";
            File.WriteAllText(bibliotecaJogos, conteudoJsonInicial);

            Console.WriteLine($"Arquivo '{bibliotecaJogos}' criado com sucesso!");
        }

        string jsonString = File.ReadAllText(bibliotecaJogos);
        return JsonSerializer.Deserialize<List<Jogo>>(jsonString);
    }

    public static void SalvarBiblioteca(List<Jogo> ListaJogosPraSalvar)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        string jsonPraSalvar = JsonSerializer.Serialize(ListaJogosPraSalvar, options);
        File.WriteAllText(bibliotecaJogos, jsonPraSalvar);
    }
}

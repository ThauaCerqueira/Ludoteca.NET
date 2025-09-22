using System.Text.Json;

namespace Projeto_Ludoteca;

using static Utilitarios;

public static class BibliotecaJogos
{
    public static List<Jogo> Jogos = new();

    //Um Campo com uma variavel usada para armazenar o nome do arquivo JSON
    // [AV1-2] --- inicio da serialização
    private static string bibliotecaJogos = "biblioteca.json";
    // [AV1-2] --- fim

    //Verifica se ha jogos cadastrados, caso haja exibe eles e seus atributos por meio de um loop,
    //caso nao, da erro
    public static void ListarJogos()
    {
        Print("LISTAGEM DE JOGOS\n"); //Explicacao comentada em: Utilitarios
        Print("---------------------\n"); //Explicacao comentada em: Utilitarios

        List<Jogo> Jogos = CarregarBiblioteca();

        if (Jogos.Count > 0)
        {
            Jogos = CarregarBiblioteca();

            foreach (Jogo jogo in Jogos)
            {
                Print($"ID: {jogo.Id:D6} | NOME: {jogo.Nome} | Preco: {jogo.Preco:C} | Status: {jogo.Status}");
            }

            AvisoEPressKey();  //Explicacao comentada em: Utilitarios
        }
        else
        {
            AvisoEPressKey("\nAinda nao ha jogos cadastrados. A biblioteca de jogos esta vazia.\n"); //Explicacao comentada em: Utilitarios
        }
    }

    //Recebe uma lista de jogos carregada da funcao CarregarBiblioteca(), recebe os dados do jogo, ou um comando de saida do metodo,
    //caso nao de erro na validacao, adiciona o jogo na lista de jogos, e atualiza a biblioteca chamando SalvarBiblioteca()
    public static void CadastrarJogo()
    {
        Print("CADASTRO DE JOGOS\n---------------------\n");
        Jogos = CarregarBiblioteca();
        Jogo jogo = new(Jogos.Count + 1);

        while (true)
        {
            string nome = Validacoes.ReceberEValidar<string>("Digite o nome do jogo que deseja cadastrar ou 'sair' para sair: "); //Explicacao comentada em: Validacoes
            if (nome.ToUpper() == "SAIR")
                break;
            jogo.Nome = Validacoes.FormatarEntrada(nome); //Explicacao comentada em: Validacoes

            if (!Jogos.Any(j => j.Nome.Equals(jogo.Nome)))
            {   
                do
                    jogo.Preco = Validacoes.ReceberEValidar<decimal>("Digite o preco do emprestimo do jogo: "); //Explicacao comentada em: Validacoes
                while (!Validacoes.ValidarEntradaZero<decimal>(jogo.Preco)); //Explicacao comentada em: Validacoes

                Jogos.Add(jogo);
                SalvarBiblioteca(Jogos);
                AvisoEPressKey($"\nO Jogo: {jogo.Nome} foi cadastrado com sucesso!!!"); //Explicacao comentada em: Utilitarios
                break;
            }
            else
                AvisoEPressKey("\nEsse jogo já existe na Biblioteca. Tente outro.\n"); //Explicacao comentada em: Utilitarios
        }
    }

    //Cria o arquivo da biblioteca adicionando uma lista vazia dentro
    // [AV1-2] --- inicio da serialização
    private static void CriarBiblioteca()
    {
        Print("O arquivo não existe. Criando agora..."); //Explicacao comentada em: Utilitarios
        string conteudoJsonInicial = "[]";
        File.WriteAllText(bibliotecaJogos, conteudoJsonInicial);

        Print($"Arquivo '{bibliotecaJogos}' criado com sucesso!"); //Explicacao comentada em: Utilitarios
    }
    // [AV1-2] --- fim

    //Atualiza a biblioteca adicionando a lista de jogos nela
    // [AV1-3] --- inicio da serialização
    public static void SalvarBiblioteca(List<Jogo> ListaJogosPraSalvar)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
        string jsonPraSalvar = JsonSerializer.Serialize(ListaJogosPraSalvar, options);
        File.WriteAllText(bibliotecaJogos, jsonPraSalvar);
    }
    // [AV1-3] --- inicio da serialização

    //Carrega a biblioteca, retornando a lista de jogos presente nela
    // [AV1-3] --- inicio da serialização
    public static List<Jogo> CarregarBiblioteca()
    {
        if (!File.Exists(bibliotecaJogos))
            CriarBiblioteca();
        string jsonString = File.ReadAllText(bibliotecaJogos);
        return JsonSerializer.Deserialize<List<Jogo>>(jsonString);
    }
    // [AV1-3] --- fim
}

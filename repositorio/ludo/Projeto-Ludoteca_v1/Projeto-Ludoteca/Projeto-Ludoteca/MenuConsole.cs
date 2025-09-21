namespace Projeto_Ludoteca;

using static Utilitarios;

public class MenuConsole
{
    public static string menu = "=== LUDOTECA .NET ===\r\n" +
        "1 Cadastrar jogo\r\n" +
        "2 Cadastrar membro\r\n" +
        "3 Listar jogos\r\n" +
        "4 Emprestar jogo\r\n" +
        "5 Devolver jogo\r\n" +
        "0 Sair\r\n" +
        "Opção: ";

    public static void Menu()
    {
        while (true)
        {

            int opcao = Validacoes.ReceberEValidar<int>(menu);
            Clear();

            if (opcao == 1)
                BibliotecaJogos.CadastrarJogo();
            else if (opcao == 2)
                Membro.CadastrarMembro();
            else if (opcao == 3)
                BibliotecaJogos.ListarJogos();
            else if (opcao == 4)
                Emprestimo.EmprestarJogo();
            else if (opcao == 5)
                Emprestimo.DevolverJogo();
            else if (opcao == 0)
                break;
            else
            {
                AvisoEntradaInvalida("\nDigite um numero que seja pertencente a alguma das opcoes\n");
            }
            Clear();
        }
    }
}

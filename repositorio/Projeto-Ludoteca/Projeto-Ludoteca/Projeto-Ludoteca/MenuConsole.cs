namespace Projeto_Ludoteca;

using static Utilitarios;

public static class MenuConsole
{
    //Um Campo com uma variavel usada para exibir o Loyout do Menu
    // [AV1-2] --- início da serialização
    private static string menu = "=== LUDOTECA .NET ===\r\n" +
        "1 Cadastrar jogo\r\n" +
        "2 Cadastrar membro\r\n" +
        "3 Listar jogos\r\n" +
        "4 Emprestar jogo\r\n" +
        "5 Devolver jogo\r\n" +
        "0 Sair\r\n" +
        "Opção: ";
    // [AV1-2] --- fim


    //Um metodo que exibe o menu e executa as funcoes minimas, dependendo da
    //opcao escolhida, e validando a mesma.
    public static void Menu() 
    {
        while (true)
        {

            int opcao = Validacoes.ReceberEValidar<int>(menu); //Explicacao comentada em: Validacoes
            Clear(); //Explicacao comentada em: Utilitarios

            if (opcao == 1)
                // [AV1-4-Cadastrar Jogo] --- início da serialização
                BibliotecaJogos.CadastrarJogo();
            // [AV1-4-Cadastrar Jogo] --- fim
            else if (opcao == 2)
                // [AV1-4-Cadastrar Membro] --- início da serialização
                Membro.CadastrarMembro();
            // [AV1-4-Cadastrar Membro] --- fim
            else if (opcao == 3)
                // [AV1-4-Listar Jogos] --- início da serialização
                BibliotecaJogos.ListarJogos();
            // [AV1-4-Listar Jogos] --- fim
            else if (opcao == 4)
                // [AV1-4-Emprestar Jogo] --- início da serialização
                Emprestimo.EmprestarJogo();
            // [AV1-4-Emprestar Jogo] --- fim
            else if (opcao == 5)
                // [AV1-4-Devolver Jogo] --- início da serialização
                Emprestimo.DevolverJogo();
            // [AV1-4-Devolver Jogo] --- fim
            else if (opcao == 0)
            {
                // [AV1-4-Sair] --- inicio da serialização
                Emprestimo.ZerarEmprestimos();
                break;
                // [AV1-4-Sair] --- fim
            }
            else
            {
                AvisoEPressKey("\nDigite um numero que seja pertencente a alguma das opcoes\n"); //Explicacao comentada em: Utilitarios
            }
            Clear(); //Explicacao comentada em: Utilitarios
        }
        AvisoEPressKey("\nObrigado por usar LUDOTECA. Ate a proxima!!!\n"); //Explicacao comentada em: Utilitarios
    }
}

namespace Projeto_Ludoteca;

public static class Utilitarios
{
    public static void Print(string mensagem)
    {
        Console.WriteLine(mensagem);
    }
    public static void PrintInLine(string mensagem)
    {
        Console.Write(mensagem);
    }

    public static string Input(string mensagem = "")
    {
        if (mensagem != "")
            PrintInLine(mensagem);

        return Console.ReadLine();
    }

    public static void PressKey()
    {
        Console.ReadKey();
    }

    public static void AvisoEntradaInvalida(string mensagem = "")
    {
        if(mensagem != "")
            Print(mensagem);
        PrintInLine("Digite qualquer coisa pra continuar...");
        PressKey();
        Clear();
    }

    public static void Clear()
    {
        Console.Clear();
    }
}

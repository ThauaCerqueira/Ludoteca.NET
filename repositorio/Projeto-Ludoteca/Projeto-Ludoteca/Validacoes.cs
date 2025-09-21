using System.Globalization;

namespace Projeto_Ludoteca;

using static Utilitarios;
public class Validacoes
{
    //Recebe e valida dados, dependendo do Tipo de dado recebido
    public static Tipo ReceberEValidar<Tipo>(string mensagem = "")
    {
        while (true)
        {
            if (mensagem != "")
                PrintInLine(mensagem); //Explicacao comentada em: Utilitarios

            string entrada = Input();
            if (string.IsNullOrEmpty(entrada))
            {
                AvisoEPressKey("\nEntrada invalida. Digite algo. Tente novamente.\n"); //Explicacao comentada em: Utilitarios
                continue;
            }
            else if (typeof(Tipo) == typeof(int))
            {
                if (int.TryParse(entrada, out int result))
                    return (Tipo)(object)result;
            }
            else if (typeof(Tipo) == typeof(decimal))
            {
                if (decimal.TryParse(entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    return (Tipo)(object)result;
            }
            else if (typeof(Tipo) == typeof(string))
            {
                if (!int.TryParse(entrada, out int resultInt) && !decimal.TryParse(entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal resultDecimal))
                    return (Tipo)(object)entrada;
            }
            AvisoEPressKey("\nEntrada invalida. Tente novamente.\n"); //Explicacao comentada em: Utilitarios
        }
    }

    //Transforma o texto para um formato padrao
    public static string FormatarEntrada(string texto)
    {
        string novoTexto = "";

        int contadorEspacos = 0;

        foreach (Char c in texto)
        {
            if (c != ' ')
            {
                novoTexto += c;
                contadorEspacos = 0;
            }
            else if (c == ' ')
            {
                if (contadorEspacos < 1)
                    novoTexto += c;
                contadorEspacos++;
            }
        }

        return novoTexto.ToUpper();
    }

    //Verifica se a entrada eh zero, retornando um booleano
    public static bool ValidarEntradaZero<Tipo>(Tipo entrada)
    {
        if (System.Collections.Generic.EqualityComparer<Tipo>.Default.Equals(entrada, default(Tipo)))
        {
            AvisoEPressKey($"\nEntrada Invalida. O valor nao pode ser 0 (ZERO). Tente um valor valido.\n"); //Explicacao comentada em: Utilitarios
            return false;
        }
        else
            return true;
    }
}

using System.Globalization;
using System.Text.Json;

namespace Projeto_Ludoteca;

using static Utilitarios;
public class Validacoes
{
    public static Tipo ReceberEValidar<Tipo>(string mensagem = "")
    {
        while (true)
        {
            if (mensagem != "")
                PrintInLine(mensagem);

            string entrada = Input();
            if (string.IsNullOrEmpty(entrada))
            {
                AvisoEntradaInvalida("\nEntrada invalida. Digite algo. Tente novamente.\n");
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
            AvisoEntradaInvalida("\nEntrada invalida. Tente novamente.\n");
        }
    }

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

    public static bool ValidarEntradaZero<Tipo>(Tipo entrada)
    {
        if (System.Collections.Generic.EqualityComparer<Tipo>.Default.Equals(entrada, default(Tipo)))
        {
            AvisoEntradaInvalida($"\nEntrada Invalida. O valor nao pode ser 0 (ZERO). Tente um valor valido.\n");
            return false;
        }
        else
            return true;
    }
}

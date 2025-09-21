namespace Projeto_Ludoteca;

using System.Linq.Expressions;
using System.Text.Json;
using static Utilitarios;

public class Membro
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public static List<Membro> Membros { get; set; } = new();

    public Membro(int id)
    {
        this.Id = id;
    }

    public static void CadastrarMembro()
    {
        Print("CADASTRO DE MEMBRO\n");
        Print("---------------------\n");

        Membro membro = new(Membros.Count + 1);

        while (true)
        {
            string nome = Validacoes.ReceberEValidar<string>("Digite o nome do membro: ");
            membro.Nome = Validacoes.FormatarEntrada(nome);
            if (membro.Nome.Any(char.IsDigit))
                AvisoEntradaInvalida("\nEntrada Invalida. O nome do membro so pode conter letras. Tente Novamente\n");
            else
                break;
        }

        if (!Membros.Any(m => m.Nome.Equals(membro.Nome)))
        {
            Membros.Add(membro);
            ListaJogosAlugados jogosAlugados = new(membro.Id, membro.Nome, []);
        }
        else
        {
            AvisoEntradaInvalida("\nEsse membro ja esta cadastrado. Ele ja pode alugar um jogo.\n");
        }
    }
}

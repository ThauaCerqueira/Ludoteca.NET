namespace Projeto_Ludoteca;

using static Utilitarios;

public class Membro
{
    // [AV1-2] --- inicio da serialização
    public int Id { get; private set; }
    // [AV1-2] --- fim
    public string Nome { get; set; } = string.Empty;
    public static List<Membro> Membros { get; set; } = new(); //Cria e instancia Membros. Que eh uma lista de Membros.

    public Membro(int id)
    {
        this.Id = id;
    }

    //Recebe e valida os dados do Membro, e caso nao haja erro, adiciona na lista de Membros
    public static void CadastrarMembro()
    {
        Print("CADASTRO DE MEMBRO\n"); //Explicacao comentada em: Utilitarios
        Print("---------------------\n"); //Explicacao comentada em: Utilitarios

        Membro membro = new(Membros.Count + 1);

        while (true)
        {
            string nome = Validacoes.ReceberEValidar<string>("Digite o nome do membro ou 'sair' para sair: "); //Explicacao comentada em: Validacoes
            if (nome.ToUpper() == "SAIR")
                return;
            membro.Nome = Validacoes.FormatarEntrada(nome); //Explicacao comentada em: Validacoes
            if (membro.Nome.Any(char.IsDigit))
                AvisoEPressKey("\nEntrada Invalida. O nome do membro so pode conter letras. Tente Novamente\n"); //Explicacao comentada em: Utilitarios
            else
                break;
        }

        if (!Membros.Any(m => m.Nome.Equals(membro.Nome)))
        {
            Membros.Add(membro);
            AvisoEPressKey($"\nO membro: {membro.Nome} foi cadastrado com sucesso!!!"); //Explicacao comentada em: Utilitarios
            ListaJogosAlugados jogosAlugados = new(membro.Id, membro.Nome, []);
        }
        else
        {
            AvisoEPressKey("\nEsse membro ja esta cadastrado. Ele ja pode alugar um jogo.\n"); //Explicacao comentada em: Utilitarios
        }
    }
}

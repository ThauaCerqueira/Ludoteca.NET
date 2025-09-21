namespace Projeto_Ludoteca;

public class ListaJogosAlugados
{
    private int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Jogo> ListaJogos { get; set; } = new();
    public ListaJogosAlugados(int id, string Nome, List<Jogo> ListaJogos)
    {
        this.Id = id;
        this.Nome = Nome;
        this.ListaJogos = ListaJogos;
    }
}

using System.Diagnostics.Metrics;

namespace Projeto_Ludoteca;

public class ListaJogosAlugados 
{
    // [AV1-2] --- inicio da serialização
    private int Id { get; set; }
    // [AV1-2] --- fim
    public string Nome { get; set; } = string.Empty;
    public List<Jogo> ListaJogos { get; set; } = new();
    public ListaJogosAlugados(int id, string Nome, List<Jogo> ListaJogos)
    {
        this.Id = id;
        this.Nome = Nome;
        this.ListaJogos = ListaJogos;
    }
}

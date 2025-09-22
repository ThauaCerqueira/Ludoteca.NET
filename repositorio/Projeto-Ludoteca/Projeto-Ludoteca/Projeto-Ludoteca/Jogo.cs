namespace Projeto_Ludoteca;

public class Jogo
{
    // [AV1-2] --- inicio da serialização
    public int Id { get; private set; }
    // [AV1-2] --- fim
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Status { get; set; } = "DISPONIVEL";
    public Jogo(int id)
    {
        this.Id = id;
    }
}

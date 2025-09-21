namespace Projeto_Ludoteca;

using static Utilitarios;
public class Jogo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Status { get; set; } = "DISPONIVEL";
    public Jogo(int id)
    {
        this.Id = id;
    }
}

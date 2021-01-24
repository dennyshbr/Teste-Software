namespace LeilaoOnline.Core.Classes
{
    public class Interessado
    {
        public string Nome { get; set; }

        public Leilao Leilao { get; set; }

        public Interessado(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }
    }
}

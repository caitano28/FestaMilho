using SQLite;

namespace FestaMilho.Model
{
    public class Cardapio
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string FormaPagamento { get; set; }
        public string Valor { get; set; }
    

        public Cardapio()
        {
          //  ValorFormatado = string.Format("{0:0.00}",Valor);
        }
    }

}

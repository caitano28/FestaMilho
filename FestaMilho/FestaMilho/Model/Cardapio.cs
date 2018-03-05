using SQLite;

namespace FestaMilho.Model
{
    public class Cardapio
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string _id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string formaPagamento { get; set; }
        public string valor { get; set; }
    

        public Cardapio()
        {
          //  ValorFormatado = string.Format("{0:0.00}",Valor);
        }
    }

}

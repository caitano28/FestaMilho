using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
     public class BarracaReturn
    {
       // public virtual IList<CardapioReturn> cardapios { get; set; }
        [PrimaryKey]
        public string _id { get; set; }
        public string nome { get; set; }
        public string curso { get; set; }
        public string semestre { get; set; }
        public string periodo { get; set; }
        public string localizacao { get; set; }
        public string formapagamento { get; set; }
       // public virtual UsuarioReturn usuario { get; set; }
        public int __v { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;

namespace FestaMilho.Model
{
    public class Barraca
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string _id { get; set; }
        public string Nome { get; set; }
        public Curso Curso { get; }
        public String Localizacao { get; set; }
        public int Visitantes { get; set; }
        public double Avaliacao { get; set; }
        public string Cor { get; set; }
        public List<Cardapio> Pratos { get; set; }
       // public List<Avaliacao> Avaliacoes { get; set; }
    }
}

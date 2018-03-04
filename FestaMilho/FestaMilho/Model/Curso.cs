using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
    public class Curso
    {
        [PrimaryKey]
        public string _id { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Semestre { get; set; }
        public string Periodo { get; set; }

    }
}

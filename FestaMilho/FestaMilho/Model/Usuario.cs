using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
     public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string _id  { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string Token { get; set; }
        public bool LembrarSenha { get; set; }
        // public Barraca barraca { get; }
        public int nivel { get; set; }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}

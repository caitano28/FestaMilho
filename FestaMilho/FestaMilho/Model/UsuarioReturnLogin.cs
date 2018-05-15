using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
    public class UsuarioReturnLogin
    {
        public int nivel { get; set; }
        public string _id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataCreate { get; set; }
        public int __v { get; set; }
    }
}

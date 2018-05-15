using FestaMilho.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
    public class LoginReturn
    {
        public UsuarioReturnLogin usuario { get; set; }
        public String token { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FestaMilho.Model
{
   public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public List<CardapioReturn> CardapioResult { get; set; }
        public List<BarracaReturn> BarracaResult { get; set; }
        public List<MediaAvaliacao> MediaAvaliacao { get; internal set; }
        public bool IsReLogin { get; internal set; }
        public LoginReturn Usuario { get; internal set; }
    }
}

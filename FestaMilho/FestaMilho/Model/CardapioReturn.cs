using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Model
{
    public class CardapioReturn
    {
        [Newtonsoft.Json.JsonProperty("_id")]
        [PrimaryKey]
        public string _id { get; set; }
        [Newtonsoft.Json.JsonProperty("nomeprato")]
        public string nomeprato { get; set; }
        [Newtonsoft.Json.JsonProperty("valor")]
        public string valor { get; set; }
        [Newtonsoft.Json.JsonProperty("descricao")]
        public string descricao { get; set; }
        [Newtonsoft.Json.JsonProperty("barraca")]
        public string barraca { get; set; }
        [Newtonsoft.Json.JsonProperty("__V")]
        public string __V { get; set; }
        
    }
}

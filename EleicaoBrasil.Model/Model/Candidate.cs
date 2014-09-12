using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model
{
    public class Candidate
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string idTitleJob { get; set; }
        public int total { get; set; }
        public int qtd { get; set; }
        public float average { get; set; }
    }
}

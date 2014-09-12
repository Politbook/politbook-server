using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public string photo { get; set; }
        public string email { get; set; }
        public string idSocial { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EleicaoBrasil.Model
{
    public class Comment
    {
        [Key, Column(Order = 0), ForeignKey("user")]
        public int idUser { get; set; }

        [Key, Column(Order = 1), ForeignKey("candidate")]
        public string idCandidate { get; set; }

        public int rating { get; set; }

        public DateTime date { get; set; }

        public string comment { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public virtual User user { get; set; }

        public virtual Candidate candidate { get; set; }

    }
}

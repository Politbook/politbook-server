using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model
{
    public class EleicaoContext : DbContext
    {
        public EleicaoContext()
            : base("name=EleicaoContext")
        {
        }



        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
    }
}

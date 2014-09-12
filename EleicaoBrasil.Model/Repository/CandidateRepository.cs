using EleicaoBrasil.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model.Repository
{
    public class CandidateRepository : IDisposable, ICandidateRepository
    {
        private EleicaoContext db = new EleicaoContext();

        public object GetCandidates(string idTitleJob, string state)
        {

            var comments = (from p in db.Candidates
                           where p.idTitleJob.Equals(idTitleJob) && (p.state.Equals(state) || idTitleJob.Equals("1"))
                           select new { id = p.id, qtd = p.qtd, average = p.average, total = p.total }).ToList();

            return comments;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

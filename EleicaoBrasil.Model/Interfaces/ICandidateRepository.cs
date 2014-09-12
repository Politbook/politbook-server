using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleicaoBrasil.Model.Interfaces
{
    public interface ICandidateRepository
    {
        object GetCandidates(string idTitleJob, string state);
    }
}

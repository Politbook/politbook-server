using EleicaoBrasil.Model;
using EleicaoBrasil.Model.Interfaces;
using EleicaoBrasil.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EleicaoBrasil.Web.Controllers
{
    public class CandidatesController : ApiController
    {
        ICandidateRepository _repository;

        public CandidatesController(ICandidateRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/v1/candidates")]
        public HttpResponseMessage Get(string idTitleJob, string state, int _offset = 20)
        {
            try
            {
                object o = _repository.GetCandidates(idTitleJob, state);

                return this.Request.CreateResponse(HttpStatusCode.OK, o);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
 
        }
    }
}
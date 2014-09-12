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
    public class UsersController : ApiController
    {
        IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("api/v1/users")]
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                User user1 = _repository.Add(user);

                return this.Request.CreateResponse<User>(HttpStatusCode.OK, user1);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
 
        }
    }
}
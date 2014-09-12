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
    public class CommentsController : ApiController
    {
        ICommentRepository _repository;

        public CommentsController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("api/v1/comments/{idCandidate}")]
        public HttpResponseMessage Get(string idCandidate, [FromUri] int _page = 1, [FromUri] int _offset = 10)
        {
            try
            {
                var comments = _repository.GetCommentsCandidate(idCandidate, _page, _offset);
                return this.Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
        }

        [HttpGet]
        [Route("api/v1/comments/{idCandidate}/users/{idUser}")]
        public HttpResponseMessage User(string idCandidate, int idUser)
        {
            try
            {
                object o = getCommentsUser(idCandidate, idUser);
                return this.Request.CreateResponse(HttpStatusCode.OK, o);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
        }

        private object getCommentsUser(string idCandidate, int idUser)
        {
            var commentsCandidate = _repository.GetCommentsCandidate(idCandidate);
            var commentUser = _repository.GetComment(idUser, idCandidate);
            var candidate = _repository.GetCandidateRating(idCandidate);

            var r = new { candidateAverage = candidate != null ? candidate.average : 0, 
                candidateQtd =  candidate != null ? candidate.qtd : 0,
                commentUser = commentUser, commentsCandidate = commentsCandidate };

            return r;
        }

        [HttpPost]
        [Route("api/v1/comments/{idCandidate}/users/{idUser}")]
        public HttpResponseMessage Post(string idCandidate, int idUser, [FromBody] Comment comment)
        {
            try
            {
                comment.idUser = idUser;
                comment.idCandidate = idCandidate;
                _repository.Add(comment);
                object o = getCommentsUser(idCandidate, idUser);
                return this.Request.CreateResponse(HttpStatusCode.OK, o);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
        }

        [HttpDelete]
        [Route("api/v1/comments/{idCandidate}/users/{idUser}")]
        public HttpResponseMessage Delete(string idCandidate, int idUser)
        {
            try
            {
                _repository.Delete(idCandidate, idUser);

                object o = getCommentsUser(idCandidate, idUser);
                return this.Request.CreateResponse(HttpStatusCode.OK, o);
            }
            catch (Exception e)
            {
                return HttpUtil.TrataException(e, Request);
            }
        }
    }
}
using EleicaoBrasil.Web.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace EleicaoBrasil.Web.Util
{
    public class HttpUtil
    {
        public static HttpResponseMessage TrataException(Exception e, HttpRequestMessage request)
        {

            ErrorMV errorvm = new ErrorMV()
            {
                Message = e.Message
            };


            if (e is EleicaoBrasil.Model.DataExceptions.DataException)
                return request.CreateResponse<ErrorMV>((HttpStatusCode)422, errorvm);
            else
                return request.CreateResponse<ErrorMV>(HttpStatusCode.InternalServerError, errorvm);
        }
    }
}
using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class BannerController : ApiController
    {
        
        trampowEntidades context = new trampowEntidades();
        // GET: api/Banner
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public dynamic GetBanners(string token)
        {
            if (Utils.ValidateToken(token))
            {
                return context.APP_BANNER.ToList();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,"Token Inválido");
            }
        }


    }
}

using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class CartaoController : ApiController
    {

        trampowEntidades context = new trampowEntidades();
        // POST: api/Cartao
        /// <summary>
        /// Cadastro de cartões
        /// </summary>
        /// <param name="cartao"></param>
        public dynamic Post([FromBody]CARTAO cartao,string token)
        {
            if (Utils.ValidateToken(token))
            {
                context.CARTAO.Add(cartao);
                context.SaveChanges();
                return cartao.HASH;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
            
        }

    }
}

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

        trampowEntities context = new trampowEntities();
        // POST: api/Cartao
        /// <summary>
        /// Cadastro de cartões
        /// </summary>
        /// <param name="cartao"></param>
        public void Post([FromBody]CARTAO cartao)
        {           
            context.CARTAOs.Add(cartao);
            context.SaveChanges();
        }

    }
}

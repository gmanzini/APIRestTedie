using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class CheckoutController : ApiController
    {
        trampowEntidades context = new trampowEntidades();
        /// <summary>
        /// Cadastro de pedido e itens do pedido
        /// </summary>
        /// <param name="pedido"></param>
        // POST: api/Checkout
        public dynamic Post([FromBody]PEDIDO pedido,string token)
        {

            if (Utils.ValidateToken(token))
            {
                context.PEDIDO.Add(pedido);
                context.SaveChanges();
                int id = pedido.NUMERO_PEDIDO;

                foreach (PEDIDO_ITEM item in pedido.ITEMS)
                {
                    item.NUMERO_PEDIDO = id;
                    context.PEDIDO_ITEM.Add(item);
                }
                context.SaveChanges();
                return pedido;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
          
        }

    }
}

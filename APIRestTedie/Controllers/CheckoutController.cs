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
        trampowEntities context = new trampowEntities();
        /// <summary>
        /// Cadastro de pedido e itens do pedido
        /// </summary>
        /// <param name="pedido"></param>
        // POST: api/Checkout
        public void Post([FromBody]PEDIDO pedido)
        {
            context.PEDIDOes.Add(pedido);
            context.SaveChanges();
            int id = pedido.NUMERO_PEDIDO;

            foreach (PEDIDO_ITEM item in pedido.ITEMS)
            {
                item.NUMERO_PEDIDO = id;
                context.PEDIDO_ITEM.Add(item);
            }
            context.SaveChanges();
            
        }

    }
}

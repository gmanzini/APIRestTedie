using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace APIRestTedie.Controllers
{
    public class PedidoController : ApiController
    {
        trampowEntidades context = new trampowEntidades();
        // GET: api/Pedido
        /// <summary>
        /// Pedidos de um usuário
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("api/usuário/{idUsuario}/pedidos")]
        public dynamic GetPedidos(int idUsuario,string token)
        {
            if (Utils.ValidateToken(token))
            {
                dynamic lst = (from c in context.PEDIDO
                               join e in context.PEDIDO_ITEM
                               on c.NUMERO_PEDIDO equals e.NUMERO_PEDIDO
                               select new
                               {
                                   c.NUMERO_PEDIDO,
                                   c.DESCONTO,
                                   c.FRETE,
                                   c.OBSERVACAO,
                                   c.PONTOS,
                                   c.QTDEPARCELA,
                                   c.STATUS,
                                   c.TIPOVENDA,
                                   c.IDCLIENTE,
                                   e.IDPRODUTO,
                                   e.QTDE,
                                   e.VALOR,
                                   e.VALOR_UNIT

                               }).Where(w => w.IDCLIENTE == idUsuario).ToList();

                return lst;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPedido"></param>
        /// <returns></returns>
        /// 
        [Route("api/pedidos/{idPedido}")]
        public dynamic Get(int idPedido,string token)
        {
            if (Utils.ValidateToken(token))
            {
                dynamic lst = (from c in context.PEDIDO
                               join e in context.PEDIDO_ITEM
                               on c.NUMERO_PEDIDO equals e.NUMERO_PEDIDO
                               select new
                               {
                                   c.NUMERO_PEDIDO,
                                   c.DESCONTO,
                                   c.FRETE,
                                   c.OBSERVACAO,
                                   c.PONTOS,
                                   c.QTDEPARCELA,
                                   c.STATUS,
                                   c.TIPOVENDA,
                                   c.IDCLIENTE,
                                   e.IDPRODUTO,
                                   e.QTDE,
                                   e.VALOR,
                                   e.VALOR_UNIT

                               }).Where(w => w.NUMERO_PEDIDO == idPedido).ToList();

                return lst;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
          
        }

    }
}

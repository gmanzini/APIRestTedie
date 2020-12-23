using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class CupomController : ApiController
    {
        trampowEntities context = new trampowEntities();
        /// <summary>
        /// Busca de cupons de um determinado usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic GetCupons(int idCliente)
        {
            dynamic lst = (from c in context.CUPOMs
                                                join e in context.CUPOM_CLIENTE
                       on c.IDCUPOM equals e.IDCUPOM
                                                select new
                                                { status =  e.STATUS,
                                                  datacadastro = e.DATACADASTRO,
                                                  data_fim = c.DATA_FIM,
                                                  data_inicio = c.DATA_INICIO,
                                                  descricao = c.DESCRICAO,
                                                  porcentagem = c.PORCENTAGEM,
                                                  quantidade = c.QTDE_CLIENTE,
                                                  tipo = c.TIPO                                                                                                                                                                                                     
                                                }
                                                ).ToList();
                                
            return lst;
        }
    }
}

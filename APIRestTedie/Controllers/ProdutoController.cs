using APIRestTedie.Util;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace APIRestTedie.Controllers
{
    public class ProdutoController : ApiController
    {
        trampowEntidades context = new trampowEntidades();
        /// <summary>
        /// Busca de um produto específico
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns></returns>
        /// 
        [Route("api/produtos/{idProduto}")]
        public dynamic GetProduto(int idProduto,string token)
        {
            if (Utils.ValidateToken(token))
            {
                dynamic product = (from p in context.PRODUTO
                                   join c in context.CATEGORIA
                                   on p.IDCATEGORIA equals c.IDCATEGORIA
                                   select new
                                   {
                                       c.NOMECATEGORIA,
                                       p.DESCRICAO_BREVE,
                                       p.DESCRICAO_COMPLETA,
                                       p.GARANTIA,
                                       p.IDCATEGORIA,
                                       p.DATACADASTRO,
                                       p.IDPRODUTO,
                                       p.LINKPRODUTO,
                                       p.METADESCRICAO,
                                       p.METATAGS,
                                       p.MINIATURA,
                                       p.STATUS,
                                       p.TIPO,
                                       p.TITULO,
                                       p.VALOR,
                                       p.VALOR_PROMOCIONAL,
                                       p.VIEWS,
                                   }).FirstOrDefault(w => w.IDPRODUTO == idProduto);

                return product;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
           

         


        }
        /// <summary>
        /// Destaques da home
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("api/home/destaque")]
        public dynamic GetProdutosHome(string token)
        {
            if (Utils.ValidateToken(token))
            {
                dynamic products = (from p in context.PRODUTO
                                    join c in context.CATEGORIA
                                    on p.IDCATEGORIA equals c.IDCATEGORIA
                                    select new
                                    {
                                        c.NOMECATEGORIA,
                                        p.DESCRICAO_BREVE,
                                        p.DESCRICAO_COMPLETA,
                                        p.GARANTIA,
                                        p.IDCATEGORIA,
                                        p.DATACADASTRO,
                                        p.IDPRODUTO,
                                        p.LINKPRODUTO,
                                        p.METADESCRICAO,
                                        p.METATAGS,
                                        p.MINIATURA,
                                        p.STATUS,
                                        p.TIPO,
                                        p.TITULO,
                                        p.VALOR,
                                        p.VALOR_PROMOCIONAL,
                                        p.VIEWS,
                                        p.DESTAQUE
                                    }).Where(w => w.DESTAQUE.ToUpper() == "S");

                return products;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
          
        }
        // GET: api/Produto
        /// <summary>
        /// Busca de produtos com filtros específicos. 
        /// </summary>
        /// <param name="subcategoria"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="search"></param>
        /// <param name="order">maisvendidos/lancamentos/
        /// maiorpreco/relevancia/maisbemavaliados</param>
        /// <param name="minpreco"></param>
        /// <param name="maxpreco"></param>
        /// <returns></returns>
       // [Route("api/produtos/subcategoria={subcategoria}&offset={offset}&limit={limit}&search={search}&order={order}&minpreco={minpreco}&maxpreco={maxpreco}")]
        public dynamic GetProdutos(int subcategoria, int offset, int limit, string search, string order, double? minpreco, double? maxpreco,string token)
        {
            var query = (from p in context.PRODUTO
                         join c in context.CATEGORIA
                         on p.IDCATEGORIA equals c.IDCATEGORIA
                         where c.IDCATEGORIA == subcategoria
                         orderby p.IDPRODUTO
                         select new
                         {
                             c.NOMECATEGORIA,
                             p.DESCRICAO_BREVE,
                             p.DESCRICAO_COMPLETA,
                             p.GARANTIA,
                             p.IDCATEGORIA,
                             p.DATACADASTRO,
                             p.IDPRODUTO,
                             p.LINKPRODUTO,
                             p.METADESCRICAO,
                             p.METATAGS,
                             p.MINIATURA,
                             p.STATUS,
                             p.TIPO,
                             p.TITULO,
                             p.VALOR,
                             p.VALOR_PROMOCIONAL,
                             p.VIEWS,
                             QUANTIDADEVENDIDO = context.PEDIDO_ITEM
                              .Where(x => x.IDPRODUTO == p.IDPRODUTO)
                                 .Sum(x => x.QTDE),
                             AVALIACAO = context.SCORE_PRODUTO.Where(x => x.IDPRODUTO == p.IDPRODUTO).Average(x => x.SCORE)
                         }
                );
            if (Utils.ValidateToken(token))
            {
                switch (order)
                {
                    case "maisvendidos":
                        query = query.OrderByDescending(w => w.QUANTIDADEVENDIDO).AsEnumerable()
                                     .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                                     .Skip(offset).Take(limit).AsQueryable();
                        break;
                    case "lancamentos":
                        query = query.OrderByDescending(w => w.DATACADASTRO).AsEnumerable()
                                 .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                               .Skip(offset).Take(limit).AsQueryable();
                        break;
                    case "maiorpreco":
                        query = query.OrderByDescending(w => (w.VALOR_PROMOCIONAL != 0 && w.VALOR_PROMOCIONAL != null ? w.VALOR_PROMOCIONAL : w.VALOR)).AsEnumerable()
                                .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                               .Skip(offset).Take(limit).AsQueryable();
                        break;
                    case "menorpreco":
                        query = query.OrderBy(w => (w.VALOR_PROMOCIONAL != 0 && w.VALOR_PROMOCIONAL != null ? w.VALOR_PROMOCIONAL : w.VALOR)).AsEnumerable()
                             .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                            .Skip(offset).Take(limit).AsQueryable();
                        break;
                    case "relevancia":
                        query = query.OrderByDescending(w => w.AVALIACAO).AsEnumerable()
                            .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                            .Skip(offset).Take(limit).AsQueryable();
                        break;
                    case "maisbemavaliados":
                        query = query.OrderByDescending(w => w.AVALIACAO).AsEnumerable()
                             .Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                            .Skip(offset).Take(limit).AsQueryable();
                        break;

                    default:
                        query = query.AsEnumerable().Where(w => Utils.RemoveAccents(w.TITULO).ToLower().Contains(Utils.RemoveAccents(search).ToLower()) && (w.VALOR > minpreco && w.VALOR < maxpreco))
                            .Skip(offset).Take(limit).AsQueryable();
                        break;
                }

                return query.ToList();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }

          

         
        }
       
    }
}

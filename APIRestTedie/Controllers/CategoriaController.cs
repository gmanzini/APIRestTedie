using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    /// <summary>
    /// Controller para enndpoints de categorias
    /// </summary>
    public class CategoriaController : ApiController
    {
        trampowEntidades context = new trampowEntidades();
        // GET: api/Categoria
        /// <summary>
        /// Todas categorias
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("api/categorias")]
        public dynamic GetCategoria(string token)
        {
            if (Utils.ValidateToken(token))
            {
                var lst = (from p in context.CATEGORIA
                           select new
                           {
                               p.CATEGORIAPAI,
                               p.DATACADASTRO,
                               p.IDCATEGORIA,
                               p.NOMECATEGORIA,
                               p.STATUS

                           });
                return lst;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
            
        }
        /// <summary>
        /// Sub-Categorias de uma determinada categoria
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        /// 
        [Route("api/categorias/{idCategoria}/sub")]
        public dynamic GetSubCategoria(int idCategoria,string token)
        {
            if (Utils.ValidateToken(token))
            {
                var lst = (from p in context.CATEGORIA
                           where p.CATEGORIAPAI == idCategoria
                           select new
                           {
                               p.CATEGORIAPAI,
                               p.DATACADASTRO,
                               p.IDCATEGORIA,
                               p.NOMECATEGORIA,
                               p.STATUS

                           });
                return lst;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
          
        }
        /// <summary>
        /// Categorias da Home ( Não tem categoria Pai)
        /// </summary>
        /// <returns></returns>
        [Route("api/home/categorias")]
        public dynamic GetCategoriasHome(string token)
        {
            if (Utils.ValidateToken(token))
            {
                var lst = (from p in context.CATEGORIA
                           where p.CATEGORIAPAI == 0
                           select new
                           {
                               p.CATEGORIAPAI,
                               p.DATACADASTRO,
                               p.IDCATEGORIA,
                               p.NOMECATEGORIA,
                               p.STATUS

                           });
                return lst;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
        
        }
    }
}

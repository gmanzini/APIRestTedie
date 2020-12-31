using APIRestTedie.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class LoginController : ApiController
    {
        
        trampowEntidades context = new trampowEntidades();
        // POST: api/Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        public dynamic Login(string email,string senha)
        {
            //  bool exists = context.USUARIOs.Any(w => w.SENHA == senha && w.EMAIL == email);
            var token = Utils.Base64Encode(email);
            var user = (from p in context.USUARIO
                        where p.SENHA == senha && p.EMAIL == email
                       select new
                       {
                           p.EMAIL,
                           p.IDCLIENTE,
                           p.STATUS,
                           Message = p.EMAIL == null ?  "usuário não encontrado" : "Login efetuado com sucesso",
                           Token = token
                       });
            if (user.Any())
            {
                return user;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Login Inválido") ;
            }
            //return user.Any() ? user : new { EMAIL = email,IDCLIENTE = 0,STATUS = "",Message = "Login Inválido",Token ="" } as Queryable<string,int,string,string,string>;//  AsQueryable<string,int,string>();
        }

    }
}

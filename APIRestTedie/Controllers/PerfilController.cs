﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    public class PerfilController : ApiController
    {

        trampowEntities context = new trampowEntities();
        // GET: api/Perfil/5
        /// <summary>
        /// Dados do usuário utilizados na tela
        ///  de perfil do usuário.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public dynamic GetDadosUsuario(int idUsuario)
        {
            dynamic lst = (
                 from p in context.USUARIOs
                 join c in context.CLIENTEs on
                 p.IDCLIENTE equals c.IDCLIENTE
                 join cp in context.CLIENTE_PERFIL on 
                 c.IDCLIENTE equals cp.IDCLIENTE
                 join pf in context.PERFILs on
                 cp.IDPERFIL equals pf.IDPERFIL
                 where p.IDCLIENTE == idUsuario
                 select new
                 {
                     p.EMAIL,
                     p.IDCLIENTE,
                     c.NOME_PUBLICO,
                     c.NOME,
                     c.SEXO,
                     c.ICONE,
                     c.FACEBOOK,
                     c.APELIDO,
                     c.CPF_CNPJ,
                     c.DATANASC,
                     c.INSTAGRAM,
                     c.PINTEREST,
                     c.MODO_PLAYER,
                     c.DATACADASTRO,                     
                     c.PAIS,
                     c.SITE,
                     c.TELEFONE1,
                     c.TELEFONE2,
                     c.WEBSITE,
                     c.YOUTUBE,
                     c.COMISSAO,
                     c.SOBRE_NEGOCIO,
                     pf.NOMEPERFIL
                 }
                );
            return lst;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestTedie.Controllers
{
    /// <summary>
    /// Controller para manutenção dos dados do Cliente
    /// </summary>
    public class ClienteController : ApiController
    {
        trampowEntidades context = new trampowEntidades();


        // POST: api/Cliente
        /// <summary>
        /// Cadastro de Clientes
        /// </summary>
        /// <param name="Cliente"></param>
        public CLIENTE Post([FromBody]CLIENTE Cliente)
        {
            context.CLIENTE.Add(Cliente);
            context.SaveChanges();
            return Cliente;
        }


    }
}

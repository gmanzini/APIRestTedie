﻿
using APIRestTedie.Models;
using APIRestTedie.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace APIRestTedie.Controllers
{

    public class EnderecoController : ApiController
    {
        trampowEntidades context = new trampowEntidades();
        /// <summary>
        /// Buscar endereço por localização
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="longt"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("endereco/geolocalizacao")]

        public async Task<dynamic> GetEnderecoByGeoLoc(string lat, string longt, string token)
        {
            if (Utils.ValidateToken(token))
            {
                Geolocation end = new Geolocation();
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://maps.googleapis.com/");
                    var resp = await client.GetAsync("/maps/api/geocode/json?latlng=-" + lat + ",-" + longt + "&key=AIzaSyBoTECWyjxczIwsvLtysnPh2J45DXRRbU8");
                    if (resp.IsSuccessStatusCode)
                    {
                        string response = resp.Content.ReadAsStringAsync().Result;
                        end = JsonConvert.DeserializeObject<Geolocation>(response);

                    }
                }
                CLIENTE_ENDERECO endereco = new CLIENTE_ENDERECO();
                Result dadosend = end.results.FirstOrDefault();
                endereco.CEP = dadosend.address_components.FirstOrDefault(w => w.types.Contains("postal_code")).long_name;
                endereco.ENDERECO = dadosend.address_components.FirstOrDefault(w => w.types.Contains("route")).long_name;
                endereco.NUM = dadosend.address_components.FirstOrDefault(w => w.types.Contains("street_number")).long_name;
                endereco.UF = dadosend.address_components.FirstOrDefault(w => w.types.Contains("administrative_area_level_1")).long_name;
                endereco.CIDADE = dadosend.address_components.FirstOrDefault(w => w.types.Contains("administrative_area_level_2")).long_name;
                endereco.BAIRRO = dadosend.address_components.FirstOrDefault(w => w.types.Contains("sublocality")).long_name;
                return endereco;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }


        }
        // DELETE: api/enderecos/{idEndereco}
        /// <summary>
        /// Excluir um endereço específico através de um ID
        /// </summary>
        /// <param name="idEndereco"></param>
        public dynamic Delete(int idEndereco, string token)
        {
            if (Utils.ValidateToken(token))
            {
                var endereco = context.CLIENTE_ENDERECO.Where(w => w.IDENDERECO == idEndereco).FirstOrDefault();
                context.CLIENTE_ENDERECO.Remove(endereco);
                context.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Endereço excluído com sucesso");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }


        }
        // PUT: api/enderecos/{idEndereco}
        /// <summary>
        /// Editar dados de um endereço
        /// </summary>

        /// <param name="endereco"></param>
        public dynamic Put([FromBody]CLIENTE_ENDERECO endereco, string token)
        {
            if (Utils.ValidateToken(token))
            {
                context.CLIENTE_ENDERECO
                   .Where(p => p.IDENDERECO == endereco.IDENDERECO)
                   .ToList()
                   .ForEach(x =>
                   {
                       x.BAIRRO = endereco.BAIRRO;
                       x.CEP = endereco.CEP;
                       x.CIDADE = endereco.CIDADE;
                       x.COMPLEMENTO = endereco.COMPLEMENTO;
                       x.ENDERECO = endereco.ENDERECO;
                       x.IDCLIENTE = endereco.IDCLIENTE;
                       x.IDENDERECO = endereco.IDENDERECO;
                       x.NUM = endereco.NUM;
                       x.UF = endereco.UF;
                   });
                context.SaveChanges();
                return endereco;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }


        }
        /// <summary>
        /// Recupera lista de endereços do cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("endereco/idcliente/{id}")]
        public dynamic GetEnderecoPorID(int id,string token)
        {
            if (Utils.ValidateToken(token))
            {
                IQueryable<CLIENTE_ENDERECO> lst = from p in context.CLIENTE_ENDERECO.Where(w => w.IDCLIENTE == id) select p;

                return lst.ToList();
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }

          
        }
        // GET: api/Endereco/5
        /// <summary>
        /// Recupera os dados do endereço via CEP
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        ///  [HttpGet]
        [Route("endereco/{cep}")]
        public async Task<dynamic> GetEnderecoPorCEP(string cep,string token)
        {
            if (Utils.ValidateToken(token))
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://viacep.com.br/");
                    var resp = await client.GetAsync("ws/" + cep + "/json");
                    if (resp.IsSuccessStatusCode)
                    {
                        string response = resp.Content.ReadAsStringAsync().Result;
                        EnderecoCEP end = JsonConvert.DeserializeObject<EnderecoCEP>(response);
                        return end;
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Favor validar o CEP");
                    }

                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Token Inválido");
            }
           
        }
    }
}

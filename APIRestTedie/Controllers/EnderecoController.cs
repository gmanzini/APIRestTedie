
using APIRestTedie.Models;
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
        trampowEntities context = new trampowEntities();

        [HttpGet]
        [Route("endereco/geolocalizacao")]
        public async Task<CLIENTE_ENDERECO> GetEnderecoByGeoLoc(string lat,string longt)
        {
            Geolocation end = new Geolocation();
            using (HttpClient client = new HttpClient())
            {
               
                client.BaseAddress = new Uri("https://maps.googleapis.com/");
                var resp = await client.GetAsync("/maps/api/geocode/json?latlng=-" +lat+",-"+longt+"&key=AIzaSyBoTECWyjxczIwsvLtysnPh2J45DXRRbU8");
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
        /// <summary>
        /// Recupera lista de endereços do cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("endereco/idcliente/{id}")]
        public  List<CLIENTE_ENDERECO> GetEnderecoPorID(int id)
        {
            IQueryable<CLIENTE_ENDERECO> lst = from p in context.CLIENTE_ENDERECO.Where(w=> w.IDCLIENTE == id) select p;

            return lst.ToList();
        }
        // GET: api/Endereco/5
        /// <summary>
        /// Recupera os dados do endereço via CEP
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        ///  [HttpGet]
        [Route("endereco/cep/{cep}")]
        public  async Task<EnderecoCEP> GetEnderecoPorCEP(string cep)
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
                    EnderecoCEP end = JsonConvert.DeserializeObject<EnderecoCEP>(response) ;
                    return end;
                }
                else
                {
                    return null;
                }

            }
        }
    }
}

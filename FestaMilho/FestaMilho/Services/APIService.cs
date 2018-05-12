using FestaMilho.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FestaMilho.Services
{
    public class APIService
    {
        public DataService dataService; //acesso ao sq lite
        public HttpClient client = new HttpClient();
        public static readonly string ServidorApi = "http://festadomilho.kibt.com.br"; //ip do backend caso seje local ip da placa de rede
        public APIService()
        {
            dataService = new DataService();
        } //contrutor

        public async Task<Response> GetMedia() //rota votacao 
        {
            try
            {
                client = new HttpClient();
                var uri = new Uri(String.Format("{0}/votacao/lista", ServidorApi));
                var user = dataService.GetUser();
                client = new HttpClient();
                var bearer = String.Format("bearer {0}", user.Token);
                client.DefaultRequestHeaders.Add("Authorization", bearer);
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var MediaAvaliacao = JsonConvert.DeserializeObject<List<MediaAvaliacao>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Get Média Ok",
                    MediaAvaliacao = MediaAvaliacao,
                };
            }
            catch (Exception ex)
            {
                return
                new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetBarraca()// rota barraca
        {
            try
            {
                client = new HttpClient();
                var uri = new Uri(String.Format("{0}/barraca",ServidorApi));
                var user = dataService.GetUser();
                client = new HttpClient();
                var bearer = String.Format("bearer {0}", user.Token);
                client.DefaultRequestHeaders.Add("Authorization", bearer);
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    var list = new List<BarracaReturn>();
                    var Barraca = new BarracaReturn
                    {
                        nome ="Erro de Conexão!"
                    };
                    list.Add(Barraca);
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Falha de conexao",
                        BarracaResult =  list    
                    };

                }
                var result = await response.Content.ReadAsStringAsync();
                var barraca = JsonConvert.DeserializeObject<List<BarracaReturn>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Download de barraca OK!",
                    BarracaResult = barraca,
                };
            }
            catch (Exception ex)
            {
                return
                new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetCardapio() // rota cardapio
        {
            try
            {
                var uri = new Uri(String.Format("{0}/cardapio", ServidorApi));
                var user = dataService.GetUser();
                client = new HttpClient();
                var bearer = String.Format("bearer {0}", user.Token);
                client.DefaultRequestHeaders.Add("Authorization", bearer);
                HttpResponseMessage response = null;
                response = await client.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var list = new List<CardapioReturn>();
                    var Barraca = new CardapioReturn
                    {
                        nomeprato = "Erro de Conexão!"
                    };
                    list.Add(Barraca);
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Falha de conexao",
                        CardapioResult = list
                    };

                }
                var cardapio = JsonConvert.DeserializeObject<List<CardapioReturn>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Download de Cardapio Ok",
                    CardapioResult = cardapio,
                };
            }
            catch (Exception ex)
            {
                return
                new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                //throw ex;
            }
        }

        public async Task<Response> Votar (Avaliacao avaliacao) // rota votacao post
        {
            try
            {

                var jsonRequest = JsonConvert.SerializeObject(avaliacao);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var user = dataService.GetUser();
                var bearer = String.Format("bearer {0}", user.Token);
                client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", bearer);
                var uri = new Uri(String.Format("{0}/votacao/", ServidorApi));
                HttpResponseMessage response = null;
                response = await client.PostAsync(uri, httpContent);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<ObjectError>(result);
                    return new Response
                    {
                        IsSuccess = false,
                        Message = error.error,

                    };

                }

                var voto = JsonConvert.DeserializeObject<Avaliacao>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Avaliada!",
                   // Result = voto,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Falha na conexão!",
                };
                throw;
            }
        }

        public async Task<Response> Cadastrar(CadastroRequest cadastro) // rota publica cadastro 
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cadastro);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri(String.Format("{0}/auth/registro", ServidorApi));
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, httpContent);
                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ObjectError>(result);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = error.error,

                    };

                }

              
                var user = JsonConvert.DeserializeObject<Usuario>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "cadastro Ok",
                    Result = user,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }
        } 

        public async Task<Response> Login(LoginRequest login) //rota publica Login
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(login);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri(String.Format("{0}/auth/autenticacao", ServidorApi));
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, httpContent);
                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ObjectError>(result);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = error.error
                    };

                }

               
                var user = JsonConvert.DeserializeObject<Usuario>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Login Ok",
                    Result = user.Token,
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Falha na Conexão!",
                };
                throw;
            }
        }

        public async Task<Response> Recuperar (RecuperarRequest recuperar) // rota publica recuperar
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(recuperar);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri(String.Format("{0}/auth/recuperasenha", ServidorApi));
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, httpContent);

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ObjectError>(result);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = user.error,

                    };

                }

                

                return new Response
                {
                    IsSuccess = true,
                    Message = user.error,
                    //Result = user.Token,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
                throw;
            }
        }

    }
}


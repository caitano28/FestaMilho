using FestaMilho.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FestaMilho.Services
{
    public class APIService
    {
        public HttpClient client = new HttpClient();
        
    
        public async Task<Response> Cadastrar(CadastroRequest cadastro)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cadastro);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri("http://192.168.2.101:4000/auth/registro");
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
        public async Task<Response> Login(LoginRequest login)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(login);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri("http://192.168.2.101:4000/auth/autenticacao");
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
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Falha na Conexão!",
                };
                throw;
            }
        }


        public async Task<Response> Recuperar (RecuperarRequest recuperar)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(recuperar);
                var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                var uri = new Uri("http://192.168.2.101:4000/auth/recuperasenha");
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

        //public async Task UpdateProdutoAsync(Produto produto)
        //{
        //    string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
        //    var uri = new Uri(string.Format(url, produto.Id));

        //    var data = JsonConvert.SerializeObject(produto);
        //    var content = new StringContent(data, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;
        //    response = await client.PutAsync(uri, content);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Erro ao atualizar produto");
        //    }

        //}

        //public async Task DeletaProdutoAsync(Produto produto)
        //{
        //    string url = "http://www.macwebapi.somee.com/api/produtos/{0}";
        //    var uri = new Uri(string.Format(url, produto.Id));
        //    await client.DeleteAsync(uri);
        //}
    }
}


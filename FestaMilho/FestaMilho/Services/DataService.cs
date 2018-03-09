using FestaMilho.Data;
using FestaMilho.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Services
{
    public class DataService
    {

       // public Usuario olduser { get; set; }
        public DataService()
        {
            
        }
        public Response DeleteUser(Usuario usuario)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Delete(usuario);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuário deletado com Sucesso!",
                    Result = usuario
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Response UpdateUser(Usuario usuario)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Update(usuario);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuário editado com Sucesso!",
                    Result = usuario
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Response InsertUser(Usuario usuario)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Add(usuario);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuário Cadastrado com Sucesso!",
                    Result = usuario
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Usuario GetUser()
        {
            using (var dt = new Conexao())
            {
                return dt.FirstUser();

            }
        }
        public IEnumerable<CardapioReturn> GetAllCardapio()
        {
            using (var dt = new Conexao())
            {
                return dt.GetCardapios();
            }
        }
        public CardapioReturn GetCardapio()
        {
            using (var dt = new Conexao())
            {
                return dt.FirstCardapio();

            }
        }
        public BarracaReturn GetBarraca()
        {
            using (var dt = new Conexao())
            {
                return dt.FirstBarraca();

            }
        }
        public Response UpdateBarraca(BarracaReturn barracaReturn)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Update(barracaReturn);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Barraca editado com Sucesso!",
                    Result = barracaReturn
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Response InsertBarraca(BarracaReturn barracaReturn)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Add(barracaReturn);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Barraca Cadastrado com Sucesso!",
                    Result = barracaReturn
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Response UpdateCardapio(CardapioReturn cardapioReturn)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Update(cardapioReturn);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Cardapio editado com Sucesso!",
                    Result = cardapioReturn
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public Response InsertCardapio(CardapioReturn cardapioReturn)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Add(cardapioReturn);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "Cardapio Cadastrado com Sucesso!",
                    Result = cardapioReturn
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
    }
}

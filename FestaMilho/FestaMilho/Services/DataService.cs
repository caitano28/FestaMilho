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
                return dt.First<Usuario>();

            }
        }
    }
}

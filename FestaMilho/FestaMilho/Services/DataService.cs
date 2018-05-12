using FestaMilho.Data;
using FestaMilho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Rank> GetRankNota()
        {
            using (var dt = new Conexao())
            {
                return dt.GetList<Rank>()
                .OrderBy(i => i.Nota).Reverse()
                .ToList();
            }
        }

        public List<CardapioReturn> GetCardapioID(string id)
        {
            using (var dt = new Conexao())
            {
                return dt.GetList<CardapioReturn>()
                .Where(i => (i.barraca == id))
                .OrderBy(i=> i.nomeprato)
                .ToList();
            }
        }

        public Response InsertRank(Rank rank)
        {
            try
            {
                using (var dt = new Conexao())
                {
                    dt.Add(rank);
                }
                return new Response
                {
                    IsSuccess = true,
                    Message = "ok",
                    Result = rank
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

        public Model.Rank GetRank()
        {
            using (var dt = new Conexao())
            {
                return dt.FirstRank();

            }
        }
        public Usuario GetUser()
        {
            using (var dt = new Conexao())
            {
                return dt.FirstUser();

            }
        }

        public List<BarracaReturn> GetBarracaID(string id)
        {
            using (var dt = new Conexao())
            {
                return dt.GetList<BarracaReturn>()
                .Where(i => (i._id == id))
                .ToList();
            }
        }
        public List<BarracaReturn> GetBarracas(string filter)
        {
            using (var dt = new Conexao())
            {
                return dt.GetList<BarracaReturn>()
                .OrderBy(p => p.nome)
                .Where(i => (i.nome.ToLower().Contains(filter.ToLower())) || (i.curso.ToLower().Contains(filter.ToLower())))
                .ToList();
            }
        }

        public List<CardapioReturn> GetCardapios(string filter)
        {
            using (var dt = new Conexao())
            {
                return dt.GetList<CardapioReturn>()
                .OrderBy(p => p.nomeprato)
                .Where(i => (i.descricao.ToLower().Contains(filter.ToLower()) ) || (i.nomeprato.ToLower().Contains(filter.ToLower()) ) )
                .ToList();
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

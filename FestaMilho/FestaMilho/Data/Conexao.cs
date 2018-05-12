using FestaMilho.Interfaces;
using FestaMilho.Model;
using SQLite;

using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FestaMilho.Data
{
    public class Conexao : IDisposable
    {
        private readonly ISQLitePlatform _platform;
        private SQLiteConnection _conexao;

        public Conexao()
        {
            _platform = DependencyService.Get<ISQLitePlatform>();

            _conexao = _platform.GetConnection();                
            _conexao.CreateTable<Usuario>();
            _conexao.CreateTable<CardapioReturn>();
            _conexao.CreateTable<BarracaReturn>();
            _conexao.CreateTable<Rank>();
        }
        public void DropTable<T>() where T : class
        {
            _conexao.DropTable<T>();
        }

        public void CreateTable<T>() where T : class
        {
            _conexao.CreateTable<T>();
        }

        public void Add<T>(T model)
        {
             _conexao.Insert(model);
        }
        public void Update<T>(T model)
        {
            _conexao.Update(model);
        }

        public void Delete<T>(T model)
        {
            _conexao.Delete(model);
        }

        //public T Find<T>(int id) where T : class
        //{
        //    return _conexao.Table<T>().Where(i => i.GetHashCode() == id).FirstOrDefault();
        //}
        public Usuario FirstUser() 
        {
            return _conexao.Table<Usuario>().FirstOrDefault();
        }
        public Model.Rank FirstRank()
        {
            return _conexao.Table<Model.Rank>().FirstOrDefault();
        }
        public CardapioReturn FirstCardapio()
        {
            return _conexao.Table<CardapioReturn>().FirstOrDefault();
        }
        public BarracaReturn FirstBarraca()
        {
            return _conexao.Table<BarracaReturn>().FirstOrDefault();
        }



        public IEnumerable<T> GetList<T>() where T : new ()
        {
            
                return _conexao.Table<T>();
            
        }
        //public IEnumerable<T> GetIEnum<T>() where T : class
        //{
        //   return _conexao.Table<T>();

        //}
        public IEnumerable<BarracaReturn> GetBarracas()
        {
            return _conexao.Table<BarracaReturn>();
        }
        public IEnumerable<CardapioReturn> GetCardapios()
        {
            return _conexao.Table<CardapioReturn>();
        }
        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}

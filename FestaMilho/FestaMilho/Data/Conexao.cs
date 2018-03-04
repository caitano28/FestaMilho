using FestaMilho.Interfaces;
using FestaMilho.Model;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FestaMilho.Data
{
    public class Conexao : IDisposable
    {

        private SQLiteConnection _conexao;

        public Conexao()
        {
            var config = DependencyService.Get<IConfiguration>();
            _conexao = new SQLiteConnection(config.Plataform,
                System.IO.Path.Combine(config.DiretorioDataBase, "FestaMilho.db"));
            _conexao.CreateTable<Usuario>();
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

        public T Find<T>(int id) where T : class
        {
            return _conexao.Table<T>().Where(i => i.GetHashCode() == id).FirstOrDefault();
        }
        public T First<T>() where T : class
        {
            return _conexao.Table<T>().FirstOrDefault();
        }
        public IEnumerable<T> GetIEnum<T>() where T : class
        {
           return _conexao.Table<T>();
           
        }
        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}

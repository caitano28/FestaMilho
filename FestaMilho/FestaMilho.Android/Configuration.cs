using FestaMilho.Interfaces;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(FestaMilho.Droid.Configuration))]
namespace FestaMilho.Droid
{
    public class Configuration : IConfiguration
    {
        public Configuration() { }
        private string _diretorioDataBase;

        public string DiretorioDataBase
        {
            get
            {
                if (String.IsNullOrEmpty(_diretorioDataBase))
                {
                    _diretorioDataBase = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }
                return _diretorioDataBase;
            }
        }

        private ISQLitePlatform _plataform;

        public ISQLitePlatform Plataform
        {
            get
            {
                if (_plataform == null)
                {
                    _plataform = new SQLitePlatformAndroid();
                }
                return _plataform;
            }
        }
    }
}
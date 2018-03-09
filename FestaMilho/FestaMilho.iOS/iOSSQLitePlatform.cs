using FestaMilho.Interfaces;
using FestaMilho.iOS;
using SQLite;
using System;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(iOSSQLitePlatform))]
namespace FestaMilho.iOS
{
    public class iOSSQLitePlatform : ISQLitePlatform
    {
        private string GetPath()
        {
            var dbName = "FestaMilho.db3";
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return path;
        }
        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(GetPath());
        }
        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(GetPath());
        }
    }
}
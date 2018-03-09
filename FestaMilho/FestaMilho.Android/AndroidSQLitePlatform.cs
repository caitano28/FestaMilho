using FestaMilho.Droid;
using FestaMilho.Interfaces;
using SQLite;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(AndroidSQLitePlatform))]
namespace FestaMilho.Droid
{
    public class AndroidSQLitePlatform : ISQLitePlatform
    {
        private string GetPath()
        {
            var dbName = "FestaMilho.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
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
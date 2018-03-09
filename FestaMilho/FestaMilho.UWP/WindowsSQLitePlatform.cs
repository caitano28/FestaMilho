using FestaMilho.Interfaces;
using FestaMilho.UWP;
using SQLite;
using System.IO;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(WindowsSQLitePlatform))]
namespace FestaMilho.UWP
{
    public class WindowsSQLitePlatform : ISQLitePlatform
    {
        private string GetPath()
        {
            var dbName = "FestaMilho.db3";
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
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

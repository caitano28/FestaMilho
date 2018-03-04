using SQLite.Net.Interop;

namespace FestaMilho.Interfaces
{
    interface IConfiguration
    {
        string DiretorioDataBase { get; }
        ISQLitePlatform Plataform { get; }
    }
}

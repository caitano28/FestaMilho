using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FestaMilho.Interfaces
{
    public interface ISQLitePlatform
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetConnectionAsync();
    }
}

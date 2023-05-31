using SQLite;

namespace MauiApp3.Data
{
    class Constants
    {
        public const string DbName = "MyDatabase.db3";
        public static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
        public const SQLite.SQLiteOpenFlags Flags =
         SQLite.SQLiteOpenFlags.ReadWrite |
         SQLite.SQLiteOpenFlags.Create |
         SQLite.SQLiteOpenFlags.SharedCache;

    }
}

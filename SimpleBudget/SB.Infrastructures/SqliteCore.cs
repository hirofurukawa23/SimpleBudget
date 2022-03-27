using System.Reflection;

namespace SB.Infrastructures
{
    public static class SqliteCore
    {
        /// <summary>
        /// データベースのパスを取得する
        /// </summary>
        /// <returns></returns>
        public static string GetDbPath()
        {
            return Path.Combine(Path.Combine(Environment.CurrentDirectory, "DB"), "memory.db"); ;
        }

        /// <summary>
        /// DBの初期準備をする
        /// </summary>
        public static void InitDb()
        {
            var path = Assembly.GetEntryAssembly()?.Location;
            path = path.Replace("SB.Presentation.dll", "");
            var dbDirPath = Path.Combine(path, "DB");
            if (!Directory.Exists(dbDirPath))
            { //ディレクトリがなければ作成
                Directory.CreateDirectory(dbDirPath);
            }

            var dbPath = Path.Combine(dbDirPath, "memory.db");
            if (!File.Exists(dbPath))
            { //ファイルがなければ作成
                using (FileStream fs = File.Create(dbPath)) { }
            }
        }
    }
}

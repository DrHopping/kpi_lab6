using System.Data;
using System.Data.SqlClient;
using System.IO;
using kpi_lab6.Comparers;

namespace kpi_lab6
{
    public class SimpleTblExporter
    {
        public static void ExportWhereOrderBy()
        {
            var csvFilePath = @$"{Settings.Path}\Export.csv";
            var connectionString = "Server=tcp:blogproject.database.windows.net,1433;Initial Catalog=BlogDB;Persist Security Info=False;User ID=hopping;Password=1NnOoVVII;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sqlExpression = "SELECT * FROM Articles WHERE BlogId = 1 ORDER BY Id";

            using var connection = new SqlConnection(connectionString);
            using var writer = new StreamWriter(csvFilePath);

            connection.Open();
            var command = new SqlCommand(sqlExpression, connection);
            var reader = command.ExecuteReader();
                

            if (reader.HasRows)
            {
                writer.WriteLine("id,title");
                while (reader.Read())
                {
                    var id = reader.GetInt32("Id");
                    var name = reader.GetString("Title");
                    writer.WriteLine($"{id},{name}");
                }
                reader.Close();
            }
        }

        public static void ExportGroupBy()
        {
            var csvFilePath = @$"{Settings.Path}\Export2.csv";
            var connectionString = "Server=tcp:blogproject.database.windows.net,1433;Initial Catalog=BlogDB;Persist Security Info=False;User ID=hopping;Password=1NnOoVVII;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var sqlExpression = "SELECT BlogId, Name, ArticlesCount FROM Blogs B JOIN (SELECT BlogId, Count(*) AS ArticlesCount FROM Articles GROUP BY BlogId) A ON A.BlogId = B.Id";

            using var connection = new SqlConnection(connectionString);
            using var writer = new StreamWriter(csvFilePath);

            connection.Open();
            var command = new SqlCommand(sqlExpression, connection);
            var reader = command.ExecuteReader();


            if (reader.HasRows)
            {
                writer.WriteLine("blogId,name,articlesCount");
                while (reader.Read())
                {
                    var id = reader.GetInt32("BlogId");
                    var name = reader.GetString("Name");
                    var count = reader.GetInt32("ArticlesCount");
                    writer.WriteLine($"{id},{name},{count}");
                }
                reader.Close();
            }
        }
    }
}
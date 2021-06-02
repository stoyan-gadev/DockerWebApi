using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DockerQuickStart.DataAccess.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = GetScriptData($"{GetType().Namespace}.{GetType().Name}.sql");
            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Posts; DELETE FROM Blogs");
        }

        private string GetScriptData(string filename)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

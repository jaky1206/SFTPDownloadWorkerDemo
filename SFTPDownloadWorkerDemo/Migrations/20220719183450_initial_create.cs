using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SFTPDownloadWorkerDemo.Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SftpLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastAccessTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastWriteTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastAccessTimeUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastWriteTimeUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    LocalFilePath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SftpLogs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SftpLogs");
        }
    }
}

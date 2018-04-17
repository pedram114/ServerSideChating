using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "clients",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CId = table.Column<int>(nullable: false),
                    IsConnected = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_clients", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "clients");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaLojaExpress.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SetupSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("652c6058-c225-4352-ba34-01f9df0af9b0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9caf62cc-6c5e-45d0-ba73-1a6f68a6e2d4"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("96bd4915-991d-46cb-8df9-401931d5b892"), "meuemail@gmail.com", "Cliente1", "31 91111-1111" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Nome", "Preco", "Quantidade" },
                values: new object[] { new Guid("73c9129e-169f-4d24-8d90-c13e006381b4"), "Item 1", 10m, 10L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("96bd4915-991d-46cb-8df9-401931d5b892"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("73c9129e-169f-4d24-8d90-c13e006381b4"));

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("652c6058-c225-4352-ba34-01f9df0af9b0"), "meuemail@gmail.com", "Cliente1", "31 91111-1111" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Nome", "Preco", "Quantidade" },
                values: new object[] { new Guid("9caf62cc-6c5e-45d0-ba73-1a6f68a6e2d4"), "Item 1", 10m, 10L });
        }
    }
}

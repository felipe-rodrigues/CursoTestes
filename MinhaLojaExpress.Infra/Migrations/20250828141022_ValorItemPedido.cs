using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaLojaExpress.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ValorItemPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDesconto");

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("47b62d09-8fd6-4baf-bcfb-255168a65b45"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("2882d13e-d256-4ccd-a06b-54055100ec4d"));

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "ItemsPedido",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("652c6058-c225-4352-ba34-01f9df0af9b0"), "meuemail@gmail.com", "Cliente1", "31 91111-1111" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Nome", "Preco", "Quantidade" },
                values: new object[] { new Guid("9caf62cc-6c5e-45d0-ba73-1a6f68a6e2d4"), "Item 1", 10m, 10L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: new Guid("652c6058-c225-4352-ba34-01f9df0af9b0"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("9caf62cc-6c5e-45d0-ba73-1a6f68a6e2d4"));

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "ItemsPedido");

            migrationBuilder.CreateTable(
                name: "ItemDesconto",
                columns: table => new
                {
                    DescontoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDesconto", x => new { x.DescontoId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_ItemDesconto_Descontos_DescontoId",
                        column: x => x.DescontoId,
                        principalTable: "Descontos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemDesconto_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("47b62d09-8fd6-4baf-bcfb-255168a65b45"), "meuemail@gmail.com", "Cliente1", "31 91111-1111" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Nome", "Preco", "Quantidade" },
                values: new object[] { new Guid("2882d13e-d256-4ccd-a06b-54055100ec4d"), "Item 1", 10m, 10L });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDesconto_ItemId",
                table: "ItemDesconto",
                column: "ItemId");
        }
    }
}

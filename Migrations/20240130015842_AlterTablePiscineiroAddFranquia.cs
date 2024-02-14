using Microsoft.EntityFrameworkCore.Migrations;

namespace PoolPiscinas.Migrations
{
    public partial class AlterTablePiscineiroAddFranquia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FranquiaID",
                table: "Piscineiros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Piscineiros_FranquiaID",
                table: "Piscineiros",
                column: "FranquiaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Piscineiros_Franquias_FranquiaID",
                table: "Piscineiros",
                column: "FranquiaID",
                principalTable: "Franquias",
                principalColumn: "FranquiaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Piscineiros_Franquias_FranquiaID",
                table: "Piscineiros");

            migrationBuilder.DropIndex(
                name: "IX_Piscineiros_FranquiaID",
                table: "Piscineiros");

            migrationBuilder.DropColumn(
                name: "FranquiaID",
                table: "Piscineiros");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace PoolPiscinas.Migrations
{
    public partial class agendaNullableOrdemServico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServicos_Agendas_AgendaID",
                table: "OrdemServicos");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaID",
                table: "OrdemServicos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServicos_Agendas_AgendaID",
                table: "OrdemServicos",
                column: "AgendaID",
                principalTable: "Agendas",
                principalColumn: "AgendaID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServicos_Agendas_AgendaID",
                table: "OrdemServicos");

            migrationBuilder.AlterColumn<int>(
                name: "AgendaID",
                table: "OrdemServicos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServicos_Agendas_AgendaID",
                table: "OrdemServicos",
                column: "AgendaID",
                principalTable: "Agendas",
                principalColumn: "AgendaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsCatalog.Migrations
{
    public partial class removeProducer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Producers_ProducerId",
                table: "Cinemas");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_ProducerId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Cinemas");

            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Cinemas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "Cinemas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_ProducerId",
                table: "Cinemas",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Producers_ProducerId",
                table: "Cinemas",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

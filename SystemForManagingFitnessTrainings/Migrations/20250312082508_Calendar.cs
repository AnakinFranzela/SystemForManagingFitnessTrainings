using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemForManagingFitnessTrainings.Migrations
{
    /// <inheritdoc />
    public partial class Calendar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionId",
                table: "Exercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainingSessionId",
                table: "Exercises",
                column: "TrainingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_UserId",
                table: "TrainingSessions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_TrainingSessions_TrainingSessionId",
                table: "Exercises",
                column: "TrainingSessionId",
                principalTable: "TrainingSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_TrainingSessions_TrainingSessionId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_TrainingSessionId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TrainingSessionId",
                table: "Exercises");
        }
    }
}

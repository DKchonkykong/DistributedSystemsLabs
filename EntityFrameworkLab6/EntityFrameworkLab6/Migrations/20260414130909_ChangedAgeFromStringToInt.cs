using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkLab6.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAgeFromStringToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
"UPDATE People SET Age = CASE " +
"WHEN Age = 'Forty' THEN 40 " +
"END WHERE Age IN('Forty')");
            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "People",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
"UPDATE People SET Age = CASE " +
"WHEN Age = 40 THEN 'Forty' " +
"END WHERE Age IN(40)");
            migrationBuilder.AlterColumn<string>(
                name: "Age",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

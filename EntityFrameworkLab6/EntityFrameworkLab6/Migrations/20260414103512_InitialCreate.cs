using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkLab6.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        /// apparently up and down relate to the database like deleting and adding stuff like tables? idk how to check the actual sql database and also same error of not being able to see it?
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressIdentifier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    House_Name_or_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Middle_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_of_Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressIdentifier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_People_Addresses_AddressIdentifier",
                        column: x => x.AddressIdentifier,
                        principalTable: "Addresses",
                        principalColumn: "AddressIdentifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_AddressIdentifier",
                table: "People",
                column: "AddressIdentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}

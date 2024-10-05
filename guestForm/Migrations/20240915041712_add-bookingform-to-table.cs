using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guestForm.Migrations
{
    /// <inheritdoc />
    public partial class addbookingformtotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingForms",
                columns: table => new
                {
                    FormId = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    FacultyName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MobileNo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Department = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DateTimeFrom = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DateTimeUpto = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    GuestNumMaleSin = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GuestNuFemaleSin = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GuestNumCouple = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GuestNumChildren = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IsAdminApproved = table.Column<bool>(type: "BOOLEAN", nullable: false),
                    IsRegistrarApproved = table.Column<bool>(type: "BOOLEAN", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingForms", x => x.FormId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingForms");
        }
    }
}

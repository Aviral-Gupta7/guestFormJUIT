using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guestForm.Migrations
{
    /// <inheritdoc />
    public partial class guidToStringFormid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FormId",
                table: "BookingForms",
                type: "NVARCHAR2(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "RAW(16)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "FormId",
                table: "BookingForms",
                type: "RAW(16)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(450)");
        }
    }
}

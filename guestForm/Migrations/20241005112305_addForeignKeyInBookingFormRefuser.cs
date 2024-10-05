using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guestForm.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyInBookingFormRefuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNo",
                table: "BookingForms",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "BookingForms",
                type: "NVARCHAR2(2000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "BookingForms");

            migrationBuilder.AlterColumn<int>(
                name: "MobileNo",
                table: "BookingForms",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }
    }
}

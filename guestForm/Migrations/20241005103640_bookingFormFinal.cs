using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace guestForm.Migrations
{
    /// <inheritdoc />
    public partial class bookingFormFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Relation",
                table: "BookingForms",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AddColumn<bool>(
                name: "MealsRequired",
                table: "BookingForms",
                type: "BOOLEAN",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MealsRequired",
                table: "BookingForms");

            migrationBuilder.AlterColumn<int>(
                name: "Relation",
                table: "BookingForms",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }
    }
}

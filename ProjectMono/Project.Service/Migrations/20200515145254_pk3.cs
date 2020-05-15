using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Service.Migrations
{
    public partial class pk3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "Abrv", "MakeId", "Name", "VehicleMakeId" },
                values: new object[] { 1, "Corolla", 3, "New redisegnet Corolla 2 gen model b", null });

            migrationBuilder.InsertData(
                table: "VehicleModel",
                columns: new[] { "Id", "Abrv", "MakeId", "Name", "VehicleMakeId" },
                values: new object[] { 2, "MX-6", 4, "Mazda cabrio MX-6", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleModel",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

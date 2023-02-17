using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedisCacheApi.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "FolderForeignId", "LastTimeUsed", "Name" },
                values: new object[] { "File:971f975d-0c7b-4ff7-941a-7c4120100859", "File 1 Content", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 1" });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderForeignId", "Name" },
                values: new object[] { "Folder:64717275-2d1b-4aa7-91bd-da2c2a097842", null, "Folder 1" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "FolderForeignId", "LastTimeUsed", "Name" },
                values: new object[,]
                {
                    { "File:00230b6a-b454-4d0b-9fb9-a5a532264487", "File 3 Content", "Folder:64717275-2d1b-4aa7-91bd-da2c2a097842", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 3" },
                    { "File:9e9861b1-124d-45b4-ba53-501b3e018262", "File 2 Content", "Folder:64717275-2d1b-4aa7-91bd-da2c2a097842", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 2" }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderForeignId", "Name" },
                values: new object[] { "Folder:b9d00fc5-27c8-4a81-8708-2282f8052912", "Folder:64717275-2d1b-4aa7-91bd-da2c2a097842", "Folder 2" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "FolderForeignId", "LastTimeUsed", "Name" },
                values: new object[] { "File:dc840741-0cd5-405c-afa5-f2e55817e1a7", "File 4 Content", "Folder:b9d00fc5-27c8-4a81-8708-2282f8052912", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 4" });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "FolderForeignId", "Name" },
                values: new object[,]
                {
                    { "Folder:62eae15b-fbef-4f6d-9445-89ba67797ba6", "Folder:b9d00fc5-27c8-4a81-8708-2282f8052912", "Folder 3" },
                    { "Folder:bc52e188-520e-4d2a-b010-99ad0993cf2f", "Folder:b9d00fc5-27c8-4a81-8708-2282f8052912", "Folder 4" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "FolderForeignId", "LastTimeUsed", "Name" },
                values: new object[,]
                {
                    { "File:462cae2c-eef9-4624-aea3-35c304bba233", "File 7 Content", "Folder:62eae15b-fbef-4f6d-9445-89ba67797ba6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 7" },
                    { "File:7b803093-480d-4d2e-8d4d-dbe6e514c4f0", "File 6 Content", "Folder:62eae15b-fbef-4f6d-9445-89ba67797ba6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 6" },
                    { "File:7de65e90-7608-470d-a2d7-d0cc56e66e3c", "File 5 Content", "Folder:62eae15b-fbef-4f6d-9445-89ba67797ba6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "File 5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:00230b6a-b454-4d0b-9fb9-a5a532264487");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:462cae2c-eef9-4624-aea3-35c304bba233");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:7b803093-480d-4d2e-8d4d-dbe6e514c4f0");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:7de65e90-7608-470d-a2d7-d0cc56e66e3c");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:971f975d-0c7b-4ff7-941a-7c4120100859");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:9e9861b1-124d-45b4-ba53-501b3e018262");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: "File:dc840741-0cd5-405c-afa5-f2e55817e1a7");

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: "Folder:bc52e188-520e-4d2a-b010-99ad0993cf2f");

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: "Folder:62eae15b-fbef-4f6d-9445-89ba67797ba6");

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: "Folder:b9d00fc5-27c8-4a81-8708-2282f8052912");

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: "Folder:64717275-2d1b-4aa7-91bd-da2c2a097842");
        }
    }
}

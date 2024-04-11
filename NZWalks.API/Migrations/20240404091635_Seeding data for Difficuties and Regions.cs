using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficutiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Names",
                table: "Difficulties",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("947bed47-1b7f-4ed7-89d3-caff90030b3a"), "Hard" },
                    { new Guid("aac0b969-806a-426d-aeba-095be6b354c0"), "Medium" },
                    { new Guid("c75f1fbc-e53b-4684-a6e4-1b418be72111"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("1ef6a9d2-48a1-4528-a1ff-e41dfe38c06b"), "5990", "NelSon", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMhaMUyMmwZFNmsB3fNsXJARE6p8RHXsYCfSM_4qAGHOgn0dhNin0siPsuQpJF66SqrQo&usqp=CAU" },
                    { new Guid("9905f19a-dca5-40a4-8b4f-ffc2090f2976"), "5899", "AuckLand", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fsnapshot.canon-asia.com%2Fvn%2Farticle%2Fviet%2Flandscape-photography-quick-tips-for-stunning-deep-focused-images&psig=AOvVaw10bfNlfnVznIXoqSjdr7n6&ust=1712307601380000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCMDj-ISZqIUDFQAAAAAdAAAAABAE" },
                    { new Guid("b50812fa-ee88-4d12-af90-85e597e45251"), "6983", "SoutnLand", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRejEC0q_n6SqMGbJMC5Q8H1bgsqN0YMln_amAkMSOmHw&s" },
                    { new Guid("e12cb6cb-1560-41ad-a653-34f5d92eca7e"), "6214", "AN Do", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("947bed47-1b7f-4ed7-89d3-caff90030b3a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("aac0b969-806a-426d-aeba-095be6b354c0"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c75f1fbc-e53b-4684-a6e4-1b418be72111"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1ef6a9d2-48a1-4528-a1ff-e41dfe38c06b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9905f19a-dca5-40a4-8b4f-ffc2090f2976"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b50812fa-ee88-4d12-af90-85e597e45251"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e12cb6cb-1560-41ad-a653-34f5d92eca7e"));

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Difficulties",
                newName: "Names");
        }
    }
}

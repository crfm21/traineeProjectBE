using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeApp.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "NVARCHAR(25)", nullable: false),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false),
                    Profile = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteRecipes_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecipePhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "DECIMAL(3,2)", nullable: false),
                    NumberRatings = table.Column<int>(type: "int", nullable: false),
                    TotalRating = table.Column<int>(type: "int", nullable: false),
                    CreatorMemberId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_Members_CreatorMemberId",
                        column: x => x.CreatorMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "NVARCHAR(250)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    NotApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientCompositions_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientCompositions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CreationDate", "IsDeleted", "Name", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 30, 14, 1, 36, 30, DateTimeKind.Local).AddTicks(8520), false, "Egg yolk", null },
                    { 14, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8240), false, "Sour cream", null },
                    { 13, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8230), false, "Lemon", null },
                    { 12, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8230), false, "Butter", null },
                    { 11, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8220), false, "Mushrooms", null },
                    { 10, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8210), false, "Celery", null },
                    { 8, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8190), false, "White onion", null },
                    { 9, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8200), false, "Carrots", null },
                    { 6, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8180), false, "Cloves", null },
                    { 5, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8170), false, "Vanilla", null },
                    { 4, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8160), false, "Cream", null },
                    { 3, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8150), false, "Milk (Cow)", null },
                    { 2, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(7540), false, "Sugar", null },
                    { 7, new DateTime(2022, 6, 30, 14, 1, 36, 47, DateTimeKind.Local).AddTicks(8190), false, "Veal", null }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "CreationDate", "Email", "FirstName", "Gender", "IsBanned", "IsDeleted", "LastName", "NickName", "Password", "Profile", "Salt", "UpdateDate", "UserPhotoPath" },
                values: new object[,]
                {
                    { 4, new DateTime(1951, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 14, 1, 36, 366, DateTimeKind.Local).AddTicks(2660), "lsky@gmail.com", "Luke", 0, false, false, "Skywalker", "LukeSky", "t0PEV7M5bsAQN6S/xSrkiWuluGxFXT+59oSpvHWinjc=", 1, "N4iywHZduExi/ySloRWfDw==", null, null },
                    { 2, new DateTime(1956, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 14, 1, 36, 55, DateTimeKind.Local).AddTicks(4070), "leiaorgana@gmail.com", "Leia", 1, false, false, "Organa", "Leia56", "S1ACOMQSLTfJsGpwYjnNhM4GH2HYRra6A94DwVUEZJg=", 1, "wMyGt8HoMVdxffgKwil9hg==", null, null },
                    { 3, new DateTime(1990, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 14, 1, 36, 209, DateTimeKind.Local).AddTicks(7080), "hp@gmail.com", "Harry", 0, false, false, "Potter", "HarryP", "N14bWq7nmFIqzHDrX6SWDYEgjF38VW6/BzY6KzUlYdQ=", 1, "0GuIFO5154j74BsW/wiBLQ==", null, null },
                    { 1, new DateTime(1992, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 14, 1, 36, 518, DateTimeKind.Local).AddTicks(6700), "c@email.com", "Cintia", 1, false, false, "Mendes", "cintiaM", "tu5mm7icrjKPqEiAjW4oor55/73/+flEyzXPSUqPMfs=", 0, "P6LqA5jSxqz3g1dEe5p4rg==", null, null }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Category", "CreationDate", "CreatorMemberId", "Description", "Difficulty", "Duration", "IsDeleted", "NumberRatings", "PublishDate", "Rating", "RecipePhotoPath", "Servings", "Title", "TotalRating", "UpdateDate", "isPublished" },
                values: new object[] { 1, 6, new DateTime(2022, 6, 30, 14, 1, 36, 656, DateTimeKind.Local).AddTicks(1550), 2, "...", 0, "2:15", false, 0, new DateTime(2022, 6, 30, 14, 1, 36, 656, DateTimeKind.Local).AddTicks(8290), 0m, null, 4, "Crème brûlée", 0, null, true });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Category", "CreationDate", "CreatorMemberId", "Description", "Difficulty", "Duration", "IsDeleted", "NumberRatings", "PublishDate", "Rating", "RecipePhotoPath", "Servings", "Title", "TotalRating", "UpdateDate", "isPublished" },
                values: new object[] { 2, 2, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(630), 4, "...", 0, "0:45", false, 0, null, 0m, null, 6, "Blanquette de veau", 0, null, false });

            migrationBuilder.InsertData(
                table: "IngredientCompositions",
                columns: new[] { "Id", "CreationDate", "IngredientId", "IsDeleted", "Name", "Quantity", "RecipeId", "Unit", "UpdateDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(3930), 1, false, "Egg yolk", 4.0, 1, 0, null },
                    { 2, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(7960), 2, false, "Sugar", 130.0, 1, 2, null },
                    { 3, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8930), 3, false, "Milk (Cow)", 12.0, 1, 5, null },
                    { 4, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8950), 4, false, "Cream", 35.0, 1, 5, null },
                    { 5, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(8960), 5, false, "Vanilla", 2.0, 1, 0, null },
                    { 6, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9020), 6, false, "Cloves", 3.0, 2, 0, null },
                    { 7, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9040), 7, false, "Veal", 1.0, 2, 3, null },
                    { 8, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9050), 8, false, "White onion", 2.0, 2, 0, null },
                    { 9, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9060), 9, false, "Carrots", 2.0, 2, 0, null },
                    { 10, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9070), 10, false, "Celery", 1.0, 2, 0, null },
                    { 11, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9130), 11, false, "Mushrooms", 250.0, 2, 2, null },
                    { 12, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9140), 12, false, "Butter", 85.0, 2, 2, null },
                    { 13, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9150), 13, false, "Lemon", 0.5, 2, 0, null },
                    { 14, new DateTime(2022, 6, 30, 14, 1, 36, 657, DateTimeKind.Local).AddTicks(9160), 14, false, "Sour cream", 2.0, 2, 6, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRecipes_MemberId_RecipeId",
                table: "FavoriteRecipes",
                columns: new[] { "MemberId", "RecipeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientCompositions_IngredientId",
                table: "IngredientCompositions",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientCompositions_RecipeId",
                table: "IngredientCompositions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_NickName",
                table: "Members",
                column: "NickName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CreatorMemberId",
                table: "Recipes",
                column: "CreatorMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FavoriteRecipes");

            migrationBuilder.DropTable(
                name: "IngredientCompositions");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}

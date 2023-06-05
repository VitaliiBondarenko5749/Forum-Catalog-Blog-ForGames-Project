using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog_of_Games_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "gamecatalog");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developers",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MainImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "gamecatalog",
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedReplies",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedReplies_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalSchema: "gamecatalog",
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesCategories",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "gamecatalog",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesCategories_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesComments",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesComments_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesDevelopers",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesDevelopers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesDevelopers_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalSchema: "gamecatalog",
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesDevelopers_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesImages",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Directory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesImages_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesLanguages",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesLanguages_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "gamecatalog",
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesPlatforms",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlatforms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamesPlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalSchema: "gamecatalog",
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPlatforms_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalSchema: "gamecatalog",
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentsReplies",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReplyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsReplies_GamesComments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "gamecatalog",
                        principalTable: "GamesComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentsReplies_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalSchema: "gamecatalog",
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedComments",
                schema: "gamecatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedComments_GamesComments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "gamecatalog",
                        principalTable: "GamesComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Categories",
                columns: new[] { "Id", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { new Guid("01742c20-c834-4ef2-ad48-169152a8b5c8"), "Natus odio quia.", "https://kenya.net", "corporis" },
                    { new Guid("1cb1904e-6f7d-4cae-a93b-0d423e548d65"), "Et laborum magni dolorum culpa.", "https://joe.com", "quas" },
                    { new Guid("59dda66b-0782-458f-993a-6a7f0e8e03f5"), "Cumque ipsam aut dicta sit.", "https://salvatore.info", "et" },
                    { new Guid("5e054627-9fa1-4ed2-941d-3bee0f996d8e"), "Et illum explicabo quidem ut.", "https://irwin.net", "sunt" },
                    { new Guid("800b66e4-2c2b-47ab-9f29-d367acaea4f4"), "Ut ipsa tenetur nisi ab aut quos necessitatibus possimus.", "https://creola.info", "sed" },
                    { new Guid("839a3dfe-d8d6-42b5-a91e-c523e095bad9"), "Asperiores et corrupti.", "https://presley.info", "aut" },
                    { new Guid("890cf45a-f134-44bb-b58e-bf1423b6aab7"), "Eum fugiat ut.", "http://demetrius.info", "neque" },
                    { new Guid("aba57c67-5bbc-4d58-ac2a-a69f6b31b7cc"), "Voluptatem officiis ut neque et quis omnis.", "https://allen.biz", "eum" },
                    { new Guid("c27b5421-01d5-444d-8139-2f6732f14790"), "Molestias voluptatum et.", "http://sebastian.name", "inventore" },
                    { new Guid("f0a335ca-f6c7-493d-b355-f6666b170f4b"), "Necessitatibus quas aspernatur aliquam qui voluptatem eos perspiciatis.", "http://sonia.info", "tempora" }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Developers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00666526-5c80-4e0c-979d-0c538c2ea370"), "Swaniawski Inc" },
                    { new Guid("230510cb-7ffe-47d7-87b1-ee148c8f6318"), "Wolff Group" },
                    { new Guid("2b4835fe-eb0c-4ebf-b691-9ca03c629485"), "Blick - Schulist" },
                    { new Guid("30c6d8a3-3051-4730-9562-371060b99329"), "Macejkovic - Maggio" },
                    { new Guid("3dbea998-d4eb-442a-a18b-6c1322cf9d71"), "Towne - Goyette" },
                    { new Guid("4c5cf49f-08ae-468b-809f-55075906c2af"), "Weber - Kohler" },
                    { new Guid("95dc0713-870d-48e8-9593-2369cd8a2826"), "Kihn - Hauck" },
                    { new Guid("a1d4422a-7492-4d40-b1b5-a19914d5ad42"), "Veum, VonRueden and Bradtke" },
                    { new Guid("be70b939-5fd8-4b95-87fe-a9c449401fdc"), "Kozey and Sons" },
                    { new Guid("d9f4651f-1fa8-4a4f-9f90-69c2a1520f7d"), "Emmerich, Jerde and Torphy" }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0f09457e-94eb-4f1c-a84a-a6bc4677c3fa"), "III" },
                    { new Guid("43e08303-4eab-4889-a535-1c0bb478aa11"), "Jr." },
                    { new Guid("4bb0b704-b6c0-418f-9d61-8b38ba7d4b8b"), "V" },
                    { new Guid("577da1fe-53b2-4c51-ba91-b02aedb6c1b0"), "PhD" },
                    { new Guid("7e5c42ce-9641-4537-9f6c-5e5498cfff4d"), "I" },
                    { new Guid("9a60245c-c291-4ea9-a15f-41d8133d25b2"), "IV" },
                    { new Guid("b644f2c9-0c99-4481-88e3-646d1f44f225"), "DDS" },
                    { new Guid("bc174d83-d658-4015-bf0d-359f2e62003a"), "DD" },
                    { new Guid("cb371803-f1be-43a0-ae22-1172238ce6a3"), "Ph" },
                    { new Guid("fc883fb7-5c8c-4a2e-b8d3-ea27c1930141"), "V5" }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4c43c3f4-1901-4a9a-930b-3172f769e15f"), "Koss - Bogan" },
                    { new Guid("4d40cfeb-ed95-48c9-bf29-48e56c22ae2d"), "Bernhard - Emard" },
                    { new Guid("5ed8c52b-c458-4d3b-835e-b647d0b40344"), "Beier and Sons" },
                    { new Guid("61b7f7d5-72c7-4053-9149-ce4e769ea7d2"), "Kuphal, Gleason and Lehner" },
                    { new Guid("6b0af07e-ed30-40ed-9ac9-5b106ae07e69"), "Mosciski, Fadel and Sanford" },
                    { new Guid("78cf3d8c-e3ae-4736-95a5-510672b70287"), "Bechtelar - Hills" },
                    { new Guid("7d86b2dd-1f37-4e13-abe3-bd8202917c31"), "Casper, McGlynn and Auer" },
                    { new Guid("c68082c3-8087-48cb-b78e-b859ae52878b"), "Rutherford, Pagac and Rempel" },
                    { new Guid("e8f89eef-e554-42fd-9b9b-e9e885f123b8"), "Farrell, Barrows and Reichel" },
                    { new Guid("f54b372b-fd4c-4813-b761-844ffd40d997"), "Parisian, Lesch and Kihn" }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0771ece2-bdb8-449f-b5c7-fc25f5f16be8"), "Streich - Kozey" },
                    { new Guid("46ba4cf6-642f-4e9f-b04e-230741c175fc"), "Legros Inc" },
                    { new Guid("7dde6eb8-52f6-4e41-bed3-758aecb06451"), "Mayert - Lubowitz" },
                    { new Guid("89cc8431-26ea-4b39-b7bf-942998b96586"), "Hodkiewicz - Jacobs" },
                    { new Guid("affadb3f-ca23-4a79-95ea-863fe53f24bb"), "Wolf - Goyette" },
                    { new Guid("c38465cb-0d33-4098-9b85-7e441224d5e5"), "Erdman, Reilly and Walker" },
                    { new Guid("c668c3a6-71b6-4421-bf39-bdf714bb18a2"), "Bradtke - Olson" },
                    { new Guid("dae2fcf3-5146-4c32-9085-d6e4b2b50a62"), "Powlowski, Lynch and Gutkowski" },
                    { new Guid("eac90e78-203d-4386-ab50-335d79d60f55"), "Jaskolski, Lueilwitz and King" },
                    { new Guid("f25add5d-2e8d-43df-9d2a-bd3e959276a8"), "Bauch, Rohan and Herzog" }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Replies",
                columns: new[] { "Id", "Content", "CreatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("04c856ee-1e60-4308-a965-8c3b6467a4e4"), "Et ut voluptates est cupiditate qui assumenda suscipit harum corporis.", new DateTime(2023, 1, 16, 16, 50, 37, 989, DateTimeKind.Local).AddTicks(7473), new Guid("6f77e1ac-1066-4cee-96ca-28b9573765f8") },
                    { new Guid("2f417d92-9c7c-4757-8c2e-7fdb9e19e5a9"), "Optio et voluptatem.", new DateTime(2022, 7, 31, 9, 12, 58, 286, DateTimeKind.Local).AddTicks(9905), new Guid("e33f14c7-fdff-4a6d-bdf5-11a355225ed5") },
                    { new Guid("3541f196-d263-4e72-b714-34550d90cd6b"), "Fugit ut repellendus quia voluptatibus.", new DateTime(2022, 10, 25, 13, 14, 30, 821, DateTimeKind.Local).AddTicks(2117), new Guid("82da35fb-3b69-4251-8b19-690b1035c0b2") },
                    { new Guid("371cd1dd-088a-4370-acb2-16d2282f18aa"), "Ipsum et consequuntur soluta ex necessitatibus nobis inventore voluptatem voluptatibus.", new DateTime(2022, 10, 8, 23, 29, 57, 488, DateTimeKind.Local).AddTicks(4843), new Guid("fda6b3db-1e3c-495f-aaed-f63f4324b4ac") },
                    { new Guid("43212b27-1c33-4c38-99b9-f9a5bbbf3cd6"), "Autem vel deleniti est voluptatibus dolor eligendi adipisci ut provident.", new DateTime(2023, 6, 3, 3, 22, 39, 932, DateTimeKind.Local).AddTicks(58), new Guid("be23fffc-724e-4cc6-8d6b-8161a6d33432") },
                    { new Guid("4c7d4265-62b3-4b5b-8c57-b468ff0be799"), "Eligendi fugiat et animi earum suscipit non.", new DateTime(2022, 7, 27, 20, 41, 43, 229, DateTimeKind.Local).AddTicks(9723), new Guid("adcf7c95-b573-4b13-8ef2-5cbf831a2da3") },
                    { new Guid("580f2831-d24c-4068-9317-3f4c799a0548"), "Praesentium amet modi assumenda excepturi illo repellendus.", new DateTime(2023, 3, 25, 13, 4, 39, 581, DateTimeKind.Local).AddTicks(7271), new Guid("0bf8662c-4138-4e56-8f81-f56c8dfdb80c") },
                    { new Guid("613193ff-a3a0-4de7-99c1-b032a9e75bc3"), "Ut atque et et accusamus maxime.", new DateTime(2022, 8, 19, 3, 59, 31, 555, DateTimeKind.Local).AddTicks(3538), new Guid("0b0d47d4-ba3e-47cd-a266-d27cc03042a8") },
                    { new Guid("78cdd64e-da6f-45e9-91df-ec9891563eb0"), "Beatae labore est dolorem.", new DateTime(2023, 3, 8, 18, 17, 47, 934, DateTimeKind.Local).AddTicks(307), new Guid("89e2229f-6ae3-4831-8f90-aaed6fb7ec95") },
                    { new Guid("7ba17ea3-1117-4372-a8bf-771b17ed8e4f"), "Quod est voluptatem et.", new DateTime(2022, 6, 25, 3, 13, 45, 519, DateTimeKind.Local).AddTicks(4815), new Guid("a21f682d-4d48-4927-8cb7-a029b83e215b") },
                    { new Guid("8ff74edd-673d-4f69-963b-aa650d34936c"), "Natus accusamus cumque provident vero ea.", new DateTime(2023, 5, 20, 17, 31, 38, 140, DateTimeKind.Local).AddTicks(3595), new Guid("435a178b-c1e5-4325-982b-1439c7c0cd06") },
                    { new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), "Quis maiores non deserunt est aliquam.", new DateTime(2023, 1, 19, 12, 33, 49, 611, DateTimeKind.Local).AddTicks(149), new Guid("aed81599-954d-4116-ab4a-bc575278092e") },
                    { new Guid("949305fa-093f-4c7f-9daf-fb60deef2816"), "Ipsa enim corrupti.", new DateTime(2023, 2, 19, 0, 18, 56, 172, DateTimeKind.Local).AddTicks(6477), new Guid("081631e2-d7f2-4ac3-a236-c6da26d8723a") },
                    { new Guid("b7215396-84ef-47b5-8498-838b794583b2"), "Deleniti ea molestiae voluptatum atque ut quo sed.", new DateTime(2022, 12, 15, 4, 1, 55, 716, DateTimeKind.Local).AddTicks(6640), new Guid("49ff5d3b-003d-4c86-a93e-4326538d44ae") },
                    { new Guid("c9771339-9f5e-4359-b2e9-057b2d70d7a4"), "Blanditiis voluptates eum omnis.", new DateTime(2022, 8, 10, 4, 17, 36, 932, DateTimeKind.Local).AddTicks(9400), new Guid("f77a564a-3f28-4dde-aac3-aecb29e15b5f") },
                    { new Guid("cb48d029-3295-4be0-9389-7a7ca1d4a361"), "Ut vel omnis quam totam quis quo ut voluptatem.", new DateTime(2022, 8, 24, 21, 21, 52, 85, DateTimeKind.Local).AddTicks(6550), new Guid("c23b3706-23ce-4dda-8595-b7361b28832e") },
                    { new Guid("cc0548f9-8af9-4bff-a9a5-5d65de4b306f"), "Dolores fugiat qui distinctio quos nobis aut est et.", new DateTime(2023, 2, 20, 15, 12, 26, 975, DateTimeKind.Local).AddTicks(1156), new Guid("b88ee8e3-8bbb-48ba-9f09-840437fa4252") },
                    { new Guid("ccd21cf0-ec13-476a-90f0-aeffba3eb893"), "Quis fuga voluptatibus adipisci omnis ut aut optio dolores impedit.", new DateTime(2023, 3, 15, 23, 28, 2, 346, DateTimeKind.Local).AddTicks(9888), new Guid("d3e3c11a-9813-40a2-b4d8-bec9c7470112") },
                    { new Guid("da796c51-871a-4fd1-b5e8-6e20057c7357"), "Repudiandae nobis cumque.", new DateTime(2022, 8, 7, 13, 3, 58, 273, DateTimeKind.Local).AddTicks(595), new Guid("d22d34cb-8b3b-47d4-9063-ed5d0d36ce78") },
                    { new Guid("e0e20395-6887-4ef2-98e9-ea7249acf461"), "Esse aut non et et quae facilis.", new DateTime(2022, 10, 2, 15, 15, 8, 974, DateTimeKind.Local).AddTicks(7626), new Guid("7dbd7ebd-f699-42a1-8ba4-1ee06702b8e4") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "Games",
                columns: new[] { "Id", "Description", "MainImage", "Name", "PublisherId", "Rating", "ReleaseDate" },
                values: new object[,]
                {
                    { new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), "Natus quam impedit. Nisi et quas ut dolor ut nam itaque aut. Deleniti soluta et dolorem alias eum magni sint aperiam. Qui non perferendis est ad nihil. Voluptas praesentium tenetur excepturi unde voluptates deleniti.", "https://picsum.photos/640/480/?image=665", "Unbranded Concrete Computer", new Guid("c38465cb-0d33-4098-9b85-7e441224d5e5"), 5.027195f, new DateTime(2023, 3, 24, 0, 27, 2, 368, DateTimeKind.Local).AddTicks(5313) },
                    { new Guid("0acface4-b544-4f75-a601-48f24c46da9d"), "Rerum soluta adipisci. Quo commodi rerum error sequi sed enim odit eos. Voluptatem non magnam et laborum dolorem eum veritatis.", "https://picsum.photos/640/480/?image=329", "Licensed Rubber Ball", new Guid("0771ece2-bdb8-449f-b5c7-fc25f5f16be8"), 5.5814447f, new DateTime(2019, 5, 15, 17, 25, 6, 264, DateTimeKind.Local).AddTicks(8273) },
                    { new Guid("12cf8d44-f0ae-41e4-a309-848c4f819b86"), "Est et molestias alias tenetur. Animi voluptatum eius quasi a vero praesentium. Ducimus non non. Asperiores officia est placeat et inventore consequuntur nobis ex. Et libero qui sunt.", "https://picsum.photos/640/480/?image=22", "Handcrafted Granite Chair", new Guid("7dde6eb8-52f6-4e41-bed3-758aecb06451"), 8.50773f, new DateTime(2021, 11, 23, 17, 20, 10, 998, DateTimeKind.Local).AddTicks(7011) },
                    { new Guid("2013d294-ef22-4325-8a85-6f00832247f8"), "Adipisci iusto eum natus. Sint odit quod qui nesciunt ut quia ducimus aut voluptas. Animi minus dolore itaque consectetur quaerat. Architecto laboriosam inventore aut labore.", "https://picsum.photos/640/480/?image=460", "Licensed Soft Soap", new Guid("c668c3a6-71b6-4421-bf39-bdf714bb18a2"), 8.593234f, new DateTime(2020, 12, 22, 8, 0, 54, 793, DateTimeKind.Local).AddTicks(8530) },
                    { new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), "Id est libero ad. Aut nulla distinctio quis deserunt tempora dolor ratione minima quas. Optio ratione sunt dignissimos aut eos deserunt sed. Eos dolor magni et voluptatem qui. Quam rerum et excepturi deleniti architecto.", "https://picsum.photos/640/480/?image=556", "Incredible Granite Gloves", new Guid("46ba4cf6-642f-4e9f-b04e-230741c175fc"), 2.0580785f, new DateTime(2019, 4, 10, 7, 41, 58, 34, DateTimeKind.Local).AddTicks(6047) },
                    { new Guid("2db4b7ed-55b1-4900-b4f1-5fbed31706ed"), "Dolorum sapiente consectetur optio distinctio. Eius natus dolorum. Rem ad fugit illo repellendus animi sit veritatis sint. Quia fugit provident in ea sit tenetur fugit et quas. Quos minima necessitatibus ea quo rem hic qui non sit. Nihil iusto dolorem.", "https://picsum.photos/640/480/?image=699", "Licensed Fresh Chips", new Guid("0771ece2-bdb8-449f-b5c7-fc25f5f16be8"), 2.418077f, new DateTime(2018, 8, 11, 5, 7, 42, 492, DateTimeKind.Local).AddTicks(476) },
                    { new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f"), "Cumque corrupti ea. Eos illo nesciunt delectus aspernatur. Delectus illo nostrum sint eligendi.", "https://picsum.photos/640/480/?image=878", "Practical Granite Fish", new Guid("eac90e78-203d-4386-ab50-335d79d60f55"), 5.873503f, new DateTime(2022, 11, 14, 11, 28, 26, 104, DateTimeKind.Local).AddTicks(9354) },
                    { new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), "Tempore laudantium omnis ipsa in. Distinctio quia odit dolor commodi quos et ab et. Sint voluptatem est voluptas excepturi tempora dolorum quae. Quos rerum aut commodi ad consequatur et repudiandae.", "https://picsum.photos/640/480/?image=11", "Practical Rubber Keyboard", new Guid("7dde6eb8-52f6-4e41-bed3-758aecb06451"), 5.002521f, new DateTime(2022, 5, 11, 8, 5, 51, 595, DateTimeKind.Local).AddTicks(4507) },
                    { new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b"), "Neque labore et quisquam optio perspiciatis. Laborum qui ut voluptatum enim nam quos et at reiciendis. Totam aut totam consequatur.", "https://picsum.photos/640/480/?image=241", "Sleek Fresh Salad", new Guid("c668c3a6-71b6-4421-bf39-bdf714bb18a2"), 8.853101f, new DateTime(2018, 12, 21, 21, 2, 0, 230, DateTimeKind.Local).AddTicks(7882) },
                    { new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), "Tenetur ut eius dignissimos adipisci aliquid adipisci. Recusandae ut impedit quo sint. Accusantium neque in ut. Et voluptate perferendis voluptas ab et dolorum deleniti nihil. Est voluptatem molestiae et ut aut autem autem dolores.", "https://picsum.photos/640/480/?image=697", "Intelligent Frozen Mouse", new Guid("c38465cb-0d33-4098-9b85-7e441224d5e5"), 5.4712977f, new DateTime(2022, 9, 2, 10, 1, 16, 425, DateTimeKind.Local).AddTicks(1873) },
                    { new Guid("83b7fb3d-b400-46f0-882f-d8bd72c58ad2"), "Et numquam odit sed quis earum dolore ipsa occaecati ut. Cupiditate delectus doloribus. Quam in aperiam ipsa veritatis enim et sapiente.", "https://picsum.photos/640/480/?image=106", "Generic Cotton Towels", new Guid("affadb3f-ca23-4a79-95ea-863fe53f24bb"), 4.820331f, new DateTime(2021, 10, 14, 6, 50, 1, 526, DateTimeKind.Local).AddTicks(6092) },
                    { new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374"), "Fugit laudantium quae laboriosam repudiandae ut quos. Ratione quia ipsa quasi. Vel in culpa ut ipsam aut labore velit doloremque. Qui veritatis nostrum repellat officiis maxime ipsa incidunt eaque commodi. Eius amet quidem. Ex vel quo ut qui nobis ea sapiente nostrum sed.", "https://picsum.photos/640/480/?image=708", "Licensed Granite Tuna", new Guid("c38465cb-0d33-4098-9b85-7e441224d5e5"), 2.376966f, new DateTime(2022, 8, 4, 17, 38, 39, 19, DateTimeKind.Local).AddTicks(6488) },
                    { new Guid("a53146ef-533e-4c99-b24b-5d3d5b30c3d9"), "Quia molestias a sint vel porro id. Commodi labore animi reprehenderit est aperiam occaecati in. Fugit nesciunt omnis quaerat omnis veniam maiores. Modi reiciendis porro consequuntur at.", "https://picsum.photos/640/480/?image=132", "Sleek Metal Computer", new Guid("affadb3f-ca23-4a79-95ea-863fe53f24bb"), 3.176398f, new DateTime(2018, 7, 27, 18, 16, 56, 21, DateTimeKind.Local).AddTicks(3430) },
                    { new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48"), "Et corrupti ut. Eos quia sapiente odio. Dolor facilis voluptate. Inventore eius reiciendis tenetur fugiat voluptatem itaque voluptas.", "https://picsum.photos/640/480/?image=980", "Ergonomic Wooden Hat", new Guid("f25add5d-2e8d-43df-9d2a-bd3e959276a8"), 4.4026155f, new DateTime(2021, 10, 22, 4, 6, 19, 432, DateTimeKind.Local).AddTicks(5279) },
                    { new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658"), "Ut voluptas numquam porro quod. Qui harum error dolor dolor beatae ipsum. Qui corrupti sed et maxime facilis repellat fugiat. Et soluta occaecati et.", "https://picsum.photos/640/480/?image=898", "Tasty Fresh Shirt", new Guid("f25add5d-2e8d-43df-9d2a-bd3e959276a8"), 3.0362406f, new DateTime(2021, 10, 24, 22, 3, 44, 760, DateTimeKind.Local).AddTicks(8486) },
                    { new Guid("c7cba1c7-088e-4486-855b-8c129372933e"), "Esse autem qui asperiores earum suscipit eius. Mollitia et nulla ea et enim ut. Odio accusantium itaque. Incidunt accusantium qui veritatis aliquam aperiam autem ut provident quia.", "https://picsum.photos/640/480/?image=113", "Awesome Fresh Pants", new Guid("eac90e78-203d-4386-ab50-335d79d60f55"), 5.475442f, new DateTime(2019, 11, 14, 1, 4, 16, 480, DateTimeKind.Local).AddTicks(5348) },
                    { new Guid("e4653a5d-eae3-45e9-a225-bddfd0ec3d6b"), "Rerum rem quo. Accusantium quaerat et quidem commodi adipisci mollitia. Aut qui velit odit ratione illo quia. Eveniet odio illum. Atque esse maxime deleniti.", "https://picsum.photos/640/480/?image=150", "Practical Cotton Car", new Guid("eac90e78-203d-4386-ab50-335d79d60f55"), 1.2486457f, new DateTime(2020, 8, 22, 2, 23, 53, 891, DateTimeKind.Local).AddTicks(4310) },
                    { new Guid("ec84054e-c615-4eb8-b3fd-d42031c15229"), "Totam magnam at quo quidem minima cum architecto et. Modi eius facere ut eum. Iste aspernatur enim sapiente. Voluptatem itaque sit veniam perferendis est nisi aut.", "https://picsum.photos/640/480/?image=570", "Unbranded Concrete Bacon", new Guid("c38465cb-0d33-4098-9b85-7e441224d5e5"), 3.1077425f, new DateTime(2022, 9, 19, 2, 51, 15, 541, DateTimeKind.Local).AddTicks(6890) },
                    { new Guid("edd4a334-2ea6-4e9c-8082-64182528de44"), "Expedita incidunt officia quisquam vero qui tenetur nesciunt. Dignissimos minus nemo aut sint sint aut. Laboriosam dolor dolore. Aspernatur accusantium officia qui accusamus qui. Blanditiis voluptas voluptas. Dolorem officiis placeat explicabo qui mollitia temporibus reprehenderit dolor.", "https://picsum.photos/640/480/?image=167", "Licensed Steel Salad", new Guid("46ba4cf6-642f-4e9f-b04e-230741c175fc"), 4.5374684f, new DateTime(2022, 6, 30, 2, 8, 44, 722, DateTimeKind.Local).AddTicks(4494) },
                    { new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8"), "Hic et fugiat id repellat asperiores autem. Dignissimos alias dolore id. Nihil voluptatibus saepe voluptatem. Quia facere quas aspernatur sed possimus illo dignissimos. Accusamus ut esse quo. Et modi cupiditate quam aut molestiae.", "https://picsum.photos/640/480/?image=62", "Intelligent Soft Computer", new Guid("dae2fcf3-5146-4c32-9085-d6e4b2b50a62"), 8.189544f, new DateTime(2022, 4, 29, 14, 27, 15, 540, DateTimeKind.Local).AddTicks(9385) }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "LikedReplies",
                columns: new[] { "Id", "ReplyId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0bdb4377-6afd-4859-b39d-c23c823d4486"), new Guid("3541f196-d263-4e72-b714-34550d90cd6b"), new Guid("67f9a62f-f748-44e1-b27f-abcdc91bc799") },
                    { new Guid("16d2e7ed-9fbc-419b-add7-79b19397b92f"), new Guid("949305fa-093f-4c7f-9daf-fb60deef2816"), new Guid("a047acd3-e5a1-479e-9d44-2c4731ac49f2") },
                    { new Guid("3221c6f5-6ae2-4476-81e8-0f290491399c"), new Guid("cb48d029-3295-4be0-9389-7a7ca1d4a361"), new Guid("cf378e6e-f7e0-497c-a856-0d47ed337d81") },
                    { new Guid("50991ebe-c964-426f-967e-f0a8c4c3faf6"), new Guid("580f2831-d24c-4068-9317-3f4c799a0548"), new Guid("fad43cff-2a72-49a8-88db-fd93bad97584") },
                    { new Guid("52c1679e-fadf-47ed-8e59-6208056bca41"), new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), new Guid("3651e107-dbed-422c-948b-adc7bb9a57e2") },
                    { new Guid("79fa9021-ec95-418b-932f-fcec52ec9ead"), new Guid("c9771339-9f5e-4359-b2e9-057b2d70d7a4"), new Guid("1f62abe8-46ed-48da-bfae-5247aee6768e") },
                    { new Guid("7add1da7-a070-4440-a5a5-ea87d49ab74a"), new Guid("580f2831-d24c-4068-9317-3f4c799a0548"), new Guid("9c5a7eb4-bd7e-4d86-b43a-fa70264de8f8") },
                    { new Guid("8e3204f4-8aba-41b7-8c9d-a86bfc58e0dc"), new Guid("ccd21cf0-ec13-476a-90f0-aeffba3eb893"), new Guid("9c4fad20-5a1b-4654-812f-0aedd4b667d3") },
                    { new Guid("91fdb0fd-6fe6-40f1-bd63-e63c454e5be3"), new Guid("da796c51-871a-4fd1-b5e8-6e20057c7357"), new Guid("2201e0d2-2c0e-43b1-9cd6-bbc5b747ed1d") },
                    { new Guid("97b3210a-5508-4482-ab76-f40845beff14"), new Guid("7ba17ea3-1117-4372-a8bf-771b17ed8e4f"), new Guid("78fa5c6b-5d38-4f66-857e-0c4fe112e5bc") },
                    { new Guid("abd54cee-5709-41bf-83c5-627a0c65539b"), new Guid("04c856ee-1e60-4308-a965-8c3b6467a4e4"), new Guid("b7d4a1fe-e743-49e6-bd8d-816fbe2b2ea6") },
                    { new Guid("b5d43e39-08fa-4819-86d8-ebded9f1d8ae"), new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), new Guid("40a9d013-593f-4266-9cbf-0e472224b094") },
                    { new Guid("b9534c7a-b093-4c11-9ea1-a3ed045372e8"), new Guid("b7215396-84ef-47b5-8498-838b794583b2"), new Guid("2675804e-5649-4539-857d-49cc829ddc9f") },
                    { new Guid("c231a6b7-e7a7-4bbd-9242-77cb24a2b103"), new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), new Guid("595c74b7-6f4b-4511-bc08-cc9b41b3ae31") },
                    { new Guid("ca89f220-0dba-47ae-ad49-3ce8fe60fd96"), new Guid("7ba17ea3-1117-4372-a8bf-771b17ed8e4f"), new Guid("dfba4a58-947a-4755-b496-0f7687fa037c") },
                    { new Guid("e6bf93f0-59e1-49ba-b148-808b38651cfc"), new Guid("8ff74edd-673d-4f69-963b-aa650d34936c"), new Guid("7f368fe5-87aa-4307-96eb-5c8ac2ef92cd") },
                    { new Guid("e90ab119-d586-40b5-a5b5-c3aabd6ff39b"), new Guid("43212b27-1c33-4c38-99b9-f9a5bbbf3cd6"), new Guid("aeca4234-2f10-454d-8f1a-11175e33a090") },
                    { new Guid("f1e6571d-4550-419c-9ab9-1edf7ae861da"), new Guid("c9771339-9f5e-4359-b2e9-057b2d70d7a4"), new Guid("79444cc9-65b6-40d5-a3b6-3671067a74d9") },
                    { new Guid("fa1e30e4-f62d-45c9-b5a7-ea79857d5b30"), new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), new Guid("8ad68933-b0d9-447b-90a6-43d23b58ae29") },
                    { new Guid("fa2933ba-670e-4924-a742-d732a79ef4fa"), new Guid("90b73cb8-7977-43d3-9319-df3f6538796c"), new Guid("8f58939b-db18-46cc-913c-3fb8683f4e30") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesCategories",
                columns: new[] { "Id", "CategoryId", "GameId" },
                values: new object[,]
                {
                    { new Guid("0b44ca1b-b7b4-4ddf-9bc3-351872913f22"), new Guid("59dda66b-0782-458f-993a-6a7f0e8e03f5"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b") },
                    { new Guid("138a21a0-ab47-4521-b1c6-975c5d5950b5"), new Guid("01742c20-c834-4ef2-ad48-169152a8b5c8"), new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374") },
                    { new Guid("1c7c852c-594c-4601-84a6-dc4ef6b922e6"), new Guid("aba57c67-5bbc-4d58-ac2a-a69f6b31b7cc"), new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658") },
                    { new Guid("1fc22465-a262-4fef-a9ac-fb1a45503293"), new Guid("800b66e4-2c2b-47ab-9f29-d367acaea4f4"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c") },
                    { new Guid("27f905e4-cebf-4bde-bb04-fb517614c0d2"), new Guid("c27b5421-01d5-444d-8139-2f6732f14790"), new Guid("83b7fb3d-b400-46f0-882f-d8bd72c58ad2") },
                    { new Guid("3812c0fd-b89c-4674-a7ed-e14282f313a0"), new Guid("c27b5421-01d5-444d-8139-2f6732f14790"), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92") },
                    { new Guid("58c43022-e5da-4766-84db-82c3095de0ab"), new Guid("839a3dfe-d8d6-42b5-a91e-c523e095bad9"), new Guid("2db4b7ed-55b1-4900-b4f1-5fbed31706ed") },
                    { new Guid("641f6a8c-c0b8-4da7-912b-19405613c8b4"), new Guid("1cb1904e-6f7d-4cae-a93b-0d423e548d65"), new Guid("0acface4-b544-4f75-a601-48f24c46da9d") },
                    { new Guid("762383e1-27a4-4864-8732-d89f5e37aeb1"), new Guid("01742c20-c834-4ef2-ad48-169152a8b5c8"), new Guid("83b7fb3d-b400-46f0-882f-d8bd72c58ad2") },
                    { new Guid("7a07f535-01cf-4ac0-ac66-d4402f9acc6b"), new Guid("5e054627-9fa1-4ed2-941d-3bee0f996d8e"), new Guid("83b7fb3d-b400-46f0-882f-d8bd72c58ad2") },
                    { new Guid("b33aa9df-138d-4eed-a880-58ff899c2f9e"), new Guid("839a3dfe-d8d6-42b5-a91e-c523e095bad9"), new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8") },
                    { new Guid("b74da591-3f13-435e-b727-d9c07af77590"), new Guid("59dda66b-0782-458f-993a-6a7f0e8e03f5"), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48") },
                    { new Guid("b88e9030-e3c9-40bd-becb-fda80083f0bd"), new Guid("800b66e4-2c2b-47ab-9f29-d367acaea4f4"), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48") },
                    { new Guid("bb8bf41f-bc49-45b7-a75b-d72728403f22"), new Guid("890cf45a-f134-44bb-b58e-bf1423b6aab7"), new Guid("2db4b7ed-55b1-4900-b4f1-5fbed31706ed") },
                    { new Guid("bdd643e4-ed96-417b-97c2-cbfef3ea82d6"), new Guid("aba57c67-5bbc-4d58-ac2a-a69f6b31b7cc"), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92") },
                    { new Guid("be4f9640-da46-48a9-90ad-2bb7488b3896"), new Guid("c27b5421-01d5-444d-8139-2f6732f14790"), new Guid("c7cba1c7-088e-4486-855b-8c129372933e") },
                    { new Guid("bf92c190-8445-4e18-b0df-e1217b58203e"), new Guid("839a3dfe-d8d6-42b5-a91e-c523e095bad9"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b") },
                    { new Guid("ce9b00ac-3814-4f50-b101-73becbc37fe6"), new Guid("aba57c67-5bbc-4d58-ac2a-a69f6b31b7cc"), new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f") },
                    { new Guid("df17a4df-2d52-4ac0-b844-da78f1138972"), new Guid("5e054627-9fa1-4ed2-941d-3bee0f996d8e"), new Guid("edd4a334-2ea6-4e9c-8082-64182528de44") },
                    { new Guid("f4406cd7-4455-499a-ae48-946c7454d79d"), new Guid("01742c20-c834-4ef2-ad48-169152a8b5c8"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesComments",
                columns: new[] { "Id", "Content", "CreatedAt", "GameId", "UserId" },
                values: new object[,]
                {
                    { new Guid("07396699-0add-4c50-87cd-35ba839a719e"), "magni", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6820), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("247fc4ae-b64f-47f3-998c-30ea5cc792ac") },
                    { new Guid("082539de-b846-40ae-9d1e-b5ab675309ca"), "saepe", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6862), new Guid("0acface4-b544-4f75-a601-48f24c46da9d"), new Guid("bbfc1089-b65a-4afb-8402-fbc5b3088525") },
                    { new Guid("096292b6-17ff-41e0-b475-6be6f0e492ba"), "est", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6804), new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8"), new Guid("bb3d2565-bf0d-47f1-8bcf-13aaf7e10725") },
                    { new Guid("0a1152c6-3098-4dbc-a5cb-01ad895f3638"), "explicabo", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(5654), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("9ed6dc6c-ea73-4cbf-b853-764e11357325") },
                    { new Guid("0cb7da8b-c771-4c88-8f05-b5982bb964d4"), "omnis", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6848), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b"), new Guid("ff3cf158-f410-4aa3-b461-8c9ad94e95d4") },
                    { new Guid("134772f7-7b35-4b52-ab29-c9d70f27bd23"), "ratione", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6759), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48"), new Guid("a5869143-924a-404c-9658-461ed7190e3b") },
                    { new Guid("144fc8b7-eb47-44d6-91ef-d51eba6e413b"), "aut", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6740), new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658"), new Guid("8e2ccb8f-ffc6-44b5-b339-c00419a8cc6f") },
                    { new Guid("1bbb7d87-184f-4ac0-b7ea-8330f5c44705"), "architecto", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6915), new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f"), new Guid("3b7daf23-7977-4ba0-9935-b46a57f8181b") },
                    { new Guid("42ee39e7-8905-4884-9351-af4dc6c03117"), "reiciendis", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6834), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48"), new Guid("50be4768-354d-44ae-941c-24e50fcf31e6") },
                    { new Guid("5380fa65-6134-4084-b9a6-6cb27dd5932b"), "ut", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6930), new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8"), new Guid("d5c7635d-56c5-4625-b0e8-d9e18805c046") },
                    { new Guid("55af45a6-a7e3-4730-8ca7-5e76eb35d2ff"), "provident", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6637), new Guid("e4653a5d-eae3-45e9-a225-bddfd0ec3d6b"), new Guid("627cf26a-eb12-4e4d-8f73-78fe937740e8") },
                    { new Guid("61915d46-ec7e-400e-a7c3-ace371f84f58"), "aut", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6889), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), new Guid("d60ded60-f829-414f-bcbc-177a3d04f150") },
                    { new Guid("83cb4f65-6881-44e9-8a84-dc54fbe565d3"), "fugit", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6776), new Guid("ec84054e-c615-4eb8-b3fd-d42031c15229"), new Guid("b115b4e1-d5f0-4385-9b96-7d9652474f22") },
                    { new Guid("87a1338d-4f45-4d17-b8d6-b00626c3f7d0"), "dolore", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6877), new Guid("ec84054e-c615-4eb8-b3fd-d42031c15229"), new Guid("6ea7b288-eca7-476b-b8f4-1f7980264260") },
                    { new Guid("888db12a-9b4f-45b8-a908-b12f6a842bbc"), "voluptate", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6944), new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658"), new Guid("6d3acd9d-d18e-4558-a7dd-a1203006ff8d") },
                    { new Guid("a14f2668-a36e-4bbe-a958-84815a2abe64"), "veniam", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6724), new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), new Guid("48f4abba-dbf0-4988-af45-2f2a349be77b") },
                    { new Guid("bf1fb9dc-d084-423c-8a91-8ad0e8401a61"), "expedita", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6902), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), new Guid("4166717d-d49b-43c3-bb03-8e2bfa1dc23d") },
                    { new Guid("c74a1405-c397-4e33-80a4-e46c0e0156b4"), "culpa", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6682), new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8"), new Guid("9086fd85-d393-45b0-8e8a-e8fbfb37b8eb") },
                    { new Guid("df8e5aca-11f6-4230-850e-4c66f6ebc46d"), "et", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6705), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("b2236fd8-3489-47c8-8c1a-bbc95597d6e5") },
                    { new Guid("f478f577-5a5d-4b8d-a99a-2c70c6aeed24"), "aperiam", new DateTime(2023, 6, 5, 13, 50, 7, 594, DateTimeKind.Local).AddTicks(6790), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("93be6ce6-12ba-4c70-ba0f-4a25d7e6ef02") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesDevelopers",
                columns: new[] { "Id", "DeveloperId", "GameId" },
                values: new object[,]
                {
                    { new Guid("027cd721-247c-4bca-b78f-43ba529f77f9"), new Guid("00666526-5c80-4e0c-979d-0c538c2ea370"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291") },
                    { new Guid("03d3260d-95fe-44c0-854c-9ae740cc5c16"), new Guid("d9f4651f-1fa8-4a4f-9f90-69c2a1520f7d"), new Guid("c7cba1c7-088e-4486-855b-8c129372933e") },
                    { new Guid("05e16007-61e6-4c5d-82d3-c0c8f7ae41b6"), new Guid("d9f4651f-1fa8-4a4f-9f90-69c2a1520f7d"), new Guid("ec84054e-c615-4eb8-b3fd-d42031c15229") },
                    { new Guid("125fad11-ef21-4f38-83fd-c2378c98e4d1"), new Guid("30c6d8a3-3051-4730-9562-371060b99329"), new Guid("12cf8d44-f0ae-41e4-a309-848c4f819b86") },
                    { new Guid("1434b719-6f80-48e8-9c02-36531b943d29"), new Guid("00666526-5c80-4e0c-979d-0c538c2ea370"), new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374") },
                    { new Guid("1d623398-5353-4d80-b5d2-09c863bcb179"), new Guid("230510cb-7ffe-47d7-87b1-ee148c8f6318"), new Guid("e4653a5d-eae3-45e9-a225-bddfd0ec3d6b") },
                    { new Guid("2c1b6800-3c93-4c7e-a0fe-b30c0273e902"), new Guid("3dbea998-d4eb-442a-a18b-6c1322cf9d71"), new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374") },
                    { new Guid("3fb846cc-4648-4bbe-98bb-98093d421f46"), new Guid("230510cb-7ffe-47d7-87b1-ee148c8f6318"), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48") },
                    { new Guid("588d57f0-4ef9-488b-9cf0-6235180d04c3"), new Guid("95dc0713-870d-48e8-9593-2369cd8a2826"), new Guid("235f42cc-a17c-42de-9909-a248a91727b4") },
                    { new Guid("7706428b-d22b-4d90-af83-0a46607009f3"), new Guid("3dbea998-d4eb-442a-a18b-6c1322cf9d71"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291") },
                    { new Guid("778618ad-2d74-42d4-a200-1d53c597ae41"), new Guid("230510cb-7ffe-47d7-87b1-ee148c8f6318"), new Guid("a53146ef-533e-4c99-b24b-5d3d5b30c3d9") },
                    { new Guid("91855e53-0989-4fc2-8c30-b195652bfaf9"), new Guid("230510cb-7ffe-47d7-87b1-ee148c8f6318"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b") },
                    { new Guid("b0a3c43a-9624-465e-801c-19e141cf96ee"), new Guid("3dbea998-d4eb-442a-a18b-6c1322cf9d71"), new Guid("edd4a334-2ea6-4e9c-8082-64182528de44") },
                    { new Guid("c47f3d36-e094-41bd-b484-707a443f042b"), new Guid("2b4835fe-eb0c-4ebf-b691-9ca03c629485"), new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f") },
                    { new Guid("c54aa097-0b3c-4256-9394-025cdccf2a3d"), new Guid("95dc0713-870d-48e8-9593-2369cd8a2826"), new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f") },
                    { new Guid("d9200fe2-f32b-4f8b-b167-3ae151a2166f"), new Guid("2b4835fe-eb0c-4ebf-b691-9ca03c629485"), new Guid("12cf8d44-f0ae-41e4-a309-848c4f819b86") },
                    { new Guid("d9bea977-151e-4bff-9e4f-a047323e1db9"), new Guid("a1d4422a-7492-4d40-b1b5-a19914d5ad42"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b") },
                    { new Guid("dcf25168-d785-44e0-a310-b629312bf7d4"), new Guid("2b4835fe-eb0c-4ebf-b691-9ca03c629485"), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesImages",
                columns: new[] { "Id", "Directory", "GameId" },
                values: new object[,]
                {
                    { new Guid("0dbd4aa4-a344-4506-b1f2-27ae62d128af"), "/var/mail/transmit_process_improvement_investment_account.eot", new Guid("a53146ef-533e-4c99-b24b-5d3d5b30c3d9") },
                    { new Guid("315811a8-2dbf-4c30-9248-feed69092149"), "/usr/include/french_guiana_hierarchy_thx.vsd", new Guid("f2ae5fcf-8285-4160-9202-c70855a8eae8") },
                    { new Guid("496f0f41-5a6e-472c-aea7-3ec63170ff32"), "/proc/bus.pcap", new Guid("83b7fb3d-b400-46f0-882f-d8bd72c58ad2") },
                    { new Guid("54fcd08f-ae9e-404b-85c9-baf11a8945d6"), "/usr/share/well.list3820", new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374") },
                    { new Guid("945e6677-c116-4229-8d20-80b7f22efc25"), "/media/wooden_blockchains.fli", new Guid("12cf8d44-f0ae-41e4-a309-848c4f819b86") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesLanguages",
                columns: new[] { "Id", "GameId", "LanguageId" },
                values: new object[,]
                {
                    { new Guid("01112cb5-d4d7-44eb-bb1c-dfdd90cf067e"), new Guid("36728a43-41ed-4b7a-8ab3-5ec9b4d6647f"), new Guid("577da1fe-53b2-4c51-ba91-b02aedb6c1b0") },
                    { new Guid("142d6a26-e94e-4891-ac48-91033d701ef7"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("43e08303-4eab-4889-a535-1c0bb478aa11") },
                    { new Guid("232257e2-9390-49b6-bd7a-c5a8b27a639b"), new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658"), new Guid("cb371803-f1be-43a0-ae22-1172238ce6a3") },
                    { new Guid("5f9fe2b5-a30b-46e1-b0a0-a3fb37a8c217"), new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), new Guid("fc883fb7-5c8c-4a2e-b8d3-ea27c1930141") },
                    { new Guid("6ba385d4-7311-478c-8728-d0d95669eefc"), new Guid("2013d294-ef22-4325-8a85-6f00832247f8"), new Guid("9a60245c-c291-4ea9-a15f-41d8133d25b2") },
                    { new Guid("6d3a430d-ca34-421f-b771-0c21857243d5"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b"), new Guid("7e5c42ce-9641-4537-9f6c-5e5498cfff4d") },
                    { new Guid("7d3f400b-014f-4b3e-83f8-50b510f2396b"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), new Guid("7e5c42ce-9641-4537-9f6c-5e5498cfff4d") },
                    { new Guid("a1acd7c6-deb5-463a-9abd-534bae265522"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("fc883fb7-5c8c-4a2e-b8d3-ea27c1930141") },
                    { new Guid("a9fe77bc-9346-4713-8484-b6d53ba94138"), new Guid("2db4b7ed-55b1-4900-b4f1-5fbed31706ed"), new Guid("7e5c42ce-9641-4537-9f6c-5e5498cfff4d") },
                    { new Guid("b73e7bc3-f77e-4bd1-b44f-b4d813d0bcd6"), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), new Guid("9a60245c-c291-4ea9-a15f-41d8133d25b2") },
                    { new Guid("c7830fdc-e96c-470f-a31a-f34c9f00c536"), new Guid("e4653a5d-eae3-45e9-a225-bddfd0ec3d6b"), new Guid("0f09457e-94eb-4f1c-a84a-a6bc4677c3fa") },
                    { new Guid("cc68a8de-4d02-40aa-96f1-a55d929c8222"), new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), new Guid("cb371803-f1be-43a0-ae22-1172238ce6a3") },
                    { new Guid("cd51e934-6b3a-4fc5-9da8-a0e42bfb86cf"), new Guid("a53146ef-533e-4c99-b24b-5d3d5b30c3d9"), new Guid("577da1fe-53b2-4c51-ba91-b02aedb6c1b0") },
                    { new Guid("e0016a67-b975-404e-9ff2-cf76943a749f"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), new Guid("fc883fb7-5c8c-4a2e-b8d3-ea27c1930141") },
                    { new Guid("eb363739-9b72-47e9-be1a-dc7ff30a457e"), new Guid("bf3ca41b-40f2-4b1f-9beb-44071ddf1658"), new Guid("b644f2c9-0c99-4481-88e3-646d1f44f225") },
                    { new Guid("ebb8e8ed-4d8b-4bf5-ae3c-2f11aa3d732b"), new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), new Guid("577da1fe-53b2-4c51-ba91-b02aedb6c1b0") },
                    { new Guid("ebf0054e-3dfb-4995-9ed0-ab9f9fd09968"), new Guid("2013d294-ef22-4325-8a85-6f00832247f8"), new Guid("577da1fe-53b2-4c51-ba91-b02aedb6c1b0") },
                    { new Guid("f616932e-96ad-4e76-8131-f8568a2e0b80"), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), new Guid("bc174d83-d658-4015-bf0d-359f2e62003a") },
                    { new Guid("fd3a543e-43b5-4ce7-a68d-1e82c30b6c65"), new Guid("a53146ef-533e-4c99-b24b-5d3d5b30c3d9"), new Guid("0f09457e-94eb-4f1c-a84a-a6bc4677c3fa") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "GamesPlatforms",
                columns: new[] { "Id", "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("0b492d1e-8f8a-470e-82bb-92df25be1862"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("f54b372b-fd4c-4813-b761-844ffd40d997") },
                    { new Guid("0ddd559f-b516-42f1-8665-6625fbc31668"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("6b0af07e-ed30-40ed-9ac9-5b106ae07e69") },
                    { new Guid("10dbc555-b364-4836-9ccf-8f6c671a69a6"), new Guid("ec84054e-c615-4eb8-b3fd-d42031c15229"), new Guid("61b7f7d5-72c7-4053-9149-ce4e769ea7d2") },
                    { new Guid("11764590-0fe0-4268-ad4e-a847b9d786f4"), new Guid("edd4a334-2ea6-4e9c-8082-64182528de44"), new Guid("c68082c3-8087-48cb-b78e-b859ae52878b") },
                    { new Guid("11a7eb52-4556-4686-8193-80c815fb05ba"), new Guid("bb4e85a0-dbbc-45e6-975e-750653c70a48"), new Guid("6b0af07e-ed30-40ed-9ac9-5b106ae07e69") },
                    { new Guid("13b4cc70-0d69-4683-aa15-9ffeae042a62"), new Guid("2013d294-ef22-4325-8a85-6f00832247f8"), new Guid("78cf3d8c-e3ae-4736-95a5-510672b70287") },
                    { new Guid("168397a7-b1bb-48e0-aed2-70b5560590b8"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b"), new Guid("4c43c3f4-1901-4a9a-930b-3172f769e15f") },
                    { new Guid("28728544-81f1-4b6f-b7d8-ec730676f85b"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), new Guid("f54b372b-fd4c-4813-b761-844ffd40d997") },
                    { new Guid("47812de5-838d-4d00-b1a5-25c4d155cb65"), new Guid("8794dcb8-f8fb-4b8a-a856-9ef1c5987374"), new Guid("f54b372b-fd4c-4813-b761-844ffd40d997") },
                    { new Guid("4b783ef6-8fd3-49b4-b88d-c56c87f1f4a4"), new Guid("3cdec759-9844-4219-a497-4f0d3f4a0e92"), new Guid("4c43c3f4-1901-4a9a-930b-3172f769e15f") },
                    { new Guid("668f796f-324d-4e7a-b5ec-00c83ceeddb1"), new Guid("235f42cc-a17c-42de-9909-a248a91727b4"), new Guid("78cf3d8c-e3ae-4736-95a5-510672b70287") },
                    { new Guid("6e1e7f40-de1e-43ac-9bf8-02430e79ddc6"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), new Guid("c68082c3-8087-48cb-b78e-b859ae52878b") },
                    { new Guid("782ade2c-c5fc-4bf3-84c1-b43cb553d344"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("61b7f7d5-72c7-4053-9149-ce4e769ea7d2") },
                    { new Guid("7bfa33b5-92e9-4413-b28a-abe38313dd05"), new Guid("65ec3c0c-cef0-446d-8f8d-ebe16c86d291"), new Guid("5ed8c52b-c458-4d3b-835e-b647d0b40344") },
                    { new Guid("7e9e6506-88b6-4423-b408-6fd3177eeec9"), new Guid("498ea45e-1426-44b2-ba1c-61e6ff8d737b"), new Guid("78cf3d8c-e3ae-4736-95a5-510672b70287") },
                    { new Guid("89f8c271-2ce5-4a26-b03f-639b94dac7af"), new Guid("0acface4-b544-4f75-a601-48f24c46da9d"), new Guid("c68082c3-8087-48cb-b78e-b859ae52878b") },
                    { new Guid("ad638654-2bed-4bb5-b0cb-394d9a832838"), new Guid("010e0ecc-4105-4eb1-bbf7-0914ffe7e38c"), new Guid("61b7f7d5-72c7-4053-9149-ce4e769ea7d2") },
                    { new Guid("c2c2a09a-812f-40fd-abad-9af1880ebb26"), new Guid("2db4b7ed-55b1-4900-b4f1-5fbed31706ed"), new Guid("78cf3d8c-e3ae-4736-95a5-510672b70287") },
                    { new Guid("e577f10c-9faf-4cab-80ac-c764cc2ce632"), new Guid("c7cba1c7-088e-4486-855b-8c129372933e"), new Guid("4c43c3f4-1901-4a9a-930b-3172f769e15f") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "CommentsReplies",
                columns: new[] { "Id", "CommentId", "ReplyId" },
                values: new object[,]
                {
                    { new Guid("05fa205a-00a4-4d95-8e66-2156fd2b64bb"), new Guid("082539de-b846-40ae-9d1e-b5ab675309ca"), new Guid("ccd21cf0-ec13-476a-90f0-aeffba3eb893") },
                    { new Guid("08a8142a-03e6-40f1-8956-b2b25898fef7"), new Guid("5380fa65-6134-4084-b9a6-6cb27dd5932b"), new Guid("da796c51-871a-4fd1-b5e8-6e20057c7357") },
                    { new Guid("2a089212-a93a-4daa-8882-ccb056cf5cde"), new Guid("bf1fb9dc-d084-423c-8a91-8ad0e8401a61"), new Guid("cb48d029-3295-4be0-9389-7a7ca1d4a361") },
                    { new Guid("2e78648c-69bf-4d18-8f94-d8c70309abb3"), new Guid("42ee39e7-8905-4884-9351-af4dc6c03117"), new Guid("ccd21cf0-ec13-476a-90f0-aeffba3eb893") },
                    { new Guid("5e43410d-f47e-42f2-881f-18e997d00fa4"), new Guid("0cb7da8b-c771-4c88-8f05-b5982bb964d4"), new Guid("04c856ee-1e60-4308-a965-8c3b6467a4e4") },
                    { new Guid("626cd726-0dfe-4fb9-94bb-dd62c0bfaf3e"), new Guid("df8e5aca-11f6-4230-850e-4c66f6ebc46d"), new Guid("e0e20395-6887-4ef2-98e9-ea7249acf461") },
                    { new Guid("6e773057-ec47-4d4b-a07f-fad8fb9d87ef"), new Guid("07396699-0add-4c50-87cd-35ba839a719e"), new Guid("371cd1dd-088a-4370-acb2-16d2282f18aa") },
                    { new Guid("762834de-d0b9-449f-8c10-48328b7e54f2"), new Guid("888db12a-9b4f-45b8-a908-b12f6a842bbc"), new Guid("8ff74edd-673d-4f69-963b-aa650d34936c") },
                    { new Guid("af46dcbe-2c19-4a37-bcba-8017cf724e50"), new Guid("a14f2668-a36e-4bbe-a958-84815a2abe64"), new Guid("8ff74edd-673d-4f69-963b-aa650d34936c") },
                    { new Guid("ca425880-d85e-41af-95fb-5bc89dfd3dbb"), new Guid("83cb4f65-6881-44e9-8a84-dc54fbe565d3"), new Guid("c9771339-9f5e-4359-b2e9-057b2d70d7a4") },
                    { new Guid("ef422f00-c950-4956-b6e7-223cc73881a4"), new Guid("144fc8b7-eb47-44d6-91ef-d51eba6e413b"), new Guid("b7215396-84ef-47b5-8498-838b794583b2") },
                    { new Guid("f31d45f6-08d1-4363-b1a6-3040777c9a2b"), new Guid("0a1152c6-3098-4dbc-a5cb-01ad895f3638"), new Guid("cc0548f9-8af9-4bff-a9a5-5d65de4b306f") }
                });

            migrationBuilder.InsertData(
                schema: "gamecatalog",
                table: "LikedComments",
                columns: new[] { "Id", "CommentId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0c47d761-c7fb-4a59-b3a6-0973be8c45f8"), new Guid("5380fa65-6134-4084-b9a6-6cb27dd5932b"), new Guid("95d0f3b5-6494-4c58-968e-ff1fb531c3dd") },
                    { new Guid("1b675322-caaa-4d93-9364-d2d74bcbeb7e"), new Guid("bf1fb9dc-d084-423c-8a91-8ad0e8401a61"), new Guid("99f99d18-16c8-47a9-993f-f0b0f8c11444") },
                    { new Guid("2325c5a0-5026-41a4-8992-0c89570dd9f3"), new Guid("a14f2668-a36e-4bbe-a958-84815a2abe64"), new Guid("1737b964-c754-4025-8e01-1e04ce8d2c81") },
                    { new Guid("24a70021-9182-4c74-b749-c551d2b12feb"), new Guid("0cb7da8b-c771-4c88-8f05-b5982bb964d4"), new Guid("902c3d40-3e9d-4b9e-a986-aadb1dbff288") },
                    { new Guid("3617ade7-49e0-42ee-92dd-f4517a7027a5"), new Guid("bf1fb9dc-d084-423c-8a91-8ad0e8401a61"), new Guid("f0522a50-bec3-400a-99e0-a333016f8294") },
                    { new Guid("533b3d3d-8e19-416f-9a5c-8485b740bedf"), new Guid("87a1338d-4f45-4d17-b8d6-b00626c3f7d0"), new Guid("2c7f7be8-224a-48ee-8866-b7437de6c1e3") },
                    { new Guid("701aebfa-086f-408e-86ed-46177e1d7091"), new Guid("888db12a-9b4f-45b8-a908-b12f6a842bbc"), new Guid("c08ba2c8-37b5-4f30-af31-988708471fc9") },
                    { new Guid("7f596ae1-68ef-4e4a-a90e-825fe6306aa3"), new Guid("144fc8b7-eb47-44d6-91ef-d51eba6e413b"), new Guid("2f741463-e97b-483d-a12d-c659d75a5fc2") },
                    { new Guid("81de45a7-fa19-4465-ac64-fc30ea364d4f"), new Guid("c74a1405-c397-4e33-80a4-e46c0e0156b4"), new Guid("02e5fc76-1863-4895-a94d-89032a576964") },
                    { new Guid("944bf908-9a87-463f-8772-ed8c7c93b450"), new Guid("f478f577-5a5d-4b8d-a99a-2c70c6aeed24"), new Guid("6d1b8a1e-46cd-4ee5-bde0-c95cada7410c") },
                    { new Guid("9ccd3778-3683-4004-b4dd-84a1f47fe9aa"), new Guid("83cb4f65-6881-44e9-8a84-dc54fbe565d3"), new Guid("41f831b8-3b9d-4f0c-b534-410b08c0a63d") },
                    { new Guid("a07df5ad-51f2-4dfd-a52b-dace6b05f663"), new Guid("87a1338d-4f45-4d17-b8d6-b00626c3f7d0"), new Guid("a709f2f9-7369-423b-88a5-5367a582f702") },
                    { new Guid("a9fac9bb-58d7-4856-b0fd-cfb929dda5f9"), new Guid("c74a1405-c397-4e33-80a4-e46c0e0156b4"), new Guid("20bd789d-7937-47b3-9a64-a95dcfda9039") },
                    { new Guid("aadabe12-688f-43dd-8894-d527936423d7"), new Guid("bf1fb9dc-d084-423c-8a91-8ad0e8401a61"), new Guid("7307a4ff-bbc2-4182-a004-b73ef90d2ed5") },
                    { new Guid("adfd50dc-d4b1-4173-9ad6-8cd5de4e1c8d"), new Guid("61915d46-ec7e-400e-a7c3-ace371f84f58"), new Guid("bb31313b-10f4-4b0b-8b05-9ba01d08da11") },
                    { new Guid("baedfc0e-483c-4032-be2f-645434cdef14"), new Guid("42ee39e7-8905-4884-9351-af4dc6c03117"), new Guid("fa3db255-f34e-4473-a437-f648a2aa40bc") },
                    { new Guid("d5d48072-9ed6-44c8-baa5-c9df315f2c4d"), new Guid("1bbb7d87-184f-4ac0-b7ea-8330f5c44705"), new Guid("417016dd-b3a4-42c8-8625-472ed49fe34f") },
                    { new Guid("e33beacd-fca1-41e0-9286-eb75f2b5730d"), new Guid("0cb7da8b-c771-4c88-8f05-b5982bb964d4"), new Guid("1e4ed33e-49c2-47a2-a5d5-683b539eebaf") },
                    { new Guid("e4056fe3-eb2b-4a8f-b7b6-653b00cff0b7"), new Guid("5380fa65-6134-4084-b9a6-6cb27dd5932b"), new Guid("8b69e782-45f3-41c9-950d-a466743da305") },
                    { new Guid("fa286bd9-f006-4d35-8192-a7cdc36e076c"), new Guid("42ee39e7-8905-4884-9351-af4dc6c03117"), new Guid("d29e0eeb-718a-4076-aac1-6a9661479377") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "gamecatalog",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentsReplies_CommentId",
                schema: "gamecatalog",
                table: "CommentsReplies",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsReplies_ReplyId",
                schema: "gamecatalog",
                table: "CommentsReplies",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_Name",
                schema: "gamecatalog",
                table: "Developers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Name",
                schema: "gamecatalog",
                table: "Games",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                schema: "gamecatalog",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesCategories_CategoryId",
                schema: "gamecatalog",
                table: "GamesCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesCategories_GameId",
                schema: "gamecatalog",
                table: "GamesCategories",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesComments_GameId",
                schema: "gamecatalog",
                table: "GamesComments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesDevelopers_DeveloperId",
                schema: "gamecatalog",
                table: "GamesDevelopers",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesDevelopers_GameId",
                schema: "gamecatalog",
                table: "GamesDevelopers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesImages_GameId",
                schema: "gamecatalog",
                table: "GamesImages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesLanguages_GameId",
                schema: "gamecatalog",
                table: "GamesLanguages",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesLanguages_LanguageId",
                schema: "gamecatalog",
                table: "GamesLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlatforms_GameId",
                schema: "gamecatalog",
                table: "GamesPlatforms",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlatforms_PlatformId",
                schema: "gamecatalog",
                table: "GamesPlatforms",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                schema: "gamecatalog",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LikedComments_CommentId",
                schema: "gamecatalog",
                table: "LikedComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedReplies_ReplyId",
                schema: "gamecatalog",
                table: "LikedReplies",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Name",
                schema: "gamecatalog",
                table: "Platforms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                schema: "gamecatalog",
                table: "Publishers",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsReplies",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesCategories",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesDevelopers",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesImages",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesLanguages",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesPlatforms",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "LikedComments",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "LikedReplies",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Developers",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Platforms",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "GamesComments",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Replies",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Games",
                schema: "gamecatalog");

            migrationBuilder.DropTable(
                name: "Publishers",
                schema: "gamecatalog");
        }
    }
}

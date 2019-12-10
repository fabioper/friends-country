using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FriendsCountry.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagUri = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlagUri = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    CountryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FamilyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    CountryId = table.Column<long>(nullable: false),
                    StateId = table.Column<long>(nullable: false),
                    FriendId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friends_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_CountryId",
                table: "Friends",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_StateId",
                table: "Friends",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");


            var spCountriesGet = @"CREATE PROCEDURE [dbo].[GetCountries]
                AS
                BEGIN
                    select * from Countries
                END";

            var spCountriesInsert = @"CREATE PROCEDURE [dbo].[InsertCountry]
                AS
                BEGIN
                    
                END";

            var spCountriesUpdate = @"CREATE PROCEDURE [dbo].[UpdateCountry]
                AS
                BEGIN
                    
                END";

            var spStatesGet = @"CREATE PROCEDURE [dbo].[GetState]
                AS
                BEGIN
                    select * from Countries
                END";


            var spStatesInsert = @"CREATE PROCEDURE [dbo].[InsertState]
                AS
                BEGIN
                    
                END";

            var spStatesUpdate = @"CREATE PROCEDURE [dbo].[UpdateState]
                AS
                BEGIN
                    
                END";

            var spFriendsGet = @"CREATE PROCEDURE [dbo].[GetFriends]
                AS
                BEGIN
                    select * from Friends
                END";

            var spFriendsInsert = @"CREATE PROCEDURE [dbo].[InsertFriend]
                    @Name NVARCHAR
                    @FamilyName NVARCHAR
                    @Email NVARCHAR
                    @Phone NVARCHAR
                    @Birthdate DATETIME2
                AS
                BEGIN
                    insert into Friends values (@Name, @FamilyName, @Email, @Phone, @Birthdate)
                END";


            var spFriendsUpdate = @"CREATE PROCEDURE [dbo].[UpdateFriend]
                    @Id BIGINT,
                    @Name NVARCHAR,
                    @FamilyName NVARCHAR,
                    @Email NVARCHAR,
                    @Phone NVARCHAR,
                    @Birthdate DATETIME2
                AS
                BEGIN
                    update friends set Name = @Name, FamilyName = @FamilyName, Email = @Email, Phone = @Phone, Birthdate = @Birthdate where Id = @Id
                END";

            migrationBuilder.Sql(spCountriesGet);
            migrationBuilder.Sql(spCountriesInsert);
            migrationBuilder.Sql(spCountriesUpdate);
            migrationBuilder.Sql(spStatesGet);
            migrationBuilder.Sql(spStatesInsert);
            migrationBuilder.Sql(spStatesUpdate);
            migrationBuilder.Sql(spFriendsGet);
            migrationBuilder.Sql(spFriendsInsert);
            migrationBuilder.Sql(spFriendsUpdate);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

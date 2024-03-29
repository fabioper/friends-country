﻿using System;
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
                    PhotoUri = table.Column<string>(nullable: true),
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

            var spCountriesGet = @"CREATE PROCEDURE [dbo].[GetCountry]
                    @Id BIGINT
                AS
                BEGIN
                    select * from dbo.Countries where Id = @Id
                END";

            var spCountryDelete = @"CREATE PROCEDURE [dbo].[DeleteCountry]
                    @Id BIGINT
                AS
                BEGIN
                    delete from dbo.Countries where Id = @Id
                END";

            var spCountriesInsert = @"CREATE PROCEDURE [dbo].[InsertCountry]
                    @FlagUri NVARCHAR (MAX),
                    @Name NVARCHAR (MAX)
                AS
                BEGIN
                    insert into dbo.Countries(FlagUri, Name) values (@FlagUri, @Name)
                END";

            var spCountriesUpdate = @"CREATE PROCEDURE [dbo].[UpdateCountry]
                    @Id BIGINT,
                    @FlagUri NVARCHAR (MAX),
                    @Name NVARCHAR (MAX)
                AS
                BEGIN
                    update dbo.Countries set FlagUri = @FlagUri, Name = @Name where Id = @Id
                END";

            var spStatesGet = @"CREATE PROCEDURE [dbo].[GetState]
                    @Id BIGINT
                AS
                BEGIN
                    select * from dbo.States where Id = @Id
                END";

            var spStatesDelete = @"CREATE PROCEDURE [dbo].[DeleteState]
                    @Id BIGINT
                AS
                BEGIN
                    delete from dbo.States where Id = @Id
                END";


            var spStatesInsert = @"CREATE PROCEDURE [dbo].[InsertState]
                    @Name NVARCHAR (MAX),
                    @FlagUri NVARCHAR (MAX)
                AS
                BEGIN
                    INSERT INTO dbo.States(Name, FlagUri) VALUES(@Name, @FlagUri)
                END";

            var spStatesUpdate = @"CREATE PROCEDURE [dbo].[UpdateState]
                   @Id BIGINT, 
                   @Name NVARCHAR (MAX),
                   @FlagUri NVARCHAR (MAX)
                AS
                BEGIN
                    UPDATE dbo.States SET Name = @Name, FlagUri = @FlagUri WHERE Id = @Id
                END";

            var spFriendsGet = @"CREATE PROCEDURE [dbo].[GetFriend]
                    @Id BIGINT
                AS
                BEGIN
                    select * from Friends where Id = @Id
                END";

            var spFriendsInsert = @"CREATE PROCEDURE [dbo].[InsertFriend]
                    @PhotoUri NVARCHAR (MAX),
                    @Name NVARCHAR (MAX),
                    @FamilyName NVARCHAR (MAX),
                    @Email NVARCHAR (MAX),
                    @Phone NVARCHAR (MAX),
                    @Birthdate DATETIME2 (7),
                    @CountryId BIGINT,
                    @StateId BIGINT
                AS
                BEGIN
                    insert into dbo.Friends(PhotoUri, Name, FamilyName, Email, Phone, Birthdate, CountryId, StateId) values (@PhotoUri, @Name, @FamilyName, @Email, @Phone, @Birthdate, @CountryId, @StateId)
                END";


            var spFriendsUpdate = @"CREATE PROCEDURE [dbo].[UpdateFriend]
                    @Id BIGINT,
                    @PhotoUri NVARCHAR (MAX),
                    @Name NVARCHAR (MAX),
                    @FamilyName NVARCHAR (MAX),
                    @Email NVARCHAR (MAX),
                    @Phone NVARCHAR (MAX),
                    @Birthdate DATETIME2 (7),
                    @CountryId BIGINT,
                    @StateId BIGINT
                AS
                BEGIN
                    update dbo.Friends set PhotoUri = @PhotoUri, Name = @Name, FamilyName = @FamilyName, Email = @Email, Phone = @Phone, Birthdate = @Birthdate where Id = @Id
                END";

            var spFriendsDelete = @"CREATE PROCEDURE [dbo].[DeleteFriend]
                    @Id BIGINT
                AS
                BEGIN
                    delete from dbo.Friends where Id = @Id
                END";

            migrationBuilder.Sql(spCountriesGet);
            migrationBuilder.Sql(spCountriesInsert);
            migrationBuilder.Sql(spCountriesUpdate);
            migrationBuilder.Sql(spCountryDelete);
            migrationBuilder.Sql(spStatesGet);
            migrationBuilder.Sql(spStatesInsert);
            migrationBuilder.Sql(spStatesUpdate);
            migrationBuilder.Sql(spStatesDelete);
            migrationBuilder.Sql(spFriendsGet);
            migrationBuilder.Sql(spFriendsInsert);
            migrationBuilder.Sql(spFriendsUpdate);
            migrationBuilder.Sql(spFriendsDelete);
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

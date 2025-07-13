using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messenger.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureUserBlockProperly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Users_BlockedId",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlockedUsers_Users_BlockerId",
                table: "BlockedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_IncomingMessages_OutgoingMessages_OutcomingMessageId",
                table: "IncomingMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers");

            migrationBuilder.DropIndex(
                name: "IX_BlockedUsers_BlockerId_BlockedId",
                table: "BlockedUsers");

            migrationBuilder.RenameTable(
                name: "BlockedUsers",
                newName: "UserBlocks");

            migrationBuilder.RenameColumn(
                name: "OutcomingMessageId",
                table: "IncomingMessages",
                newName: "OutgoingMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingMessages_OutcomingMessageId",
                table: "IncomingMessages",
                newName: "IX_IncomingMessages_OutgoingMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_BlockedUsers_BlockedId",
                table: "UserBlocks",
                newName: "IX_UserBlocks_BlockedId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "OutgoingMessages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "OutgoingMessages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "IncomingMessages",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadAt",
                table: "IncomingMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserBlocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UserBlocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBlocks",
                table: "UserBlocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingMessages_GroupId",
                table: "OutgoingMessages",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_BlockerId",
                table: "UserBlocks",
                column: "BlockerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_UserId",
                table: "UserBlocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlocks_UserId1",
                table: "UserBlocks",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingMessages_OutgoingMessages_OutgoingMessageId",
                table: "IncomingMessages",
                column: "OutgoingMessageId",
                principalTable: "OutgoingMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingMessages_Groups_GroupId",
                table: "OutgoingMessages",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_Users_BlockedId",
                table: "UserBlocks",
                column: "BlockedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_Users_BlockerId",
                table: "UserBlocks",
                column: "BlockerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_Users_UserId",
                table: "UserBlocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_Users_UserId1",
                table: "UserBlocks",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomingMessages_OutgoingMessages_OutgoingMessageId",
                table: "IncomingMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingMessages_Groups_GroupId",
                table: "OutgoingMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_Users_BlockedId",
                table: "UserBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_Users_BlockerId",
                table: "UserBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_Users_UserId",
                table: "UserBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_Users_UserId1",
                table: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_OutgoingMessages_GroupId",
                table: "OutgoingMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBlocks",
                table: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_UserBlocks_BlockerId",
                table: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_UserBlocks_UserId",
                table: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_UserBlocks_UserId1",
                table: "UserBlocks");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "OutgoingMessages");

            migrationBuilder.DropColumn(
                name: "ReadAt",
                table: "IncomingMessages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserBlocks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserBlocks");

            migrationBuilder.RenameTable(
                name: "UserBlocks",
                newName: "BlockedUsers");

            migrationBuilder.RenameColumn(
                name: "OutgoingMessageId",
                table: "IncomingMessages",
                newName: "OutcomingMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_IncomingMessages_OutgoingMessageId",
                table: "IncomingMessages",
                newName: "IX_IncomingMessages_OutcomingMessageId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBlocks_BlockedId",
                table: "BlockedUsers",
                newName: "IX_BlockedUsers_BlockedId");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "OutgoingMessages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "IncomingMessages",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlockedUsers",
                table: "BlockedUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUsers_BlockerId_BlockedId",
                table: "BlockedUsers",
                columns: new[] { "BlockerId", "BlockedId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Users_BlockedId",
                table: "BlockedUsers",
                column: "BlockedId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedUsers_Users_BlockerId",
                table: "BlockedUsers",
                column: "BlockerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingMessages_OutgoingMessages_OutcomingMessageId",
                table: "IncomingMessages",
                column: "OutcomingMessageId",
                principalTable: "OutgoingMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

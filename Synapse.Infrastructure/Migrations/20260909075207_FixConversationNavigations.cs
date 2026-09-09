using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synapse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixConversationNavigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId1",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ConversationId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_ConversationParticipants_ConversationId1",
                table: "ConversationParticipants");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "ConversationParticipants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConversationId1",
                table: "Messages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConversationId1",
                table: "ConversationParticipants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId1",
                table: "Messages",
                column: "ConversationId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipants_ConversationId1",
                table: "ConversationParticipants",
                column: "ConversationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId1",
                table: "ConversationParticipants",
                column: "ConversationId1",
                principalTable: "Conversations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId1",
                table: "Messages",
                column: "ConversationId1",
                principalTable: "Conversations",
                principalColumn: "Id");
        }
    }
}

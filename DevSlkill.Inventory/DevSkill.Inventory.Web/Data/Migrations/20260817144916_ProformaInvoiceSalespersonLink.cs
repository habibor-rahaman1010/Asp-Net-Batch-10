using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProformaInvoiceSalespersonLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Until now this column held an Identity user id, because the salesperson
            // was picked from the login list. Those ids name no salesperson, so the
            // foreign key below would refuse to be created while they are still there.
            // They are released rather than guessed at: the document simply has no
            // salesperson until someone names a real one.
            migrationBuilder.Sql(@"
                UPDATE ProformaInvoices
                SET SalespersonId = NULL,
                    SalespersonName = ''
                WHERE SalespersonId IS NOT NULL
                  AND SalespersonId NOT IN (SELECT Id FROM Salespersons);");

            migrationBuilder.CreateIndex(
                name: "IX_ProformaInvoices_SalespersonId",
                table: "ProformaInvoices",
                column: "SalespersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProformaInvoices_Salespersons_SalespersonId",
                table: "ProformaInvoices",
                column: "SalespersonId",
                principalTable: "Salespersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProformaInvoices_Salespersons_SalespersonId",
                table: "ProformaInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ProformaInvoices_SalespersonId",
                table: "ProformaInvoices");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorFacturas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerRun = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Rejected = table.Column<bool>(type: "INTEGER", nullable: false),
                    InvoiceDate = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "TEXT", nullable: true),
                    TotalAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysToDue = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentDueDate = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceCreditNotes",
                columns: table => new
                {
                    CreditNoteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditNoteNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditNoteDate = table.Column<string>(type: "TEXT", nullable: false),
                    CreditNoteAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCreditNotes", x => x.CreditNoteId);
                    table.ForeignKey(
                        name: "FK_InvoiceCreditNotes_Invoices_InvoiceNumber",
                        column: x => x.InvoiceNumber,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    DetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: true),
                    UnitPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.DetailId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_Invoices_InvoiceNumber",
                        column: x => x.InvoiceNumber,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Invoices_InvoiceNumber",
                        column: x => x.InvoiceNumber,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceCreditNotes_InvoiceNumber",
                table: "InvoiceCreditNotes",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceNumber",
                table: "InvoiceDetails",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceCreditNotes");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

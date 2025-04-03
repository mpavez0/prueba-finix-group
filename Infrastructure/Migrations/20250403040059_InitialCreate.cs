using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorFacturas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerRun = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerRun);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    DaysToDue = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentDueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentStatus = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerRun = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceNumber);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerRun",
                        column: x => x.CustomerRun,
                        principalTable: "Customers",
                        principalColumn: "CustomerRun",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceCreditNotes",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditNoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditNoteNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditNoteDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreditNoteAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCreditNotes", x => x.InvoiceNumber);
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
                    InvoiceNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.InvoiceNumber);
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
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "IX_Invoices_CustomerRun",
                table: "Invoices",
                column: "CustomerRun");
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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPNewApi2.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_surname = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_contactdetails = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_province = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_city = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    user_password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    merch_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    merch_name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_surname = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_type = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_verify = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_city = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_province = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_contactdetails = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    merch_file = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_profilepicture = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_pictures1 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_pictures2 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_pictures3 = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_idnumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    merch_taxnumber = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.merch_id);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    book_date = table.Column<DateTime>(type: "date", nullable: false),
                    book_time = table.Column<TimeSpan>(type: "time", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    merch_id = table.Column<int>(type: "int", nullable: false),
                    book_message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Booking_Client",
                        column: x => x.user_id,
                        principalTable: "Client",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Booking_Merchant",
                        column: x => x.merch_id,
                        principalTable: "Merchant",
                        principalColumn: "merch_id");
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_status = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    job_datestart = table.Column<DateTime>(type: "date", nullable: true),
                    job_timestart = table.Column<TimeSpan>(type: "time", nullable: true),
                    job_dateend = table.Column<DateTime>(type: "date", nullable: true),
                    job_timeend = table.Column<TimeSpan>(type: "time", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    merch_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_Job_Booking",
                        column: x => x.book_id,
                        principalTable: "Booking",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Job_Client",
                        column: x => x.user_id,
                        principalTable: "Client",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Job_Merchant",
                        column: x => x.merch_id,
                        principalTable: "Merchant",
                        principalColumn: "merch_id");
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    quot_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    merch_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    quot_amount = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    quot_description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.quot_id);
                    table.ForeignKey(
                        name: "FK_Quotation_Booking",
                        column: x => x.book_id,
                        principalTable: "Booking",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Quotation_Client",
                        column: x => x.user_id,
                        principalTable: "Client",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Quotation_Merchant",
                        column: x => x.merch_id,
                        principalTable: "Merchant",
                        principalColumn: "merch_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    review_rating = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    review_message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    merch_id = table.Column<int>(type: "int", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Review_Client",
                        column: x => x.user_id,
                        principalTable: "Client",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Review_Job",
                        column: x => x.job_id,
                        principalTable: "Job",
                        principalColumn: "job_id");
                    table.ForeignKey(
                        name: "FK_Review_Merchant",
                        column: x => x.merch_id,
                        principalTable: "Merchant",
                        principalColumn: "merch_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_merch_id",
                table: "Booking",
                column: "merch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_user_id",
                table: "Booking",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_book_id",
                table: "Job",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_merch_id",
                table: "Job",
                column: "merch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_user_id",
                table: "Job",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_book_id",
                table: "Quotation",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_merch_id",
                table: "Quotation",
                column: "merch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_user_id",
                table: "Quotation",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_job_id",
                table: "Review",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_merch_id",
                table: "Review",
                column: "merch_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_user_id",
                table: "Review",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotation");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Merchant");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectWork.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    amount = table.Column<int>(type: "integer", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_order_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_order_status_status_id",
                        column: x => x.status_id,
                        principalTable: "order_status",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_client_id",
                table: "order",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_status_id",
                table: "order",
                column: "status_id");

            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION BirthdayCompleted()
    RETURNS TABLE(ID INT, Surname TEXT, Name TEXT, Amount INT) as $$
    BEGIN
        RETURN QUERY
        SELECT
            c.client_id,
            c.name,
            c.surname,
            cast(SUM(o.amount) as INT) AS total_order_amount
        FROM
            client c
        JOIN
            ""order"" o ON c.client_id = o.client_id
        JOIN
            order_status s ON o.status_id = s.status_id
        WHERE
            s.name = 'completed'
        AND DATE(o.time) = DATE(c.birth_date)
        GROUP BY
        c.client_id, c.name, c.surname;
    END
    $$ LANGUAGE plpgsql;
            ");

            migrationBuilder.Sql(@"CREATE OR REPLACE FUNCTION AverageCheck()
    RETURNS TABLE(Hour INT, AverageCheck BIGINT) as $$
    BEGIN
SELECT
    EXTRACT(HOUR FROM time)::INTEGER AS hour,
    AVG(amount) AS average_check
FROM
    ""order""
JOIN order_status on ""order"".status_id = order_status.status_id
WHERE
    order_status.name = 'Completed'
GROUP BY
    EXTRACT(HOUR FROM time)
ORDER BY
    hour DESC;
END
$$ LANGUAGE plpgsql;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS BirthdayCompleted();");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS AverageCheck();");
        }
    }
}

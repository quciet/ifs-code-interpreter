using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IFs.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ifs_dim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_dim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ifs_reg",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Seq = table.Column<short>(nullable: false),
                    InputName = table.Column<string>(nullable: true),
                    OutputName = table.Column<string>(nullable: true),
                    StdError = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_reg", x => new { x.Name, x.Seq });
                });

            migrationBuilder.CreateTable(
                name: "ifs_reg_coeff",
                columns: table => new
                {
                    RegressionName = table.Column<string>(nullable: false),
                    RegressionSeq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_reg_coeff", x => new { x.Name, x.RegressionName, x.RegressionSeq });
                });

            migrationBuilder.CreateTable(
                name: "ifs_reg_point",
                columns: table => new
                {
                    RegressionName = table.Column<string>(nullable: false),
                    RegressionSeq = table.Column<short>(nullable: false),
                    Seq = table.Column<short>(nullable: false),
                    X = table.Column<double>(nullable: false),
                    Y = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_reg_point", x => new { x.RegressionName, x.RegressionSeq, x.Seq });
                });

            migrationBuilder.CreateTable(
                name: "ifs_var",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Bilat = table.Column<int>(nullable: false),
                    Min = table.Column<double>(nullable: true),
                    Max = table.Column<double>(nullable: true),
                    CommonTarget = table.Column<bool>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Aggregation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_var", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ifs_dim_bucket",
                columns: table => new
                {
                    DimensionId = table.Column<int>(nullable: false),
                    Seq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_dim_bucket", x => new { x.DimensionId, x.Seq });
                    table.ForeignKey(
                        name: "FK_ifs_dim_bucket_ifs_dim_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "ifs_dim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ifs_var_blob",
                columns: table => new
                {
                    VariableName = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_var_blob", x => x.VariableName);
                    table.ForeignKey(
                        name: "FK_ifs_var_blob_ifs_var_VariableName",
                        column: x => x.VariableName,
                        principalTable: "ifs_var",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ifs_var_dim",
                columns: table => new
                {
                    VariableName = table.Column<string>(nullable: false),
                    Seq = table.Column<short>(nullable: false),
                    DimensionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ifs_var_dim", x => new { x.Seq, x.VariableName });
                    table.ForeignKey(
                        name: "FK_ifs_var_dim_ifs_dim_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "ifs_dim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ifs_var_dim_ifs_var_VariableName",
                        column: x => x.VariableName,
                        principalTable: "ifs_var",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ifs_dim_Id",
                table: "ifs_dim",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_dim_bucket_Name",
                table: "ifs_dim_bucket",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_reg_Name",
                table: "ifs_reg",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_var_Name",
                table: "ifs_var",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_var_Type",
                table: "ifs_var",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_var_dim_DimensionId",
                table: "ifs_var_dim",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_ifs_var_dim_VariableName",
                table: "ifs_var_dim",
                column: "VariableName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ifs_dim_bucket");

            migrationBuilder.DropTable(
                name: "ifs_reg");

            migrationBuilder.DropTable(
                name: "ifs_reg_coeff");

            migrationBuilder.DropTable(
                name: "ifs_reg_point");

            migrationBuilder.DropTable(
                name: "ifs_var_blob");

            migrationBuilder.DropTable(
                name: "ifs_var_dim");

            migrationBuilder.DropTable(
                name: "ifs_dim");

            migrationBuilder.DropTable(
                name: "ifs_var");
        }
    }
}

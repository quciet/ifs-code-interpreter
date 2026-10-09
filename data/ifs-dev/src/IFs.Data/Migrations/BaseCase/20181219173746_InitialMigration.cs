using Microsoft.EntityFrameworkCore.Migrations;

namespace IFs.Data.Migrations.Basecase
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dim_cohort",
                columns: table => new
                {
                    Seq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dim_cohort", x => x.Seq);
                });

            migrationBuilder.CreateTable(
                name: "dim_gender",
                columns: table => new
                {
                    Seq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dim_gender", x => x.Seq);
                });

            migrationBuilder.CreateTable(
                name: "dim_region",
                columns: table => new
                {
                    Seq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dim_region", x => x.Seq);
                });

            migrationBuilder.CreateTable(
                name: "dim_time",
                columns: table => new
                {
                    Seq = table.Column<short>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dim_time", x => x.Seq);
                });

            migrationBuilder.CreateTable(
                name: "pop_base_case",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    PopulationMales = table.Column<float>(nullable: true),
                    PopulationFemales = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pop_base_case", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "fer_base_case",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    Fertility = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fer_base_case", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "mor_base_case",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    MortalityMales = table.Column<float>(nullable: true),
                    MortalityFemales = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mor_base_case", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "mor_det_base_case",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    MortPerMalesComDis = table.Column<float>(nullable: false),
                    MortPerFeMalesComDis = table.Column<float>(nullable: false),
                    MortPerMalesNonComDis = table.Column<float>(nullable: false),
                    MortPerFeMalesNonComDis = table.Column<float>(nullable: false),
                    MortPerMalesInjuries  = table.Column<float>(nullable: false),
                    MortPerFeMalesInjuries  = table.Column<float>(nullable: false),
                    MaligNeoPlasMales  = table.Column<float>(nullable: false),
                    MaligNeoPlasFemales  = table.Column<float>(nullable: false),
                    CardioVascMales  = table.Column<float>(nullable: false),
                    CardioVascFemales  = table.Column<float>(nullable: false),
                    DigestiveMales  = table.Column<float>(nullable: false),
                    DigestiveFemales  = table.Column<float>(nullable: false),
                    RespiratoryMales  = table.Column<float>(nullable: false),
                    RespiratoryFemales  = table.Column<float>(nullable: false),
                    OtherG2Males  = table.Column<float>(nullable: false),
                    OtherG2Females  = table.Column<float>(nullable: false),
                    RoadTrafficAccMales  = table.Column<float>(nullable: false),
                    RoadTrafficAccFemales  = table.Column<float>(nullable: false),
                    OtherunintInjMales  = table.Column<float>(nullable: false),
                    OtherunintInjFemales  = table.Column<float>(nullable: false),
                    IntenInjMales  = table.Column<float>(nullable: false),
                    IntenInjFemales  = table.Column<float>(nullable: false),
                    DiabetesMales  = table.Column<float>(nullable: false),
                    DiabetesFemales  = table.Column<float>(nullable: false),
                    AIDSMales  = table.Column<float>(nullable: false),
                    AIDSFemales  = table.Column<float>(nullable: false),
                    ComDisNoAIDSMales  = table.Column<float>(nullable: false),
                    ComDisNoAIDSFemales  = table.Column<float>(nullable: false),
                    DiarrheaMales  = table.Column<float>(nullable: false),
                    DiarrheaFemales  = table.Column<float>(nullable: false),
                    MalariaMales  = table.Column<float>(nullable: false),
                    MalariaFemales  = table.Column<float>(nullable: false),
                    RespInfMales  = table.Column<float>(nullable: false),
                    RespInfFemales  = table.Column<float>(nullable: false),
                    MentalHealthMales  = table.Column<float>(nullable: false),
                    MentalHealthFemales  = table.Column<float>(nullable: false)
    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mor_det_base_case", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "morb_det_base_case",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    MorbPerMalesComDis = table.Column<float>(nullable: false),
                    MorbPerFeMalesComDis = table.Column<float>(nullable: false),
                    MorbPerMalesNonComDis = table.Column<float>(nullable: false),
                    MorbPerFeMalesNonComDis = table.Column<float>(nullable: false),
                    MorbPerMalesInjuries = table.Column<float>(nullable: false),
                    MorbPerFeMalesInjuries = table.Column<float>(nullable: false),
                    MaligNeoPlasMales = table.Column<float>(nullable: false),
                    MaligNeoPlasFemales = table.Column<float>(nullable: false),
                    CardioVascMales = table.Column<float>(nullable: false),
                    CardioVascFemales = table.Column<float>(nullable: false),
                    DigestiveMales = table.Column<float>(nullable: false),
                    DigestiveFemales = table.Column<float>(nullable: false),
                    RespiratoryMales = table.Column<float>(nullable: false),
                    RespiratoryFemales = table.Column<float>(nullable: false),
                    OtherG2Males = table.Column<float>(nullable: false),
                    OtherG2Females = table.Column<float>(nullable: false),
                    RoadTrafficAccMales = table.Column<float>(nullable: false),
                    RoadTrafficAccFemales = table.Column<float>(nullable: false),
                    OtherunintInjMales = table.Column<float>(nullable: false),
                    OtherunintInjFemales = table.Column<float>(nullable: false),
                    IntenInjMales = table.Column<float>(nullable: false),
                    IntenInjFemales = table.Column<float>(nullable: false),
                    DiabetesMales = table.Column<float>(nullable: false),
                    DiabetesFemales = table.Column<float>(nullable: false),
                    AIDSMales = table.Column<float>(nullable: false),
                    AIDSFemales = table.Column<float>(nullable: false),
                    ComDisNoAIDSMales = table.Column<float>(nullable: false),
                    ComDisNoAIDSFemales = table.Column<float>(nullable: false),
                    DiarrheaMales = table.Column<float>(nullable: false),
                    DiarrheaFemales = table.Column<float>(nullable: false),
                    MalariaMales = table.Column<float>(nullable: false),
                    MalariaFemales = table.Column<float>(nullable: false),
                    RespInfMales = table.Column<float>(nullable: false),
                    RespInfFemales = table.Column<float>(nullable: false),
                    MentalHealthMales = table.Column<float>(nullable: false),
                    MentalHealthFemales = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mor_det_base_case", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "mor_det_factors",
                columns: table => new
                {
                    CohortId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    MortPerMalesComDis = table.Column<float>(nullable: false),
                    MortPerFeMalesComDis = table.Column<float>(nullable: false),
                    MaligNeoPlasMales = table.Column<float>(nullable: false),
                    MaligNeoPlasFemales = table.Column<float>(nullable: false),
                    CardioVascMales = table.Column<float>(nullable: false),
                    CardioVascFemales = table.Column<float>(nullable: false),
                    DigestiveMales = table.Column<float>(nullable: false),
                    DigestiveFemales = table.Column<float>(nullable: false),
                    RespiratoryMales = table.Column<float>(nullable: false),
                    RespiratoryFemales = table.Column<float>(nullable: false),
                    OtherG2Males = table.Column<float>(nullable: false),
                    OtherG2Females = table.Column<float>(nullable: false),
                    RoadTrafficAccMales = table.Column<float>(nullable: false),
                    RoadTrafficAccFemales = table.Column<float>(nullable: false),
                    OtherunintInjMales = table.Column<float>(nullable: false),
                    OtherunintInjFemales = table.Column<float>(nullable: false),
                    IntenInjMales = table.Column<float>(nullable: false),
                    IntenInjFemales = table.Column<float>(nullable: false),
                    DiabetesMales = table.Column<float>(nullable: false),
                    DiabetesFemales = table.Column<float>(nullable: false),
                    DiarrheaMales = table.Column<float>(nullable: false),
                    DiarrheaFemales = table.Column<float>(nullable: false),
                    MalariaMales = table.Column<float>(nullable: false),
                    MalariaFemales = table.Column<float>(nullable: false),
                    RespInfMales = table.Column<float>(nullable: false),
                    RespInfFemales = table.Column<float>(nullable: false),
                    MentalHealthMales = table.Column<float>(nullable: false),
                    MentalHealthFemales = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mor_det_factors", x => new { x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "mor_det_norm_factors",
                columns: table => new
                {
                    CohortId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    Males = table.Column<float>(nullable: false),
                    Females = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mor_det_norm_factors", x => new { x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "EdnGradsBaseCase",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    PriGradMales = table.Column<float>(nullable: true),
                    PriGradFemales = table.Column<float>(nullable: true),
                    SecGradMales = table.Column<float>(nullable: true),
                    SecGradFemales = table.Column<float>(nullable: true),
                    TerGradMales = table.Column<float>(nullable: true),
                    TerGradFemales = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdnGradsBaseCase", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "EdnYears25BaseCase",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    EdnYears25Males = table.Column<float>(nullable: true),
                    EdnYears25Females = table.Column<float>(nullable: true),
                    EdnYears25Total = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdnYears25BaseCase", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "EdnYears15BaseCase",
                columns: table => new
                {
                    TimeId = table.Column<short>(nullable: false),
                    RegionId = table.Column<short>(nullable: false),
                    CohortId = table.Column<short>(nullable: false),
                    EdnYears15Males = table.Column<float>(nullable: true),
                    EdnYears15Females = table.Column<float>(nullable: true),
                    EdnYears15Total = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EdnYears15BaseCase", x => new { x.TimeId, x.RegionId, x.CohortId });
                });

            migrationBuilder.CreateTable(
                name: "HLPROXIMATEDRIVERS",
                columns: table => new
                {
                    VarName = table.Column<string>(nullable: false),
                    Year = table.Column<short>(nullable: false),
                    Region = table.Column<short>(nullable: false),
                    Dim2 = table.Column<short>(nullable: true),
                    Dim3 = table.Column<short>(nullable: true),
                    DistalFormVal = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLPROXIMATEDRIVERS", x => new { x.VarName, x.Year, x.Region, x.Dim2, x.Dim3});
                });

            migrationBuilder.CreateTable(
                name: "StuntingByAge",
                columns: table => new
                {
                    Years = table.Column<short>(nullable: false),
                    NCohorts = table.Column<short>(nullable: false),
                    Region = table.Column<short>(nullable: false),
                    StuntingMales = table.Column<float>(nullable: true),
                    StuntingFemales = table.Column<float>(nullable: true)
    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StuntingByAge", x => new { x.Years, x.NCohorts, x.Region });
                });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)0, "infants" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)21, "100+" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)20, "95-99" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)19, "90-94" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)18, "85-89" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)17, "80-84" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)16, "75-79" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)15, "70-74" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)14, "65-69" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)13, "60-64" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)12, "55-59" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)11, "50-54" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)10, "45-49" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)9, "40-44" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)8, "35-39" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)7, "30-34" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)6, "25-29" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)5, "20-24" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)4, "15-19" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)3, "10-14" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)2, "5-9" });

            migrationBuilder.InsertData(
                table: "dim_cohort",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)1, "1-4" });

            migrationBuilder.InsertData(
                table: "dim_gender",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)1, "Male" });

            migrationBuilder.InsertData(
                table: "dim_gender",
                columns: new[] { "Seq", "Name" },
                values: new object[] { (short)2, "Female" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dim_cohort");

            migrationBuilder.DropTable(
                name: "dim_gender");

            migrationBuilder.DropTable(
                name: "dim_region");

            migrationBuilder.DropTable(
                name: "dim_time");

            migrationBuilder.DropTable(
                name: "pop_base_case");
        }
    }
}

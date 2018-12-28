using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IG.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HierarchyNodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyNodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeFrames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeFrameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeFrames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HierarchyMarkets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HierarchyNodeId = table.Column<long>(nullable: false),
                    DelayTime = table.Column<int>(nullable: false),
                    Epic = table.Column<string>(nullable: true),
                    NetChange = table.Column<decimal>(nullable: true),
                    LotSize = table.Column<int>(nullable: false),
                    Expiry = table.Column<string>(nullable: true),
                    InstrumentType = table.Column<string>(nullable: true),
                    InstrumentName = table.Column<string>(nullable: true),
                    High = table.Column<decimal>(nullable: true),
                    Low = table.Column<decimal>(nullable: true),
                    PercentageChange = table.Column<decimal>(nullable: true),
                    UpdateTime = table.Column<string>(nullable: true),
                    Bid = table.Column<decimal>(nullable: true),
                    Offer = table.Column<decimal>(nullable: true),
                    OtcTradeable = table.Column<bool>(nullable: false),
                    StreamingPricesAvailable = table.Column<bool>(nullable: false),
                    MarketStatus = table.Column<string>(nullable: true),
                    ScalingFactor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HierarchyMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HierarchyMarkets_HierarchyNodes_HierarchyNodeId",
                        column: x => x.HierarchyNodeId,
                        principalTable: "HierarchyNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HierarchyMarkets_HierarchyNodeId",
                table: "HierarchyMarkets",
                column: "HierarchyNodeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HierarchyMarkets");

            migrationBuilder.DropTable(
                name: "TimeFrames");

            migrationBuilder.DropTable(
                name: "HierarchyNodes");
        }
    }
}

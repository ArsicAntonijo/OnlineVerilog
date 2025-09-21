using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Section = table.Column<string>(type: "TEXT", nullable: false),
                    Header = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    TestBench = table.Column<string>(type: "TEXT", nullable: false),
                    imagePath = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolvedExamples",
                columns: table => new
                {
                    ExampleId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolvedExamples", x => new { x.UserId, x.ExampleId });
                    table.ForeignKey(
                        name: "FK_SolvedExamples_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolvedExamples_Examples_ExampleId",
                        column: x => x.ExampleId,
                        principalTable: "Examples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7158ab2a-f77f-45cb-810b-b11af8ff7aab", null, "admin", "ADMIN" },
                    { "9fa52e96-9d65-4a4e-9b0c-47c778d352cc", null, "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8158ab2a-f77f-43cb-820b-b22af8ff7aab", 0, "4468783f-d93f-4f9b-bcb9-bad82195baad", "admin@gmail.com", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEHIk+J96/+iF5loHeF1tqKWnMVpw899Z6yWrv8QKInXMRNjBqIlqwY/v3pLfmYy6eA==", null, false, "a26cd955-dc08-47c1-a55b-2f08c0219be4", false, "admin@gmail.com" },
                    { "afa52e96-9d75-4a4e-9b0c-47c888d352cc", 0, "bae5ca52-4a63-41f4-b6e7-045c7602b63e", "toni@gmail.com", true, "toni", false, null, "TONI@GMAIL.COM", "TONI@GMAIL.COM", "AQAAAAIAAYagAAAAEEHEwzeGRHOwAds+lD4zL74s2hAVZIXRBSI373Gjye7cGx31RLhDQjLep25UeCZZxw==", null, false, "f09f74f1-c35d-4cb1-8a55-545a0402b307", false, "toni@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Examples",
                columns: new[] { "Id", "Body", "Header", "Section", "TestBench", "imagePath" },
                values: new object[,]
                {
                    { 1, "Креирај логичко коло без улаза и са једним излазом који враћа нулу.", "Нула", "Основе", "module testbench;\r\n  wire y;\r\n\r\n  topmodule tm(y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ty\");\r\n\r\n		# 100; \r\n		if ( y == 0 ) \r\n			$display (\"  PASS  \\t%d\", y);\r\n		else\r\n			$display (\"  FAIL \\t%d\", y);\r\n\r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 2, "За разлику од физичких жица у Верилогу информација тече само у једном смеру, обично од једног извора ка потрошачу. Додела вредности се ради десно ка лево, вредност сигнала са десне стране се емитује на жицу са леве стране. У Верилогу додела није једнократни догађај него је \"континуална\" јер се додела одвија непрестано чак и ако се вредност са десне стране промени. \r\nПортови на модулу такође имају свој смер (обично улаз или излаз). Улазни порт је покренут нечим ван модула, док излазни порт покреће нешто споља. \r\nПотребно је да се креирати модул са јендним улазом и једним излазом који ће се понапати као жица.", "Жица", "Основе", "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a,y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n		a = 1; # 100; \r\n		if ( y == 1 ) \r\n			$display (\"  PASS  \\t%d - %d\",a,y);\r\n		else\r\n			$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n		a = 0; # 100; \r\n		if ( y == 0 ) \r\n			$display (\"  PASS  \\t%d - %d\",a,y);\r\n		else\r\n			$display (\"  FAIL  \\t%d - %d\",a,y);\r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/zica.png" },
                    { 3, "Креирајте модул са 3 улаза и 4 излаза који се понаша као жице које праве ове везе:\r\na -> w\r\nb -> x\r\nb -> y\r\nc -> z", "Четири жице", "Основе", "module testbench;\r\n  reg a, b, c;\r\n  wire w, x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, w, x, y, z);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta\\sb\\sc\\s-\\sw, x, y, z\");\r\n\r\n		a = 0; b = 0; c = 0; # 100; \r\n		if (w ==0 && x==0 && y == 0 &&  z==0) \r\n			$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\r\n		a = 0; b = 1; c = 0; # 100; \r\n		if (w ==0 && x==1 && y == 1 &&  z==0) \r\n			$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\r\n                a = 1; b = 1; c = 1; # 100; \r\n		if (w ==1 && x==1 && y == 1 &&  z==1) \r\n			$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/4zice.png" },
                    { 4, "Креирајте модул који имплементира не логичко коло. Уколико је 1 на улазу на излазу треба да добијемо 0 и уколико је 0 на излазу треба да добијемо 1.\r\n", "Негација", "Логичка кола", "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a,y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n		a = 1; # 100; \r\n		if ( y == 0 ) \r\n			$display (\"  PASS  \\t%d - %d\",a,y);\r\n		else\r\n			$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n		a = 0; # 100; \r\n		if ( y == 1 ) \r\n			$display (\"  PASS  \\t%d - %d\",a,y);\r\n		else\r\n			$display (\"  FAIL  \\t%d - %d\",a,y);\r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/ne-kolo.png" },
                    { 5, "Креаирати модул који имлементира и логичко коло.", "И коло", "Логичка кола", "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y\");\r\n\r\n		a = 0; b = 0 # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 0; b = 1 # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 1; b = 0 # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               	\r\n                a = 1; b = 1 # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/i-kolo.png" },
                    { 6, "Креаирати модул који имлементира или логичко коло.", "Или коло", "Логичка кола", "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y\");\r\n\r\n		a = 0; b = 0; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 0; b = 1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 1; b = 0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               	\r\n                a = 1; b = 1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/ili-kolo.png" },
                    { 7, "Креаирати модул који имлементира ни логичко коло.", "Ни коло", "Логичка кола", "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y\");\r\n\r\n		a = 0; b = 0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 0; b = 1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 1; b = 0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               	\r\n                a = 1; b = 1; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/ni-kolo.png" },
                    { 8, "Креаирати модул који имлементира ексклузивно или коло логичко коло.", "Ексклузивно или коло", "Логичка кола", "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y\");\r\n\r\n		a = 0; b = 0; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 0; b = 1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n		a = 1; b = 0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               	\r\n                a = 1; b = 1; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/xor-kolo.png" },
                    { 9, "Креирати модул који ће изледати као на слици испод...", "Комбинација", "Комбинација", "module testbench;\r\n  reg a, b, c;\r\n  wire out;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - y\");\r\n\r\n		a = 0;b=0;c=0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);    \r\n               \r\n		a = 1;b=0;c=0; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y); \r\n                  \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/logicka-sema-2.png" },
                    { 10, "Креирати модул који прима два двобитна улаза и на излаз даје 1 уколико су улази једнаки.", "Двобитно поређење", "Комбинација", "module testbench;\r\n  reg[1:0] a, b;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y\");\r\n\r\n		a = 1; b=1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d  - %d\", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);    \r\n               \r\n              	a = 0; b=1; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d  - %d\", a, b, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d\", a, b, y);         \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 11, "Креирати модул који ће да контролира рад једног клима уређаја при чему ће активирати по потреби грејање, хлађење или вентилатор. Модул треба да има четири једнобитна улаза (prevruce, prehladno, mod, ukljucen_ventilator) и три једнобитна излаза (grejanje, hladjenje, ventilator). \r\nУколико је mod 1 и кад је prehladno 1 потребно је да се укључи grejanje а да се hladjenje угаси. Ако је mod 1 и prevruce је 1 потребно је да се укључи hladjenje а да се угаси grejanje. Ако је grejanje или hladjenje активно потребно је да се и ventilator укључи због циркулације ваздуха. За активирање ventilator-а се такође користи улаз ukljucen_ventilator.", "Термостат", "Комбинација", "module testbench;\r\n  reg a, b, c, d;\r\n  wire x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, d, x, y, z);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b c d - x y z\");\r\n\r\n		a = 0; b = 1; c = 0; d=0;# 100; \r\n		if (x==0 && y == 1 &&  z==1) \r\n			$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\r\n		a = 1; b = 0; c = 1; d=0;# 100; \r\n		if (x==0 && y == 0 &&  z==0) \r\n			$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\r\n		a = 0; b = 0; c = 0; d=1;# 100; \r\n		if (x==0 && y == 0 &&  z==1) \r\n			$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 12, "Потребно је креирати модул који ће да прими тробитни улаз и да на излаз да колико јединица има у улазном сигналу.", "Поновљене јединице", "Комбинација", "module testbench;\r\n  reg[2:0] a;\r\n  wire[1:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - y\");\r\n\r\n		a = 1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d\", a, y);    \r\n               \r\n              	a = 3; # 100; \r\n		if (y == 2) \r\n			$display (\"  PASS  \\t%d  - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d\", a, y);  \r\n\r\n               a = 7; # 100; \r\n		if (y == 3) \r\n			$display (\"  PASS  \\t%d  - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d\", a, y);          \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 13, "Креирати модул који ће да 16 битни код са тастатуре који прима на улаз дешифрирати и вратити на излаз која је стрелица у питање.\r\nИзлази ће бити 4 једнобитне вредности лево, десно, доле и горе.", "Тастатура", "Комбинација", "module testbench;\r\n  reg[15:0] a;\r\n  wire gore, desno, dole, levo;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, gore, desno, dole, levo);\r\n  \r\n	initial\r\n	begin\r\n		$displadole (\"RESULT\\ta - gore desno dole levo\");\r\n\r\n		a = 16'he075;# 100; \r\n		if (gore == 1 && desno==0 && dole == 1 &&  levo==0) \r\n			$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n		else \r\n			$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n		a = 16'he074;# 100; \r\n		if (gore == 0 && desno==1 && dole == 1 &&  levo==0) \r\n			$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n		else \r\n			$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n		a = 16'he072;# 100; \r\n		if (gore == 0 && desno==0 && dole == 1 &&  levo==0) \r\n			$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n		else \r\n			$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n        a = 16'he06b;# 100; \r\n		if (gore == 0 && desno==0 && dole == 0 &&  levo==1) \r\n			$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n		else \r\n			$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n	end\r\n	  \r\n  //enabling the goreave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/tastatura.png" },
                    { 14, "Креирати 2 - 1 мултиплексер...", "Мултиплексер 2-1", "Плексери", "module testbench;\r\n  reg a, b, sel;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, sel, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b sel- y\");\r\n\r\n		a = 1; b = 0; sel=0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\r\n\r\n		a = 1; b = 0; sel=1; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);		\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 15, "Креирати модул мултиплексер 2 на 1, који има  два 16 битна улаза, један једнобитни улаз селекције и један излаз.", "16-Битни мултиплексер", "Плексери", "module testbench;\r\n  reg[15:0] a, b;\r\n  reg sel;\r\n  wire[15:0]  y;\r\n\r\n  topmodule tm(a, b, sel, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b sel- y\");\r\n\r\n		a = 7; b = 3; sel=0; # 100; \r\n		if (y == 7) \r\n			$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\r\n\r\n		a = 7; b = 3; sel=1; # 100; \r\n		if (y == 3) \r\n			$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);		\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 16, "Креирати модул мултиплексер 4 на 1, који ће имати четри тробитних улаза, један двотни улаз за селекцију и један излаз.  ", "Мултиплексер 4-1", "Плексери", "module testbench;\r\n  reg[2:0] a, b, c ,d;\r\n  reg[1:0]  sel;\r\n  wire[2:0]  y;\r\n\r\n  topmodule tm(a, b, c, d, sel, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b sel- y\");\r\n\r\n		a = 7; b = 5; c=3; d=1; sel=0; # 100; \r\n		if (y == 7) \r\n			$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n		a = 7; b = 5; c=3; d=1; sel=1; # 100; \r\n		if (y == 5) \r\n			$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n		a = 7; b = 5; c=3; d=1; sel=2; # 100; \r\n		if (y == 3) \r\n			$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n		a = 7; b = 5; c=3; d=1; sel=3; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);	\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 17, "Креирати модул који ће престављати приоритентни енкодер. Приоритетни енкодер на улазу прима четворобитну вредност и на излазу даје локацију навишег бита, што ће бити величине два бита.", "Приоритетни енкодер", "Плексери", "module testbench;\r\n  reg[3:0] a;\r\n  wire[1:0]  y;\r\n\r\n  topmodule tm(a, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - y\");\r\n\r\n		a = 1; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n		a = 2; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n		a = 4; # 100; \r\n		if (y == 2) \r\n			$display (\"  PASS  \\t%d - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n		a = 8; # 100; \r\n		if (y == 3) \r\n			$display (\"  PASS  \\t%d - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d  - %d\", a, y);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 18, "Креирати модул полусабирач, који сабира два једнобитна улаза и на излаз даје  једнобитну суму и пренос.", "Полусабирач", "Аритметика", "module testbench;\r\n  reg a, b;\r\n  wire y, cout;\r\n\r\n  topmodule tm(a, b, y, cout);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b - y, cout\");\r\n\r\n		a = 0; b = 0; # 100; \r\n		if (y == 0 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n\r\n		a = 0; b = 1; # 100; \r\n		if (y == 1 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n\r\n		a = 1; b = 1; # 100; \r\n		if (y == 0 && cout == 1) \r\n			$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 19, "Креирати модул сабирач, који сабира три једнобитна улаза (a, b, cin) и на излаз даје  једнобитну суму и пренос.", "Сабирач", "Аритметика", "module testbench;\r\n  reg a, b, cin;\r\n  wire y, cout;\r\n\r\n  topmodule tm(a, b, cin, y, cout);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b cin - y, cout\");\r\n\r\n		a = 0; b = 0; cin=0; # 100; \r\n		if (y == 0 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n		a = 1; b = 0; cin=0; # 100; \r\n		if (y == 1 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n		a = 1; b = 1; cin=0; # 100; \r\n		if (y == 0 && cout == 1) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n               	a = 1; b = 1; cin=1; # 100; \r\n		if (y == 1 && cout == 1) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 20, "Креирати модул сабирач, који сабира два четворобинта улаза (a, b), један једнобитно улаз (cin) и на излаз даје четворобитну суму и пренос.", "Сабирач4", "Аритметика", "module testbench;\r\n  reg[3:0] a, b;\r\n  reg cin;\r\n  wire[3:0] y, cout;\r\n\r\n  topmodule tm(a, b, cin, y, cout);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b cin - y, cout\");\r\n\r\n		a = 0; b = 0; cin=0; # 100; \r\n		if (y == 0 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n		a = 1; b = 0; cin=0; # 100; \r\n		if (y == 1 && cout == 0) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n		a = 1; b = 1; cin=0; # 100; \r\n		if (y == 0 && cout == 1) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n               	a = 4'b1110; b = 1; cin=1; # 100; \r\n		if (y == 1 && cout == 1) \r\n			$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 21, "Креирати модул који ће имплементирати шему са слике.", "КМ1", "Карнове мапе", "module testbench;\r\n  reg a, b, c;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - y\");\r\n\r\n		a = 0;b=0;c=0; # 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);    \r\n               \r\n		a = 1;b=0;c=0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y); \r\n\r\n		a = 0;b=1;c=1; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);\r\n\r\n		a = 1;b=1;c=0; # 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);\r\n                  \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/KM1.png" },
                    { 22, "Креирати модул који ће имплементирати шему са слике.", "КМ2", "Карнове мапе", "module testbench;\r\n  reg a, b, c, d;\r\n  wire out;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, d, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta b c d - y\");\r\n\r\n		a = 0;b=0;c=0;d=0;# 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);    \r\n               \r\n		a = 1;b=0;c=1;d=1;# 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n\r\n		a = 0;b=1;c=0;d=1;# 100; \r\n		if (y == 0) \r\n			$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n\r\n		a = 0;b=0;c=1;d=0;# 100; \r\n		if (y == 1) \r\n			$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n                  \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/KM2.png" },
                    { 23, "Креирати модул који ће на улаз да добије тробитни улаз, а на излаз ће вратити три једнобитна излаза.", "Вектор", "Вектори", "module testbench;\r\n  reg[2:0] a;\r\n  wire x, y, z;\r\n\r\n  topmodule tm(a, x, y, z);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - x, y, z\");\r\n\r\n		a = 5; # 100; \r\n		if (x == 1 && y == 0 && z == 1) \r\n			$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d  -  %d %d %d\", a, x, y, z);\r\n\r\n		a = 2; # 100; \r\n		if (x == 0 && y == 1 && z == 0) \r\n			$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d  -  %d %d %d\", a, x, y, z);		\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/vektor.png" },
                    { 24, "Креирати модул који на улазу прима 16 битни вектор а на излазу даје два 8 битна вектора.", "Полу вектор", "Вектори", "module testbench;\r\n  reg[15:0] a;\r\n  wire[7:0] x, y;\r\n\r\n  topmodule tm(a, x, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - x, y\");\r\n\r\n		a = 16'hacbf; # 100; \r\n		if (x == 8'hac && y == 8'hbf) \r\n			$display (\"  PASS  \\t%d - %d %d\", a, x, y);\r\n		else \r\n			$display (\"  FAIL \\t%d  -  %d %d\", a, x, y);\r\n			\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/poluvektor.png" },
                    { 25, "Креирати модул где имамо 32 битни улаз који се састоји од 4 осмобитне речи и на излазу враћа исте те 4 речи али у обрнутом редоследу. Ево пример:\r\n11111111222222223333333344444444 => 44444444333333332222222211111111 ", "Обрнуте речи", "Вектори", "module testbench;\r\n  reg[31:0] a;\r\n  wire[31:0] x;\r\n\r\n  topmodule tm(a, x);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta - x\");\r\n\r\n		a = 32'h12345678; # 100; \r\n		if (x == 32'h78563412) \r\n			$display (\"  PASS  \\t%d - %d\", a, x);\r\n		else \r\n			$display (\"  FAIL \\t%d  -  %d\", a, x);\r\n			\r\n	end\r\n	  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 26, "Креирати модул који добија на улаз тробитну вредност и потребно је да је на излаз да врати три једнобитна излаза (a, o, x).\r\no излаз а ће бити резултат тробитног и кола\r\nо излаз о ће бити резултат тробитног или кола\r\nо излаз х ће бити резултат тробитног ексклузивног или коло", "Логичка кола", "Вектори", "module testbench;\r\n  reg[2:0] a;\r\n  wire x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, x, y, z);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta\\sb\\sc\\s- x, y, z\");\r\n\r\n		a = 5; # 100; \r\n		if ( x==0 && y == 1 &&  z==0) \r\n			$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d %d %d\", a, x, y, z);\r\n\r\n		a = 7; # 100; \r\n		if ( x==1 && y == 1 &&  z==1) \r\n			$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d %d %d\", a, x, y, z);              \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 27, "Креирати модул који извршити пребацивање три петобитна улаза у четири четворобитна излаза.", "Спајање", "Вектори", "module testbench;\r\n  reg[4:0] a, b, c;\r\n  wire[3:0] w, x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, w, x, y, z);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta\\sb\\sc\\s- w, x, y, z\");\r\n\r\n		a = 5'b10101;b=5'b01010;c=5'b10101; # 100; \r\n		if (w == 4'b1010 && x==4'b1010 && y == 4'b1010 &&  z==4'b1011) \r\n			$display (\"  PASS  \\t%d %d %d  - %d %d %d %d\", a, b, c, w, x, y, z);\r\n		else \r\n			$display (\"  FAIL \\t%d %d %d  - %d %d %d %d\", a, b, c, w, x, y, z);           \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "/uploads/spajanje.png" },
                    { 28, "Креирати модул који улазну осмобитну вредност баци на излазу са обрнутом редоследу.\r\n12345678 => 87654321", "Обрнут редослед", "Вектори", "module testbench;\r\n  reg[7:0]a;\r\n  wire[7:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta- y\");\r\n\r\n		a = 8'b10101010; # 100; \r\n		if (y == 8'b01010101) \r\n			$display (\"  PASS  \\t%d  - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d\", a, y);           \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" },
                    { 29, "Креирати модул који има осмобитни улаз и на излаз даје улазну вредност која је проширена да буде тридесетдвобитна. Улазна вредност је проширена тако што је седми бит дуплициран 24 пута и онда следи осмобитни улаз.", "Проширивање", "Вектори", "module testbench;\r\n  reg[7:0]a;\r\n  wire[31:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n	initial\r\n	begin\r\n		$display (\"RESULT\\ta- y\");\r\n\r\n		a = 8'hac; # 100; \r\n		if (y == 32'hffffffac) \r\n			$display (\"  PASS  \\t%d  - %d\", a, y);\r\n		else \r\n			$display (\"  FAIL \\t%d - %d\", a, y);           \r\n	end\r\n	  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule", "" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7158ab2a-f77f-45cb-810b-b11af8ff7aab", "8158ab2a-f77f-43cb-820b-b22af8ff7aab" },
                    { "9fa52e96-9d65-4a4e-9b0c-47c778d352cc", "afa52e96-9d75-4a4e-9b0c-47c888d352cc" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolvedExamples_ExampleId",
                table: "SolvedExamples",
                column: "ExampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SolvedExamples");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Examples");
        }
    }
}

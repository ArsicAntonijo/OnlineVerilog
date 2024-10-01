using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OnlineVerilog.Models
{
    public class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            // temp values
            string admRoleId = "7158ab2a-f77f-45cb-810b-b11af8ff7aab";
            string usrRoleId = "9fa52e96-9d65-4a4e-9b0c-47c778d352cc";
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = admRoleId, Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = usrRoleId, Name = "user", NormalizedName = "USER" });

            var hasher = new PasswordHasher<IdentityUser>();
            string admId = "8158ab2a-f77f-43cb-820b-b22af8ff7aab";//Guid.NewGuid().ToString();
            string usrId = "afa52e96-9d75-4a4e-9b0c-47c888d352cc";//Guid.NewGuid().ToString();
            builder.Entity<User>().HasData(
                new User()
                {
                    Id = admId, // This is the User ID
                    FirstName = "admin",
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, ""),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User()
                {
                    Id = usrId, // This is the User ID
                    FirstName = "toni",
                    UserName = "toni@gmail.com",
                    NormalizedUserName = "TONI@GMAIL.COM",
                    Email = "toni@gmail.com",
                    NormalizedEmail = "TONI@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, ""),
                    SecurityStamp = Guid.NewGuid().ToString()
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = admId, // Admin User ID
                RoleId = admRoleId  // Admin Role ID
            },
            new IdentityUserRole<string>
            {
                UserId = usrId, // User User ID
                RoleId = usrRoleId  // User Role ID
            });
            int i = 1;
            builder.Entity<Example>().HasData(
                new Example
                {
                    Id = i++,
                    Section = "Основе",
                    Header = "Нула",
                    Body = "Креирај логичко коло без улаза и са једним излазом који враћа нулу.",
                    TestBench = "module testbench;\r\n  wire y;\r\n\r\n  topmodule tm(y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ty\");\r\n\r\n\t\t# 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d\", y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d\", y);\r\n\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Основе",
                    Header = "Жица",
                    Body = "За разлику од физичких жица у Верилогу информација тече само у једном смеру, обично од једног извора ка потрошачу. Додела вредности се ради десно ка лево, вредност сигнала са десне стране се емитује на жицу са леве стране. У Верилогу додела није једнократни догађај него је \"континуална\" јер се додела одвија непрестано чак и ако се вредност са десне стране промени. \r\nПортови на модулу такође имају свој смер (обично улаз или излаз). Улазни порт је покренут нечим ван модула, док излазни порт покреће нешто споља. \r\nПотребно је да се креирати модул са јендним улазом и једним излазом који ће се понапати као жица.",
                    TestBench = "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a,y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif ( y == 1 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n\t\ta = 0; # 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL  \\t%d - %d\",a,y);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/zica.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Основе",
                    Header = "Четири жице",
                    Body = "Креирајте модул са 3 улаза и 4 излаза који се понаша као жице које праве ове везе:\r\na -> w\r\nb -> x\r\nb -> y\r\nc -> z",
                    TestBench = "module testbench;\r\n  reg a, b, c;\r\n  wire w, x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, w, x, y, z);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\sb\\sc\\s-\\sw, x, y, z\");\r\n\r\n\t\ta = 0; b = 0; c = 0; # 100; \r\n\t\tif (w ==0 && x==0 && y == 0 &&  z==0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\r\n\t\ta = 0; b = 1; c = 0; # 100; \r\n\t\tif (w ==0 && x==1 && y == 1 &&  z==0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\r\n                a = 1; b = 1; c = 1; # 100; \r\n\t\tif (w ==1 && x==1 && y == 1 &&  z==1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d- %d %d %d %d\", a, b, c, w, x, y, z);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/4zice.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Негација",
                    Body = "Креирајте модул који имплементира не логичко коло. Уколико је 1 на улазу на излазу треба да добијемо 0 и уколико је 0 на излазу треба да добијемо 1.\r\n",
                    TestBench = "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a,y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n\t\ta = 0; # 100; \r\n\t\tif ( y == 1 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL  \\t%d - %d\",a,y);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/ne-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "И коло",
                    Body = "Креаирати модул који имлементира и логичко коло.",
                    TestBench = "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y\");\r\n\r\n\t\ta = 0; b = 0 # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 0; b = 1 # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 1; b = 0 # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               \t\r\n                a = 1; b = 1 # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/i-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Или коло",
                    Body = "Креаирати модул који имлементира или логичко коло.",
                    TestBench = "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y\");\r\n\r\n\t\ta = 0; b = 0; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 0; b = 1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 1; b = 0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               \t\r\n                a = 1; b = 1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/ili-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Ни коло",
                    Body = "Креаирати модул који имлементира ни логичко коло.",
                    TestBench = "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y\");\r\n\r\n\t\ta = 0; b = 0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 0; b = 1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 1; b = 0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               \t\r\n                a = 1; b = 1; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/ni-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Ексклузивно или коло",
                    Body = "Креаирати модул који имлементира ексклузивно или коло логичко коло.",
                    TestBench = "module testbench;\r\n  reg a, b;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y\");\r\n\r\n\t\ta = 0; b = 0; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 0; b = 1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\r\n\t\ta = 1; b = 0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n               \t\r\n                a = 1; b = 1; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d \", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/xor-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Комбинација",
                    Body = "Креирати модул који ће изледати као на слици испод...",
                    TestBench = "module testbench;\r\n  reg a, b, c;\r\n  wire out;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - y\");\r\n\r\n\t\ta = 0;b=0;c=0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);    \r\n               \r\n\t\ta = 1;b=0;c=0; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y); \r\n                  \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/logicka-sema-2.png"
                }, 
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Двобитно поређење",
                    Body = "Креирати модул који прима два двобитна улаза и на излаз даје 1 уколико су улази једнаки.",
                    TestBench = "module testbench;\r\n  reg[1:0] a, b;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y\");\r\n\r\n\t\ta = 1; b=1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d  - %d\", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);    \r\n               \r\n              \ta = 0; b=1; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d  - %d\", a, b, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d\", a, b, y);         \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Термостат",
                    Body = "Креирати модул који ће да контролира рад једног клима уређаја при чему ће активирати по потреби грејање, хлађење или вентилатор. Модул треба да има четири једнобитна улаза (prevruce, prehladno, mod, ukljucen_ventilator) и три једнобитна излаза (grejanje, hladjenje, ventilator). \r\nУколико је mod 1 и кад је prehladno 1 потребно је да се укључи grejanje а да се hladjenje угаси. Ако је mod 1 и prevruce је 1 потребно је да се укључи hladjenje а да се угаси grejanje. Ако је grejanje или hladjenje активно потребно је да се и ventilator укључи због циркулације ваздуха. За активирање ventilator-а се такође користи улаз ukljucen_ventilator.",
                    TestBench = "module testbench;\r\n  reg a, b, c, d;\r\n  wire x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, d, x, y, z);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b c d - x y z\");\r\n\r\n\t\ta = 0; b = 1; c = 0; d=0;# 100; \r\n\t\tif (x==0 && y == 1 &&  z==1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\r\n\t\ta = 1; b = 0; c = 1; d=0;# 100; \r\n\t\tif (x==0 && y == 0 &&  z==0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\r\n\t\ta = 0; b = 0; c = 0; d=1;# 100; \r\n\t\tif (x==0 && y == 0 &&  z==1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d %d %d\", a, b, c, d, x, y, z);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Поновљене јединице",
                    Body = "Потребно је креирати модул који ће да прими тробитни улаз и да на излаз да колико јединица има у улазном сигналу.",
                    TestBench = "module testbench;\r\n  reg[2:0] a;\r\n  wire[1:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - y\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d\", a, y);    \r\n               \r\n              \ta = 3; # 100; \r\n\t\tif (y == 2) \r\n\t\t\t$display (\"  PASS  \\t%d  - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d\", a, y);  \r\n\r\n               a = 7; # 100; \r\n\t\tif (y == 3) \r\n\t\t\t$display (\"  PASS  \\t%d  - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d\", a, y);          \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Тастатура",
                    Body = "Креирати модул који ће да 16 битни код са тастатуре који прима на улаз дешифрирати и вратити на излаз која је стрелица у питање.\r\nИзлази ће бити 4 једнобитне вредности лево, десно, доле и горе.",
                    TestBench = "module testbench;\r\n  reg[15:0] a;\r\n  wire gore, desno, dole, levo;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, gore, desno, dole, levo);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$displadole (\"RESULT\\ta - gore desno dole levo\");\r\n\r\n\t\ta = 16'he075;# 100; \r\n\t\tif (gore == 1 && desno==0 && dole == 1 &&  levo==0) \r\n\t\t\t$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\t\telse \r\n\t\t\t$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n\t\ta = 16'he074;# 100; \r\n\t\tif (gore == 0 && desno==1 && dole == 1 &&  levo==0) \r\n\t\t\t$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\t\telse \r\n\t\t\t$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n\t\ta = 16'he072;# 100; \r\n\t\tif (gore == 0 && desno==0 && dole == 1 &&  levo==0) \r\n\t\t\t$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\t\telse \r\n\t\t\t$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\r\n        a = 16'he06b;# 100; \r\n\t\tif (gore == 0 && desno==0 && dole == 0 &&  levo==1) \r\n\t\t\t$displadole (\"  PASS  \\t%d  - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\t\telse \r\n\t\t\t$displadole (\"  FAIL \\t%d - %d %d %d %d\", a, gore, desno, dole, levo);\r\n\tend\r\n\t  \r\n  //enabling the goreave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/tastatura.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Мултиплексер 2-1",
                    Body = "Креирати 2 - 1 мултиплексер...",
                    TestBench = "module testbench;\r\n  reg a, b, sel;\r\n  wire y;\r\n\r\n  topmodule tm(a, b, sel, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b sel- y\");\r\n\r\n\t\ta = 1; b = 0; sel=0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\r\n\r\n\t\ta = 1; b = 0; sel=1; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\t\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {   
                    Id = i++,
                    Section = "Плексери",
                    Header = "16-Битни мултиплексер",
                    Body = "Креирати модул мултиплексер 2 на 1, који има  два 16 битна улаза, један једнобитни улаз селекције и један излаз.",
                    TestBench = "module testbench;\r\n  reg[15:0] a, b;\r\n  reg sel;\r\n  wire[15:0]  y;\r\n\r\n  topmodule tm(a, b, sel, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b sel- y\");\r\n\r\n\t\ta = 7; b = 3; sel=0; # 100; \r\n\t\tif (y == 7) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\r\n\r\n\t\ta = 7; b = 3; sel=1; # 100; \r\n\t\tif (y == 3) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d\", a, b, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, sel, y);\t\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Мултиплексер 4-1",
                    Body = "Креирати модул мултиплексер 4 на 1, који ће имати четри тробитних улаза, један двотни улаз за селекцију и један излаз.  ",
                    TestBench = "module testbench;\r\n  reg[2:0] a, b, c ,d;\r\n  reg[1:0]  sel;\r\n  wire[2:0]  y;\r\n\r\n  topmodule tm(a, b, c, d, sel, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b sel- y\");\r\n\r\n\t\ta = 7; b = 5; c=3; d=1; sel=0; # 100; \r\n\t\tif (y == 7) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n\t\ta = 7; b = 5; c=3; d=1; sel=1; # 100; \r\n\t\tif (y == 5) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n\t\ta = 7; b = 5; c=3; d=1; sel=2; # 100; \r\n\t\tif (y == 3) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\r\n\t\ta = 7; b = 5; c=3; d=1; sel=3; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d %d - %d\", a, b, c, d, sel, y);\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Приоритетни енкодер",
                    Body = "Креирати модул који ће престављати приоритентни енкодер. Приоритетни енкодер на улазу прима четворобитну вредност и на излазу даје локацију навишег бита, што ће бити величине два бита.",
                    TestBench = "module testbench;\r\n  reg[3:0] a;\r\n  wire[1:0]  y;\r\n\r\n  topmodule tm(a, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - y\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n\t\ta = 2; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n\t\ta = 4; # 100; \r\n\t\tif (y == 2) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\r\n\t\ta = 8; # 100; \r\n\t\tif (y == 3) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  - %d\", a, y);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Полусабирач",
                    Body = "Креирати модул полусабирач, који сабира два једнобитна улаза и на излаз даје  једнобитну суму и пренос.",
                    TestBench = "module testbench;\r\n  reg a, b;\r\n  wire y, cout;\r\n\r\n  topmodule tm(a, b, y, cout);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b - y, cout\");\r\n\r\n\t\ta = 0; b = 0; # 100; \r\n\t\tif (y == 0 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n\r\n\t\ta = 0; b = 1; # 100; \r\n\t\tif (y == 1 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n\r\n\t\ta = 1; b = 1; # 100; \r\n\t\tif (y == 0 && cout == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d - %d %d\", a, b, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d - %d %d\", a, b, y, cout);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Сабирач",
                    Body = "Креирати модул сабирач, који сабира три једнобитна улаза (a, b, cin) и на излаз даје  једнобитну суму и пренос.",
                    TestBench = "module testbench;\r\n  reg a, b, cin;\r\n  wire y, cout;\r\n\r\n  topmodule tm(a, b, cin, y, cout);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b cin - y, cout\");\r\n\r\n\t\ta = 0; b = 0; cin=0; # 100; \r\n\t\tif (y == 0 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n\t\ta = 1; b = 0; cin=0; # 100; \r\n\t\tif (y == 1 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n\t\ta = 1; b = 1; cin=0; # 100; \r\n\t\tif (y == 0 && cout == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n               \ta = 1; b = 1; cin=1; # 100; \r\n\t\tif (y == 1 && cout == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Сабирач4",
                    Body = "Креирати модул сабирач, који сабира два четворобинта улаза (a, b), један једнобитно улаз (cin) и на излаз даје четворобитну суму и пренос.",
                    TestBench = "module testbench;\r\n  reg[3:0] a, b;\r\n  reg cin;\r\n  wire[3:0] y, cout;\r\n\r\n  topmodule tm(a, b, cin, y, cout);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b cin - y, cout\");\r\n\r\n\t\ta = 0; b = 0; cin=0; # 100; \r\n\t\tif (y == 0 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n\t\ta = 1; b = 0; cin=0; # 100; \r\n\t\tif (y == 1 && cout == 0) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n\t\ta = 1; b = 1; cin=0; # 100; \r\n\t\tif (y == 0 && cout == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\r\n               \ta = 4'b1110; b = 1; cin=1; # 100; \r\n\t\tif (y == 1 && cout == 1) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d %d\", a, b, cin, y, cout);\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Карнове мапе",
                    Header = "КМ1",
                    Body = "Креирати модул који ће имплементирати шему са слике.",
                    TestBench = "module testbench;\r\n  reg a, b, c;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - y\");\r\n\r\n\t\ta = 0;b=0;c=0; # 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);    \r\n               \r\n\t\ta = 1;b=0;c=0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y); \r\n\r\n\t\ta = 0;b=1;c=1; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);\r\n\r\n\t\ta = 1;b=1;c=0; # 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d - %d\", a, b, c, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d - %d\", a, b, c, y);\r\n                  \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/KM1.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Карнове мапе",
                    Header = "КМ2",
                    Body = "Креирати модул који ће имплементирати шему са слике.",
                    TestBench = "module testbench;\r\n  reg a, b, c, d;\r\n  wire out;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, d, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta b c d - y\");\r\n\r\n\t\ta = 0;b=0;c=0;d=0;# 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);    \r\n               \r\n\t\ta = 1;b=0;c=1;d=1;# 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n\r\n\t\ta = 0;b=1;c=0;d=1;# 100; \r\n\t\tif (y == 0) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n\r\n\t\ta = 0;b=0;c=1;d=0;# 100; \r\n\t\tif (y == 1) \r\n\t\t\t$display (\"  PASS  \\t%d  %d %d %d - %d\", a, b, c, d, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d %d - %d\", a, b, c, d, y);  \r\n                  \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/KM2.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Вектор",
                    Body = "Креирати модул који ће на улаз да добије тробитни улаз, а на излаз ће вратити три једнобитна излаза.",
                    TestBench = "module testbench;\r\n  reg[2:0] a;\r\n  wire x, y, z;\r\n\r\n  topmodule tm(a, x, y, z);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - x, y, z\");\r\n\r\n\t\ta = 5; # 100; \r\n\t\tif (x == 1 && y == 0 && z == 1) \r\n\t\t\t$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  -  %d %d %d\", a, x, y, z);\r\n\r\n\t\ta = 2; # 100; \r\n\t\tif (x == 0 && y == 1 && z == 0) \r\n\t\t\t$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  -  %d %d %d\", a, x, y, z);\t\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/vektor.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Полу вектор",
                    Body = "Креирати модул који на улазу прима 16 битни вектор а на излазу даје два 8 битна вектора.",
                    TestBench = "module testbench;\r\n  reg[15:0] a;\r\n  wire[7:0] x, y;\r\n\r\n  topmodule tm(a, x, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - x, y\");\r\n\r\n\t\ta = 16'hacbf; # 100; \r\n\t\tif (x == 8'hac && y == 8'hbf) \r\n\t\t\t$display (\"  PASS  \\t%d - %d %d\", a, x, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  -  %d %d\", a, x, y);\r\n\t\t\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/poluvektor.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Обрнуте речи",
                    Body = "Креирати модул где имамо 32 битни улаз који се састоји од 4 осмобитне речи и на излазу враћа исте те 4 речи али у обрнутом редоследу. Ево пример:\r\n11111111222222223333333344444444 => 44444444333333332222222211111111 ",
                    TestBench = "module testbench;\r\n  reg[31:0] a;\r\n  wire[31:0] x;\r\n\r\n  topmodule tm(a, x);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta - x\");\r\n\r\n\t\ta = 32'h12345678; # 100; \r\n\t\tif (x == 32'h78563412) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\", a, x);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d  -  %d\", a, x);\r\n\t\t\t\r\n\tend\r\n\t  \r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Логичка кола",
                    Body = "Креирати модул који добија на улаз тробитну вредност и потребно је да је на излаз да врати три једнобитна излаза (a, o, x).\r\no излаз а ће бити резултат тробитног и кола\r\nо излаз о ће бити резултат тробитног или кола\r\nо излаз х ће бити резултат тробитног ексклузивног или коло",
                    TestBench = "module testbench;\r\n  reg[2:0] a;\r\n  wire x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, x, y, z);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\sb\\sc\\s- x, y, z\");\r\n\r\n\t\ta = 5; # 100; \r\n\t\tif ( x==0 && y == 1 &&  z==0) \r\n\t\t\t$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d %d %d\", a, x, y, z);\r\n\r\n\t\ta = 7; # 100; \r\n\t\tif ( x==1 && y == 1 &&  z==1) \r\n\t\t\t$display (\"  PASS  \\t%d - %d %d %d\", a, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d %d %d\", a, x, y, z);              \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Спајање",
                    Body = "Креирати модул који извршити пребацивање три петобитна улаза у четири четворобитна излаза.",
                    TestBench = "module testbench;\r\n  reg[4:0] a, b, c;\r\n  wire[3:0] w, x, y, z;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, b, c, w, x, y, z);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\sb\\sc\\s- w, x, y, z\");\r\n\r\n\t\ta = 5'b10101;b=5'b01010;c=5'b10101; # 100; \r\n\t\tif (w == 4'b1010 && x==4'b1010 && y == 4'b1010 &&  z==4'b1011) \r\n\t\t\t$display (\"  PASS  \\t%d %d %d  - %d %d %d %d\", a, b, c, w, x, y, z);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d %d %d  - %d %d %d %d\", a, b, c, w, x, y, z);           \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/spajanje.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Обрнут редослед",
                    Body = "Креирати модул који улазну осмобитну вредност баци на излазу са обрнутом редоследу.\r\n12345678 => 87654321",
                    TestBench = "module testbench;\r\n  reg[7:0]a;\r\n  wire[7:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta- y\");\r\n\r\n\t\ta = 8'b10101010; # 100; \r\n\t\tif (y == 8'b01010101) \r\n\t\t\t$display (\"  PASS  \\t%d  - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d\", a, y);           \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Проширивање",
                    Body = "Креирати модул који има осмобитни улаз и на излаз даје улазну вредност која је проширена да буде тридесетдвобитна. Улазна вредност је проширена тако што је седми бит дуплициран 24 пута и онда следи осмобитни улаз.",
                    TestBench = "module testbench;\r\n  reg[7:0]a;\r\n  wire[31:0] y;\r\n\r\n  //Design Instance\r\n  topmodule tm(a, y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta- y\");\r\n\r\n\t\ta = 8'hac; # 100; \r\n\t\tif (y == 32'hffffffac) \r\n\t\t\t$display (\"  PASS  \\t%d  - %d\", a, y);\r\n\t\telse \r\n\t\t\t$display (\"  FAIL \\t%d - %d\", a, y);           \r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                }
            );
        }
    }
}

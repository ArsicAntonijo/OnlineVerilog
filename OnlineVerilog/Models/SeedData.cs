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
                new IdentityRole() { Id = admRoleId, Name = "admin", NormalizedName = "admin" },
                new IdentityRole() { Id = usrRoleId, Name = "user", NormalizedName = "user" });

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
                    TestBench = "module testbench;\r\n  wire y;\r\n\r\n  topmodule tm(y);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n\t\t# 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n\t\t# 100; \r\n\t\tif ( y == 1 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL  \\t%d - %d\",a,y);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Основе",
                    Header = "Жица",
                    Body = "За разлику од физичких жица у Верилогу информација тече само у једном смеру, обично од једног извора ка потрошачу. Додела вредности се ради десно ка лево, вредност сигнала са десне стране се емитује на жицу са леве стране. У Верилогу додела није једнократни догађај него је \"континуална\" јер се додела одвија непрестано чак и ако се вредност са десне стране промени. \r\nПортови на модулу такође имају свој смер (обично улаз или излаз). Улазни порт је покренут нечим ван модула, док излазни порт покреће нешто споља. \r\nПотребно је да се креирати модул са јендним улазом и једним излазом који ће се понапати као жица.",
                    TestBench = "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(y,a);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif ( y == 1 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n\t\ta = 0; # 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL  \\t%d - %d\",a,y);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/zica.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Основе",
                    Header = "Четири жице",
                    Body = "Креирајте модул са 3 улаза и 4 излаза који се понаша као жице које праве ове везе:\r\nа -> w\r\nб -> x\r\nб -> y\r\nц -> z",
                    TestBench = "",
                    imagePath = "/uploads/4zice.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Не kоло",
                    Body = "Креирајте модул који имплементира не логичко коло. Уколико је 1 на улазу на излазу треба да добијемо 0 и уколико је 0 на излазу треба да добијемо 1.\r\n",
                    TestBench = "module testbench;\r\n  reg a;\r\n  wire y;\r\n\r\n  //Design Instance\r\n  topmodule tm(y,a);\r\n  \r\n\tinitial\r\n\tbegin\r\n\t\t$display (\"RESULT\\ta\\s-\\sy\");\r\n\r\n\t\ta = 1; # 100; \r\n\t\tif ( y == 0 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL \\t%d - %d\",a,y);\r\n\r\n\t\ta = 0; # 100; \r\n\t\tif ( y == 1 ) \r\n\t\t\t$display (\"  PASS  \\t%d - %d\",a,y);\r\n\t\telse\r\n\t\t\t$display (\"  FAIL  \\t%d - %d\",a,y);\r\n\tend\r\n\t  \r\n  //enabling the wave dump\r\n  initial begin \r\n    $dumpfile(\"dump.vcd\"); $dumpvars;\r\n  end\r\n  \r\nendmodule",
                    imagePath = "/uploads/ne-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "И коло",
                    Body = "Креаирати модул који имлементира и логичко коло.",
                    TestBench = "",
                    imagePath = "/uploads/i-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Или коло",
                    Body = "Креаирати модул који имлементира или логичко коло.",
                    TestBench = "",
                    imagePath = "/uploads/ili-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Ни коло",
                    Body = "Креаирати модул који имлементира ни логичко коло.",
                    TestBench = "",
                    imagePath = "/uploads/ni-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Логичка кола",
                    Header = "Ексклузивно или коло",
                    Body = "Креаирати модул који имлементира ексклузивно или коло логичко коло.",
                    TestBench = "",
                    imagePath = "/uploads/xor-kolo.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Комбинација 1",
                    Body = "Креирати модул који ће изледати као на слици испод...",
                    TestBench = "",
                    imagePath = "/uploads/logicka-sema-1.png"
                }, 
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Комбинација 2",
                    Body = "Креирати модул који ће изледати као на слици испод...",
                    TestBench = "",
                    imagePath = "/uploads/logicka-sema-2.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Мултиплексер 2-1",
                    Body = "Креирати 2 - 1 мултиплексер...",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "16-Битни мултиплексер",
                    Body = "Креирати модул мултиплексер 2 на 1, који има  два 16 битна улаза, један једнобитни улаз селекције и један излаз.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Мултиплексер 8-1",
                    Body = "Креирати модул мултиплексер 8 на 1, који ће имати осам једнобитних улаза, један тробитни улаз за селекцију и једна излаз.  ",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Двобитно поређење",
                    Body = "Креирати модул који прима два двобитна улаза и на излаз даје 1 уколико су улази једнаки.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {   
                    Id = i++,
                    Section = "Основе",
                    Header = "Математичка једначина",
                    Body = "Креирати модул који има два улаза x  и y, излаз z и ће извршити математичку једначину:\r\nz = (x^y) & x",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Термостат",
                    Body = "Креирати модул који ће да контролира рад једног клима уређаја при чему ће активирати по потреби грејање, хлађење или вентилатор. Модул треба да има четири једнобитна улаза (prevruce, prehladno, mod, ukljucen_ventilator) и три једнобитна излаза (grejanje, hladjenje, ventilator). \r\nУколико је mod 1 и кад је prehladno 1 потребно је да се укључи grejanje а да се hladjenje угаси. Ако је mod 1 и prevruce је 1 потребно је да се укључи hladjenje а да се угаси grejanje. Ако је grejanje или hladjenje активно потребно је да се и ventilator укључи због циркулације ваздуха. За активирање ventilator-а се такође користи улаз ukljucen_ventilator.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Поновљене јединице",
                    Body = "Потребно је креирати модул који ће да прими тробитни улаз и да на излаз да колико јединица има у улазном сигналу.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Полусабирач",
                    Body = "Креирати модул полусабирач, који сабира два једнобитна улаза и на излаз даје  једнобитну суму и пренос.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Сабирач",
                    Body = "Креирати модул сабирач, који сабира три једнобитна улаза (a, b, cin) и на излаз даје  једнобитну суму и пренос.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Аритметика",
                    Header = "Сабирач8",
                    Body = "Креирати модул сабирач, који сабира три осмобитна улаза (a, b, cin) и на излаз даје  осмобитну суму и пренос.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Карнове мапе",
                    Header = "КМ1",
                    Body = "Креирати модул који ће имплементирати шему са слике.",
                    TestBench = "",
                    imagePath = "/uploads/KM1.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Карнове мапе",
                    Header = "КМ2",
                    Body = "Креирати модул који ће имплементирати шему са слике.",
                    TestBench = "",
                    imagePath = "/uploads/KM2.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Карнове мапе",
                    Header = "КМ3",
                    Body = "Креирати модул који ће имплементирати шему са слике.",
                    TestBench = "",
                    imagePath = "/uploads/KM3.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Вектор",
                    Body = "Креирати модул који ће на улаз да добије тробитни улаз, а на излаз ће вратити три једнобитна излаза.",
                    TestBench = "",
                    imagePath = "/uploads/vektor.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Полу вектор",
                    Body = "Креирати модул који на улазу прима 16 битни вектор а на излазу даје два 8 битна вектора.",
                    TestBench = "",
                    imagePath = "/uploads/poluvektor.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Обрнуте речи",
                    Body = "Креирати модул где имамо 32 битни улаз који се састоји од 4 осмобитне речи и на излазу враћа исте те 4 речи али у обрнутом редоследу. Ево пример:\r\n11111111222222223333333344444444 => 44444444333333332222222211111111 ",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Логичка кола",
                    Body = "Креирати модул који добија на улаз тробитну вредност и потребно је да је на излаз да врати три једнобитна излаза (a, o, x).\r\no излаз а ће бити резултат тробитног и кола\r\nо излаз о ће бити резултат тробитног или кола\r\nо излаз х ће бити резултат тробитног ексклузивног или коло",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Спајање",
                    Body = "Креирати модул који извршити пребацивање три петобитна улаза у четири четворобитна излаза.",
                    TestBench = "",
                    imagePath = "/uploads/spajanje.png"
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Обрнут редослед",
                    Body = "Креирати модул који улазну осмобитну вредност баци на излазу са обрнутом редоследу.\r\n12345678 => 87654321",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Вектори",
                    Header = "Проширивање",
                    Body = "Креирати модул који има осмобитни улаз и на излаз даје улазну вредност која је проширена да буде тридесетдвобитна. Улазна вредност је проширена тако што је седми бит дуплициран 24 пута и онда следи осмобитни улаз.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Плексери",
                    Header = "Приоритетни енкодер",
                    Body = "Креирати модул који ће престављати приоритентни енкодер. Приоритетни енкодер на улазу прима четворобитну вредност и на излазу даје локацију навишег бита, што ће бити величине два бита.",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Id = i++,
                    Section = "Комбинација",
                    Header = "Тастатура",
                    Body = "Креирати модул који ће да 16 битни код са тастатуре који прима на улаз дешифрирати и вратити на излаз која је стрелица у питање.\r\nИзлази ће бити 4 једнобитне вредности лево, десно, доле и горе.",
                    TestBench = "",
                    imagePath = "/uploads/tastatura.png"
                }/*,
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                },
                new Example
                {
                    Section = "",
                    Header = "",
                    Body = "",
                    TestBench = "",
                    imagePath = ""
                }*/
            );
        }
    }
}

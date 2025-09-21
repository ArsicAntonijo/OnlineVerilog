using Service;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OnlineVerilog.Service
{
    public class VerilogHelper : IVerilog
    {
        private readonly static string WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Env");
        private string TempDirectory = string.Empty;
        private readonly string ExeName = "tmp";
        static VerilogHelper() 
        {
            if (!Directory.Exists(WorkingDirectory)) { Directory.CreateDirectory(WorkingDirectory); }           
        }
        public (string, string, bool) ExecuteTheProcess(string moduleFileName, string modulefileContent, string testbenchFileName, string testbenchFileContent)
        {
            TempDirectory = Path.Combine(WorkingDirectory, DateTime.Now.ToString("yyMMddHHmmfffffff"));

            if (!Directory.Exists(TempDirectory)) { Directory.CreateDirectory(TempDirectory); }

            File.WriteAllText(Path.Combine(TempDirectory, moduleFileName), modulefileContent);
            File.WriteAllText(Path.Combine(TempDirectory, testbenchFileName), testbenchFileContent);

            string vcdromlink = string.Empty;
            bool status = false;
            string output = Compile(moduleFileName, testbenchFileName);
            if (string.IsNullOrEmpty(output))
            {
                (output, status) = ProcessOutput(Run());
                vcdromlink = RenameAndUpload();
            }
            else
            {
                (output, _) = ProcessOutput(output, true);
            }
            if (Directory.Exists(TempDirectory)) { Directory.Delete(TempDirectory, true); }

            return (output, vcdromlink, status);
        }

        private string Compile(string moduleFileName, string testbenchFileName)
        {
            string outp = string.Empty;
            Process p = new();
            p.StartInfo.FileName = "iverilog";
            p.StartInfo.WorkingDirectory = TempDirectory;
            p.StartInfo.Arguments = $" -o {ExeName} {testbenchFileName} {moduleFileName}";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();
            outp += p.StandardOutput.ReadToEnd();
            outp += p.StandardError.ReadToEnd();
            p.WaitForExit();

            return outp;
        }
        public string Run()
        {
            string outp = string.Empty;
            Process p = new();
            p.StartInfo.FileName = "vvp";
            p.StartInfo.WorkingDirectory = TempDirectory;
            p.StartInfo.Arguments = ExeName;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();
            outp += p.StandardOutput.ReadToEnd();
            outp += p.StandardError.ReadToEnd();
            p.WaitForExit();

            return outp;
        }

        public static string ValidateSolution(string solution)
        {
            Match m;
            m = new Regex("module(.+?)endmodule", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Твоје решење мора да има почетни таг \"module\" и завршни таг \"endmodule\""; }

            m = new Regex(@"module\s+topmodule(.+?)endmodule", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Због ограничења апликације назив модула мора бити 'topmodule'"; }

            m = new Regex(@"module\s+topmodule\s*\((.+?)?\);(.+?)endmodule", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Након име модула морате да унесете заграде '(', ')' и ';'"; }

            m = new Regex(@"(input|output)", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Морате унети улазне и/или излазне параметре модула"; }

            m = new Regex(@"assign\s+<?=", RegexOptions.Singleline).Match(solution);
            if (m.Success) { return "Након 'assign' морате унети променљиву кој треба да прихвати вредност са десне стране"; }

            m = new Regex(@"(input|output)", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Морате унети улазне и/или излазне параметре модула"; }

            return string.Empty;
        }

        private string RenameAndUpload()
        {
            string stamp = Path.GetFileName(TempDirectory) ?? string.Empty;
            string tempDumpPath = Path.Combine(TempDirectory, "dump.vcd");
            string dumpFileName = $"dump_{stamp}.vcd";          

            if (!File.Exists(tempDumpPath)) return string.Empty;

            string dumpFileContent = File.ReadAllText(tempDumpPath);
            if (GitHubApi.PushToGit(dumpFileName, dumpFileContent, stamp).Result)
            {
                return $"https://vc.drom.io/?github={GitHubApi.RepoOwner}/{GitHubApi.RepoName}/master/{dumpFileName}";
            }
            else
            {
                return string.Empty;
            }
        }

        private (string, bool) ProcessOutput(string v, bool isCompileError = false)
        {
            if (!isCompileError)
            {
                int failedTests = 0;
                var e = new Regex("\\sFAIL\\s+(?<expected>\\w+)\\s+-\\s+(?<inputs>\\w+)").Matches(v);
                if (e.Count == 0) return ("Задатак је успешно решен :)", true);

                string output = string.Empty;
                foreach (Match m in e)
                {
                    output += string.Format(" * Ако на улаз имамо: {1} на излазу треда да се добије {0}\r\n", m.Groups["expected"].Value, m.Groups["inputs"]);
                    failedTests++;
                }
                output = "Код је пао на " + failedTests + " ситуацијама:\r\n" + output;
                return (output, false);
            }
            else
            {
                string output = string.Empty;
                Match m;

                m = new Regex(@"topmodule.v:(?<line>\d+): syntax error").Match(v);
                if (m.Success) output += $"Синтаксичка грешка на линији {m.Groups["line"]}\r\n";

                m = new Regex(@"topmodule.v:(?<line>\d+): Errors in port declarations").Match(v);
                if (m.Success) output += $"Грешка при декларације порта на линији {m.Groups["line"]}\r\n";
                
                if (string.IsNullOrEmpty(output))
                    return (v.Replace("\n", "\r\n"), true);
                else
                    return (output, true);
            }

        }
    }
}

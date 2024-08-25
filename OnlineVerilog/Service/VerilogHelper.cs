using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OnlineVerilog.Service
{
    public class VerilogHelper
    {
        private readonly static string WorkingDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Env");
        private string TempDirectory = string.Empty;
        private readonly string ExeName = "tmp";
        static VerilogHelper() 
        {
            if (!Directory.Exists(WorkingDirectory)) { Directory.CreateDirectory(WorkingDirectory); }           
        }
        public (string, string) ExecuteTheProcess(string moduleFileName, string modulefileContent, string testbenchFileName, string testbenchFileContent)
        {
            TempDirectory = Path.Combine(WorkingDirectory, DateTime.Now.ToString("yyMMddHHmmfffffff"));

            if (!Directory.Exists(TempDirectory)) { Directory.CreateDirectory(TempDirectory); }

            File.WriteAllText(Path.Combine(TempDirectory, moduleFileName), modulefileContent);
            File.WriteAllText(Path.Combine(TempDirectory, testbenchFileName), testbenchFileContent);

            string output = Compile(moduleFileName, testbenchFileName);
            string vcdromlink = string.Empty;
            if (string.IsNullOrEmpty(output))
            {
                output = ProcessOutput(Run());
                vcdromlink = RenameAndUpload();
            }
            if (Directory.Exists(TempDirectory)) { Directory.Delete(TempDirectory, true); }

            return (output, vcdromlink);
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

        internal static string ValidateSolution(string solution)
        {
            Match m = new Regex("module(.+?)endmodule", RegexOptions.Singleline).Match(solution);
            if (!m.Success) { return "Solution must start with \"module\" and finish with \"endmodule\""; }

            return string.Empty;
        }

        private string RenameAndUpload()
        {
            string stamp = Path.GetFileName(TempDirectory) ?? string.Empty;
            string tempDumpPath = Path.Combine(TempDirectory, "dump.vcd");
            string dumpFileName = $"dump_{stamp}.vcd";          

            if (!File.Exists(tempDumpPath)) return string.Empty;

            string dumpFileContent = File.ReadAllText(tempDumpPath);
            GitHubApi.PushToGit(dumpFileName, dumpFileContent, stamp);
            return $"https://vc.drom.io/?github={GitHubApi.RepoOwner}/{GitHubApi.RepoName}/master/{dumpFileName}";
        }

        private string ProcessOutput(string v)
        {
            int failedTests = 0;
            var e = new Regex("\\sFAIL\\s+(?<expected>\\w+)\\s+-\\s+(?<inputs>\\w+)").Matches(v);
            if (e.Count == 0) return "Задатак је успешно решен :)";

            string output = string.Empty;
            foreach (Match m in e)
            {
                output += string.Format(" * Ако на улаз имамо: {1} на излазу треда да се добије {0}\r\n", m.Groups["expected"].Value, m.Groups["inputs"]);
                failedTests++;
            }
            output = "Код је пао на " + failedTests + " ситуацијама:\r\n" + output;
            return output;
        }
    }
}

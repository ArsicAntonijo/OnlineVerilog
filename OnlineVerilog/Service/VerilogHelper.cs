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
        public string ExecuteTheProcess(string moduleFileName, string modulefileContent, string testbenchFileName, string testbenchFileContent)
        {
            TempDirectory = Path.Combine(WorkingDirectory, DateTime.Now.ToString("yyMMddHHmmfffffff"));
            if (!Directory.Exists(TempDirectory)) { Directory.CreateDirectory(TempDirectory); }
            File.WriteAllText(Path.Combine(TempDirectory, moduleFileName), modulefileContent);
            File.WriteAllText(Path.Combine(TempDirectory, testbenchFileName), testbenchFileContent);
            string compileOutput = Compile(moduleFileName, testbenchFileName);
            if (string.IsNullOrEmpty(compileOutput))
            {
                string runOutput = Run();
                return runOutput;
            }
            if (Directory.Exists(TempDirectory)) { Directory.Delete(TempDirectory, true); }
            return compileOutput;
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
    }
}

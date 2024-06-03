using System.Diagnostics;

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
        public string ExecuteTheProcess(string fileName, string fileContent)
        {
            TempDirectory = Path.Combine(WorkingDirectory, DateTime.Now.ToString("yyMMddHHmmfffffff"));
            if (!Directory.Exists(TempDirectory)) { Directory.CreateDirectory(TempDirectory); }
            File.WriteAllText(Path.Combine(TempDirectory, fileName), fileContent);
            string compileOutput = Compile(fileName);
            if (string.IsNullOrEmpty(compileOutput))
            {
                string runOutput = Run();
                return runOutput;
            }
            if (Directory.Exists(TempDirectory)) { Directory.Delete(TempDirectory, true); }
            return compileOutput;
        }
        private string Compile(string fileName)
        {
            string outp = string.Empty;
            Process p = new();
            p.StartInfo.FileName = "iverilog";
            p.StartInfo.WorkingDirectory = TempDirectory;
            p.StartInfo.Arguments = $" -o {ExeName} {fileName}";
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
    }
}

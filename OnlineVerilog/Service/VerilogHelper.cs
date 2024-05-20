using System.Diagnostics;

namespace OnlineVerilog.Service
{
    public class VerilogHelper
    {
        private readonly string WorkingDirectory = "C:\\Users\\arsica\\source\\repos\\OnlineVerilog\\OnlineVerilog\\Env\\";
        private readonly string ExeName = "tmp";
        public string ExecuteTheProcess(string fileName, string fileContent)
        {
            File.WriteAllText(WorkingDirectory + fileName, fileContent);
            string compileOutput = Compile(fileName);
            if (string.IsNullOrEmpty(compileOutput))
            {
                string runOutput = Run();
                return runOutput;
            }
            return compileOutput;
        }
        private string Compile(string fileName)
        {
            string outp = string.Empty;
            Process p = new();
            p.StartInfo.FileName = "iverilog";
            p.StartInfo.WorkingDirectory = WorkingDirectory;
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
            p.StartInfo.WorkingDirectory = WorkingDirectory;
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

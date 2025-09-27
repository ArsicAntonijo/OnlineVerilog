using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IVerilog
    {
        (string runoutput, string dumpfilepath, bool status) ExecuteTheProcess(string v1, string solution, string v2, string testBench);
        string GetSolutionTemplate(string testBench);
    }
}

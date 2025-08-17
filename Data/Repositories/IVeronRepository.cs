using Data.Models;
using OnlineVerilog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IVeronRepository
    {
        void AddNewExample(Example example);
        void DeleteExample(int? id);
        bool ExampleExists(int id);
        int GetExampleCount();
        Example GetExample(int? id);
        List<ModifiedExample> GetModifiedExamples();
        List<User> GetUsers();
        Task<bool> UpdateExample(Example example);
        void AddSolvedExample(SolvedExample solvedExample);
        List<Example> GetExamples();
        List<SolvedExample> GetSolvedExamples(string? userId);
    }
}

using Data.Models;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Context;
using OnlineVerilog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class VeronRepository : IVeronRepository
    {
        public VeronContext _context;
        public VeronRepository(VeronContext vc)
        {
            _context = vc;
        }

        public int GetExampleCount()
        {
           return _context.Examples.ToList().Count;
        }

        public List<User> GetUsers()
        {
            return _context.Users.Include(u => u.SolvedExamples).ToList();
        }
        public List<ModifiedExample> GetModifiedExamples()
        {
            int total = GetExampleCount();
            var users = GetUsers();
            return users.Select(me => new ModifiedExample
            {
                Name = me.FirstName,
                TotalExamples = total,
                SolvedExamples = me.SolvedExamples.Count
            }).ToList();
        }
        public async void AddNewExample(Example e)
        {
            _context.Examples.Add(e);
            await _context.SaveChangesAsync();
        }
        public void DeleteExample(int? id)
        {
            try
            {
                var example = _context.Examples.Where(e => e.Id == id).Include(se => se.SolvedByUsers).FirstOrDefault();

                if (example != null)
                {
                    _context.Remove(example);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task<bool> UpdateExample(Example e)
        {
            _context.Attach(e).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExampleExists(e.Id))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public Example GetExample(int? id)
        {
            return _context.Examples.FirstOrDefault(m => m.Id == id);
        }
        public bool ExampleExists(int id)
        {
            return _context.Examples.Any(e => e.Id == id);
        }
        public void AddSolvedExample(SolvedExample se)
        {
            _context.SolvedExamples.Add(se);
            _context.SaveChanges();
        }
        public List<Example> GetExamples()
        {
           return _context.Examples.ToList();
        }
        public List<SolvedExample> GetSolvedExamples(string uid)
        {
            return _context.SolvedExamples.Where(se => se.UserId == uid).ToList();
        }
    }
}

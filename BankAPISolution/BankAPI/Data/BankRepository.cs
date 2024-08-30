using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data
{
    public class BankRepository
    {
        private readonly BankContext _context;

        public BankRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bank>> GetAllBanksAsync()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<Bank> GetBankByIdAsync(int id)
        {
            return await _context.Banks.FindAsync(id);
        }

        public async Task AddBankAsync(Bank bank)
        {
            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBankAsync(Bank bank)
        {
            _context.Banks.Update(bank);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBankAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();
            }
        }
    }
}

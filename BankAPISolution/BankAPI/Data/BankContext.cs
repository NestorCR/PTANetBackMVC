using BankAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BankAPI.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
            : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }

    }
}

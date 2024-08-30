namespace BankAPI.BusinessLogic
{
    using BankAPI.Data;
    using BankAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using BankAPI.Utilities;

    //Class for manage all the process related with Bank entity. It's a simple version.
    // In real situations its better to split the diferents usecases into independant methods.
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private readonly BankContext _context; 
        private readonly ILogger<BankService> _logger;

        public BankService(HttpClient httpClient, BankContext context, ILogger<BankService> logger)
        {
            _httpClient = httpClient;
            _context = context;
            _logger = logger;
        }

        public BankService(HttpClient httpClient, BankContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        //Get Banks info from External API
        public async Task<string> GetBanksFromExternalAPI()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.opendata.esett.com/EXP06/Banks");
                var banks = JsonSerializer.Deserialize<List<Bank>>(response);
                if (banks == null || !banks.Any())
                {
                    _logger.LogError("No data from external API");

                    return "No data from external API";
                }

                await AddBanksAsync(banks);

                var savedBanks = await GetAllBanksAsync();

                var savedBanksJson = JsonSerializer.Serialize(savedBanks);

                return savedBanksJson;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return $"Error: {ex.Message}";
            }
        }
        
        
        // Thats would be the basic structure for the functions to get the info and the results. I only developed for one.
        public async Task<Result<Bank>> AddBankAsync(Bank bank)
        {
            try
            {
                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Bank added successfully with ID {Id}", bank.Id);
                return Result<Bank>.Success(bank);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bank");
                return Result<Bank>.Failure($"Error adding bank: {ex.Message}");
            }
        }


        // Add multiple banks
        public async Task<IEnumerable<Bank>> AddBanksAsync(List<Bank> banks)
        {
            if (banks != null && banks.Any())
            {
                await _context.Banks.AddRangeAsync(banks);
                await _context.SaveChangesAsync();
            }
            return await GetAllBanksAsync();

        }


        // Read All banks
        public async Task<IEnumerable<Bank>> GetAllBanksAsync()
        {
            return await _context.Banks.ToListAsync();
        }

        // Read bank by Id
        public async Task<Bank> GetBankByIdAsync(int id)
        {
            return await _context.Banks.FindAsync(id);
        }

        // Update bank
        public async Task<Bank> UpdateBankAsync(Bank bank)
        {
            _context.Banks.Update(bank);
            await _context.SaveChangesAsync();
            return bank;
        }

        // Delete bank by id
        public async Task<bool> DeleteBankAsync(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return false;
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

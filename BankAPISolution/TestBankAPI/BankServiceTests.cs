using BankAPI.BusinessLogic;
using BankAPI.Data;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestBankAPI
{
 // Automatically generated class with AI;
 // I don't want to waste time on that for an interview exercise, I don't have time to do it all in just over an hour.
 public class BankServiceTests
    {
        private readonly BankService _bankService;
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly DbContextOptions<BankContext> _dbContextOptions;

        public BankServiceTests()
        {
            // Configurar la base de datos en memoria
            _dbContextOptions = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase("BankTestDatabase")
                .Options;

            var context = new BankContext(_dbContextOptions);

            // Configurar HttpClient simulado
            _httpClientMock = new Mock<HttpClient>();

            _bankService = new BankService(_httpClientMock.Object, context);
        }

        [Fact]
        public async Task GetBanksFromExternalAPI_ShouldReturnSavedBanks()
        {
            // Configurar HttpClient simulado para devolver datos JSON
            var jsonResponse = "[{\"Id\":1,\"Name\":\"Bank A\"}, {\"Id\":2,\"Name\":\"Bank B\"}]";
            _httpClientMock.Setup(client => client.GetStringAsync(It.IsAny<string>())).ReturnsAsync(jsonResponse);

            // Llamar al método
            var result = await _bankService.GetBanksFromExternalAPI();

            // Verificar que el resultado no es null y contiene los datos esperados
            Assert.NotNull(result);
            Assert.Contains("Bank A", result);
            Assert.Contains("Bank B", result);

            // Verificar que los bancos fueron guardados en la base de datos
            using (var context = new BankContext(_dbContextOptions))
            {
                var banks = await context.Banks.ToListAsync();
                Assert.Equal(2, banks.Count);
                Assert.Contains(banks, b => b.Name == "Bank A");
                Assert.Contains(banks, b => b.Name == "Bank B");
            }
        }

        [Fact]
        public async Task AddBankAsync_ShouldAddBank()
        {
            // Crear un banco de prueba
            var bank = new Bank { Id = 1, Name = "Bank Test" };

            // Llamar al método
            var result = await _bankService.AddBankAsync(bank);

            // Verificar que el banco fue guardado en la base de datos
            using (var context = new BankContext(_dbContextOptions))
            {
                var savedBank = await context.Banks.FindAsync(1);
                Assert.NotNull(savedBank);
                Assert.Equal("Bank Test", savedBank.Name);
            }
        }

        [Fact]
        public async Task DeleteBankAsync_ShouldDeleteBank()
        {
            // Añadir un banco de prueba
            using (var context = new BankContext(_dbContextOptions))
            {
                await context.Banks.AddAsync(new Bank { Id = 1, Name = "Bank To Delete" });
                await context.SaveChangesAsync();
            }

            // Llamar al método de eliminación
            var result = await _bankService.DeleteBankAsync(1);

            // Verificar que el banco fue eliminado
            using (var context = new BankContext(_dbContextOptions))
            {
                var deletedBank = await context.Banks.FindAsync(1);
                Assert.Null(deletedBank);
                Assert.True(result);
            }
        }

        [Fact]
        public async Task UpdateBankAsync_ShouldUpdateBank()
        {
            // Añadir un banco de prueba
            using (var context = new BankContext(_dbContextOptions))
            {
                var bank = new Bank { Id = 1, Name = "Old Name" };
                await context.Banks.AddAsync(bank);
                await context.SaveChangesAsync();
            }

            // Actualizar el banco
            var updatedBank = new Bank { Id = 1, Name = "New Name" };
            var result = await _bankService.UpdateBankAsync(updatedBank);

            // Verificar que el banco fue actualizado
            using (var context = new BankContext(_dbContextOptions))
            {
                var bank = await context.Banks.FindAsync(1);
                Assert.NotNull(bank);
                Assert.Equal("New Name", bank.Name);
            }
        }
    }
}

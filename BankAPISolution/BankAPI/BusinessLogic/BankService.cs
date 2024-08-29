namespace BankAPI.BusinessLogic
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class BankService
    {
        private readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetBanksAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.opendata.esett.com/EXP06/Banks");
            return response;
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;

namespace VibeCodeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("GlobalRateLimit")]
    public class SupabaseController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public SupabaseController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("sefrep")]
        public async Task<IActionResult> GetSefrepRegistros([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O parâmetro 'nome' é obrigatório.");
            }

            var url = _configuration["Supabase:Url"];
            var key = _configuration["Supabase:Key"];

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
            {
                return StatusCode(500, "Configuração do Supabase ausente.");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("apikey", key);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            // Construct Supabase query dynamically
            var requestUrl = $"{url}/rest/v1/sefrep_registros?nome=ilike.*{Uri.EscapeDataString(nome)}*&order=updated_at.desc";

            try
            {
                var response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json"); // Proxy exactly what Supabase returns
                }
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar SEFREP: {ex.Message}");
            }
        }

        [HttpGet("seape")]
        public async Task<IActionResult> GetSeapeRegistros([FromQuery] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O parâmetro 'nome' é obrigatório.");
            }

            var url = _configuration["Supabase:Url"];
            var key = _configuration["Supabase:Key"];

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
            {
                return StatusCode(500, "Configuração do Supabase ausente.");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("apikey", key);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            // Construct Supabase query dynamically
            var requestUrl = $"{url}/rest/v1/seape_registros?nome=ilike.*{Uri.EscapeDataString(nome)}*&order=created_at.desc";

            try
            {
                var response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json"); // Proxy exactly what Supabase returns
                }
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar SEAPE: {ex.Message}");
            }
        }
        
        [HttpGet("queue")]
        public async Task<IActionResult> GetQueuePositions()
        {
            var url = _configuration["Supabase:Url"];
            var key = _configuration["Supabase:Key"];

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
            {
                return StatusCode(500, "Configuração do Supabase ausente.");
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("apikey", key);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);

            // Consultar a fila do SEFREP para VTC ativos
            var requestUrl = $"{url}/rest/v1/sefrep_registros?tema=ilike.*VTC*&status=in.(%22em%20analise%22,%22em%20andamento%22)&select=id,data_entrada";

            try
            {
                var response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return Content(content, "application/json"); 
                }
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar fila VTC: {ex.Message}");
            }
        }
    }
}

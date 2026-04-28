using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PruebaTecnicaCarsales.BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodeController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EpisodeController> _logger;
        
        public EpisodeController(ILogger<EpisodeController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient=httpClientFactory.CreateClient();
        }

        [HttpGet("Episodes")]
        public async Task<IActionResult> GetEpisodes()
        {
            var url="https://rickandmortyapi.com/api/episode";
            var response=await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,"Error al consumir API Externa");
            }
            var content = await response.Content.ReadAsStringAsync();
            return Content(content,"application/json");
        }

        


    }
}
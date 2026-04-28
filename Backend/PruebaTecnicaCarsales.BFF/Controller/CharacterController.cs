using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PruebaTecnicaCarsales.BFF.Controllers
{
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CharacterController> _logger;
        
        public CharacterController(ILogger<CharacterController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient=httpClientFactory.CreateClient();
        }

        [HttpGet("Characters")]
        public async Task<IActionResult> GetCharacter()
        {
            var url="https://rickandmortyapi.com/api/character";
            var response=await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,"Error al obtener las localizaciones de la API Externa");
            }
            var content= await response.Content.ReadAsStringAsync();
            return Content(content,"Application/json");
        }

    }
}
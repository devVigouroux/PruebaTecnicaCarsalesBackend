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
    public class LocationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<LocationController> _logger;
        
        public LocationController(ILogger<LocationController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient=httpClientFactory.CreateClient();
        }

        [HttpGet("Locations")]
        public async Task<IActionResult> GetLocations()
        {
            var url="https://rickandmortyapi.com/api/location";
            var response=await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,"Error al obtener las localizaciones de la API Externa");

            }
            var content=await response.Content.ReadAsStringAsync();
            return Content(content,"Application/json");

        }

    }
}
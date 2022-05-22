using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace zadanieBroadridgee.Controllers;

[ApiController]
[Route("[controller]")]
public class WorldTimeController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public WorldTimeController(IConfiguration configuration)
    {
        _configuration = configuration;
    } 
    
    [HttpGet]
    public async Task<WorldTime> Get()
    {
        WorldTimeApi? worldTime = null;
        var timeZone = _configuration["Timezone"];
        string? validTimeZones = null;
        
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"http://worldtimeapi.org/api/timezone"))
            {
                var response = await httpClient.SendAsync(request);
                
                if (!response.IsSuccessStatusCode)
                {
                    return new WorldTime(null, null);
                }
                
                var contents = await response.Content.ReadAsStringAsync();

                if (!contents.Contains($@"""{timeZone}"""))
                {
                    validTimeZones = contents;
                }
                
            }

            if (validTimeZones == null)
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"),
                           $"http://worldtimeapi.org/api/timezone/{timeZone}"))
                {
                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();
                        worldTime = JsonSerializer.Deserialize<WorldTimeApi>(contents);
                    }

                }
            }
        }

        return new WorldTime(worldTime?.datetime, validTimeZones);

    }
}
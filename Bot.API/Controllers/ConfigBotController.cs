using Microsoft.AspNetCore.Mvc;
using Domain.Services.Api_Services;

namespace Bot.API.Controllers
{
    [ApiController]
    [Route("api/v1/rank")]
    public class ConfigBotController : ControllerBase
    {
        private readonly ConfigBotServices _configBot;

        public ConfigBotController(ConfigBotServices configBot)
        {
            _configBot = configBot;
        }


        [HttpGet]
        public async Task<IActionResult> GetRank()
        {
            return Ok();
        }

    }
}
using Microsoft.AspNetCore.Mvc;
using Domain.Services.Api_Services;
using Bot.API.InputModel;
using Domain.Services;
using Infrastructure.Data.Context;
using Bot.API.ViewModel;

namespace Bot.API.Controllers
{
    [ApiController]
    [Route("api/v1/rank")]
    public class ConfigBotController : ControllerBase
    {
        private readonly RankService _rankService;


        public ConfigBotController(RankService rankService)
        {
            _rankService = rankService;
        }


        [HttpGet]
        public async Task<IActionResult> GetRank()
        {
            var rank = _rankService.GetRankByGuildId(1038639386646032415);
            var viewModel = new List<UsersViewModel>();
            foreach (var item in rank)
            {
                viewModel.Add(
                    new UsersViewModel()
                    {
                        UserName = item.DiscordNickName,
                        ThumbUrl = item.ThumbUrl,
                        StatsCount = item.RankPoints
                    });
            }


            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostConfigs(ConfigBotInputModel configBotInput)
        {

            _rankService.ConfigureRank(configBotInput.BlockIds, configBotInput.RankingType, 1038639386646032415);

            return Ok("Aparentemente deu tudo certo '-'");
        }

    }
}
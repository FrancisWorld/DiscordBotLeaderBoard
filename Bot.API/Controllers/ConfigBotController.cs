using Microsoft.AspNetCore.Mvc;
using Domain.Services.Api_Services;
using Bot.API.InputModel;
using Domain.Services;
using Infrastructure.Data.Context;
using Bot.API.ViewModel;
using Domain.Interfaces;

namespace Bot.API.Controllers
{
    [ApiController]
    [Route("api/v1/rank")]
    public class ConfigBotController : ControllerBase
    {
        private readonly RankService _rankService;
        private readonly AuthenticationServices _autheticationServices;

        public ConfigBotController(RankService rankService, AuthenticationServices authenticationServices)
        {
            _rankService = rankService;
            _autheticationServices = authenticationServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetRank(ulong guildId)
        {
            
            var rank = _rankService.GetRankByGuildId(1038639386646032415);
            var viewModel = new List<UsersViewModel>();

            if(rank.Count == 0)
                return NotFound("Nenhum usuário dp servidor participa do rank");

            foreach (var item in rank)
            {
                viewModel.Add(
                    new UsersViewModel()
                    {
                        UserName = item.UserName,
                        ThumbUrl = item.ThumbUrl,
                        StatsCount = (int)item.RankPoints
                    });
            }


            return Ok(viewModel);
        }

        /*[HttpPost]
        public async Task<IActionResult> PostConfigs(ConfigBotInputModel configBotInput)
        {

            _rankService.ConfigureRank(configBotInput.BlockIds, configBotInput.RankingType, 1038639386646032415);

            return Ok("Aparentemente deu tudo certo '-'");
        }*/

    }
}
using Domain;
using System.ComponentModel.DataAnnotations;

namespace Bot.API.ViewModel
{
    public class ConfigBotViewModel
    {

        [MaxLength(3, ErrorMessage ="Selecione entre 1 e 3")]
        [MinLength(1, ErrorMessage = "Selecione entre 1 e 3")]
        public RankingTypes RankingType { get; set; }


        public List<ulong> BlockIds { get; set; }
    }
}
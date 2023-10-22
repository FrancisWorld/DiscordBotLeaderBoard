using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class Guild : TEntity
    {
#pragma warning disable

        public Guild(ulong id, string name, ulong ownerId)
        {
            this.Id = id;
            this.Name = name;
            this.OwnerId = ownerId;

            //Default
            this.RankingTypes = RankingTypes.ByKills;
        }

        [NotNull]
        public string Name {get; set;}

        [NotNull]
        public ulong OwnerId { get; set;}

        [NotNull]
        public RankingTypes RankingTypes { get; set;}
    }
}
using System.Diagnostics.CodeAnalysis;

namespace Domain.Models
{
    public class User : TEntity
    {
        #pragma warning disable

        public User(ulong id, 
            string gameNickName,
            string discordNickName,
            string thumbUrl,
            bool isRanked)
        {
            this.Id = id;
            this.GameNickName = gameNickName;
            this.DiscordNickName = discordNickName;
            this.ThumbUrl = thumbUrl;
            this.IsRanked = isRanked;
        }

        [NotNull]
        public string GameNickName { get; set; }

        [NotNull]
        public string DiscordNickName { get; set; }

        [NotNull]
        public string ThumbUrl { get; set; }

        [NotNull]
        public bool IsRanked { get; set; }

        [AllowNull]
        public int RankPoints { get; set; }

        [NotNull]
        public ulong GuildId { get; set; }

        public Guild Guild { get; set; }

    }
}
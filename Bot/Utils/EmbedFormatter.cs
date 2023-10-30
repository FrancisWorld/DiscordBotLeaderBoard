using DSharpPlus.Entities;

namespace Bot.Utils
{
    public static class EmbedFormatter
    {
        public static DiscordEmbed SimpleEmbedFormatter(List<string> list)
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.SpringGreen,
                Description = list[0],
                Title = list[1],
            };

            return embed;
        }

        public static DiscordEmbed SimpleEmbedFormatter(string title, string description)
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.SpringGreen,
                Description = description,
                Title = title
            };

            return embed;
        }

        public static DiscordEmbed CustomEmbedFormatter(string title, string description, List<string> fields)
        {

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.SpringGreen,
                Description = description,
                Title = title,
            };

            for(var i =0; i < fields.Count; i++)
            {
                var nome =  fields[i].Split('|');

                embed.AddField(nome[0], nome[1], false);
            }

            return embed;
        }
    }
}
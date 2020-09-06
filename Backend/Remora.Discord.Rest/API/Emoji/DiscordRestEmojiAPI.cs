//
//  DiscordRestEmojiAPI.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.API.Abstractions.Results;
using Remora.Discord.Core;
using Remora.Discord.Rest.Extensions;
using Remora.Discord.Rest.Results;

namespace Remora.Discord.Rest.API
{
    /// <inheritdoc />
    public class DiscordRestEmojiAPI : IDiscordRestEmojiAPI
    {
        private readonly DiscordHttpClient _discordHttpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordRestEmojiAPI"/> class.
        /// </summary>
        /// <param name="discordHttpClient">The Discord HTTP client.</param>
        /// <param name="jsonOptions">The JSON options.</param>
        public DiscordRestEmojiAPI(DiscordHttpClient discordHttpClient, IOptions<JsonSerializerOptions> jsonOptions)
        {
            _discordHttpClient = discordHttpClient;
            _jsonOptions = jsonOptions.Value;
        }

        /// <inheritdoc />
        public Task<IRetrieveRestEntityResult<IReadOnlyList<IEmoji>>> ListGuildEmojisAsync
        (
            Snowflake guildID,
            CancellationToken ct = default
        )
        {
            return _discordHttpClient.GetAsync<IReadOnlyList<IEmoji>>
            (
                $"guilds/{guildID}/emojis",
                ct: ct
            );
        }

        /// <inheritdoc />
        public Task<IRetrieveRestEntityResult<IEmoji>> GetGuildEmojiAsync
        (
            Snowflake guildID,
            Snowflake emojiID,
            CancellationToken ct = default
        )
        {
            return _discordHttpClient.GetAsync<IEmoji>
            (
                $"guilds/{guildID}/{emojiID}",
                ct: ct
            );
        }

        /// <inheritdoc />
        public async Task<ICreateRestEntityResult<IEmoji>> CreateGuildEmojiAsync
        (
            Snowflake guildID,
            string name,
            Stream image,
            IReadOnlyList<Snowflake> roles,
            CancellationToken ct = default
        )
        {
            await using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream, ct);

            var imageData = memoryStream.ToArray();

            if (imageData.Length > 256000)
            {
                return CreateRestEntityResult<IEmoji>.FromError("Image too large.");
            }

            string? mediaType = null;
            if (imageData.IsPNG())
            {
                mediaType = "png";
            }
            else if (imageData.IsJPG())
            {
                mediaType = "jpeg";
            }
            else if (imageData.IsGIF())
            {
                mediaType = "gif";
            }

            if (mediaType is null)
            {
                return CreateRestEntityResult<IEmoji>.FromError("Unknown or unsupported image format.");
            }

            return await _discordHttpClient.PostAsync<IEmoji>
            (
                $"guilds/{guildID}/emojis",
                b => b.AddJsonConfigurator
                (
                    json =>
                    {
                        json.WriteString("name", name);

                        var dataString = $"data:image/{mediaType};base64,{Convert.ToBase64String(imageData)}";
                        json.WriteString("image", dataString);

                        json.WritePropertyName("roles");
                        JsonSerializer.Serialize(json, roles, _jsonOptions);
                    }
                ),
                ct: ct
            );
        }

        /// <inheritdoc />
        public Task<IModifyRestEntityResult<IEmoji>> ModifyGuildEmojiAsync
        (
            Snowflake guildID,
            Snowflake emojiID,
            Optional<string> name = default,
            Optional<IReadOnlyList<Snowflake>?> roles = default,
            CancellationToken ct = default
        )
        {
            return _discordHttpClient.PatchAsync<IEmoji>
            (
                $"guilds/{guildID}/emojis/{emojiID}",
                b => b.AddJsonConfigurator
                (
                    json =>
                    {
                        if (name.HasValue)
                        {
                            json.WriteString("name", name.Value);
                        }

                        if (roles.HasValue)
                        {
                            if (roles.Value is null)
                            {
                                json.WriteNull("roles");
                            }
                            else
                            {
                                json.WritePropertyName("roles");
                                JsonSerializer.Serialize(json, roles.Value, _jsonOptions);
                            }
                        }
                    }
                ),
                ct
            );
        }

        /// <inheritdoc />
        public Task<IDeleteRestEntityResult> DeleteGuildEmojiAsync
        (
            Snowflake guildID,
            Snowflake emojiID,
            CancellationToken ct = default
        )
        {
            return _discordHttpClient.DeleteAsync
            (
                $"guilds/{guildID}/emojis/{emojiID}",
                ct: ct
            );
        }
    }
}
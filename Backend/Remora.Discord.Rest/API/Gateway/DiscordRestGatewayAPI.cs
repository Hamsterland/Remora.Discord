//
//  DiscordRestGatewayAPI.cs
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

using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Caching.Abstractions.Services;
using Remora.Discord.Rest.Extensions;
using Remora.Rest;
using Remora.Results;

namespace Remora.Discord.Rest.API;

/// <inheritdoc cref="Remora.Discord.API.Abstractions.Rest.IDiscordRestGatewayAPI" />
[PublicAPI]
public class DiscordRestGatewayAPI : AbstractDiscordRestAPI, IDiscordRestGatewayAPI
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscordRestGatewayAPI"/> class.
    /// </summary>
    /// <param name="restHttpClient">The Discord HTTP client.</param>
    /// <param name="jsonOptions">The JSON options.</param>
    /// <param name="rateLimitCache">The memory cache used for rate limits.</param>
    public DiscordRestGatewayAPI(IRestHttpClient restHttpClient, JsonSerializerOptions jsonOptions, ICacheProvider rateLimitCache)
        : base(restHttpClient, jsonOptions, rateLimitCache)
    {
    }

    /// <inheritdoc />
    public virtual Task<Result<IGatewayEndpoint>> GetGatewayAsync(CancellationToken ct = default)
    {
        return this.RestHttpClient.GetAsync<IGatewayEndpoint>
        (
            "gateway",
            b => b.WithRateLimitContext(this.RateLimitCache),
            ct: ct
        );
    }

    /// <inheritdoc />
    public virtual Task<Result<IGatewayEndpoint>> GetGatewayBotAsync(CancellationToken ct = default)
    {
        return this.RestHttpClient.GetAsync<IGatewayEndpoint>
        (
            "gateway/bot",
            b => b.WithRateLimitContext(this.RateLimitCache),
            ct: ct
        );
    }
}

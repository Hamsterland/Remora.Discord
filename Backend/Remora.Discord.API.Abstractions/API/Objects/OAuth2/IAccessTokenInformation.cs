//
//  IAccessTokenInformation.cs
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

namespace Remora.Discord.API.Abstractions.Objects;

/// <summary>
/// Represents access and refresh token information after a successful OAuth2 code exchange.
/// </summary>
public interface IAccessTokenInformation
{
    /// <summary>
    /// Gets the OAuth2 access token.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the type of the OAuth2 access token.
    /// </summary>
    string TokenType { get; }

    /// <summary>
    /// Gets the duration of the access token validity.
    /// </summary>
    TimeSpan ExpiresIn { get; }

    /// <summary>
    /// Gets the OAuth2 refresh token.
    /// </summary>
    string RefreshToken { get; }

    /// <summary>
    /// Gets the scopes associated with this OAuth 2 access token.
    /// </summary>
    string Scope { get; }
}

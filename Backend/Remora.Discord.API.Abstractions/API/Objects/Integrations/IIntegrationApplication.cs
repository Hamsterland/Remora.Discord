//
//  IIntegrationApplication.cs
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

using JetBrains.Annotations;
using Remora.Rest.Core;

namespace Remora.Discord.API.Abstractions.Objects;

/// <summary>
/// Represents application information for a Discord integration.
/// </summary>
[PublicAPI]
public interface IIntegrationApplication
{
    /// <summary>
    /// Gets the ID of the application.
    /// </summary>
    Snowflake ID { get; }

    /// <summary>
    /// Gets the name of the application.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the application's icon.
    /// </summary>
    IImageHash? Icon { get; }

    /// <summary>
    /// Gets the description of the application.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the bot associated with this application.
    /// </summary>
    Optional<IUser> Bot { get; }
}

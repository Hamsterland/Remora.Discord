//
//  ExplicitContentFilterLevel.cs
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

namespace Remora.Discord.API.Abstractions.Objects;

/// <summary>
/// Enumerates various content filter levels.
/// </summary>
[PublicAPI]
public enum ExplicitContentFilterLevel
{
    /// <summary>
    /// No filtering is applied.
    /// </summary>
    Disabled = 0,

    /// <summary>
    /// Filtering is applied to members without roles.
    /// </summary>
    MembersWithoutRoles = 1,

    /// <summary>
    /// Filtering is applied to all members.
    /// </summary>
    AllMembers = 2
}

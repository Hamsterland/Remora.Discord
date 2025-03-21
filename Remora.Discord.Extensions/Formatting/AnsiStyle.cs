﻿//
//  AnsiStyle.cs
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

namespace Remora.Discord.Extensions.Formatting;

/// <summary>
/// Discord supported ANSI styles.
/// </summary>
internal static class AnsiStyle
{
    /// <summary>
    /// Resets all applied styling.
    /// </summary>
    public const int Reset = 0;

    /// <summary>
    /// Makes the text bold.
    /// </summary>
    public const int Bold = 1;

    /// <summary>
    /// Makes the text underlined.
    /// </summary>
    public const int Underline = 4;
}

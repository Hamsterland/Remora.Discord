//
// ConnectEvent.cs
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

namespace Remora.Discord.Gateway.Tests.Transport.Events;

/// <summary>
/// Represents an expected connection event.
/// </summary>
public class ConnectEvent : IEvent
{
    private readonly Func<Uri, EventMatch> _matcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectEvent"/> class.
    /// </summary>
    /// <param name="matcher">The matching function.</param>
    public ConnectEvent(Func<Uri, EventMatch> matcher)
    {
        _matcher = matcher;
    }

    /// <summary>
    /// Determines whether this event matches the given arguments.
    /// </summary>
    /// <param name="uri">The URI to test against.</param>
    /// <returns>The match status.</returns>
    public EventMatch Matches(Uri uri) => _matcher(uri);
}

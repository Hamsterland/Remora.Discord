//
//  ReceiveEvent.cs
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
using Remora.Discord.API.Abstractions.Gateway;

namespace Remora.Discord.Gateway.Tests.Transport.Events;

/// <summary>
/// Represents an expected payload reception event.
/// </summary>
public class ReceiveEvent : IEvent
{
    private readonly Func<IPayload, bool, EventMatch> _matcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReceiveEvent"/> class.
    /// </summary>
    /// <param name="matcher">The matching function.</param>
    public ReceiveEvent(Func<IPayload, bool, EventMatch> matcher)
    {
        _matcher = matcher;
    }

    /// <summary>
    /// Determines whether this event matches the given arguments.
    /// </summary>
    /// <param name="payload">The payload to test against.</param>
    /// <param name="ignoreUnexpected">Whether to ignore payloads of the wrong type.</param>
    /// <returns>The match status.</returns>
    public EventMatch Matches(IPayload payload, bool ignoreUnexpected) => _matcher(payload, ignoreUnexpected);
}

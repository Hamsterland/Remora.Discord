//
//  GroupWithContextMenus.cs
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
using System.Threading.Tasks;
using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Commands.Attributes;
using Remora.Results;

#pragma warning disable CS1591, SA1600, SA1402, SA1602

namespace Remora.Discord.Commands.Tests.Data.Valid;

public class GroupWithContextMenus : CommandGroup
{
    [Command("Do user thing")]
    [CommandType(ApplicationCommandType.User)]
    public Task<IResult> UserCommand() => throw new NotImplementedException();

    [Command("Do message thing")]
    [CommandType(ApplicationCommandType.Message)]
    public Task<IResult> MessageCommand() => throw new NotImplementedException();
}

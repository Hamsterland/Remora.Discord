//
//  APITypeTestBase.cs
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

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Remora.Discord.API.Abstractions;
using Remora.Discord.API.Extensions;
using Remora.Discord.API.Tests.Services;
using Remora.Discord.Tests;
using Remora.Results;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Remora.Discord.API.Tests.TestBases
{
    /// <summary>
    /// Acts as a base class for testing API types in the Discord API. This class contains common baseline tests for all
    /// types.
    /// </summary>
    /// <typeparam name="TSampleSource">The theory data source.</typeparam>
    public abstract class APITypeTestBase<TSampleSource> where TSampleSource : TheoryData, new()
    {
        /// <summary>
        /// Gets the data sample source.
        /// </summary>
        public static TheoryData SampleSource => new TSampleSource();

        /// <summary>
        /// Gets the sample data service.
        /// </summary>
        protected SampleDataService SampleData { get; }

        /// <summary>
        /// Gets the configured JSON serializer options.
        /// </summary>
        protected JsonSerializerOptions Options { get; }

        /// <summary>
        /// Gets the assertion options.
        /// </summary>
        protected virtual JsonAssertOptions AssertOptions { get; } = JsonAssertOptions.Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="APITypeTestBase{TSampleSource}"/> class.
        /// </summary>
        protected APITypeTestBase()
        {
            var services = new ServiceCollection()
                .AddDiscordApi(allowUnknownEvents: false)
                .AddSingleton<SampleDataService>()
                .BuildServiceProvider();

            this.SampleData = services.GetRequiredService<SampleDataService>();
            this.Options = services.GetRequiredService<IOptions<JsonSerializerOptions>>().Value;
        }

        /// <summary>
        /// Tests whether the type can be deserialized from a JSON object.
        /// </summary>
        /// <param name="sampleDataPath">The sample data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [SkippableTheory]
        [MemberData(nameof(SampleSource))]
        public async Task CanDeserialize(string sampleDataPath)
        {
            await using var sampleData = File.OpenRead(sampleDataPath);
            var payload = await JsonSerializer.DeserializeAsync<IPayload>(sampleData, this.Options);
            Assert.NotNull(payload);
        }

        /// <summary>
        /// Tests whether the type can be serialized to a JSON object.
        /// </summary>
        /// <param name="sampleDataPath">The sample data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [SkippableTheory]
        [MemberData(nameof(SampleSource))]
        public async Task CanSerialize(string sampleDataPath)
        {
            await using var sampleData = File.OpenRead(sampleDataPath);
            var payload = await JsonSerializer.DeserializeAsync<IPayload>(sampleData, this.Options);

            await using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, payload, this.Options);

            Assert.NotNull(payload);
        }

        /// <summary>
        /// Tests whether data survives being round-tripped by the type - that is, it can be deserialized and then
        /// serialized again without data loss.
        /// </summary>
        /// <param name="sampleDataPath">The sample data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [SkippableTheory]
        [MemberData(nameof(SampleSource))]
        public async Task SurvivesRoundTrip(string sampleDataPath)
        {
            await using var sampleData = File.OpenRead(sampleDataPath);
            var deserialized = await JsonSerializer.DeserializeAsync<IPayload>(sampleData, this.Options);

            await using var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, deserialized, this.Options);

            await stream.FlushAsync();

            stream.Seek(0, SeekOrigin.Begin);
            sampleData.Seek(0, SeekOrigin.Begin);

            var serialized = await JsonDocument.ParseAsync(stream);
            var original = await JsonDocument.ParseAsync(sampleData);

            JsonAssert.Equivalent(original, serialized, this.AssertOptions);
        }
    }
}
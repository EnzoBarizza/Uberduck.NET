using Newtonsoft.Json;

namespace Uberduck.NET.Models
{
    /// <summary>
    /// the class that represents the final Uberduck.NET data model
    /// </summary>
    public sealed class UberduckFinalResult
    {
        /// <summary>
        /// When the audio started generating
        /// </summary>
        [JsonProperty("started_at")]
        public string StartedAt { get; set; } = string.Empty;
        /// <summary>
        /// If there was a failure in generating the audio
        /// </summary>
        [JsonProperty("failed_at")]
        public string? FailedAt { get; set; } = null;
        /// <summary>
        /// When the audio finished generating
        /// </summary>
        [JsonProperty("finished_at")]
        public string? FinishedAt { get; set; } = string.Empty;

        /// <summary>
        /// The link of the audio
        /// </summary>
        [JsonProperty("path")]
        public string? Path { get; set; } = string.Empty;

    }
}

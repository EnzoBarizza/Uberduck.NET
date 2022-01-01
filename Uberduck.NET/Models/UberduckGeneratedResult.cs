using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Uberduck.NET.Keys;

#pragma warning disable CS8603

/// <summary>
/// the class that represents the data model generated from the Uberduck API to Uberduck.NET
/// </summary>
namespace Uberduck.NET.Models
{
    public sealed class UberduckGeneratedResult
    {
        private HttpClient _httpClient { get; set; }
        public UberduckKeys Keys { get; set; }

        [JsonProperty("uuid")]
        public string UUID { get; set; } = string.Empty;

        internal UberduckGeneratedResult(UberduckKeys keys)
        {
            _httpClient = new HttpClient();
            Keys = keys;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic", Convert.ToBase64String(
            ASCIIEncoding.ASCII.GetBytes($"{Keys.PublicKey}:{Keys.SecretKey}")));
        }

        /// <summary>
        /// Get the audio that was generated from the API
        /// </summary>
        /// <param name="untilFinal">When true, it will only return when the audio finishes generating</param>
        /// <returns>The link of the audio</returns>
        /// <exception cref="Exception">An exception that is thrown when the attempt to deserialize the request is null</exception>
        public async Task<string> GetAudioAsync(bool untilFinal = false)
        {
            do
            {
                var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

                string resultContent = await request.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<UberduckFinalResult>(resultContent);

                if (json == null) throw new Exception("Result was null, contact me on Discord Neuki#1325 or open a Issue on Github");

                if (untilFinal)
                {
                    if (json.FinishedAt == null)
                    {
                        await Task.Delay(5000);
                    }
                    else
                    {
                        untilFinal = false;
                        return json.Path;
                    }
                }
                else
                {
                    return json.Path;
                }
            } while (untilFinal);
            return string.Empty;
        }

        /// <summary>
        ///  Get o raw response of the api
        /// </summary>
        /// <returns>Raw response of the api</returns>
        public async Task<string> GetRawAudioData()
        {
            var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

            string resultContent = await request.Content.ReadAsStringAsync();

            return resultContent;
        }

        /// <summary>
        /// Get the deserialized request that was responded from the API
        /// </summary>
        /// <param name="untilFinal">When true, it will only return when the audio finishes generating</param>
        /// <returns>The deserialized request</returns>
        /// <exception cref="Exception">An exception that is thrown when the attempt to deserialize the request is null</exception>
        public async Task<UberduckFinalResult> GetDeserializedAudioData(bool untilFinal = false)
        {
            do
            {
                var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

                string resultContent = await request.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<UberduckFinalResult>(resultContent);

                if (json == null) throw new Exception("Result was null, contact me on Discord Neuki#1325 or open a Issue on Github");

                if (untilFinal)
                {
                    if (json.FinishedAt == null)
                    {
                        await Task.Delay(5000);
                    }
                    else
                    {
                        untilFinal = false;
                        return json;
                    }
                }
                else
                {
                    return json;
                }
            } while (untilFinal);
            return new UberduckFinalResult();

        }
    }
}

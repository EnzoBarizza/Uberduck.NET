using Newtonsoft.Json;
using System.Net.Http.Headers;
using Uberduck.NET.Keys;
using System.Text;
using Uberduck.NET.Models;
using Uberduck.NET.Exceptions;

namespace Uberduck.NET
{
    /// <summary>
    /// Main class
    /// </summary>
    public class UberduckClient
    {
        private HttpClient _httpClient { get; set; }
        /// <summary>
        /// The object with your Uberduck API Keys
        /// </summary>
        public UberduckKeys Keys { get; private set; }

        /// <summary>
        /// Instantiate a new Uberduck Client
        /// </summary>
        /// <param name="keys">The keys of Uberduck API</param>
        public UberduckClient(UberduckKeys keys)
        {
            Keys = keys;
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                ASCIIEncoding.ASCII.GetBytes($"{Keys.PublicKey}:{Keys.SecretKey}")));
        }

        /// <summary>
        /// Make a request for Uberduck API to generate the audio
        /// </summary>
        /// <param name="text">The text to speak</param>
        /// <param name="voice">Which voice are you going to use (found the voices on the offical website)</param>
        /// <returns>A UberduckGeneratedResult Object</returns>
        /// <exception cref="UberduckUnauthorizedException">Throws when Uberduck Credentials are incorrect</exception>
        /// <exception cref="UberduckBadRequestException">Throws when the voice are incorrect</exception>
        /// <exception cref="HttpRequestException">Throws when occurs a unknow error</exception>
        public async Task<UberduckGeneratedResult> GenerateVoiceAsync(string text, string voice)
        {
            var content = new StringContent($"{{\"speech\":\"{text}\",\"voice\":\"{voice}\"}}", Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync("https://api.uberduck.ai/speak", content);
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.StatusCode.ToString().ToLower() == "unauthorized") throw new UberduckUnauthorizedException("Incorrect Credentials",
                new HttpRequestException($"Raw Content: {resultContent}"));
            if (result.StatusCode.ToString().ToLower() == "badrequest") throw new UberduckBadRequestException("The provided voice doesn't exist",
                new HttpRequestException($"Raw Content: {resultContent}"));
            if (result.StatusCode.ToString().ToLower() != "ok") throw new HttpRequestException($"Unkown error, API Error Message {resultContent}");

            var json = new UberduckGeneratedResult(Keys);

            JsonConvert.PopulateObject(resultContent, json);

            json.Keys = Keys;

            return await Task.FromResult(json);
        }
    }
}

using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Uberduck.NET.Keys;

namespace Uberduck.NET.Models
{
    /// <summary>
    /// the class that represents the data model generated from the Uberduck API to Uberduck.NET
    /// </summary>
    public sealed class UberduckGeneratedResult
    {
        private HttpClient _httpClient { get; set; }
        /// <summary>
        /// The object with your Uberduck API Keys
        /// </summary>
        public UberduckKeys Keys { get; set; }

        /// <summary>
        /// The UUID from Uberduck Request
        /// </summary>
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
        public async Task<string> GetAudioLinkAsync(bool untilFinal = false)
        {
            do
            {
                var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

                string resultContent = await request.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<UberduckFinalResult>(resultContent);

                if (json == null) throw new Exception("Result was null, contact me on Discord Neuki#1325 or open a Issue on Github");

                if (untilFinal)
                {
                    if (json.FinishedAt == null || json.Path == null)
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
                    return json.Path!;
                }
            } while (untilFinal);
            return string.Empty;
        }

        /// <summary>
        /// Save the audio file to the disk
        /// </summary>
        /// <param name="fileName">The file name to save</param>
        /// <param name="path">The path to the file</param>
        /// <returns></returns>
        /// <exception cref="Exception">An exception that is thrown when the attempt to deserialize the request is null</exception>
        public async Task SaveAudioFileAsync(string fileName = "audio", string path = "./")
        {
            bool untilFinal = true;
            do
            {
                var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

                var resultContent = await request.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<UberduckFinalResult>(resultContent);

                if (json == null) throw new Exception("Result was null, contact me on Discord Neuki#1325 or open a Issue on Github");

                if (json.Path != null)
                {
                    #pragma warning disable SYSLIB0014
                    HttpWebRequest? request2 = WebRequest.CreateHttp(json.Path) as HttpWebRequest;
                    HttpWebResponse? response2 = request2!.GetResponse() as HttpWebResponse;
                    Stream receiveStream = response2!.GetResponseStream();
                    #pragma warning restore SYSLIB0014

                    using(var fs = new FileStream($"{path}{fileName}.wav", FileMode.Create, FileAccess.Write))
                    {
                        receiveStream.CopyTo(fs);

                        fs.Dispose();
                        untilFinal = false;
                    }

                }
            } while (untilFinal);
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

        /// <summary>
        /// Loads the Audio file into a Stream
        /// </summary>
        /// <returns>Returns a Stream with the audio file loaded in</returns>
        /// <exception cref="Exception">An exception that is thrown when the attempt to deserialize the request is null</exception>
        public async Task<Stream> GetAudioStreamAsync()
        {
            bool untilFinal = true;
            do
            {
                var request = await _httpClient.GetAsync($"https://api.uberduck.ai/speak-status?uuid={UUID}");

                var resultContent = await request.Content.ReadAsStringAsync();

                var json = JsonConvert.DeserializeObject<UberduckFinalResult>(resultContent);

                if (json == null) throw new Exception("Result was null, contact me on Discord Neuki#1325 or open a Issue on Github");

                if (json.Path != null)
                {
                    #pragma warning disable SYSLIB0014
                    HttpWebRequest? request2 = WebRequest.CreateHttp(json.Path) as HttpWebRequest;
                    HttpWebResponse? response2 = request2!.GetResponse() as HttpWebResponse;
                    Stream receiveStream = response2!.GetResponseStream();
                    #pragma warning restore SYSLIB0014

                    return receiveStream;
                    
                }
            } while (untilFinal);
            return null!;
        }
    }
}

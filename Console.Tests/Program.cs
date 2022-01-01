// See https://aka.ms/new-console-template for more information
using Uberduck.NET;
using Uberduck.NET.Models;
using Uberduck.NET.Keys;

UberduckClient client = new UberduckClient(
    new UberduckKeys(publicKey:"YOUR_API_PUBLIC_KEY", secretKey:"YOUR_API_SECRET_KEY"));

UberduckGeneratedResult generatedResult = await client.GenerateVoiceAsync("This is just a test", "eminem");

var audio = await generatedResult.GetAudioAsync(untilFinal:true);

Console.WriteLine(audio);
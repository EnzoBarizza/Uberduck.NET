// See https://aka.ms/new-console-template for more information
using Uberduck.NET;
using Uberduck.NET.Models;
using Uberduck.NET.Keys;

UberduckClient client = new UberduckClient(
    new UberduckKeys(publicKey:"YOUR_PUBLIC_API_KEY", secretKey:"YOUR_SECRET_API_KEY"));

UberduckGeneratedResult generatedResult = await client.GenerateVoiceAsync("Hello World from C Sharp", "eminem");

await generatedResult.SaveAudioFileAsync();
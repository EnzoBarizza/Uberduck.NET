# Uberduck.NET
An unofficial .Net wrapper for the Uberduck API

# Install
Just go to [NuGet](https://www.nuget.org/packages/Uberduck.NET/) and install in your project

# Documentation

## UberduckClient
### `Uberduck.NET`
### Properties
```c#
public UberduckKeys Keys { get; private set };
```

### Constructors
```c#
public UberduckClient(UberduckKeys keys) { }
```

```c#
UberduckKeys keys; //The keys of Uberduck API
```

### Methods
```c#
public async Task<UberduckGeneratedResult> GenerateVoiceAsync(string text, string voice) { }
```

## UberduckKeys
### `Uberduck.NET.Keys`
### Properties
```c# 
public string PublicKey { get; private set; }
```

### Constructors
```c#
public UberduckKeys(string publicKey, string secretKey) { }
```

```c#
string publicKey; // Your API Public Key
string secretKey; // Your API Secret Key
```

## UberduckGeneratedResult
### `Uberduck.NET.Models`
### Properties
```c#
public UberduckKeys Keys { get; set; }
public string UUID { get; set; };
```

### Methods
```c#
public async Task<string> GetAudioAsync(bool untilFinal = false) { }
public async Task<string> GetRawAudioData() { }
public async Task<UberduckFinalResult> GetDeserializedAudioData(bool untilFinal = false) { }
```

## UberduckFinalResult
### `Uberduck.NET.Models`
### Properties
```c#
public string StartedAt { get; set; }
public string? FailedAt { get; set; }
public string? FinishedAt { get; set; }
public string? Path { get; set; }
```

## Exceptions
### `Uberduck.NET.Exceptions`
```c#
public class UberduckBadRequestException : Exception { }
public class UberduckUnauthorizedException : Exception { }
```

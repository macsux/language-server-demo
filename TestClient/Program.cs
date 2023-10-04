using System.Diagnostics;
using Newtonsoft.Json.Linq;
using OmniSharp.Extensions.LanguageServer.Client;

var start = new ProcessStartInfo(@"../../../../DemoLanguageServer/bin/Debug/net6.0/DemoLanguageServer.exe")
{
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    UseShellExecute = false
};
using Process process = Process.Start(start);
    
var opts = new LanguageClientOptions();
opts
    .WithInput(process.StandardOutput.BaseStream)
    .WithOutput(process.StandardInput.BaseStream)
        
    .OnNotification("window/showMessage", (MessageRequest notification) =>
    {
        Console.WriteLine($"Notification: {notification.Message}");
        
    })
    .OnNotification("window/showMessageRequest", (MessageRequest notification) =>
    {
        Console.WriteLine($"Notification: {notification.Message}");
    });
//GC.Collect();
var languageClient = OmniSharp.Extensions.LanguageServer.Client.LanguageClient.Create(opts);
await languageClient.Initialize(CancellationToken.None);
while (!Console.KeyAvailable)
{
    await Task.Delay(1000);
}

process.Kill();


public class MessageRequest
{
    public int Type { get; set; }
    public string Message { get; set; }
}
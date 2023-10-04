using System;
using System.Threading.Tasks;
using JsonRpc;
using JsonRpc.Contracts;
using JsonRpc.Messages;
using JsonRpc.Server;
using LanguageServer.VsCode.Contracts;
using Newtonsoft.Json.Linq;

namespace DemoLanguageServer.Services
{
    public class InitializationService : DemoLanguageServiceBase
    {

        [JsonRpcMethod(AllowExtensionData = true)]
        public InitializeResult Initialize(JToken processId, JToken clientInfo,  JToken rootUri, JToken capabilities,
            JToken initializationOptions = null, string trace = null)
        {
            return new InitializeResult(new ServerCapabilities
            {
                HoverProvider = new HoverOptions(),
                SignatureHelpProvider = new SignatureHelpOptions("()".ToCharArray()),
                CompletionProvider = new CompletionOptions(true, new[]{'.'}),
                TextDocumentSync = new TextDocumentSyncOptions
                {
                    OpenClose = true,
                    WillSave = true,
                    Change = TextDocumentSyncKind.Incremental
                },
            });
        }

        [JsonRpcMethod(IsNotification = true)]
        public async Task Initialized()
        {
            for (int i = 0; i < 10000000; i++)
            {
                // await Client.Window.ShowMessage(MessageType.Info,
                //     $"Hello from language server. Params: {Environment.CommandLine}");
                // var choice = await Client.Window.ShowMessage(MessageType.Warning, "Wanna drink?", "Yes", "No");
                // await Client.Window.ShowMessage(MessageType.Info, $"You chose {(string)choice ?? "Nothing"}.");
                await Client.Window.ShowMessage(MessageType.Info, $"It's {i}");
                await Task.Delay(10);
            }
        }

        [JsonRpcMethod]
        public void Shutdown()
        {

        }

        [JsonRpcMethod(IsNotification = true)]
        public void Exit()
        {
            Session.StopServer();
        }

        [JsonRpcMethod("$/cancelRequest", IsNotification = true)]
        public void CancelRequest(MessageId id)
        {
            RequestContext.Features.Get<IRequestCancellationFeature>().TryCancel(id);
        }
    }
}

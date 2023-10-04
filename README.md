# LanguageServer Example

- Ensure you have .NET SDK 7.0.401
- Clone with submodules `git clone --recurse-submodules https://github.com/macsux/language-server-demo.git`
- Build solution
- Launch TestClient

Memory leak suspected due to closures capture inside anonymous functions inside `csharp-language-server-protocol/src/JsonRpc/ProcessScheduler.cs`
using ChatGptApp.Shared;
using System.Net.Http.Json;

namespace ChatGptApp.Client.Services;

public interface IChatGptService
{
    Task<ServiceResponse<string>> AskGPT(ChatGptRequest request);
}

public class ChatGptService : IChatGptService
{

    private readonly HttpClient _http;

    public ChatGptService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<string>> AskGPT(ChatGptRequest request)
    {
        var result = await _http.PostAsJsonAsync("api/openai/getanswer", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
    }


}

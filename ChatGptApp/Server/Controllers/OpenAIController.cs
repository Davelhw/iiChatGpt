using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API.Completions;
using OpenAI_API;
using OpenAI_API.Models;
using ChatGptApp.Shared;

namespace ChatGptApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAIController : ControllerBase
    {
        public readonly ILogger<OpenAIController> _logger;
        public readonly IConfiguration _configuration;

        public OpenAIController(ILogger<OpenAIController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("getanswer")]
        public async Task<ActionResult<ServiceResponse<string>>> GetResult([FromBody] ChatGptRequest request)
        {
            //your OpenAI API key
            string apiKey = "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";
            string answer = string.Empty;
            ServiceResponse<string> response = new();
            var openAi = new OpenAIAPI(apiKey);

            try
            {
                _logger.LogInformation($"posted question: {request.Question}");
                CompletionRequest completion = new()
                {
                    Prompt = request.Question,
                    Model = Model.DavinciText,
                    MaxTokens = 4000
                };

                var result = openAi.Completions.CreateCompletionAsync(completion);
                if (result != null)
                {
                    foreach (var item in result.Result.Completions)
                    {
                        answer = item.Text;
                    }
                    _logger.LogInformation($"ChatGPT replied: {answer}");

                    response.Success = true;
                    response.Message = "ChatGPT answer";
                    response.Data = answer;
                    return Ok(response);
                }
                else
                {
                    _logger.LogInformation($"ChatGPT no answer found!");
                    response.Success = false;
                    response.Message = "No answer from ChatGPT!";
                    response.Data = "No answer from ChatGPT!";
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"ChatGPT Exception Error: {ex.Message}");
                string _error = ex.Message[..ex.Message.IndexOf(".")];
                response.Success = false;
                response.Message = $"ChatGPT error: {_error}";
                response.Data = $"ChatGPT error: {_error}";
                return BadRequest(response);
            }

        }
    }
}

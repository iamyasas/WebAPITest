using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.Text.Json;
using WebAPITest1.Models;
using WebAPITest1.Utils;

namespace WebAPITest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FillMissingFieldsController : ControllerBase
    {
        [HttpGet]
        public IActionResult FillMissingFields()
        {

            // Initialize OpenAI API with your API key
            ChatClient client = new ChatClient("gpt-4o-mini", "REPLACE_WITH_YOUR_KEY");

            // Prompt based on essential fields only
            string prompt = "Based on the following information:\n" +
                            "meetingPoints: \"Vatnshellir Cave, Vatnshellir Cave, Snaefellsbaer, IS360, IS\"\n" +
                            "description: \"Follow the path of the lava flow 200 meters into the cave and 35 meters below the surface. Vatnshellir cave is one of the most easily accessible lava tubes in Iceland.\"\n" +
                            "name: \"Vatnshellir Cave Tour\"\n" +
                            "provider: \"Summit Adventure Guides\"\n" +
                            // "latitude: \"64.7478897\"\n" +
                            // "longitude: \"-23.8182513\"\n" +
                            //"Please return the output in the following JSON format:\n" +
                            //"{ \"countryName\": \"AAA\", \"countryCode\": \"IS\", \"city\": \"Snaefellsnes Peninsula\", \"location\": \"Vatnshellir Cave, Snaefellsnes Peninsula, Iceland\", \"property\": \"Vatnshellir Cave Tour Facility\", \"latitude\": 64.7478897, \"longitude\": -23.8182513 }";
                            "Please return the output in the provided JSON schema";

            List<ChatMessage> messages = [
                new UserChatMessage(prompt)
            ];

            ChatCompletionOptions options = new ChatCompletionOptions()
            {
                Temperature = 0.1f,
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "LocationInfo",
                    jsonSchema: BinaryData.FromString(JsonSchemeGenerator.GenerateJsonSchema(typeof(LocationInfo))),
                    jsonSchemaIsStrict: true
                )
            };


            // Retrieve the completion
            ChatCompletion completion = client.CompleteChat(messages, options);

            // Parse result to JSON format
            string response = completion.Content[0].Text;

            // Deserialize into a LocationInfo object
            LocationInfo? locationInfo = JsonSerializer.Deserialize<LocationInfo>(response);

            // Output results
            return Ok(locationInfo);
        }

        [HttpPost]
        public IActionResult FillMissingFieldsKV([FromForm] Dictionary<string, string> context)
        {

            var prompt = "";

            foreach (var kv in context)
            {
                prompt += kv.Key + ": " + kv.Value + ", ";
            }

            //Console.WriteLine("===Prompt: " + prompt + "===");

            // Initialize OpenAI API with your API key
            ChatClient client = new ChatClient("gpt-4o-mini", "REPLACE_WITH_YOUR_KEY");

            List<ChatMessage> messages = [
                new UserChatMessage(prompt)
            ];

            ChatCompletionOptions options = new ChatCompletionOptions()
            {
                Temperature = 0.1f,
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    jsonSchemaFormatName: "LocationInfo",
                    jsonSchema: BinaryData.FromString(JsonSchemeGenerator.GenerateJsonSchema(typeof(LocationInfo))),
                    jsonSchemaIsStrict: true
                )
            };

            // Retrieve the completion
            ChatCompletion completion = client.CompleteChat(messages, options);

            // Parse result to JSON format
            string response = completion.Content[0].Text;

            // Deserialize into a LocationInfo object
            LocationInfo? locationInfo = JsonSerializer.Deserialize<LocationInfo>(response);

            // Output results
            return Ok(locationInfo);
        }
    }
}

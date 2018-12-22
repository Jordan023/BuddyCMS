using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Buddy.WebApp.Helpers
{
    public static class JsonHelper
    {
        public static string AddToJson(string jsonInput, Dictionary<string, dynamic> addInput)
        {
            var jsonObject = JObject.Parse(jsonInput);

            var inputObject = JObject.Parse(JsonConvert.SerializeObject(addInput));
            jsonObject.Merge(inputObject);

            var jsonOutput = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            
            return jsonOutput;
        }

        public static string UpdateJson(string jsonInput, string key, dynamic newValue)
        {
            var jsonObject = JObject.Parse(jsonInput);
            jsonObject.SelectToken(key).Replace(newValue);

            var jsonOutput = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            return jsonOutput;
        }

        public static string RemoveJson(string jsonInput, string key)
        {
            var jsonObject = JObject.Parse(jsonInput);
            jsonObject.Remove(key);

            var jsonOutput = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

            return jsonOutput;
        }

        public static string GetValueFromKey(string jsonInput, string key)
        {
            var jsonObject = JObject.Parse(jsonInput);
            var jsonOutput = jsonObject.SelectToken(key).ToString();

            return jsonOutput;
        }
    }
}
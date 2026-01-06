using System.Collections.Generic;
using SDTaskRunner.Models;

namespace SDTaskRunner.Core.Clients
{
    public static class GenerationRequestConverter
    {
        public static Dictionary<string, object> ToTxt2ImgPayload(GenerationRequest request)
        {
            var payload = new Dictionary<string, object>();

            AddIfDefined(payload, "prompt", request.Prompt);
            AddIfDefined(payload, "negative_prompt", request.NegativePrompt);
            AddIfDefined(payload, "width", request.Width);
            AddIfDefined(payload, "height", request.Height);
            AddIfDefined(payload, "steps", request.Steps);
            AddIfDefined(payload, "seed", request.Seed);

            return payload;
        }

        private static void AddIfDefined<T>(Dictionary<string, object> dict, string key, RequestField<T> field)
        {
            if (field.IsDefined)
            {
                dict[key] = field.Value!;
            }
        }
    }
}
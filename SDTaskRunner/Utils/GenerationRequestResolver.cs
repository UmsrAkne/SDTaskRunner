using System;
using SDTaskRunner.Models;

namespace SDTaskRunner.Utils
{
    public sealed class GenerationRequestResolver
    {
        public static ResolvedGenerationRequest Resolve(
            GenerationRequest parent,
            GenerationVariant child)
        {
            T ResolveField<T>(
                RequestField<T> parentField,
                RequestField<T> childField)
            {
                if (parentField.IsDefined)
                {
                    return parentField.Value!;
                }

                if (childField is not null && childField.IsDefined)
                {
                    return childField.Value!;
                }

                throw new InvalidOperationException("Required field is not defined.");
            }

            return new ResolvedGenerationRequest
            {
                Prompt = ResolveField(parent.Prompt, child?.Prompt),
                NegativePrompt = ResolveField(parent.NegativePrompt, child?.NegativePrompt),
                Width = ResolveField(parent.Width, child?.Width),
                Height = ResolveField(parent.Height, child?.Height),
                Steps = ResolveField(parent.Steps, child?.Steps),
                Seed = ResolveField(parent.Seed, child?.Seed),
            };
        }
    }
}
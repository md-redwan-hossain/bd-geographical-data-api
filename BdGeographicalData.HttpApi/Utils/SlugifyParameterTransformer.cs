using System.Text.RegularExpressions;

namespace BdGeographicalData.HttpApi.Utils
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null)
                return null;


            return Regex.Replace(value.ToString()!,
                "([a-z])([A-Z])",
                "$1-$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)).ToLowerInvariant();
        }
    }
}
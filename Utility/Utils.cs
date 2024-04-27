using Newtonsoft.Json;

namespace SchnaeppchenJaeger.Utility
{
    /// <summary>
    /// Utility class for common operations.
    /// </summary>
    public class Utils
    {
        public List<string> products = new List<string>();
        public List<string> selectedShops = new List<string>();
        public Dictionary<string, string> populatedData = new Dictionary<string, string>();

        /// <summary>
        /// Extracts properties from the response content and populates them into a dictionary.
        /// </summary>
        /// <param name="response">The HTTP response message containing the content.</param>
        /// <returns>A dictionary containing extracted properties.</returns>
        public Dictionary<string, string> GetPropertiesFromResponse(HttpResponseMessage response)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var contents = response.Content.ReadAsStringAsync().Result;
            dynamic root = JsonConvert.DeserializeObject(contents, settings)!;

            for (int i = 0; i < root.results.Count; i++)
            {
                string advertiserName = root.results[i].advertisers[0].name;
                if (selectedShops.Any(shop => advertiserName.ToLower().Contains(shop.ToLower())))
                {
                    populatedData[$"AdvertiserName_{i}"] = root.results[i].advertisers[0].name;
                    populatedData[$"Description_{i}"] = root.results[i].description;
                    populatedData[$"Price_{i}"] = root.results[i].price.ToString();
                    populatedData[$"ReferencePrice_{i}"] = root.results[i].referencePrice.ToString();
                    populatedData[$"Unit_{i}"] = root.results[i].unit.name;
                    populatedData[$"FromDate_{i}"] = root.results[i].validityDates[0].from.ToString();
                    populatedData[$"ToDate_{i}"] = root.results[i].validityDates[0].to.ToString();
                    populatedData[$"RequiresLoyaltyMembership_{i}"] = root.results[i].requiresLoyalityMembership.ToString();
                }
            }

            KeepCheapestEntries(populatedData);

            return populatedData;
        }

        public void KeepCheapestEntries(Dictionary<string, string> populatedData)
        {
            // Create a list to store KeyValuePair of referencePrice and corresponding key
            var referencePrices = new List<KeyValuePair<decimal, int>>();

            // Iterate through populatedData to extract reference prices
            foreach (var entry in populatedData)
            {
                if (entry.Key.StartsWith("ReferencePrice_") && decimal.TryParse(entry.Value, out decimal price))
                {
                    // Extract the index from the key
                    int index = int.Parse(entry.Key.Substring("ReferencePrice_".Length));

                    referencePrices.Add(new KeyValuePair<decimal, int>(price, index));
                }
            }

            // Sort referencePrices by price
            referencePrices.Sort((x, y) => x.Key.CompareTo(y.Key));

            // Keep only the two cheapest entries
            var cheapestIndices = referencePrices.Take(2).Select(pair => pair.Value).ToList();

            // Create a new dictionary to store the cheapest entries
            var cheapestEntries = new Dictionary<string, string>();

            // Copy all information for the cheapest entries to the new dictionary
            foreach (var index in cheapestIndices)
            {
                foreach (var entry in populatedData)
                {
                    // Check if the key contains the index
                    if (entry.Key.Contains($"_{index}"))
                    {
                        cheapestEntries[entry.Key] = entry.Value;
                    }
                }
            }

            // Clear the original populatedData
            populatedData.Clear();

            // Copy back the cheapest entries to the original populatedData
            foreach (var entry in cheapestEntries)
            {
                populatedData[entry.Key] = entry.Value;
            }
        }

        /// <summary>
        /// Reverses the characters in a string.
        /// </summary>
        /// <param name="s">The string to reverse.</param>
        /// <returns>The reversed string.</returns>
        public unsafe string Reverse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            char[] chars = s.ToCharArray();
            fixed (char* p = chars)
            {
                int start = 0;
                int end = chars.Length - 1;

                while (start < end)
                {
                    char temp = p[start];
                    p[start] = p[end];
                    p[end] = temp;

                    start++;
                    end--;
                }
            }

            return new string(chars);
        }
    }
}
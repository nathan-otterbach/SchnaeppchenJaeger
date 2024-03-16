using Newtonsoft.Json;

namespace SchnaeppchenJaeger.Utils
{
    public class Utils
    {
        public Dictionary<string, string> GetPropertiesFromResponse(HttpResponseMessage response)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            var contents = response.Content.ReadAsStringAsync().Result;
            dynamic root = JsonConvert.DeserializeObject<DTO.DTO.Root>(contents, settings)!;

            var populatedData = new Dictionary<string, string>();

            for (int i = 0; i < root.results.Count; i++)
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

            return populatedData;
        }

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
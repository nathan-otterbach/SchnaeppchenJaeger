using Newtonsoft.Json;

namespace SchnaeppchenJaeger.Utils
{
    internal class Utils
    {
        public Dictionary<string, string> GetPropertiesFromResponse(HttpResponseMessage response, params string[] propertyNames)
        {
            // Read the content of the response as a string
            var contents = response.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON content to a dynamic object
            dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(contents)!;

            // Create a dictionary to store the extracted properties
            var result = new Dictionary<string, string>();

            // Extract the specified properties from the dynamic object
            foreach (var propertyName in propertyNames)
            {
                // Check if the property exists in the JSON response
                var propertyValue = jsonResponse[propertyName].ToString();

                // Add the property and its value to the dictionary
                result.Add(propertyName, propertyValue);
            }

            return result;
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
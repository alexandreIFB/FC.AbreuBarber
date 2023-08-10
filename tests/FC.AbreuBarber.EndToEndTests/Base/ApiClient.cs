

using System.Text;
using System.Text.Json;

namespace FC.AbreuBarber.EndToEndTests.Base
{
    public class ApiClient
    {

        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
            =>  _httpClient = httpClient;

        public async Task<(HttpResponseMessage?, TOutput?)> Post<TOutput>(
            string route,
            object payload
            ) where TOutput : class
        {
            var response = await _httpClient.PostAsync(route,
                new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8
                    )
                );

            var outputString = await response.Content.ReadAsStringAsync();

            if( String.IsNullOrWhiteSpace(outputString) )
            {
                return (response, null);
            }


            var output = JsonSerializer.Deserialize<TOutput>( outputString , 
                new JsonSerializerOptions   {
                PropertyNameCaseInsensitive = true
                } 
            );


            return (response, output);
        }
    }
}

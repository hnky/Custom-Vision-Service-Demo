using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomVisionApiApp
{
    static class Program
    {
        private static string _predictionKey;
        private static string _predictionEndpointUrl;

        static void Main()
        {
            _predictionKey = "[Insert Prediction Key]";
            _predictionEndpointUrl = "[Insert Prediction Endpoint]";

            Console.Write("Enter image path: ");
            string filePath = Console.ReadLine();

            byte[] byteData = GetFileAsByteArray(filePath);
            MakeRequest(byteData).Wait();

            Console.WriteLine("\nHit any key to exit");
            Console.ReadLine();
        }

        static async Task MakeRequest(byte[] byteData)
        {
            using (var content = new ByteArrayContent(byteData))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Prediction-Key", _predictionKey);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                HttpResponseMessage response = await client.PostAsync(_predictionEndpointUrl, content);
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        static byte[] GetFileAsByteArray(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

    }
}
using System;
using System.IO;
using Microsoft.Cognitive.CustomVision;
using Microsoft.Cognitive.CustomVision.Models;

namespace CustomVisionSdkApp
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Guid projectId = Guid.Parse("[Insert project ID]");
            string predictionKey = "[Insert prediction Key]";
            string imagePath = @"d:\sample.jpg";

            MemoryStream testImage = new MemoryStream(File.ReadAllBytes(imagePath));

            PredictionEndpointCredentials predictionEndpointCredentials = new PredictionEndpointCredentials(predictionKey);
            PredictionEndpoint endpoint = new PredictionEndpoint(predictionEndpointCredentials);

            ImagePredictionResultModel result = endpoint.PredictImage(projectId, testImage);

            // Loop over each prediction and write out the results
            foreach (ImageTagPrediction prediction in result.Predictions)
            {
                Console.WriteLine($"\t{prediction.Tag}: {prediction.Probability:P1}");
            }

            Console.ReadKey();
        }
    }
}

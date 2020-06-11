using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PubSubdemp
{
    class Program
    {
        private const string ProjectId = "hip-voyager-241415";
        private const string TopicId = "MyQueues";
        private const string SubscriptionId = "s9980475008";
        private  static Google.Cloud.PubSub.V1.TopicName topicName= new TopicName(ProjectId,TopicId);

       public async static Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Environment.SetEnvironmentVariable(
       "GOOGLE_APPLICATION_CREDENTIALS",
       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "My First Project-065d320bab2e.json"));
           // GoogleCredential credential = GoogleCredential.GetApplicationDefault();
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName, null, null);
            var pubsubMessage = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8("this is new  sample message")

            };
            string messageId = await publisher.PublishAsync( pubsubMessage);
        }
       
  
    }
}

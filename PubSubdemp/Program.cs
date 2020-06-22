using Google.Apis.Auth.OAuth2;
using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using System;
using System.IO;
using System.Linq;
using Google.Apis.CloudTalentSolution.v3p1beta1;
using System.Threading.Tasks;
using Google.Apis.CloudTalentSolution.v3p1beta1.Data;
using Google.Cloud.Talent.V4Beta1;

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
            CompanyServiceClient companyServiceClient = CompanyServiceClient.Create();
            // GoogleCredential credential = GoogleCredential.GetApplicationDefault();
            Google.Cloud.Talent.V4Beta1.Company c = new Google.Cloud.Talent.V4Beta1.Company
            {
                Name = "ADI",
                DisplayName="ADI",
               
                ExternalId="123"


            };
            Google.Cloud.Talent.V4Beta1.CreateCompanyRequest request = new Google.Cloud.Talent.V4Beta1.CreateCompanyRequest
            {
               
                Company = c
            };
            
            Google.Cloud.Talent.V4Beta1.Company response = companyServiceClient.CreateCompany(request);
            Console.WriteLine("Created Company");
            Console.WriteLine($"Name: {response.Name}");
            Console.WriteLine($"Display Name: {response.DisplayName}");
            Console.WriteLine($"External ID: {response.ExternalId}");

            //JobServiceClient jobServiceClient = JobServiceClient.Create();
            // TenantName tenantName = TenantName.FromProjectTenant(projectId, tenantId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName, null, null);
            var pubsubMessage = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8("this is new  sample message")

            };
            string messageId = await publisher.PublishAsync( pubsubMessage);
            var subscriptionName = new SubscriptionName(ProjectId, SubscriptionId);
            var subscription = await SubscriberClient.CreateAsync(subscriptionName);
            try
            {
               // PullResponse response = subscription.(subscriptionName, true, 10);
                await subscription.StartAsync((pubsubMessage, cancellationToken) =>
                {
                    // Process the message here.
                    var all= Task.FromResult(SubscriberClient.Reply.Ack);
                    return all;
                });

                

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: {0}", e.Message);
               
            }
        }
       
  
    }
}

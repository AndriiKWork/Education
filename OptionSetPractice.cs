using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionSetPractice
{

    class Program
    {


        static void GetOptionsetText(string entityLogicalName, string attributeName, IOrganizationService organizationService)
        {

            RetrieveEntityRequest RetrieveEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.All,
                LogicalName = entityLogicalName
            };

            RetrieveEntityResponse retrieveEntityResponseObj = (RetrieveEntityResponse)organizationService.Execute(RetrieveEntityRequest);
            EntityMetadata metadata = retrieveEntityResponseObj.EntityMetadata;
            PicklistAttributeMetadata picklistMetadata =
             metadata.Attributes.FirstOrDefault(attribute =>
             String.Equals(attribute.LogicalName, attributeName, StringComparison.OrdinalIgnoreCase)) as PicklistAttributeMetadata;

            OptionSetMetadata optionSet = picklistMetadata.OptionSet;
            IList<OptionMetadata> OptionList = (from o in optionSet.Options
                                                select o).ToList();

            Console.WriteLine("Press 'Enter' to continue");
            Console.ReadKey();
            Console.WriteLine($"OptionList.Count: {OptionList.Count}\n");
            foreach (var item in OptionList)
            {
                Console.WriteLine($"item.Value: {item.Value} ");
                Console.WriteLine($"item.Label.UserLocalizedLabel.Label: {item.Label.UserLocalizedLabel.Label}\n");
            }

        }
        static void Main(string[] args)
        {

            string connectionString = @"AuthType=OAuth;

Username=;

Password=;

Url=;

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient service = new CrmServiceClient(connectionString);


            GetOptionsetText("akl_rent", "akl_pickuplocation", service);

            Console.ReadKey();


        }
    }
}

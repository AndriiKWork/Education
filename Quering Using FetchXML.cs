using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;


namespace Quering_Using_FetchXML
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = @"AuthType=OAuth;

Username=;

Password=;

Url=;

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";


            CrmServiceClient service = new CrmServiceClient(connectionString);

            Entity akl_car = new Entity("akl_car");
            akl_car.Attributes.Add("akl_name", "new car");
            Guid guid = service.Create(akl_car);
            Console.WriteLine(guid);
            Console.ReadKey();

            //----------------------------------------------------------------------------------------------
            //Quering Using FetchXML

            #region Quering Using FetchXML
            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
              <entity name='akl_car'>
                <attribute name='akl_name' />
                <attribute name='createdon' />
                <attribute name='akl_vinnumber' />
                <attribute name='akl_purchasedate' />
                <attribute name='akl_productiondate' />
                <attribute name='akl_carmodel' />
                <attribute name='akl_carmanufacturer' />
                <attribute name='akl_carclass' />
                <attribute name='akl_carid' />
                <order attribute='akl_name' descending='false' />
                <filter type='and'>
                  <condition attribute='statecode' operator='eq' value='0' />
                </filter>
              </entity>
            </fetch>";

            EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

            foreach (Entity car in collection.Entities)
            {

                Console.WriteLine(car.Attributes["akl_name"].ToString());
            }

            Console.ReadKey();
            #endregion



        }
    }
}

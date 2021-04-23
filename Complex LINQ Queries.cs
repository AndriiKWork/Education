using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace Complex_LINQ_Queries
{
    class Program
    {
        static void Main(string[] args)
        {

            #region ConnectionString
            string connectionString = @"AuthType=OAuth;

Username=;

Password=;

Url=;

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";
            #endregion

            CrmServiceClient service = new CrmServiceClient(connectionString);

            using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            {

                var records = from car in context.CreateQuery("akl_car")
                              join
                              carclass in context.CreateQuery("akl_carclass")
                              on car["akl_carclass"] equals carclass["akl_carclassid"]
                              where car["akl_carclass"] != null
                              select new
                              {
                                  CarName = car["akl_name"],
                                  CarClass = carclass["akl_name"]
                              };

                foreach (var record in records)
                {
                    Console.WriteLine(record.CarName + " " + record.CarClass);
                }

                Console.ReadKey();
            }
        }
    }
}

using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;
namespace FieldsRandomSelection
{
    class Programg
    {


        static void Main(string[] args)
        {


            #region ConnectionString
            string connectionString = @"AuthType=OAuth;

Username=;

Password

Url=

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";
            #endregion

            CrmServiceClient service = new CrmServiceClient(connectionString);

            Random gen = new Random();
            //Выбор всех ID класса машины
            using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            {
                var carClassIDRecords = (from carclass in context.CreateQuery("akl_carclass")
                                         where carclass["akl_classcode"] != null
                                         where carclass["akl_carclassid"] != null
                                         select new
                                         {
                                             carClassID = carclass["akl_carclassid"]

                                         }).ToList();

                //Выбор рандомного ID класса машины
                {
                    Guid randomCarClassId = new Guid(carClassIDRecords.ElementAt(gen.Next(carClassIDRecords.Count)).carClassID.ToString());
                    Console.WriteLine($"New random car class ID is : {randomCarClassId}");
                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                }

            }

            Console.ReadKey();
        }
    }
}


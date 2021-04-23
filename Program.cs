using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;


namespace Dynamics_365_integrations
{
    class Program
    {
        static void Main(string[] args)
        {

            #region ConnectionString
            string connectionString = @"AuthType=OAuth;

Username=Andrii.K@NewEnvironment365.onmicrosoft.com;

Password=PI3.1415926;

Url=https://orgad83b881.crm.dynamics.com;

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";
            #endregion

            CrmServiceClient service = new CrmServiceClient(connectionString);
            ////-------------------------------------------------------------------------------------------
            //Entity akl_car = new Entity("akl_car");
            //akl_car.Attributes.Add("akl_name", "new car");
            //Guid guid = service.Create(akl_car);
            //Console.WriteLine(guid);
            //Console.ReadKey();

            //----------------------------------------------------------------------------------------------
            //Quering Using FetchXML

            #region Quering Using FetchXML
            //            string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
            //  <entity name='akl_car'>
            //    <attribute name='akl_name' />
            //    <attribute name='createdon' />
            //    <attribute name='akl_vinnumber' />
            //    <attribute name='akl_purchasedate' />
            //    <attribute name='akl_productiondate' />
            //    <attribute name='akl_carmodel' />
            //    <attribute name='akl_carmanufacturer' />
            //    <attribute name='akl_carclass' />
            //    <attribute name='akl_carid' />
            //    <order attribute='akl_name' descending='false' />
            //    <filter type='and'>
            //      <condition attribute='statecode' operator='eq' value='0' />
            //    </filter>
            //  </entity>
            //</fetch>";

            //EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

            //foreach (Entity car in collection.Entities)
            //{

            //    Console.WriteLine(car.Attributes["akl_name"].ToString());
            //}

            //Console.ReadKey();
            #endregion


            //----------------------------------------------------------------------------------------------
            //Aggregate Operations using Fetch XM

            #region Quering Using FetchXML

            //string query = @"<fetch distinct='false' mapping='logical' aggregate='true'>
            //<entity name='akl_car'>
            //<attribute name='akl_name' alias='NumberOfCar' aggregate='count' />
            //</entity>
            //</fetch>";

            //EntityCollection collection = service.RetrieveMultiple(new FetchExpression(query));

            //foreach(Entity item in collection.Entities)
            //{
            //    Console.WriteLine(((AliasedValue)item.Attributes["NumberOfCar"]).Value.ToString());
            //}

            //Console.ReadKey();

            #endregion


            #region Querying data using LINQ - Late Binding

            //using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            //{

            //    var records = from car in context.CreateQuery("akl_car")
            //                  where car["akl_carmodel"].Equals("Q4")
            //                  select car;

            //    foreach (var record in records)
            //    {
            //        Console.WriteLine(record.Attributes["akl_name"].ToString());

            //        if (record.Attributes.Contains("akl_vinnumber"))
            //        {
            //            Console.WriteLine(record.Attributes["akl_name"].ToString() + "|" + record.Attributes["akl_vinnumber"].ToString());
            //        }
            //        else

            //            Console.WriteLine(record.Attributes["akl_name"].ToString() + " " + record.Attributes["akl_carmodel"].ToString());
            //    }

            //}

            // Console.ReadKey();
            #endregion

            #region Complex LINQ Queries

            //using (OrganizationServiceContext context = new OrganizationServiceContext(service))
            //{

            //    var records = from car in context.CreateQuery("akl_car")
            //                  join
            //                  carclass in context.CreateQuery("akl_carclass")
            //                  on car["akl_carclass"] equals carclass["akl_carclassid"]
            //                  where car["akl_carclass"] != null
            //                  select new
            //                  {
            //                      CarName = car["akl_name"],
            //                      CarClass = carclass["akl_name"]
            //                  };

            //    foreach (var record in records)
            //    {
            //        Console.WriteLine(record.CarName + " " + record.CarClass);
            //    }

                Console.ReadKey();
                //}
                #endregion

                #region Early Binding

                akl_car car = new akl_car();
                car.akl_name = "Early binding Car";
                car.akl_VINnumber = "XXX09736333";
                service.Create(car);

                #endregion

                #region Using LINQ with Early Binding

                //using (svcContext context = new svcContext(service))
                //{
                //    var someCars = from cars in context.akl_carSet
                //                   where cars.akl_Carmodel == "Q4"
                //                   select cars;

                //    foreach (var car in someCars)
                //    {
                //        Console.WriteLine(car.akl_name + " " + car.akl_Carmodel);
                //    }
                //}
                //Console.ReadKey();
                #endregion Introduction to Service.Execute Method






                #region  Working with ExecuteMultipleRequest
                //Entity akl_car = new Entity("akl_car");
                //akl_car.Attributes.Add("akl_name", "new car");
                //CreateRequest req = new CreateRequest();
                //req.Target = akl_car;

                //CreateResponse response = (CreateResponse)service.Execute(req);

                #endregion


                Console.ReadKey();

            }
        }
    }



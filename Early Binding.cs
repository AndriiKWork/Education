using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Linq;

namespace Early_Binding
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ConnectionString
            string connectionString = @"AuthType=OAuth;

Username=

Password=

Url=

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";
            #endregion

            CrmServiceClient service = new CrmServiceClient(connectionString);
            #region Early Binding

            //akl_car car = new akl_car();
            //car.akl_name = "Early binding Car";
            //car.akl_VINnumber = "XXX09736333";
            //service.Create(car);

            #endregion
            #region Using LINQ with Early Binding


            using (svcContext context = new svcContext(service))
            {
                var someCars = from cars in context.akl_carSet
                               where cars.akl_Carmodel == "Q4"
                               select cars;

                foreach (var car in someCars)
                {
                    Console.WriteLine(car.akl_name + " " + car.akl_Carmodel);
                }
            }
            Console.ReadKey();
            #endregion Introduction to Service.Execute Method

        }
    }
}

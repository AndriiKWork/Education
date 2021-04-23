using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Report_Create
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
            string delite = null;

            Entity newReport = new Entity("akl_cartransferreport");
            Guid newReportGUID = service.Create(newReport);
            Console.WriteLine(newReportGUID);
            Guid rundomCarGuid = new Guid("3725ea48-d49d-eb11-b1ac-0022480294c0");

            ColumnSet retrieveReportAttribunes = new ColumnSet("akl_name", "akl_carid",
           "akl_type", "akl_date", "akl_damages", "akl_damagedescription");

            Entity retriveReport = service.Retrieve("akl_cartransferreport", newReportGUID, retrieveReportAttribunes);
            Console.WriteLine("Retrieved");
            
            retriveReport["akl_name"] = "Report name";
            retriveReport["akl_carid"] = new EntityReference("akl_car", rundomCarGuid);
            retriveReport["akl_type"] = new OptionSetValue(964140000);
            retriveReport["akl_date"] = new DateTime(2021, 10, 12, 8, 30, 0); 
            retriveReport["akl_damages"] = true;
            retriveReport["akl_damagedescription"] = "damage";

            Console.WriteLine("New values ​​assigned press 'Enter'");
            Console.ReadKey();
            service.Update(retriveReport);
            Console.WriteLine("New values ​​updated press 'Enter'");
            Console.ReadKey();
            Console.WriteLine("Press 'D' for dlite new record");
            delite = Console.ReadLine().ToLower();
            if (delite == "d")
            {
                service.Delete("akl_cartransferreport", newReportGUID);
                Console.WriteLine("Delited");
            }
            Console.WriteLine("press 'Enter' to exit");
            Console.ReadKey();

        }
    }
}


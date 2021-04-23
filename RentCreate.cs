using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace Rent_Create
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

            //Создание записи
            CrmServiceClient service = new CrmServiceClient(connectionString);
            Entity newRent = new Entity("akl_rent");
            Guid newRentGUID = service.Create(newRent);
            Console.WriteLine(newRentGUID);
            Console.WriteLine("created pess 'Enter'");
            Console.ReadKey();


            Guid rundomCarClassGuid = new Guid("d5f429da-d09d-eb11-b1ac-00224808620e");
            Guid rundomCarGuid = new Guid("3725ea48-d49d-eb11-b1ac-0022480294c0");
            Guid rundomCustomerGuid = new Guid("afd8efbf-d69d-eb11-b1ac-002248086a60");

            ColumnSet retrieveRentAttribunes = new ColumnSet("akl_name", "akl_carclassid", "akl_carid",
            "akl_customerid", "akl_reservedpickup", "akl_reservedhandover", "akl_pickuplocation",
            "akl_returnlocation", "akl_actualpickup", "akl_actualreturn", "akl_pickupreportid",
            "akl_returnreportid", "statecode", "statuscode", "akl_price", "akl_paid", "ownerid");
            Entity retriveRent = service.Retrieve("akl_rent", newRentGUID, retrieveRentAttribunes);
            Console.WriteLine("Retrieved");

            //Заполнение полей 
            retriveRent["akl_name"] = "rent name";
            retriveRent["akl_carclassid"] = new EntityReference("akl_carclass", rundomCarClassGuid);
            retriveRent["akl_carid"] = new EntityReference("akl_car", rundomCarGuid);
            retriveRent["akl_customerid"] = new EntityReference("contact", rundomCustomerGuid);
            retriveRent["akl_reservedpickup"] = new DateTime(2021, 10, 12, 8, 30, 0);
            retriveRent["akl_reservedhandover"] = new DateTime(2021, 10, 20, 10, 00, 0);
            retriveRent["akl_pickuplocation"] = new OptionSetValue(964140002);
            retriveRent["akl_returnlocation"] = new OptionSetValue(964140003);
            retriveRent["akl_actualpickup"] = new DateTime(2021, 10, 12, 10, 30, 0);
            retriveRent["akl_actualreturn"] = new DateTime(2021, 10, 20, 10, 00, 0);
            retriveRent["akl_price"] = new Money(300);
            retriveRent["akl_paid"] = false;
            Console.WriteLine("Chouse entity state:\n A - active, D-inactive ");

            //Изменение статуса записи
            string requiredStatusReason;
            string requiredStatecode = Console.ReadLine().ToLower();
            if (requiredStatecode == "a")
            {
                retriveRent["statecode"] = new OptionSetValue(0);
                Console.WriteLine("Record state is active, chouse 'Status reason'\n 1- Created;\n 2- Confirmed;\n 3- Renting");
                requiredStatusReason = Console.ReadLine();
                switch (requiredStatusReason)
                {
                    case "3":
                        retriveRent["statuscode"] = new OptionSetValue(964140002);
                        Console.WriteLine("Status reason set as 'Renting'");
                        break;
                    case "2":
                        retriveRent["statuscode"] = new OptionSetValue(964140001);
                        Console.WriteLine("Status reason set as 'Confirmed'");
                        break;
                    default:
                        retriveRent["statuscode"] = new OptionSetValue(964140000);
                        Console.WriteLine("Status reason set as 'Created'");
                        break;
                }
            }

            else if (requiredStatecode == "d")
            {
                retriveRent["statecode"] = new OptionSetValue(1);
                Console.WriteLine("Record state is inactive, chouse 'Status reason'\n 1-Returned ;\n 2- Canceled;");
                requiredStatusReason = Console.ReadLine();
                switch (requiredStatusReason)
                {
                    case "1":
                        retriveRent["statuscode"] = new OptionSetValue(964140003);
                        Console.WriteLine("Status reason set as 'Returned'");
                        break;
                    default:
                        retriveRent["statuscode"] = new OptionSetValue(964140004);
                        Console.WriteLine("Status reason set as 'Canceled'");
                        break;
                }
            }

            Console.WriteLine("state is set, press 'Enter'");
            Console.WriteLine("New values ​​assigned press 'Enter'");
            Console.ReadKey();

            //Обновление записи
            service.Update(retriveRent);
            Console.WriteLine("New values ​​updated press 'Enter'");
            Console.ReadKey();

            //Удаление записи
            Console.WriteLine("Press 'D' for dlite new record");
            string delite = Console.ReadLine().ToLower();
            if (delite == "d")
            {
                service.Delete("akl_rent", newRentGUID);
                Console.WriteLine("Delited");
            }
            Console.WriteLine("press 'Enter' to exit");
            Console.ReadKey();

        }
    }
}

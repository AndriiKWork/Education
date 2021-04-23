using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent_retrive
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"AuthType=OAuth;

Username=

Password=

Url=

AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;

RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;";

            CrmServiceClient service = new CrmServiceClient(connectionString);
            
            Guid retrieveRentId = new Guid("4c8381d7-d89d-eb11-b1ac-00224802944a");

            ColumnSet retrieveRentAttribunes = new ColumnSet("akl_name", "akl_carclassid", "akl_carid",
            "akl_customerid", "akl_reservedpickup", "akl_reservedhandover", "akl_pickuplocation",
            "akl_returnlocation", "akl_actualpickup", "akl_actualreturn", "akl_pickupreportid",
            "akl_returnreportid", "statecode", "statuscode", "akl_price", "akl_paid", "ownerid");
            Entity retriveRent = service.Retrieve("akl_rent", retrieveRentId,retrieveRentAttribunes);
            Console.WriteLine("Retrieved");

            Console.WriteLine($"akl_name: {retriveRent["akl_name"]};");
            //КАК ПОЛУЧИТЬ ID СВЯЗАННОЙ ЗАПИСИ БЕЗ ИСКЛЮЧЕНИЯ ?
            Console.WriteLine( $" akl_carclassid: {retriveRent?.GetAttributeValue<EntityReference>("akl_carclassid").Id}");
            Console.WriteLine($" akl_carid: {(Guid)retriveRent?.GetAttributeValue<EntityReference>("akl_carid").Id};");
            Console.WriteLine($"akl_customerid: { retriveRent.GetAttributeValue<EntityReference>("akl_customerid").Id}");

            Console.WriteLine($" akl_reservedpickup:  { retriveRent["akl_reservedpickup"]};");
            Console.WriteLine($" akl_reservedhandover: { retriveRent["akl_reservedhandover"]};");
            Console.WriteLine($" akl_pickuplocation: { retriveRent.GetAttributeValue<OptionSetValue>("akl_pickuplocation").Value};");
            Console.WriteLine($" akl_returnlocation: { retriveRent.GetAttributeValue<OptionSetValue>("akl_returnlocation").Value};");
            Console.WriteLine($" akl_actualpickup: { retriveRent["akl_actualpickup"]};");
            Console.WriteLine($" akl_actualreturn: { retriveRent["akl_actualreturn"]};");
            Console.WriteLine($" akl_pickupreportid: { retriveRent.GetAttributeValue<EntityReference>("akl_pickupreportid").Id};"); 
            Console.WriteLine($"akl_returnreportid: { retriveRent.GetAttributeValue<EntityReference>("akl_returnreportid").Id}; ");
            Console.WriteLine($" statescode: { retriveRent.GetAttributeValue<OptionSetValue>("statecode").Value};");//МОГУ ПОЛУЧИТЬ СТАТУС
            Console.WriteLine($" statuscode: { retriveRent.GetAttributeValue<OptionSetValue>("statuscode").Value};");//МОГУ ПОЛУЧИТЬ СТАТУСКОД
            Console.WriteLine( $" akl_price: { retriveRent.GetAttributeValue<Money>("akl_price")};");
            Console.WriteLine($" akl_paid: { retriveRent["akl_paid"]}");
            Console.WriteLine($" ownerid: { retriveRent.GetAttributeValue<EntityReference>("ownerid").Id};");

            // retriveRent["akl_name"] = "Updated Rent";

            //  service.Update(retriveRent);
            Console.WriteLine("Updated");








            Console.ReadKey();

        }
    }
}

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Utility
{
    class Program
    {
    //Генерация отчётов
        public static Guid CreateReport(string reportType, Guid carGuid, DateTime picupDate, DateTime returnDate, IOrganizationService service)
        {
            Random gen = new Random();
            int damage = gen.Next(0, 100);
            Entity newReport = new Entity("akl_cartransferreport");
            Guid newReportGUID = service.Create(newReport);
            Console.WriteLine(newReportGUID);
            Console.WriteLine("Report created pess 'Enter'");
            Console.ReadKey();
            ColumnSet retrieveReportAttribunes = new ColumnSet("akl_name", "akl_carid",
           "akl_type", "akl_date", "akl_damages", "akl_damagedescription");
            Entity retriveReport = service.Retrieve("akl_cartransferreport", newReportGUID, retrieveReportAttribunes);
            Console.WriteLine("Retrieved");

            if (reportType.ToLower() == "picup report")
            {
                retriveReport["akl_name"] = "Pickup report";
                retriveReport["akl_type"] = new OptionSetValue(964140000);
                retriveReport["akl_date"] = picupDate;
            }
            else if (reportType.ToLower() == "return report")
            {
                retriveReport["akl_name"] = "Return report";
                retriveReport["akl_type"] = new OptionSetValue(964140001);
                retriveReport["akl_date"] = returnDate;
                if (damage <= 5)
                {
                    retriveReport["akl_damages"] = true;
                    retriveReport["akl_damagedescription"] = "damage";
                }
            }

            retriveReport["akl_carid"] = new EntityReference("akl_car", carGuid);
            service.Update(retriveReport);
            Console.WriteLine("New report values ​​updated press 'Enter'");
            Console.ReadKey();
            return newReportGUID;
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
            Guid? randomCarClassId = null;
            Guid randomCarId = new Guid("00000000-0000-0000-0000-000000000000");
            Guid? contactID = null;
            int[] picupLocationValues = new int[4] { 964140000, 964140001, 964140002, 964140003 };
            int[] returnLocationValues = new int[4] { 964140000, 964140001, 964140002, 964140003 };
            OptionSetValue randomPicupLocation;
            OptionSetValue randomReturnLocation;
            bool paid = false;
            Random gen = new Random();
            string exit = null;

            //Рандомный выбор даты
            RandomDateCalculator randomDateCalculator = new RandomDateCalculator();
            DateTime pickupDate = randomDateCalculator.ReservedPickupDate;
            DateTime returnDate = randomDateCalculator.ReservedReturnDate;
            Console.WriteLine($"Reserved Pickup Date: {pickupDate}");
            Console.WriteLine($"Reserved Return Date: {returnDate}\n ");

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
                while (exit != "e")
                {
                    randomCarClassId = new Guid(carClassIDRecords.ElementAt(gen.Next(carClassIDRecords.Count)).carClassID.ToString());
                    Console.WriteLine($"New random car class ID is : {randomCarClassId}");

                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                    exit = Console.ReadLine();
                }
                exit = null;

                //Выбор всех машин для соответствующего ID класса 
                var carsRecords = (from car in context.CreateQuery("akl_car")
                                   where car["akl_carclassid"].Equals(randomCarClassId.ToString())
                                   select new
                                   {
                                       vinNumber = car["akl_vinnumber"],
                                       carID = car["akl_carid"],
                                       carClassID = car["akl_carclassid"]
                                   }).ToList();

                //Выбор рандомного ID машины
                while (exit != "e")
                {
                    randomCarId = new Guid(carsRecords.ElementAt(gen.Next(carsRecords.Count)).carID.ToString());
                    Console.WriteLine($"New random car ID is : {randomCarId}");
                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                    exit = Console.ReadLine();
                }
                exit = null;

                //Выбор всех ID контактов (пользователей)
                var contactsIdRecords = (from contact in context.CreateQuery("contact")
                                         where contact["contactid"] != null
                                         select new
                                         {
                                             contactID = contact["contactid"]

                                         }).ToList();

                //Выбор рандомного ID контакта (пользователя)
                while (exit != "e")
                {
                    contactID = new Guid(contactsIdRecords.ElementAt(gen.Next(contactsIdRecords.Count)).contactID.ToString());
                    Console.WriteLine($"New random contact is : {contactID}");

                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                    exit = Console.ReadLine();
                }
                exit = null;

                while (exit != "e")
                {
                    randomPicupLocation = new OptionSetValue(picupLocationValues[gen.Next(picupLocationValues.Length)]);
                    randomReturnLocation = new OptionSetValue(returnLocationValues[gen.Next(returnLocationValues.Length)]);

                    Console.WriteLine($"Random Picup Location: {randomPicupLocation.Value};\nRandom Return Location: {randomReturnLocation.Value};");
                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");

                    exit = Console.ReadLine();
                }
                exit = null;

                //Выбор статуса
                int[] stateCodeValues = new int[2] { 0, 1 };
                int[] activeStatusReasonValues = new int[3] { 964140000, 964140001, 964140002 };
                int[] inactiveStatusReasonValue = new int[2] { 964140003, 964140004 };
                OptionSetValue randomStateCodeValue = new OptionSetValue(0);
                OptionSetValue randomStatusReasonValue = new OptionSetValue(964140000);

                while (exit != "e")
                {
                    int status = gen.Next(0, 1000);
                    if (status <= 150)
                    {
                        randomStateCodeValue = new OptionSetValue(0);
                        {
                            int statusReason = gen.Next(0, 150);

                            if (statusReason <= 50)
                            {
                                randomStatusReasonValue = new OptionSetValue(activeStatusReasonValues[0]);
                            }
                            else if (statusReason <= 100)
                            {
                                randomStatusReasonValue = new OptionSetValue(activeStatusReasonValues[1]);
                            }
                            else if (statusReason <= 150)
                                randomStatusReasonValue = new OptionSetValue(activeStatusReasonValues[2]);
                        }
                    }
                    else
                    {
                        randomStateCodeValue = new OptionSetValue(1);

                        int statusReason = gen.Next(0, 850);
                        if (statusReason <= 100)
                        {
                            randomStatusReasonValue = new OptionSetValue(inactiveStatusReasonValue[1]);

                        }
                        else
                        {
                            randomStatusReasonValue = new OptionSetValue(inactiveStatusReasonValue[0]);

                        }
                    }
                    Console.WriteLine($"Statecode: {randomStateCodeValue.Value};\nStatus reason: {randomStatusReasonValue.Value};");
                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                    exit = Console.ReadLine();
                }
                exit = null;

                //Создание отчётов после установки статуса
                switch (randomStatusReasonValue.Value)
                {
                    //Если статус "Renting"
                    case 964140002:
                        Console.WriteLine("Status is 'Renting' - сreating 'Pickup report'");
                        Guid rentingPicupReportGuid = CreateReport("Pickup report", randomCarId, pickupDate, returnDate, service);
                        Console.WriteLine($"Pickup report reportID:{rentingPicupReportGuid} ");
                        break;
                    //Если статус "Returned"
                    case 964140003:
                        Console.WriteLine("Status is 'Returned':\nсreating 'Pickup report'");
                        //Создать новый Pickup report
                        Guid returnedPicupReportGuid = CreateReport("picup report", randomCarId, pickupDate, returnDate, service);
                        Console.WriteLine($"Pickup report created reportID: {returnedPicupReportGuid}");

                        Console.WriteLine("сreating 'Return report'");
                        //Создать новый return report
                        Guid returnedReportGuid = CreateReport("Return report", randomCarId, pickupDate, returnDate, service);
                        Console.WriteLine($"Return report created reportID: {returnedReportGuid}");

                        break;

                    default:
                        break;
                }
                Console.WriteLine("press Enter ");
                Console.ReadKey();

                //Записываю ссылку на репорт в запись
                switch (randomStatusReasonValue.Value)
                {
                    //If Status = Confirmed
                    case 964140001:
                        Console.WriteLine("Status 'Confirmed' paid set 'Yes' 0.9");
                        break;
                    //If Status = Renting
                    case 964140002:
                        Console.WriteLine("Status 'Renting' paid set 'Yes' 0.999");
                        break;
                    //If Status = Returned
                    case 964140003:
                        Console.WriteLine("Status 'Returned' paid set 'Yes' 0.9998");
                        break;
                    default:
                        break;
                }

            }

            Console.WriteLine("Укажите количество записей");
            int recordsNumber = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < recordsNumber; i++)
            {
            
            }
            Console.ReadLine();
        }
    }
}

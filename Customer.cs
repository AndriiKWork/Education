using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer
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
                var contactsRecords = (from contact in context.CreateQuery("contact")
                                        where contact["firstname"] != null
                                        select new
                                        {
                                            firstName = contact["firstname"],
                                            lastName=contact["lastname"],
                                            contactID=contact["contactid"]
                                        }).ToList();
                
                Console.WriteLine("Press 'Enter' to see all contacts:\n ");
                Console.ReadKey();
                foreach (var contact in contactsRecords)
                {
                    
                    Console.WriteLine($"{ contact.contactID}, {contact.firstName}, {contact.lastName}");
                }

                Random gen = new Random();
                string contactID;
                string a = null;
                while (a != "exit")
                {
                    contactID = contactsRecords.ElementAt(gen.Next(contactsRecords.Count)).contactID.ToString();
                    Console.WriteLine($"New random contact is : {contactID}");
                    Console.WriteLine("\nPress 'Enter' for repeat or 'exit' for out :");
                    a = Console.ReadLine();
                }

                
                Console.ReadKey();

            }
        }
    }
}

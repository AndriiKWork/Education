using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status_Piced_Up
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] statusReasonValue = new string[] { "Created (0.05)", "Confirmed(0.05)", "Renting(0.05)", "Returned (0.75)", "Canceled(0.1)" };
            Random gen = new Random();
           
            string exit = null;
            while (exit != "exit")
            {
             int createdCount = 0;
            int confirmedCount = 0;
            int rentingCount = 0;
            int returnedCount = 0;
            int canceledCount = 0;
                Console.WriteLine("Укажите количество повторений");
                int n = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < n; i++)
                {
                    int status = gen.Next(0, 1000);
                    if (status <= 150)
                    {
                        int statusReason = gen.Next(0, 150);
                        if (statusReason <= 50)
                        {
                          //  Console.WriteLine(statusReasonValue[0]);
                            createdCount++;
                        }
                        else if (statusReason <= 100)
                        {
                           // Console.WriteLine(statusReasonValue[1]);
                            confirmedCount++;
                        }
                        else if (statusReason <= 150)
                        {
                          //  Console.WriteLine(statusReasonValue[2]);
                            rentingCount++;
                        }
                    }
                    else
                    {
                        int statusReason = gen.Next(0, 850);
                        if (statusReason <= 100)
                        {
                           // Console.WriteLine(statusReasonValue[4]);
                            canceledCount++;
                        }
                        else
                        {
                          //  Console.WriteLine(statusReasonValue[3]);
                            returnedCount++;
                        }
                    }

                }
                Console.WriteLine("Просмотреть результаты наэмите 'Enter'");
                Console.ReadLine();
                Console.WriteLine($"\n createdCount (5%) ({Convert.ToDouble(createdCount) / n}): {createdCount};\n confirmedCount (5%) ({Convert.ToDouble(confirmedCount) / n}): {confirmedCount};\n  rentingCount (5%) ({Convert.ToDouble(rentingCount) / n}): {rentingCount};\n canceledCount (10%) ({Convert.ToDouble(canceledCount) / n}): {canceledCount};\n returnedCount (75%) ({Convert.ToDouble(returnedCount) / n}): { returnedCount};");
                Console.ReadKey();
                Console.WriteLine("Для завершения введите 'exit'");
                exit = Console.ReadLine();
            }
        }
    }
}

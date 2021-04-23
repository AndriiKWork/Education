using System;

namespace My_Utility
{
    class RandomDateCalculator
    {
        
        private Random gen = new Random();
        private DateTime startDate;
        private DateTime lastDate;
        private int daysRange;

        public RandomDateCalculator()
        {
            startDate = new DateTime(2019,01,01);
            lastDate = new DateTime(2020,12,31);
            daysRange = 30;
        }
        public RandomDateCalculator(DateTime startDate, DateTime lastDate, int daysRange)
        {
            this.startDate = startDate;
            this.lastDate = lastDate;
            this.daysRange = daysRange;
        }

        private DateTime CalculateReservedPickupDate(DateTime startDate, DateTime lastDate)
        {
            int range = (lastDate - startDate).Days;
            return startDate.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }

        private DateTime CalculateReservedReturnDate(DateTime reservedPickupDate, int range)
        {
            return startDate.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
        
            //DateTime start;
            //Random gen;
            //int range;

            //public RandomDateTime()
            //{
            //    start = new DateTime(1995, 1, 1);
            //    gen = new Random();
            //    range = (DateTime.Today - start).Days;
            //}

            //public DateTime Next()
            //{
            //    return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
            //}


        }
}

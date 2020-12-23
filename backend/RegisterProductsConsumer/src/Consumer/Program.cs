using Consumer.JobServices;
using Consumer.Settings;
using Data.MySqlDB.ProductTable;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var jobService = new ServiceCollection()
                    .BuildDependencyInjection()
                    .BuildServiceProvider()
                    .GetService<IJobService>();

                Parallel.Invoke(new Action[]
                {
                    jobService.Execute
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

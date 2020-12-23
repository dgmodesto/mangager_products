using Application.Services.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.JobServices
{
    public class JobService : IJobService
    {
        private readonly IProductApplicationService _productApplicationService;

        public JobService(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        public void Execute()
        {
            Parallel.Invoke(
                () => _productApplicationService.ReceiveEventMessage()
               );
        }
    }
}

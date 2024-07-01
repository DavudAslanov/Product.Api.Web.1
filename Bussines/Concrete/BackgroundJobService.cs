using Bussines.Abstract;
using DataAcces.SqlServerDbContext;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Concrete
{
    public class BackgroundJobService : IBackgroundJobService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundJobService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void ScheduleRevertPriceJob(int productId, DateTime revertTime)
        {
            BackgroundJob.Schedule(() => RevertPrice(productId), revertTime);
        }
        public void RevertPrice(int productId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var product = context.Products.Find(productId);

                if (product != null)
                {
                    product.CurrentPrice = product.Price;
                    product.DiscountEndTime = null;
                    context.SaveChanges();
                }
            }
        }
    }
}

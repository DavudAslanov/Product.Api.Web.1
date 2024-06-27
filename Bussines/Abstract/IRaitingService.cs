using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface IRaitingService
    {
        Task<bool> AddRatingAsync(int productId, int rating); 
        Task<double> GetAverageRatingAsync(int productId);
    }
}

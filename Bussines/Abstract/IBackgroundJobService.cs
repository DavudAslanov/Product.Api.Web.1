﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Abstract
{
    public interface IBackgroundJobService
    {
        void ScheduleRevertPriceJob(int productId, DateTime revertTime);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace DatabaseAccessLayer
{
    public class BaseService : IDisposable
    {
        protected EnetCareEntities context;

        public BaseService()
        {
            context = new EnetCareEntities();
        }

        public BaseService(EnetCareEntities context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

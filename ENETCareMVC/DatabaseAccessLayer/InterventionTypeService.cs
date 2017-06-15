using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace DatabaseAccessLayer
{
    public class InterventionTypeService:BaseService
    {
        public InterventionTypeService() : base() { }
        public InterventionTypeService(EnetCareEntities context) : base(context) { }
       
        public virtual EnetInterventionType GetInterventionById(int? id)
        {
            return context.Set<EnetInterventionType>().FirstOrDefault(i => i.InterventionTypeId == id);
        }
    }
}

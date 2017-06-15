using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DatabaseAccessLayer;
using BusinessLogicLayer;
using BusinessLogicLayer.Models;

namespace DatabaseAccessLayer
{
    public class EnetDistrictService : BaseService
    {
        public EnetDistrictService() : base() { }
        public EnetDistrictService(EnetCareEntities context) : base(context) { }


        public virtual ICollection<EnetDistrict> EnetDistricts
        {
            get
            {
                return context.EnetDistricts.OrderBy(x => x.DistrictId).ToList();
            }
        }
    }
}
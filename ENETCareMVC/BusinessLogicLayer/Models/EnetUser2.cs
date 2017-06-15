using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public partial class EnetUser
    {
        public virtual ICollection<EnetUser> GetSiteEngineerAndManagers(List<EnetUser> users)
        {
            //var userResult = from user in users
            //                 orderby user.Name
            //                 select user;

            //return userResult.ToList();

            var userList = new List<EnetUser>();

            var roles = new List<UserType> { UserType.SiteEngineer, UserType.Manager };

            foreach (var role in roles)
            {
                var userResult = from user in users
                                 where user.Role == role
                                 select user;

                foreach (var uResult in userResult)
                {
                    userList.Add(uResult);
                }

            }

            var sortedList = userList.OrderBy(o => o.Name).ToList();

            return sortedList;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using System.Data.Entity.Validation;

namespace DatabaseAccessLayer
{
    public class UserService : BaseService
    {
        public UserService() : base() { }
        public UserService(EnetCareEntities context) : base(context) { }

        public virtual int Add(EnetUser user)
        {
            context.EnetUsers.Add(user);
            context.SaveChanges();
            return user.UserId;
        }

        public virtual EnetUser GetUserById(int id)
        {
            return context.Set<EnetUser>().FirstOrDefault(i => i.UserId == id);
        }

        public virtual EnetUser GetUserByName(string name)

        {
            return context.Set<EnetUser>().FirstOrDefault(i => i.Name == name);
        }


        public virtual ICollection<EnetUser> GetUsers()
        {
            var userResult = from users in context.EnetUsers
                             orderby users.Name
                             select users;

            return userResult.ToList();
        }

        public void UpdateUser(EnetUser user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }

                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbEntityValidationException Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}

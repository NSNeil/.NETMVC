using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;

namespace DatabaseAccessLayer
{
    public class ClientService : BaseService
    {
        public ClientService() : base() { }
        public ClientService(EnetCareEntities context) : base(context) { }

        public virtual int Add(EnetClient client)
        {
            context.EnetClients.Add(client);
            context.SaveChanges();
            return client.ClientId;
        }

        public virtual EnetClient GetClientById(int? id)
        {
            return context.Set<EnetClient>().FirstOrDefault(i => i.ClientId == id);
        }
    }
}

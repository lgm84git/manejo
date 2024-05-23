using CrmEarlyBound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashme.Crm.Condo.Service.Interface
{
    public interface ICriarContatoService
    {
        Contact BuildContatoParaCriar(Contact contact);
        Guid CriaContato(Contact contact); 
    }
}

using CrmEarlyBound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashme.Crm.Condo.Service.Interface
{
    public interface IAtualizarContatoService
    {
        void AtualizarContato(Guid ownerId);
        Contact BuildContatoParaAtualizar(Contact lead);
        Lead BuildClientePotencialParaAtualizar(Lead lead);
    }
}

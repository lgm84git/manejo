using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using System;

namespace Cashme.Crm.Condo.Data.Interfaces
{
    public interface IContatoRepository
    {
        Guid CriarContato(Entity contato);

        void AtualizarContato(Entity contato);

        void DeletarContato(Entity contato);
        Contact ObterContatoPor(Guid contactId);

    }
}

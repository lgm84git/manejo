using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using System;


namespace Cashme.Crm.Condo.Data.Interfaces
{
    public interface IClientePotencialRepository
    {
        Guid CriarClientePotencial(Entity clientePotencial);

        void AtualizarClientePotencial(Entity clientePotencial);

        void DeletarClientePotencial(Entity clientePotencial);

        Lead ObterClientePotencialPorOriginador(Guid leadOriginadorId, Guid ownerId);

    }
}

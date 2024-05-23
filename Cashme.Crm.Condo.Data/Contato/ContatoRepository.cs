using Cashme.Crm.Condo.Data.Interfaces;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Cashme.Crm.Condo.Data.Contato
{
    public class ContatoRepository : IContatoRepository
    {
        public IOrganizationService Service { get; set; }
        public ITracingService Tracing { get; set; }

        public ContatoRepository(IOrganizationService service, ITracingService tracing)
        {
            Service = service;
            Tracing = tracing;
        }

        public Guid CriarContato(Entity contato)
        {
            return Service.Create(contato);
        }

        public void AtualizarContato(Entity contato)
        {
            Tracing.Trace("AtualizarContato");
            Service.Update(contato);
        }

        public void DeletarContato(Entity contato)
        {
            Service.Delete(contato.LogicalName, contato.Id);
        }

        public Contact ObterContatoPor(Guid contactId)
        {
            string fetch = @"<fetch>
                              <entity name='contact'>
                                <attribute name='contactid' />
                                <attribute name='name' />
                                <attribute name='statuscode' />
                                <filter type='and'>
                                  <condition attribute='contactid' operator='eq' value='" + contactId + @"' />
                                </filter>
                                      <condition attribute='contactid' operator='eq' uitype='systemuser' value='" + contactId + @"' />
                                </link-entity>
                              </entity>
                            </fetch>";

            var resultLead = Service.RetrieveMultiple(new FetchExpression(fetch));

            if (resultLead?.Entities?.Count > 0)
                return resultLead.Entities[0].ToEntity<Contact>();
            return new Contact();
        }
    }
}

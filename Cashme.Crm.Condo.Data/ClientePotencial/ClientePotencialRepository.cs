using Cashme.Crm.Condo.Data.Interfaces;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Cashme.Crm.Condo.Data.ClientePotencial
{
    public class ClientePotencialRepository : IClientePotencialRepository
    {
        private const int tipoContatoCandidato = 8;
        public IOrganizationService Service { get; set; }
        public ITracingService Tracing { get; set; }

        public ClientePotencialRepository(IOrganizationService service, ITracingService tracing)
        {
            Service = service;
            Tracing = tracing;
        }

        public void AtualizarClientePotencial(Entity clientePotencial)
        {
            Tracing.Trace("AtualizarCandidato");
            Service.Update(clientePotencial);
        }

        public Guid CriarClientePotencial(Entity clientePotencial)
        {
            Tracing.Trace("CriarCandidato");
            return Service.Create(clientePotencial);
        }

        public void DeletarClientePotencial(Entity clientePotencial)
        {
            Tracing.Trace("DeletarCandidato");
            Service.Delete(clientePotencial.LogicalName, clientePotencial.Id);
        }

        public Lead ObterClientePotencialPorOriginador(Guid leadOriginadorId, Guid ownerId)
        {
            string fetch = @"<fetch>
                              <entity name='lead'>
                                <attribute name='leadid' />
                                <attribute name='parentcontactid' />
                                <attribute name='statuscode' />
                                <attribute name='ownerid' />
                                <attribute name='firstname'/>                                                     
						        <attribute name='lastname'/>
                                <attribute name='donotphone'/>
                                <attribute name='address1_telephone2'/>
                                <attribute name='donotemail'/>
                                <attribute name='emailaddress1'/>
                                <attribute name='emailaddress2'/>
                                <attribute name='address1_postalcode'/>
                                <attribute name='address1_line1'/>
                                <attribute name='address1_telephone1'/>
                                <attribute name='address1_line2'/>
                                <attribute name='address1_line3'/>                             
                                <attribute name='mobilephone'/>
                                <order attribute='ownerid' descending='false' />
                                <filter type='and'>
                                  <condition attribute='leadid' operator='eq' value='" + leadOriginadorId + @"' />
                                </filter>
                                <link-entity name='contact' from='contactid' to='parentcontactid' link-type='inner' alias='ad'>
                                   <filter type='and'>
                                      <condition attribute='ownerid' operator='eq' uitype='systemuser' value='" + ownerId + @"' />
                                   </filter>
                                </link-entity>
                              </entity>
                            </fetch>";

            var resultLead = Service.RetrieveMultiple(new FetchExpression(fetch));

            if (resultLead?.Entities?.Count > 0)
                return resultLead.Entities[0].ToEntity<Lead>();
            return new Lead();
        }
    }
}

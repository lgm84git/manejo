using Cashme.Crm.Condo.Data.ClientePotencial;
using Cashme.Crm.Condo.Data.Contato;
using Cashme.Crm.Condo.Data.Interfaces;
using Cashme.Crm.Condo.Service.Interface;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using System;

namespace Cashme.Crm.Condo.Service.Contato
{
    public class AtualizarContatoService : IAtualizarContatoService
    {
        public IOrganizationService Service { get; set; }
        public Entity Target { get; set; }
        public ITracingService Tracing { get; set; }
        public IClientePotencialRepository ClientePotencialRepository { get; set; }
        public IContatoRepository ContatoRepository { get; set; }


        public AtualizarContatoService(IOrganizationService service, Entity target, ITracingService tracing)
        {
            Service = service;
            Target = target;
            Tracing = tracing;
            ClientePotencialRepository = new ClientePotencialRepository(service, tracing);
            ContatoRepository = new ContatoRepository(service, tracing);
        }

        public void AtualizarContato(Guid ownerId)
        {
            Tracing.Trace("AtualizarContato");
            var contactTarget = Target.ToEntity<Contact>();

            Tracing.Trace("ObterClientePotencialOriginador");
            var lead = ClientePotencialRepository.ObterClientePotencialPorOriginador(contactTarget.Id, ownerId);

            Tracing.Trace("ObterContato");
            var contact = ContatoRepository.ObterContatoPor(contactTarget.Id);

            Tracing.Trace("BuildContatoParaAtualizar");
            var contatoAtualizar = BuildContatoParaAtualizar(contact);

            ContatoRepository.AtualizarContato(contatoAtualizar.ToEntity<Entity>());

            Tracing.Trace("BuildLeadParaAtualizar");
            var atualizaLead = BuildClientePotencialParaAtualizar(lead);

            ClientePotencialRepository.AtualizarClientePotencial(atualizaLead.ToEntity<Entity>());
        }

        public Contact BuildContatoParaAtualizar(Contact contact)
        {
            var contato = new Contact();
            contato.ContactId = contact.ContactId;

            Tracing.Trace($"Nome lógico : {contato.LogicalName}");
            Tracing.Trace($"ID : {contato.Id}");
            contato.FirstName = contact.FirstName;
            contato.LastName = contact.LastName;
                       
            if (!string.IsNullOrEmpty(contact.MobilePhone)) contato.MobilePhone = contact.MobilePhone;
            else contato.MobilePhone = null;

            if (!string.IsNullOrEmpty(contact.EMailAddress1)) contato.EMailAddress1 = contact.EMailAddress1;
            else contato.EMailAddress1 = null;

            if (!string.IsNullOrEmpty(contact.Address1_PostalCode)) contato.Address1_PostalCode = contact.Address1_PostalCode;
            else contato.Address1_PostalCode = null;

            if (!string.IsNullOrEmpty(contact.Address1_Line1)) contato.Address1_Line1 = contact.Address1_Line1;
            else contato.Address1_Line1 = null;

            if (!string.IsNullOrEmpty(contact.Address1_Line2)) contato.Address1_Line2 = contact.Address1_Line2;
            else contato.Address1_Line2 = null;

            if (!string.IsNullOrEmpty(contact.Address1_Line3)) contato.Address1_Line3 = contact.Address1_Line3;
            else contato.Address1_Line3 = null;

            if (!string.IsNullOrEmpty(contact.Address1_Telephone2)) contato.Address1_Telephone1 = contact.Address1_Telephone2;
            else contato.Address1_Telephone1 = null;

            if (!string.IsNullOrEmpty(contact.EMailAddress2)) contato.EMailAddress2 = contact.EMailAddress2;
            else contato.EMailAddress2 = null;


            return contato;
        }
        public Lead BuildClientePotencialParaAtualizar(Lead lead)
        {
            var atualizaLead = new Lead();
            atualizaLead.LeadId = lead.LeadId;

            return atualizaLead;
        }
    }
}

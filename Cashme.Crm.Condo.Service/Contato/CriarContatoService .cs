using Cashme.Crm.Condo.Data.ClientePotencial;
using Cashme.Crm.Condo.Data.Contato;
using Cashme.Crm.Condo.Data.Interfaces;
using Cashme.Crm.Condo.Service.Interface;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using System;

namespace Cashme.Crm.Condo.Service.Contato
{
    public class CriarContatoService : ICriarContatoService
    {
        public IOrganizationService Service { get; set; }
        public Entity Target { get; set; }
        public ITracingService Tracing { get; set; }
        public IClientePotencialRepository ClientePotencialRepository { get; set; }
        public IContatoRepository ContatoRepository { get; set; }


        public CriarContatoService(IOrganizationService service, Entity target, ITracingService tracing)
        {
            Service = service;
            Target = target;
            Tracing = tracing;
            ClientePotencialRepository = new ClientePotencialRepository(service, tracing);
            ContatoRepository = new ContatoRepository(service, tracing);
        }


        public Contact BuildContatoParaCriar(Contact contact)
        {
            var contato = new Contact();

            Tracing.Trace($"Criar Entidade : {contato.LogicalName}");
            Tracing.Trace($"ID : {contato.Id}");
            contato.FirstName = contact.FirstName;
            contato.LastName = contact.LastName;
            contato.csh_bairro = contact.csh_bairro;
            contato.csh_cidadeid = contact.csh_cidadeid;
            contato.csh_estadoid = contact.csh_estadoid;
            contato.csh_paisid = contact.csh_paisid;
            contato.csh_cepid = contact.csh_cepid;
            contato.csh_cpf = contact.csh_cpf;
            contato.csh_nacionalidade = contact.csh_nacionalidade;

            // ou
            contato = contact;

            return contato;
        }

        public Guid CriaContato(Contact contact)
        {
            return ContatoRepository.CriarContato(contact.ToEntity<Entity>());
        }

    }
}

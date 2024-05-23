using Cashme.Crm.Condo.Service;
using Cashme.Crm.Condo.Service.Contato;
using CrmEarlyBound;
using Microsoft.Xrm.Sdk;
using System;

namespace Cashme.Crm.Condo.ClientePotencial
{
    public class AtualizarContato : SuperPlugin
    {
        protected override void InitPlugin()
        {
            try
            { 
                if (this.Context.Depth > 1)
                    return;

                if (!this.Context.InputParameters.Contains("Target")) return;

                if (Context.InputParameters.Contains("Target") && Context.InputParameters["Target"] is Entity)
                {
                    if (Context.MessageName.ToLower() == "update")
                        Target = (Entity)Context.InputParameters["Target"];
                }

                if (Target != null)
                { 
                    TrackingService.Trace("Target valido.");

                    var PostImage = Context.PostEntityImages["PostImage"].ToEntity<Lead>();
                    var contatoService = new AtualizarContatoService(Service, Target, TrackingService);
                    contatoService.AtualizarContato(PostImage.OwnerId.Id);
                }
            }
            catch (InvalidPluginExecutionException e)
            {
                throw new InvalidPluginExecutionException("Erro na execução do Plugin. Mensagem:" + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Erro plugin" + e.Message);
            }
        }
    }
}

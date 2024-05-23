using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashme.Crm.Condo.Service
{
    public abstract class SuperPlugin : IPlugin
    {
        protected IPluginExecutionContext Context { get; set; }
        protected IOrganizationService Service { get; set; }
        protected ITracingService TrackingService { get; set; }
        protected IOrganizationServiceFactory ServiceFactory { get; set; }
        protected Entity Target { get; set; }

        public void Execute(IServiceProvider serviceProvider)
        {
            Context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = ServiceFactory.CreateOrganizationService(Context.UserId);
            TrackingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            InitPlugin();
        }

        protected abstract void InitPlugin();

        protected void TrackingLog(string mensagem)
        {
            this.TrackingService.Trace("-------------------------------------------------------");
            this.TrackingService.Trace($"Depth         : {this.Context.Depth}");
            this.TrackingService.Trace($"MessageName : {this.Context.MessageName}");
            this.TrackingService.Trace($"PrimaryEntityName             : {this.Context.PrimaryEntityName}");
            this.TrackingService.Trace($"Mensagem de Erro     : {mensagem}");
            this.TrackingService.Trace("-------------------------------------------------------");
        }

        protected Entity PreImage(string image)
        {
            return Context.PreEntityImages[image];
        }

        protected Entity PostImage(string image)
        {
            return Context.PostEntityImages[image];
        }
    }
}

using Funq;
using inventory_server.Server;
using ServiceStack;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;

namespace inventory_server
{

    //public class AppHost : AppHostBase
    public class AppHost : AppHostHttpListenerBase
     { 
        public AppHost() 
            : base("Customer REST Example", typeof(InventoryServer).Assembly) { }

        public override void Configure(Container container)
        {
            this.Plugins.Add(new CorsFeature());//跨域
        }
        
    }
}

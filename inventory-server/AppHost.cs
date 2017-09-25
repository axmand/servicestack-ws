using Funq;
using inventory_server.Server;
using ServiceStack;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;

namespace inventory_server
{

    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
            : base("Customer REST Example", typeof(InventoryServer).Assembly) { }

        public override void Configure(Container container)
        {
            // this.Plugins.Add(new CorsFeature());//跨域
            base.SetConfig(new EndpointHostConfig()
            {
                GlobalResponseHeaders =
                {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                    { "Access-Control-Allow-Headers", "Content-Type" },
                },
            });
        }

    }
}

using Funq;
using inventory_server.Server;
using ServiceStack;

namespace inventory_server
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost()
            : base("Customer REST Example", typeof(InventoryServer).Assembly) { }

        public override void Configure(Container container)
        {


        }
    }
}

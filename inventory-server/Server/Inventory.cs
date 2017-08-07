using inventory_server.Route;
using ServiceStack;

namespace inventory_server.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class InventoryServer: Service
    {


        public string Get(ProjectRemoveGet request)
        {
            var s =new {
                aaa = request.id
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(s);


        }

        public string Get(ProjecListGet request)
        {
            return null;
        }


    }
}

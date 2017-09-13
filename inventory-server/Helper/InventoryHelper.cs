namespace inventory_server.Helper
{
    /// <summary>
    /// singleton 
    /// </summary>
    public class InventoryHelper
    {
        static AppHost _appHost;

        public static void Start(string url)
        {
            _appHost = new AppHost();
            _appHost.Init();//.Start(url);
            _appHost.Start(url);
           
        }
    }
}

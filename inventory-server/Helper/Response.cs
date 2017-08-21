namespace inventory_server
{
    /// <summary>
    /// 服务端返回数据对象结构
    /// </summary>
    public class Response
    {
        public int status { get; set; }

        public string data { get; set; }

        public Response(int _status, object _data)
        {
            status = _status;
            data = Newtonsoft.Json.JsonConvert.SerializeObject(_data);
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// 成功，默认status=200
    /// </summary>
    public class OkResponse : Response
    {
        public OkResponse(object _data) : base(200, _data)
        {

        }
    }
    /// <summary>
    /// 失败，默认status=500,内部错误，若需要其他错误类型，可自定义
    /// </summary>
    public class FailResponse : Response
    {
        public FailResponse(object _data) : base(500, _data)
        {

        }
    }

}


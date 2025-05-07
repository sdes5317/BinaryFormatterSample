using System;
// 1. 實作介面並繼承 MarshalByRefObject，提供遠端服務方法
public class RemoteService : MarshalByRefObject, IRemoteService
{
    public string GetData(int id)
    {
        return $"【RemoteService】取回 ID = {id} 的資料，時間：{DateTime.Now}";
    }

    public LocalClassSample GetLocalClass(int id)
    {
        return new LocalClassSample() { Id = id };
    }

    public RemoteClassSample GetRemoteClass(int id)
    {
        return new RemoteClassSample() { Id = id };
    }
}

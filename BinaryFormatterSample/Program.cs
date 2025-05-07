using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class ClientProgram
{
    static void Main(string[] args)
    {
        // 1. 建立 BinaryClientFormatterSinkProvider（設定二進位序列化提供器）
        var clientProvider = new BinaryClientFormatterSinkProvider();

        // 2. 設定通道屬性：name 表示通道名稱，port 設為 0 表示不監聽本地埠
        var channelProps = new Hashtable
        {
            { "name", "tcpClient" },
            { "port", 0 }
        };

        // 3. 建立並註冊 TcpChannel，指定 clientProvider，serverProvider 設為 null
        var channel = new TcpChannel(channelProps, clientProvider, null);
        ChannelServices.RegisterChannel(channel, ensureSecurity: false);

        // 4. 透過 Activator.GetObject 取得遠端物件代理，URL 指向遠端服務所公開的 Endpoint
        var remote = (IRemoteService)Activator.GetObject(
            typeof(IRemoteService),
            "tcp://127.0.0.1:9000/RemoteService.rem"
        );

        if (remote == null)
        {
            Console.WriteLine("無法連線到遠端服務。請確認服務端已啟動並正確公開。");
            return;
        }

        // 5. 呼叫遠端 GetData 方法並接收回傳的字串資料
        int queryId = 42;
        string result = remote.GetData(queryId);
        Console.WriteLine($"取回資料（ID={queryId}）: {result}");

        // 6. 呼叫遠端 GetLocalClass 方法並在本地執行其 RunLocally
        queryId = 51;
        var localClass = remote.GetLocalClass(queryId);
        localClass.RunLocally();
        Console.WriteLine($"本地執行完成，ID = {localClass.Id}");

        // 7. 呼叫遠端 GetRemoteClass 方法並在遠端執行其 Run
        queryId = 99;
        var remoteClass = remote.GetRemoteClass(queryId);
        remoteClass.Run();

        // 8. 作業結束，解除註冊通道
        ChannelServices.UnregisterChannel(channel);
    }
}

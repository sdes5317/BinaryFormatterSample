using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class ServerProgram
{
    static void Main(string[] args)
    {
        // 2. 建立 BinaryServerFormatterSinkProvider（設定二進位序列化提供器及 TypeFilterLevel）
        var serverProvider = new BinaryServerFormatterSinkProvider
        {
            TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
        };

        // 3. 設定 TCP 通道屬性：名稱與監聽埠號（9000）
        var channelProps = new Hashtable
        {
            { "name", "tcpServer" },
            { "port", 9000 }
        };

        // 4. 建立並註冊 TcpChannel，指定 serverProvider，clientProvider 設為 null
        var channel = new TcpChannel(channelProps, null, serverProvider);
        ChannelServices.RegisterChannel(channel, ensureSecurity: false);

        // 5. 註冊遠端物件，別名須與客戶端 URL 結尾一致
        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(RemoteService),
            "RemoteService.rem",
            WellKnownObjectMode.Singleton
        );

        // 6. 提示服務啟動狀態並等待使用者輸入以結束
        Console.WriteLine("【服務已啟動】正在監聽 TCP 埠 9000...");
        Console.WriteLine("按下 Enter 可停止服務。");
        Console.ReadLine();

        // 7. 停止服務並清除通道
        ChannelServices.UnregisterChannel(channel);
    }
}

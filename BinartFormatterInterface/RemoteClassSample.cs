using System;

public class RemoteClassSample: MarshalByRefObject
{
    public int Id { get; set; }
    public void Run()
    {
        //在這裡可以下中斷點, 驗證是在遠端執行
        Console.WriteLine("MyClass is running.");
    }
}

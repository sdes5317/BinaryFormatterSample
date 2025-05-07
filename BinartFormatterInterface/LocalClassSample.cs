using System;

[Serializable]
public class LocalClassSample
{
    public int Id { get; set; }
    public void RunLocally()
    {
        //在這裡可以下中斷點, 驗證是在本地執行
        Console.WriteLine("MyClass is running.");
    }
}

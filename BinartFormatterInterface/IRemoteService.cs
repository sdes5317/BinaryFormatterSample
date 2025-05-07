public interface IRemoteService
{
    string GetData(int id);
    LocalClassSample GetLocalClass(int id);
    RemoteClassSample GetRemoteClass(int id);
}
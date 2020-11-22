
public static class ResponseHandler 
{
    private static string host;
    private static string projectID;
    private static string password;

    public static void Init(string _host, string _projectID, string _password)
    {
        host = _host;
        projectID = _projectID;
        password = _password;
    }

    public static string GetResponse<T>()
    {
        return string.Empty;
    }
}

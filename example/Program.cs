using System.Net.Http.Headers;
using System.Text;
using ls.svr.webdav;

internal class Program
{
    private static void Main(string[] args)
    {
        MainAsync().Wait();
    }

    private static async Task MainAsync()
    {
        var username = "exampleuser";
        var password = "";

        var baseUri = new Uri("https://cloud.example.com/remote.php/dav/files/exampleuser/");
        var httpclient = new HttpClient() { BaseAddress = baseUri };
        var credential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

        httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);
        httpclient.DefaultRequestHeaders.UserAgent.Clear();
        httpclient.DefaultRequestHeaders.UserAgent.ParseAdd("ls.svr.webdav/1.0");

        var dav = new WebDavClient(httpclient);

        var items = await dav.ListDirectory("./Testdata", 1);

        if (!items.IsSuccess)
        {
            Console.WriteLine("Request failed.");
            Console.WriteLine(items.StatusCode);
            Environment.Exit(1);
        }

        foreach (var item in items.Responses!)
        {
            Console.WriteLine(item.FullName);
        }
    }
}

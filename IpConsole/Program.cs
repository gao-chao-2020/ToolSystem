using Flurl.Http;
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("启动成功!");
inputUrl:
Console.ForegroundColor = ConsoleColor.Gray;
Console.WriteLine("请输入请求IP地址(例：https://localhost:7127/Ip/UpdateIp?name=computer)：");
Console.ForegroundColor = ConsoleColor.Blue;
var input = Console.ReadLine();
try
{
    string url = "";
    string methead = "get";
    if (input != null)
    {
        url = input.Trim().ToLower();
        if (url.StartsWith("post"))
        {
            url = url.Substring(5);
            methead = "post";
        }
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("输入错误!");
            goto inputUrl;
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("输入错误!");
        goto inputUrl;
    }

    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.Write("{0}>>", DateTime.Now.ToString());
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("发起请求!", DateTime.Now.ToString());
    while (true)
    {
        string result = "";
        if (methead == "post")
        {
            result = await url.PostAsync().ReceiveString();
        }
        else
        {
            result = await url.GetStringAsync();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("{0}>>", DateTime.Now.ToString());
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(result);
        Thread.Sleep(5000);
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine(ex.ToString());
    Console.WriteLine(ex.Message);
}
Console.ReadLine();
using chat_tians;

Console.ForegroundColor = ConsoleColor.White;
chat chat = new chat();
Console.WriteLine("我是基于GPT-3.5-Turbo模型,欢迎提问");
chat_01();

void  chat_01()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("请输入问题：");
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    string message = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.White;
    if (!string.IsNullOrWhiteSpace(message))
    {
        Console.WriteLine("下面是回答：");
        Console.ForegroundColor =ConsoleColor.Cyan;

        string result = string.Empty;
        //Task.Factory.StartNew(() => {

        result = chat.BeginChat(message);
        //});

        //while (true)
        //{
        //    if (result.Length>0)
        //    {
        //        break;
        //    }
            
        //}

        foreach (char c in result)
        {
            Console.Write(c);
            System.Threading.Thread.Sleep(35); // 等待100毫秒
        }
        Console.WriteLine();
        chat_01();
    }
    else
    {
        chat_01();
    }
}

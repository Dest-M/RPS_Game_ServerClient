using System.Net.Sockets;

string host = "127.0.0.1";
int port = 8000;
using TcpClient client = new TcpClient();
Console.WriteLine("Welcome");
StreamReader? Reader = null;
StreamWriter? Writer = null;
string? userName = "Player";

try
{
    client.Connect(host, port); //подключение клиента
    Reader = new StreamReader(client.GetStream());
    Writer = new StreamWriter(client.GetStream());
    if (Writer is null || Reader is null) return;
    // запускаем новый поток для получения данных
    Task.Run(() => ReceiveMessageAsync(Reader));
    // запускаем ввод сообщений
    Console.WriteLine("Connected");
    await SendMessageAsync(Writer);
    
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Writer?.Close();
Reader?.Close();

async Task GetNameAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            // считываем ответ в виде строки
            string str = await reader.ReadLineAsync();
            Console.WriteLine(str);
            // если пустой ответ, ничего не выводим на консоль
            if (string.IsNullOrEmpty(str)) continue;
            userName = userName+str;
            if(str!= null)
            {
                break;
            }
        }
        catch
        {
            break;
        }
    }
}
// отправка сообщений
async Task SendMessageAsync(StreamWriter writer)
{
    // сначала отправляем имя
    //await GetNameAsync(Reader);
    Console.WriteLine("SendMessageAsync");
    await writer.WriteLineAsync(userName);
    await writer.FlushAsync();
    Console.WriteLine("Pick your move by entering a number and pressing ENTER key:\n1- Rock\n2- Paper\n3-Scissors\n");

    while (true)
    {
        string? choice= Console.ReadLine();
        await writer.WriteLineAsync(choice);
        await writer.FlushAsync();
    }
}
// получение сообщений

async Task ReceiveStatusAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            // считываем ответ в виде строки
            string? stat = await reader.ReadLineAsync();
            // если пустой ответ, ничего не выводим на консоль
            if (string.IsNullOrEmpty(stat)) continue;
            Print(stat);//вывод сообщения
        }
        catch
        {
            Console.WriteLine("Status reception error");
            break;
        }
    }
}
async Task ReceiveMessageAsync(StreamReader reader)
{
    while (true)
    {
        try
        {
            // считываем ответ в виде строки
            string? message = await reader.ReadLineAsync();
            // если пустой ответ, ничего не выводим на консоль
            if (string.IsNullOrEmpty(message)) continue;
            Print(message);//вывод сообщения

            await ReceiveStatusAsync(reader);
        }
        catch
        {

            Console.WriteLine("Message reception error");
            break;
        }
    }
}
// чтобы полученное сообщение не накладывалось на ввод нового сообщения
void Print(string message)
{
    if (OperatingSystem.IsWindows())    // если ОС Windows
    {
        var position = Console.GetCursorPosition(); // получаем текущую позицию курсора
        int left = position.Left;   // смещение в символах относительно левого края
        int top = position.Top;     // смещение в строках относительно верха
        // копируем ранее введенные символы в строке на следующую строку
        Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
        // устанавливаем курсор в начало текущей строки
        Console.SetCursorPosition(0, top);
        // в текущей строке выводит полученное сообщение
        Console.WriteLine(message);
        // переносим курсор на следующую строку
        // и пользователь продолжает ввод уже на следующей строке
        Console.SetCursorPosition(left, top + 1);
    }
    else Console.WriteLine(message);
}

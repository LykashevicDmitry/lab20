using System;
using System.Net;
using System.IO;

namespace ЛР20
{
    class Program
    {
        /* Программа прослушивает все подключения по какому-то локальному адресу (указан в коде) */
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            //Устанавливаем адрес прослушки
            listener.Prefixes.Add("http://localhost:8888/connection/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            //Блокируем текущий поток методом GetContext(), ожидая получение запроса
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            //Получаем объект ответа
            HttpListenerResponse response = context.Response;
            //Ответ запишем как небольшую html-страничку
            string responseStr = "<html><head><meta charset='utf8'></head><body>Мы успешно подслушиваем какой-то адрес.</body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
            //В полученном потоке ответа пишем сам ответ
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            //Закрываем поток
            output.Close();
            //Останавливаем прослушивание
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }
    }
}
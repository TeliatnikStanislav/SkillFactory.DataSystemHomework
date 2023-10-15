using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSystemHomework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DeleteInDelay("C:\\Users\\Stanislav\\Новая папка (3)");
            Console.ReadKey();
        }

        static void DeleteInDelay(string Path)
        {
            try
            {
                if (Directory.Exists(Path))
                {
                    DirectoryInfo directory = new DirectoryInfo(Path); // Создаем объект класса DirectoryInfo
                    FileInfo fileInfo = new FileInfo(Path); // Создаем объект класса FileInfo
                    TimeSpan delay = TimeSpan.FromMinutes(30); // Указываем временной интервал
                    // Выпишем все имена файлов в корневом каталоге
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        Console.WriteLine($"{file.Name}");
                    }
                    // Выпишем все имена директорий в корневом каталоге
                    foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
                    {
                        Console.WriteLine(directoryInfo.Name);
                    }
                    // Если после последнего изменения файла или каталога прошло больше, чем время задержки, то удаляем подкаталоги и файлы
                    if (DateTime.Now - fileInfo.LastWriteTime >= delay)
                    {
                        foreach (FileInfo file in directory.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
                        {
                            directoryInfo.Delete(true);
                        }
                    }
                    // Если в корневой папке не осталось подкаталогов, то выведем сообщение об этом
                    if (directory.GetDirectories().Length == 0)
                    {
                        Console.WriteLine($"Файлы не использовались {delay} и были удалены или папка была пустой");
                    }
                }
                // Если папка не была найдена, то выведем сообщение об этом
                if (!Directory.Exists(Path))
                {
                    Console.WriteLine("Папка по указанному адресу не существует");
                }
            }
            // обработка исключений
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

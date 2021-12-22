using System;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace ConsoleAppLesson16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание массива товаров
            const int count = 2; // Установка размера массива товаров.
            Product[] products = new Product[count];
            for (int i = 0; i < count; i++ )
            { 
                products[i] = new Product();
                Console.WriteLine("Ввод товара N {0}", i+1);
                Console.Write("Введите код товара : ");
                products[i].Art = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите название товара : ");
                products[i].Name = Console.ReadLine();
                Console.Write("Введите цену товара : ");
                products[i].Price = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine(" ");
            }

            // Сериализаци\ массива товаров в Json-строку
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };
            string jsonProductsString = JsonSerializer.Serialize(products, options);
            // Console.WriteLine("В файл записана строка : {0} ", jsonProductsString);

            // Подготовка файла, если его нет.
            string path = "Products.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("Файл {0} создан заново", path);
                FileStream fs = File.Create(path);
                fs.Close();
            }

            // Запись Json-строки в файл, с "затиранием" предыдущей информации.
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(jsonProductsString); 
                // Console.WriteLine("Файл {0} очищен", path);
            }
            Console.WriteLine("В файл записана строка : {0} ", jsonProductsString);

            Console.ReadKey();
        }
    }
    class Product
    {  
        [ JsonPropertyName ("art") ]
        public int Art { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}

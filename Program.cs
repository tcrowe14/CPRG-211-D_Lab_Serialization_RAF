using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Filestream2
{
    [Serializable]
    public class Event
    {
        public string Location { get; set; }
        public int Number { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Event e = new Event() { Location = "Calgary", Number = 1 };
            IFormatter formatProvider = new BinaryFormatter();
            Stream stream = new FileStream(@"C:\test\event.txt", FileMode.Create, FileAccess.Write);
            formatProvider.Serialize(stream, e);
            stream.Close();

            stream = new FileStream(@"C:\test\event.txt", FileMode.Open, FileAccess.Read);
            Event e2 = (Event)formatProvider.Deserialize(stream);
            stream.Close();
            Console.WriteLine($"{e.Number}");
            Console.WriteLine($"{e.Location}");
            Console.WriteLine("Tech Competition");
            ReadFromFile();
            Console.ReadLine();
        }

        static void ReadFromFile()
        {
            string filePath = @"C:\test\event.txt";

            // Write "Hackathon" to the file at position 5
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.Seek(5, SeekOrigin.Begin);
                byte[] data = Encoding.UTF8.GetBytes("Hackathon");
                fs.Write(data, 0, data.Length);
            }

            byte[] readData;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Seek to the position where "Hackathon" was written
                fs.Seek(5, SeekOrigin.Begin);

                // Read the bytes representing "Hackathon"
                readData = new byte[9]; // "Hackathon" has 9 bytes in UTF-8 encoding
                fs.Read(readData, 0, readData.Length);
            }

            // Convert the bytes back to string
            string content = Encoding.UTF8.GetString(readData);

            // Extracting first, middle, and last characters
            char firstChar = content[0];
            char middleChar = content[content.Length / 2];
            char lastChar = content[content.Length - 1];

            Console.WriteLine($"In Word: {content}");
            Console.WriteLine($"The First Character is: \"{firstChar}\"");
            Console.WriteLine($"The Middle Character is: \"{middleChar}\"");
            Console.WriteLine($"The Last Character is: \"{lastChar}\"");
        }






    }
}











    







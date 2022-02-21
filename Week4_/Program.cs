using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Serialize
{
    //[Serializable]
    public class Data
    {
        [JsonInclude]
        public int Field;
        public float Prop { get; set; }
        [JsonInclude]
        public List<String> Strings;
        public Dictionary<String, String> Dict { get; set; }

        public Data()
        {
            Dict = new Dictionary<string, string>();
            Dict.Add("key1", "value1");
            Dict.Add("key2", "value2");
            Dict.Add("key3", "value3");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"Field = {Field}");
            sb.Append($"Prop = {Prop}");
            for (int i = 0; i < Strings.Count; ++i)
            {
                sb.Append($"str[{i}] = {Strings[i]}");
            }
            return sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Serialization!");
            var data = new Data
            {
                Field = 10,
                Prop = .1f,
                Strings = new List<String>
                {
                    "Hello",
                    "World"
                }
            };
            Console.WriteLine(data);
            //BinaryDemo(data);
            //XmlDemo(data);
            JsonDemo(data);


        }

        static void JsonDemo(Data data)
        {
            // JSON serialization
            using (var jsonFile = new StreamWriter("ser.json"))
            {
                jsonFile.Write(JsonSerializer.Serialize<Data>(data));
                // JsonSerializer.Serialize(data, data.GetType());
            }

            // JSON Deserialize
            using (var jsonFile = new StreamReader("ser.json"))
            {
                var res = JsonSerializer.Deserialize<Data>(jsonFile.ReadToEnd());
                Console.WriteLine(res);
            }


        }

        static void XmlDemo(Data data)
        {
            // XML serialization
            XmlSerializer xml = new XmlSerializer(data.GetType() /*typeof(Data) */ );
            using (var xmlFile = new StreamWriter("ser.xml"))
            {
                xml.Serialize(xmlFile, data);
            }

            // XML Deserialization
            using (var xmlFile = new StreamReader("ser.xml"))
            {
                var res = (Data)xml.Deserialize(xmlFile);
                Console.WriteLine(res);
            }
        }

        static void BinaryDemo(Data data)
        {
            // Binary serializer
            BinaryFormatter bin = new BinaryFormatter();
            using (var binFile = new FileStream(
                "ser.bin", FileMode.OpenOrCreate))
            {
                bin.Serialize(binFile, data);
            }

            // Deserializing
            using (var binFile = new FileStream(
                "ser.bin", FileMode.Open))
            {
                var res = (Data)bin.Deserialize(binFile);
                Console.WriteLine(res);
            }

        }
    }
}
using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text.Json;

namespace Auth
{
    class Program
    {
        public static List<User> users;
        private const String usersFile = "users.json";
        public static void SignUp(String login, String password)
        {
            // создаем коллекцию
            users = new List<User>();
            // есть ли этот файл?
            if (File.Exists(usersFile))
            {
                bool reg = true;
                // считывает - десериализуем
                using (var jsonFile = new StreamReader(usersFile))
                {
                    List<User> res = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd());

                    foreach (User user in res)
                    {
                        if (user.Login == login && user.Password == password)
                        {
                            Console.WriteLine("Такой пользователь уже существует!");
                            reg = false;
                            break;
                        }
                    }
                }
                if (reg == true)
                {
                    Console.WriteLine("Успешная регестрация!");
                    users.Add(new User()
                    {
                        Login = login,
                        Password = password
                    });
                    using (var writer = new StreamWriter(usersFile))
                    {
                        writer.Write(JsonSerializer.Serialize<List<User>>(users));
                    }
                }
            }
            else // если файла нет - создаем новый
            {
                // создаем пользователя и добавляем в коллекцию
                users.Add(new User()
                {
                    Login = "Admin",
                    Password = "123"
                });
                using (var writer = new StreamWriter(usersFile))
                {
                    writer.Write(JsonSerializer.Serialize<List<User>>(users));
                }
            }
        }
        public static void InitUsers(String login, String password)
        {
            // создаем коллекцию
            users = new List<User>();
            // есть ли этот файл?
            if (File.Exists(usersFile))
            {
                // считывает - десериализуем
                using (var jsonFile = new StreamReader(usersFile))
                {
                    List<User> res = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd());
                    bool auth = false;
                    foreach (User user in res)
                    {
                        if (user.Login == login && user.Password == password)
                        {
                            Console.WriteLine($"Добро пожаловать, {login}");
                            auth = true;
                            break;
                        }
                    }
                    if (auth == false)
                        Console.WriteLine("Такой аккаунт не существует/Логин или пароль введены неверно");
                }
            }
            else // если файла нет - создаем новый
            {
                // создаем пользователя и добавляем в коллекцию
                users.Add(new User()
                {
                    Login = login,
                    Password = password
                });
                Console.WriteLine("Файл создан. Вы первый пользователь. Добро пожаловать, {0}", login);
                using (var writer = new StreamWriter(usersFile))
                {
                    writer.Write(JsonSerializer.Serialize<List<User>>(users));
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Hello, Username");
            Console.WriteLine("1. Зарегестрироваться");
            Console.WriteLine("2. Авторизоваться");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Введите ваш логин: ");
                        String login = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        String password1 = Console.ReadLine();
                        Console.WriteLine("Повторите пароль: ");
                        String password2 = Console.ReadLine();
                        if (password1 == password2)
                        {
                            SignUp(login, password1);
                        }
                        else
                        {
                            Console.WriteLine("Пароли не совпадают.");
                        }
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите логин: ");
                        String login = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        String password = Console.ReadLine();
                        InitUsers(login, password);
                        break;
                    }
            }
        }
    }
}
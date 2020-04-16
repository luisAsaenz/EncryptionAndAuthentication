using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace EncryptionAndAuthentication
{
    class Program
    {

        public static Dictionary<string, string> database = new Dictionary<string, string>();
      
        static void Main(string[] args)
        {
            MainPage();
 
        }

        private static void MainPage()
        {
            Console.WriteLine("PASSWORD AUTHENTICATION SYSTEM");
            Console.WriteLine("\n\n1. Establish an account");
            Console.WriteLine("2. Authenticate a user");
            Console.WriteLine("3. Exit System");
            Console.Write("\n\n\nEnter Selection: ");

            ConsoleKeyInfo cki = Console.ReadKey();

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Console.Write("Input Username: ");
                    string username = Console.ReadLine();

                    Console.Write("Input PASSWORD: ");
                    string password = Console.ReadLine();
                    if (NoDuplicates(username) == true)
                    {
                        Console.WriteLine("This username already exists. Please be more creative..");
                    }
                    else
                    {

                        using (MD5 Md5Hash = MD5.Create())
                        {
                            string hash = GetMd5Hash(Md5Hash, password);
                            

                            database.Add(username, hash);
                            
                            //Console.WriteLine($"Input Password was: {password}\nEncrypted Password is: {hash}");

                        }
                        Console.WriteLine("Thank you for creating an account with us.");
                        Thread.Sleep(3000);
                        Console.Clear();

                        MainPage();
                    }
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Console.Write("Input USERNAME:");
                    string user = Console.ReadLine();

                    if (NoDuplicates(user) == false)
                    {
                        Console.WriteLine("No user with this name exists. Please check spelling..");
                    }
                    else
                    {
                        Console.Write("Input PASSWORD: ");
                        string pass = Console.ReadLine();
                        using (MD5 Md5Hash = MD5.Create())
                        {
                            string hash = GetMd5Hash(Md5Hash, pass);
                            if (Authenticate(user, hash) == true)
                            {
                                
                                Console.WriteLine("The account has been authenticated");
                                Thread.Sleep(3000);
                                Console.Clear();
                                MainPage();
                            }
                            else
                            {
                                Console.WriteLine("The user has not been authenticated..");
                                Thread.Sleep(5000);
                                Console.Clear();
                                MainPage();
                            }
                        }
                    }
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("3. system exit");
                    Environment.Exit(0);
                    break;
            }
        }

        private static bool Authenticate(string user, string hash)
        {
            bool auth = false;
            if (database.ContainsKey(user))
            {
                if (database[user] == hash) { auth = true; }
            }
            return auth;
        }

        private static bool NoDuplicates(string username)
        {
            bool verify = false;
            if (database.ContainsKey(username))
            {
                verify = true;
            }
            return verify;
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i ++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}

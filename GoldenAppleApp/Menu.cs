using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenAppleApp
{
    class Menu
    {
        public static int MainMenu()
        {
            int result = -1;
            ConsoleKeyInfo cki;
            bool cont = false;
            do
            {

                Console.Clear();
                Helper.WriteHeader("Main Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. Queries of data");
                Console.WriteLine("3. Data Entry Menu");
                Console.WriteLine("4. Data Modification Menu");
                Console.WriteLine("9. Exit");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        QueriesOfDataMenu();
                    }
                    else if (result == 3)
                    {
                        DataEntryMenu();
                    }
                    else if (result == 4)
                    {
                        DataModificationMenu();
                    }
                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            }
            while (!cont);
            return result;
        }

        static void QueriesOfDataMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                Helper.WriteHeader("Queries of data Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. List All Datas");
             //   Console.WriteLine("2. Aggregation");
                Console.WriteLine("3. Filter");
               

                Console.WriteLine("9. Exit Menu");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        Program.DisplayAllDatas();
                        Console.WriteLine("\r\nAny key to continue...");
                        Console.ReadKey();
                    }
                    else if (result == 2)
                    {
                        //Aggregation("Modify");
                    }

                    else if (result == 3)
                    {
                        FilterMenu();
                    }
                    

                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }
        static void DataEntryMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                Helper.WriteHeader("Data Entry Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. Add a New Data");
                Console.WriteLine("2. Add a New SubData");//
                Console.WriteLine("3. Add a New Property");//
                Console.WriteLine("9. Exit Menu");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        Program.AddData();
                    }
                    else if (result == 2)
                    {
                        Program.AddSubData();
                    }
                    else if (result == 3)
                    {
                        Program.AddNewProperty();
                    }
                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }
        static void DataModificationMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                Helper.WriteHeader("Data Modification Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. Delete Datas");
                Console.WriteLine("2. Modify Datas");

                Console.WriteLine("8. Delete All Datas");
                Console.WriteLine("9. Exit Menu");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        Program.SelectDatas("Delete");
                    }
                    else if (result == 2)
                    {
                        Program.SelectDatas("Modify");
                    }

                    else if (result == 8)
                    {
                        Program.DeleteAllDatas();
                    }

                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }
        
        static void FilterMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                Helper.WriteHeader("Queries of data Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. True datas for Property");
                Console.WriteLine("2. False datas for Property");
                Console.WriteLine("3. All subdatas for for Property");
               // Console.WriteLine("4. Sum of Value for Property");

                Console.WriteLine("9. Exit Menu");
                cki = Console.ReadKey();
                string title = "";
                
                try
                {
                    bool flag = false;
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        flag = true;
                    }

                    if (result == 2)
                    {
                        flag = false;
                    }

                    if (result == 1 || result == 2)
                    {
                        Program.DisplayProperties(Program.GetProperties());
                        Console.WriteLine("Enter the Property title you would like to display.");
                        title = Console.ReadLine();
                        if (Program.GetProperties().Exists(x => x.Title == title))
                        {
                            Program.DisplayAllDatasForProperty(title, flag);
                        }
                        else
                        {
                            Console.WriteLine("Title doesn't exists.");
                        }
                        Console.WriteLine("\r\nAny key to continue...");
                        Console.ReadKey();
                    }

                    else if (result == 3)
                    {
                        Program.DisplayProperties(Program.GetProperties());
                        Console.WriteLine("Enter the Property title you would like to display.");
                        title = Console.ReadLine();
                        if (Program.GetProperties().Exists(x => x.Title == title))
                        {
                            Program.DisplayAllSubDatasForProperty(title);
                        }
                        else
                        {
                            Console.WriteLine("Title doesn't exists.");
                        }
                        Console.WriteLine("\r\nAny key to continue...");
                        Console.ReadKey();
                    }

                   
                    else if (result == 9)
                    {
                        // We are exiting so nothing to do
                        cont = true;
                    }
                }
                catch (System.FormatException)
                {
                    // a key that wasn't a number
                }
            } while (!cont);
        }
    }
}

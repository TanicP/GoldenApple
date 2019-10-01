using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using GoldenAppleApp;

namespace GoldenAppleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            int result = -1;
            while (result != 9)
            {
                result = MainMenu();
            }
        }
        static int MainMenu()
        {
            int result = -1;
            ConsoleKeyInfo cki;
            bool cont = false;
            do
            {
                Console.Clear();
                WriteHeader("Main Menu");
                Console.WriteLine("\r\nPlease select from the list below for what you would like to do");
                Console.WriteLine("1. List All Datas");
                Console.WriteLine("3. Data Entry Menu");
                Console.WriteLine("4. Data Modification Menu");
                Console.WriteLine("9. Exit");
                cki = Console.ReadKey();
                try
                {
                    result = Convert.ToInt16(cki.KeyChar.ToString());
                    if (result == 1)
                    {
                        DisplayAllDatas();
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
        static void DataEntryMenu()
        {
            ConsoleKeyInfo cki;
            int result = -1;
            bool cont = false;
            do
            {
                Console.Clear();
                WriteHeader("Data Entry Menu");
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
                        AddData();
                    }
                    else if (result == 2)
                    {
                        //  AddSubData();
                    }
                    else if (result == 3)
                    {
                        //AddNewProperty();
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
                WriteHeader("Data Modification Menu");
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
                        SelectDatas("Delete");
                    }
                    else if (result == 2)
                    {
                        SelectDatas("Modify");
                    }

                    else if (result == 8)
                    {
                        DeleteAllDatas();
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

        //helper
        static void WriteHeader(string headerText)
        {
            Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) +
            headerText.Length / 2) + "}", headerText));
        }
        static bool ValidateYorN(string entry)
        {
            bool result = false;
            if (entry.ToLower() == "y" || entry.ToLower() == "n")
            {
                result = true;
            }
            return result;
        }
        static bool ValidateTorF(string entry)
        {
            bool result = false;
            if (entry.ToLower() == "t" || entry.ToLower() == "f")
            {
                result = true;
            }
            return result;
        }
        static bool CheckForExistingDatas(string dataname)
        {
            bool exists = false;
            using (var context = new ApplicationContext())
            {
                var data = context.Datas.Where(d => d.Name == dataname);
                if (data.Count() > 0)
                {
                    exists = true;
                }
            }
            return exists;
        }
        static Data GetDataByName(string name)
        {
            var context = new ApplicationContext();
            Data os = context.Datas.FirstOrDefault(i => i.Name == name);
            return os;
        }


        //functions
        static Data AddData()
        {
            Console.Clear();
            ConsoleKeyInfo cki;
            string result;
            bool cont = false;
            Data data = new Data();
            string dataName = "";
            WriteHeader("Add New line in Data");
            do
            {
                Console.WriteLine("Enter the Name");
                dataName = Console.ReadLine();
                if (dataName.Length >= 2)
                {
                    cont = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid name of at least 2 characters.\r\nPress and key to continue...");
                    Console.ReadKey();
                }
            } while (!cont);
            data.Name = dataName;

            cont = false;
            int Count;
            do
            {
                Console.WriteLine("Enter Count ");
                Count = Convert.ToInt32(Console.ReadLine());
                cont = true;
            } while (!cont);
            data.Count = Count;

            cont = false;
            do
            {
                Console.WriteLine("Enter flag [t or f]");
                cki = Console.ReadKey();
                result = cki.KeyChar.ToString();
                cont = ValidateTorF(result);
            } while (!cont);
            if (result.ToLower() == "f")
            {
                data.Flag = true;
            }
            else
            {
                data.Flag = false;
            }

            do
            {
                Console.Clear();
                Console.WriteLine($"You entered {data.Name} as the Data Name\r\nCount you entered {data.Count}.\r\n Flag you entered {data.Flag}.\r\nDo you wish to continue? [y or n]");
                cki = Console.ReadKey();
                result = cki.KeyChar.ToString();
                cont = ValidateYorN(result);
            } while (!cont);
            if (result.ToLower() == "y")
            {
                bool exists = CheckForExistingDatas(data.Name);
                if (exists)
                {
                    Console.WriteLine("\r\nData already exists in the database\r\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    using (var context = new ApplicationContext())
                    {
                        Console.WriteLine("\r\nAttempting to save changes...");
                        context.Datas.Add(data);
                        int i = context.SaveChanges();
                        if (i == 1)
                        {
                            Console.WriteLine("Contents Saved\r\nPress any key to continue...");
                            Console.ReadKey();
                        }
                    }
                }
            }
            return data;
        }
        static void DisplayAllDatas()
        {
            Console.Clear();
            Console.WriteLine("Datas");
            using (var context = new ApplicationContext())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                foreach (var data in context.Datas.ToList())
                {
                    Console.WriteLine($"Name: {data.Name,-39}\tCount: {data.Count,-10}\tFalg: {data.Flag}");
                }
            }
            Console.WriteLine("\r\nAny key to continue...");
            Console.ReadKey();
        }
        static void DeleteAllDatas()
        {
            using (var context = new ApplicationContext())
            {
                var data = (from d in context.Datas
                            select d);
                // one by one
                /*List<OperatingSys> os = context.OperatingSys.Where(o => o.StillSupported == false).ToList();
                foreach(OperatingSys o in os)
                {
                    context.Remove(o);
                    
                }
                context.SaveChanges();*/

                Console.WriteLine("\r\nDeleting all lines in Data...");
                context.Datas.RemoveRange(data);
                int i = context.SaveChanges();
                Console.WriteLine($"We have deleted {i} records");
                Console.WriteLine("Hit any key to continue...");
                Console.ReadKey();
            }
        }
        static void SelectDatas(string operation)
        {
            ConsoleKeyInfo cki;
            Console.Clear();
            WriteHeader($"{operation} an Existing Datas");
            Console.WriteLine($"{"ID",-35}|{"Name",-20}|{"Count",-20}|Flag");
            Console.WriteLine("-------------------------------------- -----------");
            using (var context = new ApplicationContext())
            {
                List<Data> lDatas = context.Datas.ToList();
                foreach (Data d in lDatas)
                {
                    Console.WriteLine($"{d.Id,-20}|{d.Name,-20}|{ d.Count,-20}|{d.Flag}");
                }
            }
            Console.WriteLine($"\r\nEnter the Name of the record you wish to { operation} and hit Enter\r\nYou can hit Esc to exit this menu");
            bool cont = false;
            string dataName = "";
            do
            {
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Escape)
                {
                    cont = true;
                    dataName = "";
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (dataName.Length >= 2)
                    {
                        cont = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter an Name that is at least 2 characters.");
                    }
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                    try
                    {
                        dataName = dataName.Substring(0, dataName.Length - 1);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        // at the 0 position, can't go any further back
                    }
                }
                else
                {
                    dataName = Console.ReadLine();
                }
                    
            } while (!cont);
            
            if ("Delete" == operation)
            {
                  DeleteData(dataName);
            }
            else if ("Modify" == operation)
            {
                 ModifyData(dataName);
            }
        }
        static void DeleteData(string name)
        {
            Data d = GetDataByName(name);
            if (d != null)
            {
                Console.WriteLine($"\r\nAre you sure you want to delete {d.Name}?[y or n]");
                ConsoleKeyInfo cki;
                string result;
                bool cont;
                do
                {
                    cki = Console.ReadKey(true);
                    result = cki.KeyChar.ToString();
                    cont = ValidateYorN(result);
                } while (!cont);

                if ("y" == result.ToLower())
                {
                    Console.WriteLine("\r\nDeleting record");

                    using (var context = new ApplicationContext())
                    {
                        context.Remove(d);
                        context.SaveChanges();
                    }
                    Console.WriteLine("Record Deleted");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Delete Aborted\r\nHit any key to continue...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("\r\nOperating System Not Found!");
                Console.ReadKey();
               // SelectDatas("Delete");
            }
        }
        static void ModifyData(string name)
        {
            Data data = GetDataByName(name);
            Console.Clear();
            char operation = '0';
            bool cont = false;
            ConsoleKeyInfo cki;
            WriteHeader("Update Data");
            if (data != null)
            {
                Console.WriteLine($"Name: {data.Name,-39}\tCount: {data.Count,-10}\tFalg: {data.Flag}");
                
                Console.WriteLine("To modify the name press 1\r\nTo modify if the Count press 2\r\nTo modify if the Flag press 3");
                Console.WriteLine("Hit Esc to exit this menu");
                do
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        cont = true;
                    else
                    {
                        if (char.IsNumber(cki.KeyChar))
                        {
                            if (cki.KeyChar == '1')
                            {
                                Console.WriteLine("Updated Data Name: ");
                                operation = '1';
                                cont = true;
                            }
                            else if (cki.KeyChar == '2')
                            {
                                Console.WriteLine("Update Data count: ");
                                operation = '2';
                                cont = true;
                            }
                            else if (cki.KeyChar == '3')
                            {
                                Console.WriteLine("Update Data Flag [t or f]: ");
                                operation = '3';
                                cont = true;
                            }
                        }
                    }
                } while (!cont);
            }
            if (operation == '1')
            {
                string dataName;
                cont = false;
                do
                {
                    dataName = Console.ReadLine();
                    if (dataName.Length >= 2)
                    {
                        cont = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid name of at least 2 characters.\r\nPress and key to continue...");
                        Console.ReadKey();
                    }
                } while (!cont);
                data.Name = dataName;
            }
            else if (operation == '2')
            {
                int Count;
                do
                {
                    Count = Convert.ToInt32(Console.ReadLine());
                    cont = true;
                } while (!cont);
                data.Count = Count;
                
            }
            else if (operation == '3')
            {
                string k;
                do
                {
                    cki = Console.ReadKey(true);
                    k = cki.KeyChar.ToString();
                    cont = ValidateTorF(k);
                } while (!cont);

                if (k == "t")
                {
                    data.Flag = true;
                }
                else
                {
                    data.Flag = false;
                }
            }
            using (var context = new ApplicationContext())
            {
                var o = context.Datas.FirstOrDefault(i => i.Id == data.Id);
                if (o != null)
                {
                    // just making sure
                    o.Name = data.Name;
                    o.Flag = data.Flag;
                    o.Count = data.Count;

                    Console.WriteLine("\r\nUpdating the database...");
                    context.SaveChanges();
                    Console.WriteLine("Done!\r\nHit any key to continue...");
                }
            }
            Console.ReadKey();
        }
    }
}

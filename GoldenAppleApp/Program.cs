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
                result = Menu.MainMenu();
            }
        }


        public static List<Property> GetProperties()
        {
            List<Property> lProperty = new List<Property>();
            using (var context = new ApplicationContext())
            {
                lProperty = context.Propertys.ToList();
            }
            return lProperty;
        }
        public static Property GetProperty(string title)
        {
            Property Property;
            List<Property> lProperty = GetProperties();
            using (var context = new ApplicationContext())
            {
                Property = lProperty.Find(x => x.Title == title);
            }
            return Property;
        }
        public static void DisplayProperties(List<Property> lProperty)
        {
            foreach (Property pr in lProperty)
            {
                Console.WriteLine($"\n Title: {pr.Title}");
            }
        }

        public static Data AddData()
        {
            Console.Clear();
            ConsoleKeyInfo cki;
            string result;
            bool cont = false;
            Data data = new Data();
            string dataName = "";
            string title = "";
            Helper.WriteHeader("Add New line in Data");
            List<Property> lProperty = GetProperties();
            Property Property = new Property();
            if (lProperty.Count == 0)
            {
                Property = AddNewProperty();
            }
            else
            {
                DisplayProperties(lProperty);
                Console.WriteLine("Enter the Property title you would like to use.");
                Console.WriteLine("To add a new Property enter [a]");
                do
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.A)
                    {
                        Property = AddNewProperty();
                        cont = true;
                    }
                    else
                    {
                        title = Console.ReadLine();
                        if (lProperty.Exists(x => x.Title == title))
                        {
                            Property = lProperty.Find(x => x.Title == title);
                            cont = true;
                        }
                        else
                        {
                            // No match, could add a counter here and after a certain number of attempts
                            // Add in some error handling
                        }
                    }
                } while (!cont);
            }
            data.PropId = Property.Id;
            do
            {
                Console.WriteLine("Enter the Name of Data");
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
                cont = Helper.ValidateTorF(result);
            } while (!cont);
            if (result.ToLower() == "f")
            {
                data.Flag = false;
            }
            else
            {
                data.Flag = true;
            }

            do
            {
                Console.Clear();
                Console.WriteLine($"You entered {data.Name} as the Data Name\r\nCount you entered {data.Count}.\r\n Flag you entered {data.Flag}.\r\nDo you wish to continue? [y or n]");
                cki = Console.ReadKey();
                result = cki.KeyChar.ToString();
                cont = Helper.ValidateYorN(result);
            } while (!cont);
            if (result.ToLower() == "y")
            {
                bool exists = Functions.CheckForExistingDatas(data.Name);
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
        public static Property AddNewProperty()
        {
            Console.Clear();
            ConsoleKeyInfo cki;
            string result;
            bool cont = false;
            Property pr = new Property();
            string title = "";
            Helper.WriteHeader("Add New line in Property");
            do
            {
                Console.WriteLine("Enter the title");
                title = Console.ReadLine();
                if (title.Length >= 2)
                {
                    cont = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid title of at least 2 characters.\r\nPress and key to continue...");
                    Console.ReadKey();
                }
            } while (!cont);
            pr.Title = title;
            do
            {
                Console.Clear();
                Console.WriteLine($"You entered {pr.Title} as the Property title.\r\nDo you wish to continue? [y or n]");
                cki = Console.ReadKey();
                result = cki.KeyChar.ToString();
                cont = Helper.ValidateYorN(result);
            } while (!cont);
            if (result.ToLower() == "y")
            {
                bool exists = Functions.CheckForExistingProperties(pr.Title);//
                if (exists)
                {
                    Console.WriteLine("\r\nProperty already exists in the database\r\nPress any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    using (var context = new ApplicationContext())
                    {
                        Console.WriteLine("\r\nAttempting to save changes...");
                        context.Propertys.Add(pr);
                        int i = context.SaveChanges();
                        if (i == 1)
                        {
                            Console.WriteLine("Contents Saved\r\nPress any key to continue...");
                            Console.ReadKey();
                        }
                    }
                }
            }
            return pr;
        }
        public static SubData AddSubData()
        {
            Console.Clear();
            ConsoleKeyInfo cki;
            string result;
            bool cont = false;
            SubData SubData = new SubData();
            decimal Value;
            string dataName = "";

            Helper.WriteHeader("Add New line in SubData");
            List<Data> lData = Functions.GetDatas();
            Data Data = new Data();
            if (lData.Count == 0)
            {
                Data = AddData();
            }
            else
            {
                DisplayAllDatas();
                Console.WriteLine("Enter the Data Name you would like to use.");
                Console.WriteLine("To add a new Data enter [a]");
                do
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.A)
                    {
                        Data = AddData();
                        cont = true;
                    }
                    else
                    {
                        dataName = Console.ReadLine();
                        if (lData.Exists(x => x.Name == dataName))
                        {
                            Data = lData.Find(x => x.Name == dataName);
                            cont = true;
                        }
                        else

                        {
                            Console.WriteLine("There is no data with such name");
                        }
                    }
                } while (!cont);
            }
            SubData.dataId = Data.Id;
            do
            {
                Console.WriteLine("Enter the Value of SubData");

                Value = Convert.ToDecimal(Console.ReadLine());
                cont = true;

            } while (!cont);
            SubData.Value = Value;
            do
            {
                Console.Clear();
                Console.WriteLine($"You entered {SubData.Value} as the SubData value.\r\nDo you wish to continue? [y or n]");
                cki = Console.ReadKey();
                result = cki.KeyChar.ToString();
                cont = Helper.ValidateYorN(result);
            } while (!cont);
            if (result.ToLower() == "y")
            {

                using (var context = new ApplicationContext())
                {
                    Console.WriteLine("\r\nAttempting to save changes...");
                    context.SubDatas.Add(SubData);
                    int i = context.SaveChanges();
                    if (i == 1)
                    {
                        Console.WriteLine("Contents Saved\r\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }

            }
            return SubData;
            ///
        }
        public static void DisplayAllDatas()
        {
            Console.Clear();
            Console.WriteLine("Datas");
            using (var context = new ApplicationContext())
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                foreach (var prop in context.Propertys.ToList())
                {
                    Console.WriteLine($"Property: {prop.Title}");
                    foreach (var data in context.Datas.Where(d => d.PropId == prop.Id).ToList())
                    {
                        Console.WriteLine($"\t Name: {data.Name,-30}\tCount: {data.Count,-10}\tFalg: {data.Flag}");
                        foreach (var subData in context.SubDatas.Where(s => s.dataId == data.Id).ToList())
                        {
                            Console.WriteLine($"\t\t Value: {subData.Value}");
                        }
                    }
                }
            }

        }
        public static void DeleteAllDatas()
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
        public static void SelectDatas(string operation)
        {
            ConsoleKeyInfo cki;
            bool exit = false;
            Console.Clear();
            Helper.WriteHeader($"{operation} an Existing Datas");
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
                    exit = true;
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
            if (!exit)
            {
                if ("Delete" == operation)
                {
                    DeleteData(dataName);
                }
                else if ("Modify" == operation)
                {
                    ModifyData(dataName);
                }
            }
        }
        static void DeleteData(string name)
        {
            Data d = Functions.GetDataByName(name);
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
                    cont = Helper.ValidateYorN(result);
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
            Data data = Functions.GetDataByName(name);
            Console.Clear();
            char operation = '0';
            bool cont = false;
            bool exit = false;
            ConsoleKeyInfo cki;
            Helper.WriteHeader("Update Data");
            if (data != null)
            {
                Console.WriteLine($"Name: {data.Name,-39}\tCount: {data.Count,-10}\tFalg: {data.Flag}");

                Console.WriteLine("To modify the name press 1\r\nTo modify if the Count press 2\r\nTo modify if the Flag press 3");
                Console.WriteLine("Hit Esc to exit this menu");
                do
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                    {
                        cont = true;
                        exit = true;
                    }
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
            if (!exit)
            {
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
                        cont = Helper.ValidateTorF(k);
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
            }
            Console.ReadKey();
        }

        public static void DisplayAllDatasForProperty(string title, Boolean flag)
        {
            Console.Clear();
            Console.WriteLine("\r\nRetrieving Property information.");
            using (ApplicationContext context = new ApplicationContext())
            {
                var prd = from d in context.Datas
                          where d.Flag == flag
                          join p in context.Propertys on d.PropId equals p.Id
                          where p.Title == title
                          join sb in context.SubDatas on d.Id equals sb.dataId into lJoin
                          from sb in lJoin.DefaultIfEmpty()
                          select new { Poperty = p.Title, dataName = d.Name, count = d.Count, Value = sb != null ? sb.Value : (decimal?)null };
                foreach (var p in prd)
                {
                    Console.WriteLine($"\t Poperty: {p.Poperty} Name: {p.dataName}\tCount: {p.count} \tValue: {p.Value}");
                }

                /* Property Property = GetProperty(title);

                 foreach (var data in context.Datas.Where(d => d.PropId == Property.Id).ToList())
                 {
                     Console.WriteLine($"\t Name: {data.Name,-30}\tCount: {data.Count,-10}\tFalg: {data.Flag}");
                     foreach (var subData in context.SubDatas.Where(s => s.dataId == data.Id).ToList())
                     {
                         Console.WriteLine($"\t Value: {subData.Value}");
                     }
                 }*/
            }
        }

        public static void DisplayAllSubDatasForProperty(string title)
        {
            Console.Clear();
            Console.WriteLine("\r\nRetrieving Property information.");
            using (ApplicationContext context = new ApplicationContext())
            {
                var prd = from d in context.Datas
                          join p in context.Propertys on d.PropId equals p.Id
                          where p.Title == title
                          join sb in context.SubDatas on d.Id equals sb.dataId
                          select new { Poperty = p.Title, Value = sb != null ? sb.Value : (decimal?)null };
                foreach (var p in prd)
                {
                    Console.WriteLine($"\t Poperty: {p.Poperty}\tValue: {p.Value}");
                }
            }
        }

        public static void DisplaySumValueForProperty(string title, Boolean flag)
        {
            Console.Clear();
            Console.WriteLine("\r\nRetrieving Property information.");
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var prop in context.Propertys.Where(p => p.Title == title))
                {
                    var prdx = context.SubDatas//.Join(context.Datas, sb => sb.dataId, d => d.id )
                        .GroupBy(d => d.Id)
                        .Select(sd => new SubData { Value = sd.Sum(s => s.Value) });
                    foreach (var p in prdx)
                    {
                        Console.WriteLine($" \tSum of Value: {p.Value}");
                    }

                    var prd = from sb in context.SubDatas
                                  //  where d.Flag == flag
                              join d in context.Datas on sb.dataId equals d.Id
                                 where d.PropId == prop.Id
                              group new { sb, d } by d.Id into grouped
                              select new { grouped.Key, sum = grouped.Select(x => x.sb.Value).Sum(), title = grouped.Select(x => x.d.Name) };

                    foreach (var p in prd)
                    {
                        Console.WriteLine($"\t Poperty: {p.title} \tSum of Value: {p.sum}");
                    }
                }
            }
        }

    }
    class Functions
    {
        public static bool CheckForExistingProperties(string prname)
            {
                bool exists = false;
                using (var context = new ApplicationContext())
                {
                    var data = context.Propertys.Where(p => p.Title == prname);
                    if (data.Count() > 0)
                    {
                        exists = true;
                    }
                }
                return exists;
            }
        public static bool CheckForExistingDatas(string dataname)
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
        public static Data GetDataByName(string name)
        {
            var context = new ApplicationContext();
            Data os = context.Datas.FirstOrDefault(i => i.Name == name);
            return os;
        }
        public static List<Data> GetDatas()
        {
            List<Data> lData = new List<Data>();
            using (var context = new ApplicationContext())
            {
                lData = context.Datas.ToList();
            }
            return lData;
        }
    }

}

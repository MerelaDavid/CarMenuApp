using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;

namespace MenuJancar2
{
    class Program
    {
        static void Main(string[] args)
        {
            int LocalCarID = 1;
            string command;
            string FilePath = @"C:\Users\David\Documents/MyCars.txt";                                                                                                                                                   
            Dictionary<int, Car> Cars = new Dictionary<int, Car>();
            String[] AllCommands = new string[] {"1","2","3","4","5","6","7","8"};

            command = NextCommand();
            

            while (true)
            {
                if (command.Equals("1"))
                {
                    Boolean ModelInSystem = false;
                    string brand;
                    string model;
                    string power;
                    string speed;
                    string weight;

                    string[] CarInfo = AddObj();

                    brand = CarInfo[0];
                    model = CarInfo[1];
                    power = CarInfo[2];
                    speed = CarInfo[3];
                    weight = CarInfo[4];

                    foreach (int key in Cars.Keys)
                    {
                        if (Cars[key].Model.Equals(model) && Cars[key].Brand.Equals(brand))
                        {
                            ModelInSystem = true;
                        }
                    }

                    if (ModelInSystem == false)
                    {
                        Cars.Add(Cars.Count, new Car() { Brand = brand, Model = model, Power = power, TopSpeed = speed, Weight = weight, CarId = Cars.Count });
                        LocalCarID++;

                        Console.WriteLine();
                        Console.WriteLine("ADD OK");
                        Console.WriteLine();

                    }

                    if (ModelInSystem == true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Model is already in system. You can add new Model or change this one.");
                        Console.WriteLine("ADD NOK");
                        Console.WriteLine();

                    }

                    command = NextCommand();

                }
                if (command.Equals("2"))
                {
                  String printcommand =  PrintObj();

                    if (printcommand.Equals("1"))
                    {
                        PrintObjectNum(Cars);
                    }

                    if (printcommand.Equals("2"))
                    {
                        PrintAllObj(Cars);
                    }

                    if (printcommand.Equals("3"))
                    {
                        PrintOneObj(Cars);
                    }

                    command = NextCommand();
                }
                if (command.Equals("3"))
                {
                    string CommandDelete = DeleteObj();

                    if (CommandDelete.Equals("1"))
                    {

                        Console.WriteLine();
                        Console.WriteLine("Izberite avtomobil, ki ga zelite odstraniti");
                        Console.WriteLine();

                        foreach (int key in Cars.Keys)
                        {
                            Console.WriteLine(key + ": " + Cars[key].ToString());
                        }

                        int Key = Int32.Parse(Console.ReadLine());
                        
                        Cars.Remove(Key);
                        Console.WriteLine("Model has been removed");
                        Console.WriteLine();
                    }

                    if (CommandDelete.Equals("2"))
                    {
                        Cars.Clear();
                    }

                    command = NextCommand();

                }
                if (command.Equals("4"))
                {
                    string[] ChangeInfo = ChangeObj(Cars);

                    int Key = Int32.Parse(ChangeInfo[0]);
                    string ChangeCommand = ChangeInfo[1];
                    string ChangeValue = ChangeInfo[2];

                        if (ChangeCommand.Equals("1"))
                        {
                            Cars[Key].Power = ChangeValue;
                            Console.WriteLine("power changed to: " + ChangeValue);
                        }

                        if (ChangeCommand.Equals("2"))
                        {
                            Cars[Key].TopSpeed = ChangeValue;
                            Console.WriteLine("Speed changed to: " + ChangeValue);
                        }

                        if (ChangeCommand.Equals("3"))
                        {

                            Cars[Key].Weight = ChangeValue;
                            Console.WriteLine("Weight changed to: " + ChangeValue);
                        }

                    command = NextCommand();

                }
                if (command.Equals("5"))
                {
                    int RemoveId = SaveObj(Cars, FilePath);
                    Cars.Remove(RemoveId);
                    command = NextCommand();
                }
                if (command.Equals("6"))
                {
                    string Car;
                    string Brand;
                    string Model;
                    string Weight;
                    string Power;
                    string TopSpeed;
                    string[] CarInfo;
                    string[] ReadFile =LoadObj(FilePath);

                    try
                    {
                        if (!File.Exists(FilePath))
                        {
                            Console.WriteLine("File at path: " + FilePath + "do not exist");
                        }

                        if (ReadFile.Length >= 1)
                        {
                            for (int i = 0; i < ReadFile.Length; i++)
                            {
                                Car = ReadFile[i];
                                CarInfo = Car.Split(' ');
                                int NewId = Cars.Keys.Max()+1;
                                Brand = CarInfo[0];
                                Model = CarInfo[1];
                                Power = CarInfo[2];
                                TopSpeed = CarInfo[3];
                                Weight = CarInfo[4].Replace(";", "");

                                if (InSystem(Cars, Brand, Model) == false)
                                {
                                    Cars.Add(NewId, new Car() { Brand = Brand, Model = Model, Power = Power, TopSpeed = TopSpeed, Weight = Weight, CarId = NewId });
                                    Console.WriteLine("Car added to system");
                                }
                            }
                        }

                        File.Delete(FilePath);
                        Console.WriteLine("File at path {0} is deleted", FilePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine();
                    }

                    command = NextCommand();

                }
                if (command.Equals("7"))
                {
                    SortObj(Cars);
                    command = NextCommand();
                }

                if (command.Equals("8"))
                {
                    Exit();
                }

                if(!AllCommands.Contains(command))
                {
                    command = NextCommand();
                }
            }


        }
        static String NextCommand()
        {
            Console.WriteLine("Car Manager");
            Console.WriteLine();
            Console.WriteLine("1. Dodaj nov vnos");
            Console.WriteLine("2. Izpis podatkov");
            Console.WriteLine("3. Izbris podatkov");
            Console.WriteLine("4. Spremeni podatek");
            Console.WriteLine("5. Zapiši v datoteko");
            Console.WriteLine("6. Beri iz datoteke");
            Console.WriteLine("7. Sortiraj objekte");
            Console.WriteLine("8. Izhod");
            String command = Console.ReadLine();
            return command;
        }

        static string[] AddObj()
        {
            string brand;
            string model;
            string power;
            string speed;
            string weight;

            Console.WriteLine();
            Console.WriteLine("Vnesite podatke o avtomobilu");
            Console.WriteLine();
            Console.WriteLine("Znamka avtomobila: ");
            brand =Console.ReadLine();
            Console.WriteLine("Model avtomobila: ");
            model = Console.ReadLine();
            Console.WriteLine("Moč avtomobila[Hp]: ");
            power = Console.ReadLine();
            Console.WriteLine("Max hitrost avtomobila[km/h]: ");
            speed = Console.ReadLine();
            Console.WriteLine("Teža avtomobila[kg]: ");
            weight = Console.ReadLine();

            string[] CarInfo = new string[] {brand,model,power,speed,weight};


            return CarInfo;
        }

        static string PrintObj()
        {
            Console.WriteLine();
            Console.WriteLine("Izpis avtomobilov");
            Console.WriteLine();
            Console.WriteLine("1. Izpis stevila avtomobilov");
            Console.WriteLine("2. Izpis vseh avtomobilov");
            Console.WriteLine("3. Izpis enega avtomobila");
            String printcommand = Console.ReadLine();

            return printcommand;
        }

        static void PrintOneObj(Dictionary<int, Car> Cars)
        {
            string brand;
            String model;

            Console.WriteLine();
            Console.WriteLine("Izpis enega avtomobila");
            Console.WriteLine();
            Console.WriteLine("1. Vnesite znamko avtomobila");
            brand = Console.ReadLine();
            Console.WriteLine("2. Vnesite model avtomobila");
            model = Console.ReadLine();

            foreach (int key in Cars.Keys)
            {
                if (Cars[key].Brand.Equals(brand) && Cars[key].Model.Equals(model))
                {
                    Console.WriteLine(Cars[key].PrintWithUnit());
                }
            }
        }

        static void PrintAllObj(Dictionary<int, Car> Cars)
        {
            foreach(int key in Cars.Keys)
            {
                Console.WriteLine(Cars[key].PrintWithUnit());
            }
        }

        static void PrintObjectNum(Dictionary<int, Car> Cars)
        {
            if (Cars.Count() > 1)
            {
                Console.WriteLine();
                Console.WriteLine("There are " + Cars.Count() + "cars in the system");
                Console.WriteLine();
            }

            if (Cars.Count() <= 1)
            {
                Console.WriteLine();
                Console.WriteLine("There is " + Cars.Count() + "car in the system");
                Console.WriteLine();
            }

        }

        static string DeleteObj()
        {
            string CommandDelete;

            Console.WriteLine("Izbris avtomobilov iz sistema");
            Console.WriteLine();
            Console.WriteLine("1. Izbris enega avtomobila");
            Console.WriteLine("2. Izbris vseh avtomobilov");

            CommandDelete = Console.ReadLine();

            return CommandDelete;


        }

        static string[] ChangeObj(Dictionary<int, Car> Cars)
        {
            string ChangeCommand;
            string ChangeValue;
            

            Console.WriteLine("Sprememba avtomobila v sistemu");
            Console.WriteLine();
            Console.WriteLine("Izberite avtomobil, ki ga zelite spremeniti");
            Console.WriteLine();

            foreach (int key in Cars.Keys)
            {
                Console.WriteLine(key + ": " + Cars[key].ToString());
            }

            string ChangeKey = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Kaj želite spremeniti");
            Console.WriteLine();
            Console.WriteLine("1. Moč");
            Console.WriteLine("2. hitrost");
            Console.WriteLine("3. težo");

            ChangeCommand = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Vpišite novo vrednost atributa: ");
            ChangeValue = Console.ReadLine();
            Console.WriteLine();



            string[] ChangeInfo = new string[] {ChangeKey, ChangeCommand, ChangeValue};

            return ChangeInfo;



        }

        static int SaveObj(Dictionary<int, Car> Cars, string path)
        {
            int SaveKey;
            Boolean saved =false;

            Console.WriteLine("Izberite avtomobil, ki ga zelite shraniti");
            Console.WriteLine();

            foreach (int key in Cars.Keys)
            {
                Console.WriteLine(key + ": "+ Cars[key].ToString());
            }

             SaveKey = Int32.Parse(Console.ReadLine());

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Cars[SaveKey].ToString() + ";");
                    Console.WriteLine(Cars[SaveKey].ToString() + ";" + "\r\n");
                    Cars.Remove(SaveKey);
                    Console.WriteLine("Car is saved to txt file. If you want to add it back to system use command LoadObj.");
                    saved = true;
                }
            }

            if (File.Exists(path) && saved==false)
            {
                File.AppendAllText(path, Cars[SaveKey].ToString() + ";" + "\r\n");
                Cars.Remove(SaveKey);
                Console.WriteLine("Car is saved to txt file. If you want to add it back to system use command LoadObj.");
            }

            return SaveKey;
        }

        static string[] LoadObj(string Path)
        {
            string[] readAllCars;

            if (File.Exists(Path))
            {
                readAllCars = System.IO.File.ReadAllLines(Path);
                return readAllCars;
            }
            return null;
        }

        static void SortObj(Dictionary<int, Car> Cars)
        {
            string SortCommand;

            Console.WriteLine("Izberite atribut po katerem želite sortirati avtomobile");
            Console.WriteLine();
            Console.WriteLine("1. Moč");
            Console.WriteLine("2. Hitrost");
            Console.WriteLine("3. Teža");
            Console.WriteLine("4. Znamka");

            SortCommand = Console.ReadLine();

            if (SortCommand.Equals("1"))
            {
                var CarsSorted = Cars.Values.OrderBy(v => v.Power);

                foreach (Car car in CarsSorted)
                {
                    Console.WriteLine(car.PrintWithUnit());
                }
            }

            if (SortCommand.Equals("2"))
            {
                var CarsSorted = Cars.Values.OrderBy(v => v.TopSpeed);

                foreach (Car car in CarsSorted)
                {
                    Console.WriteLine(car.PrintWithUnit());
                }

            }

            if (SortCommand.Equals("3"))
            {
                var CarsSorted = Cars.Values.OrderBy(v => v.Weight);

                foreach (Car car in CarsSorted)
                {
                    Console.WriteLine(car.PrintWithUnit());
                }
            }

            if (SortCommand.Equals("4"))
            {
                var CarsSorted = Cars.Values.OrderBy(v => v.Brand);

                foreach (Car car in CarsSorted)
                {
                    Console.WriteLine(car.PrintWithUnit());
                }
            }
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
 
        static Boolean InSystem(Dictionary<int, Car> AllCars, string Brand, String Model)
        {
            Boolean InSys = false;
            foreach (int i in AllCars.Keys)
            {
                if (AllCars[i].Brand.Equals(Brand) && AllCars[i].Model.Equals(Model))
                {
                    InSys = true;
                }
            }
            return InSys;
        }

    }

    class Car
    {

        public Car() { }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Power { get; set; }

        public string Weight { get; set; }

        public string TopSpeed { get; set; }

        public int CarId { get; set; }

        public override string ToString() => Brand + " " + Model + " " + Power + " " + TopSpeed + " " + Weight;

        public string PrintWithUnit() => Brand + " " + Model + " " + Power + "hp " + TopSpeed + "km/h " + Weight+"kg";

    }
}

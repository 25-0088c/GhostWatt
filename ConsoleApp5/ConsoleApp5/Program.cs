using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        // ====== MONTHLY ENERGY WASTE ======
        // totalWatts = total standby watts of all appliances
        // 24 = 24 hours per day
        // 30 = 30 days in one month
        // /1000 converts watts into kilowatts (kWh)
        // Electric bills use kWh

        // DICTIONARY AND STACK
        static Dictionary<string, double> appliances = new Dictionary<string, double>();
        static Dictionary<string, double> selectedDevices = new Dictionary<string, double>();
        static Stack<string> unpluggedList = new Stack<string>();
        static double rate;

        // STEP CONTROL // cannot continue to the next step unless prev is done
        static bool inventoryDone = false;

        static void Main(string[] args)
        {
            appliances.Add("TV", 5);
            appliances.Add("Air Conditioner", 15);
            appliances.Add("Fan", 2);
            appliances.Add("Extension", 3);
            appliances.Add("Computer", 4);

            bool programRunning = true;

            while (programRunning)
            {
                Console.Clear();
                IntroScreen();

                string input = Console.ReadLine();

                if (input == "2")
                {
                    return; // EXIT PROGRAM
                }
                else if (input == "1")
                {
                    bool running = true;

                    while (running)
                    {
                        Console.Clear();
                        Header();

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("======== MENU ========");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.WriteLine("Step 1. Setup Electricity Rate");
                        Console.WriteLine("Step 2. Tap-to-Add Inventory");
                        Console.WriteLine("Step 3. Hidden Cost Calculator");
                        Console.WriteLine("Step 4. Night Shift Checklist");
                        Console.WriteLine("(Enter 0 to Exit)");
                        Console.WriteLine();

                        Console.Write("Enter step / number: ");
                        string choice = Console.ReadLine();

                        Console.Clear();
                        Header();

                        if (choice == "1")
                        {
                            SetupElectricityRate();
                        }
                        else if (choice == "2")
                        {
                            TapToAddInventory();
                        }
                        else if (choice == "3")
                        {
                            HiddenCostCalculator();
                        }
                        else if (choice == "4")
                        {
                            NightShiftChecklist();
                        }
                        else if (choice == "0")
                        {
                            running = false;
                        }
                    }
                }
            }
        }

        // ===== INTRO SCREEN =====
        static void IntroScreen()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Header();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("        Ready to save energy and your wallet?");
            Console.WriteLine("      Eliminate vampire energy with GhostWatt now!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine("1. Enter");
            Console.WriteLine("2. Exit");
            Console.WriteLine();

            Console.Write("Choose option: ");
        }

        static void SetupElectricityRate()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======== STEP 1: ELECTRICITY RATE SETUP =========");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Set up your electricity rate for calculations !");
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine();
            Console.Write("Enter electricity rate per kWh in PHP: ");
            rate = Convert.ToDouble(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rate saved successfully!");
            Console.ForegroundColor = ConsoleColor.White;

            Pause();
        }

        static void TapToAddInventory()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======== STEP 2: TAP -TO-ADD INVENTORY ========");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Insert applications and the ages to calculate the total energy wasted !");
            Console.ForegroundColor = ConsoleColor.White;


            foreach (KeyValuePair<string, double> item in appliances)
            {
                Console.WriteLine();

                Console.Write("Do you own a " + item.Key + "? (y/n): ");
                string answer = Console.ReadLine();

                if (answer == "y")
                {
                    Console.Write("Enter age of device in years: ");
                    int age = Convert.ToInt32(Console.ReadLine());

                    double finalWatts = item.Value + (age * 0.5);

                    if (!selectedDevices.ContainsKey(item.Key))
                    {
                        selectedDevices.Add(item.Key, finalWatts);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(item.Key + " added successfully!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }

            inventoryDone = true;

            Pause();
        }

        static void HiddenCostCalculator()
        {
            if (!inventoryDone)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Please complete Inventory first!");
                Console.ForegroundColor = ConsoleColor.White;
                Pause();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======== STEP 3: HIDDEN COST CALCULATOR ========");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("The total calculated waste of energy and money !");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();


            double totalWatts = 0;

            foreach (KeyValuePair<string, double> item in selectedDevices)
            {
                totalWatts += item.Value;
            }

            double monthlyKwh = (totalWatts * 24 * 30) / 1000;
            double monthlyCost = monthlyKwh * rate;

            Console.WriteLine("Estimated standby waste: " + monthlyKwh + " kWh");
            Console.WriteLine("Money wasted monthly: PHP " + monthlyCost);

            Pause();
        }

        static void NightShiftChecklist()
        {
            if (!inventoryDone)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Please complete Inventory first!");
                Console.ForegroundColor = ConsoleColor.White;
                Pause();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===== NIGHT SHIFT CHECKLIST =====");
            Console.ForegroundColor = ConsoleColor.White;

            double totalWatts = 0;

            foreach (KeyValuePair<string, double> item in selectedDevices)
            {
                totalWatts += item.Value;
            }

            double monthlyKwh = (totalWatts * 24 * 30) / 1000;
            double monthlyCost = monthlyKwh * rate;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Ready to save PHP " + monthlyCost + "? Time to unplug !");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();

            unpluggedList.Clear();

            int count = 0;

            foreach (KeyValuePair<string, double> item in selectedDevices)
            {
                if (count < 4)
                {
                    Console.Write("Unplug " + item.Key + "? (y/n): ");
                    string unplug = Console.ReadLine();

                    if (unplug == "y")
                    {
                        unpluggedList.Push(item.Key);
                    }
                }

                count++;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("===== SAVINGS LOG =====");
            Console.ForegroundColor = ConsoleColor.White;

            double dailySavings = 0;

            // collect unplugged devices first
            List<string> unpluggedDevices = new List<string>();

            while (unpluggedList.Count > 0)
            {
                string device = unpluggedList.Pop();
                unpluggedDevices.Add(device);

                dailySavings += 2;
            }

            // SUMMARY
            if (unpluggedDevices.Count > 0)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;

                for (int i = 0; i < unpluggedDevices.Count; i++)
                {
                    if (i == 0)
                    {
                        Console.Write(unpluggedDevices[i]);
                    }
                    else if (i == unpluggedDevices.Count - 1)
                    {
                        Console.Write(" and " + unpluggedDevices[i]);
                    }
                    else
                    {
                        Console.Write(", " + unpluggedDevices[i]);
                    }
                }

                Console.WriteLine(" unplugged successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            double carbonSavings = dailySavings * 0.5;

            Console.WriteLine();
            Console.WriteLine("Estimated Daily Savings: PHP " + dailySavings);
            Console.WriteLine("Estimated Carbon Savings: " + carbonSavings + " CO2 units");

            Pause();
        }
        static void Header()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(@"
                                                                                        
                                                                                        
                 ░░░░░░░░░░░░░░░░░░░░░░               
              ░░░░░░░░░░░░░░░░░░░░░░░░░░░░           
           ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░        
         ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░      
        ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░     
        ░░░░      ░░░░░░░░░░░░░░░░░░      ░░░░       
        ░░░░    ░░▓▓▓▓░░░░░░░░░░▓▓▓▓░░    ░░░░       
        ░░░░    ░░▓▓▓▓░░░░░░░░░░▓▓▓▓░░    ░░░░       
        ░░░░          ░░░░░░░░          ░░░░         
        ░░░░        ░░░░░░░░░░░░        ░░░░         
        ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░         
            ░░░░░░░░░░  GHOSTWATT  ░░░░░░░░░          
                 ░░░░░░░░░░░░░░░░░░░░                       
            ");

            Console.ResetColor();
            Console.WriteLine();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Lab_3
{
    class Lab_3_5_6
    { 
        static void RunFights(Func<ConsoleKeyInfo, IMagicalCreature, IMagicalCreature, int> keyHandler,
                                 IMagicalCreature activeFighter, IMagicalCreature passiveFighter)
        {
            Console.WriteLine($"\n{activeFighter.CreatureName} is acting");
            Console.WriteLine("Make chooise\n1) Attack\n2) Heal\n3) Print fight info\n4)Make sound");
            Console.Write("Action: ");

            ConsoleKeyInfo key = Console.ReadKey();

            while (keyHandler?.Invoke(key, activeFighter, passiveFighter) != 1) key = Console.ReadKey();
        }

        public static void Fight(List<(IMagicalCreature, IMagicalCreature)> fights,
            Func<ConsoleKeyInfo, IMagicalCreature, IMagicalCreature, int> keyHandler)
        {
            byte turn = 1;
            uint temp;
            while (true)
            {
                while (fights[0].Item1.InFightHealth > 0 &&
                       fights[0].Item2.InFightHealth > 0)
                {
                    if (turn == 1)
                    {
                        RunFights(keyHandler ,fights[0].Item1, fights[0].Item2);
                        turn = 2;
                    }
                    else
                    {
                        RunFights(keyHandler, fights[0].Item2, fights[0].Item1);
                        turn = 1;
                    }
                }

                if (turn == 2)
                {
                    Console.WriteLine($"{fights[0].Item1.CreatureName} wins!");
                }
                else
                {
                    Console.WriteLine($"{fights[0].Item2.CreatureName} wins!");
                }

                temp = AfterFightChoise();
                fights.RemoveAt(0);

                if (fights.Count == 0 || temp == 0) return;
            }
        }

        public static uint AfterFightChoise()
        {
            Console.WriteLine($"What do you want to do now?");
            Console.WriteLine("1)Next figth\n2)Repeate this fight afterrwards\n3)End fights");
            Console.Write("Answer: ");
            return GetValidUint();
        }

        public static int AfterFight(List<(IMagicalCreature, IMagicalCreature)> fights)
        {
            uint action = AfterFightChoise();
            switch (action)
            {
                case 1:
                    {
                        Console.WriteLine("FIGHT!");
                        return 1;
                    }
                case 2:
                    {
                        fights.Add((fights[0].Item1.Clone() as IMagicalCreature,
                                    fights[0].Item2.Clone() as IMagicalCreature));
                        return 1;
                    }
                default:
                    return 0;
            }
        }
        
        static uint GetValidUint()
        {
            uint tempUint;
            bool inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);

            while (!inputCheck)
            {
                Console.WriteLine("Oops, wrong input, try again");
                inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);
            }
            return tempUint;
        }

        public static void Revive(MagicalСreature deadMonster)
        {
            Console.WriteLine($"\nSomething weired is happend: {deadMonster[3]} revived\n");
            deadMonster.Revive();
        }

        public static IMagicalCreature ChooseFighter(uint choise) 
        {
            switch (choise)
            {
                case 1:
                    {
                        return new Healer(120, 200, 20, "Ananas", 100, 5);
                    }
                case 2:
                    {
                        return new Damager(100, 120, 50, "", 6, 1.4);
                    }
                case 3:
                    {
                        return new Tank(90, 250, 25, "Cool hoel", 8, 200);
                    }
                default:
                    {
                        return new MagicalСreature(150, 150, 35);                    
                    }
            }
        }

        public static uint ShortChoise(int fighterInFight, int numberOfFights)
        {
            Console.WriteLine($"Choose {fighterInFight} fighter for the {numberOfFights}");
            Console.WriteLine("1)Healer\n2)Damager\n3)Tank\nsmth)Base creature\nChooise:");
            return GetValidUint();
        }

        public static void MakePairs(List<(IMagicalCreature, IMagicalCreature)> fights, int numberOfFights)
        {
            int fighterInFight = 1;

            uint choise = ShortChoise(fighterInFight, numberOfFights);
            IMagicalCreature temp1 = ChooseFighter(choise);
            ++fighterInFight;

            choise = ShortChoise(fighterInFight, numberOfFights);
            IMagicalCreature temp2 = ChooseFighter(choise);

            fights.Add((temp1, temp2));
        }

        public static uint ExitChoise()
        {
            Console.WriteLine($"Do you want to make pair ? (1 - yes, other number - no)");
            Console.Write("Answer: ");
            return GetValidUint();
        }

        public static event Action<List<(IMagicalCreature, IMagicalCreature)>, int> CreatingPairs;

        static void Main(string[] args)
        {
            Func<ConsoleKeyInfo, IMagicalCreature, IMagicalCreature, int> 
            keyHandler = (key, activeFighter, passiveFighter) =>
            {
                if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4) return activeFighter.MakeSound();
                return -1;
            };


            keyHandler += (key, activeFighter, passiveFighter) =>
            {
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3) return activeFighter.PrintInFightInfo();
                return -1;
            };

            keyHandler += (key, activeFighter, passiveFighter) =>
            {
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2) return activeFighter.Healing();
                return -1;
            };

            keyHandler += (key, activeFighter, passiveFighter) =>
            {
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) return activeFighter.Attack(passiveFighter);
                return -1;
            };

            keyHandler += (key, activeFighter, passiveFighter) =>
            {
                if (key.Key != ConsoleKey.D4 && key.Key != ConsoleKey.NumPad4 &&
                      key.Key != ConsoleKey.D2 && key.Key != ConsoleKey.NumPad2 &&
                      key.Key != ConsoleKey.D3 && key.Key != ConsoleKey.NumPad3 &&
                      key.Key != ConsoleKey.D1 && key.Key != ConsoleKey.NumPad1)
                    Console.WriteLine("\nThere is no such an opportunity");

                return -1;
            };

            CreatingPairs = MakePairs;

            List<(IMagicalCreature, IMagicalCreature)> listOfFights =
                new List<(IMagicalCreature, IMagicalCreature)>();
            int numberOfFights = 1;

            uint exitChoise;

            do
            {
                CreatingPairs(listOfFights, numberOfFights);
                ++numberOfFights;
                exitChoise = ExitChoise();
            }
            while (exitChoise == 1);

            Fight(listOfFights, keyHandler);
        }
    }
}

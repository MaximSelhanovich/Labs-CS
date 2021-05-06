using System;
using System.Collections.Generic;

namespace Lab_3
{
    class Lab_3_5_6
    {
        public static void FighterChoise(IMagicalCreature activeFighter,
                                         IMagicalCreature passiveFighter)
        {
            Console.WriteLine($"\n{activeFighter.CreatureName} is acting");
            Console.WriteLine("Make chooise\n1) Attack\n2) Heal\n3) Print fight info");
            Console.Write("Action: ");

            int turn = -1;
            uint choise;

            while (turn != 1)
            {
                choise = GetValidUint();

                switch (choise)
                {
                    case 1:
                        turn = activeFighter.Attack(passiveFighter);
                        break;
                    case 2:
                        turn = activeFighter.Healing();
                        break;
                    case 3:
                        turn = activeFighter.PrintInFightInfo();
                        break;
                    default:
                        Console.WriteLine("\nThere is no such an opportunity");
                        break;
                }
            }
        }

        public static void Fight(List<(IMagicalCreature, IMagicalCreature)> fights)
        {
            byte turn = 1;
            while (true)
            {
                while (fights[0].Item1.InFightHealth > 0 &&
                       fights[0].Item2.InFightHealth > 0)
                {
                    if (turn == 1)
                    {

                        FighterChoise(fights[0].Item1, fights[0].Item2);
                        turn = 2;
                    }
                    else
                    {
                        FighterChoise(fights[0].Item2, fights[0].Item1);
                        turn = 1;
                    }
                }

                if (turn == 2)
                {
                    Console.WriteLine($"{fights[0].Item1.CreatureName} wins!");

                    fights.RemoveAt(0);

                    if (fights.Count == 0) return;
                }
                else
                {
                    Console.WriteLine($"{fights[0].Item2.CreatureName} wins!");

                    fights.RemoveAt(0);

                    if (fights.Count == 0) return;
                }
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
            Console.WriteLine("1)Healer\n2)Damager\n3)Tank\nChooise:");
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

            ++numberOfFights;

            fights.Add((temp1, temp2));
        }

        public static uint ExitChoise(string message)
        {
            Console.WriteLine($"{message} (1 - yes, anithing - no)");
            Console.Write("Answer: ");
            return GetValidUint();
        }

        static void Main(string[] args)
        {
            List<(IMagicalCreature, IMagicalCreature)> listOfFights =
                new List<(IMagicalCreature, IMagicalCreature)>();
            int numberOfFights = 1;

            uint exitChoise;

            do
            {
                MakePairs(listOfFights, numberOfFights);
                ++numberOfFights;
                exitChoise = ExitChoise("Do you want to make pair?");
            }
            while (exitChoise == 1);

            Fight(listOfFights);

        }
    }
}

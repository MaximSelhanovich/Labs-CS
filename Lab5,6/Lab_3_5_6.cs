using System;

namespace Lab_3
{
    class Lab_3
    {
        public static void FighterChoise(MagicalСreature activeFighter,
                                         MagicalСreature passiveFighter)
        {
            Console.WriteLine($"\n{activeFighter[3]} is acting");
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

        public static int Fight(MagicalСreature firstFighter,
                                MagicalСreature secondFighter)
        {
            byte turn = 1;
            while (firstFighter.InFightHealth > 0 &&
                   secondFighter.InFightHealth > 0)
            {
                if (turn == 1)
                {
                    FighterChoise(firstFighter, secondFighter);
                    turn = 2;
                }
                else 
                {
                    FighterChoise(secondFighter, firstFighter);
                    turn = 1;
                }
            }

            if (turn == 2)
            {
                Console.WriteLine($"{firstFighter[3]} wins!");
                turn = 1;
            }
            else
            {
                Console.WriteLine($"{secondFighter[3]} wins!");
                turn = 2;
            }
            return turn;
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

        static void Main(string[] args)
        {
            Tank qwe = new Tank(123, 123, 13, "123", 123, 123);

            //MagicalСreature monster = qwe;
            qwe.MakeSound();
            qwe.PrintInFightInfo();

            MagicalСreature notMonster = new MagicalСreature(100, 80, 11, "Kitty");

            notMonster.MakeSound();
            notMonster.PrintInFightInfo();

            int winner = Fight(qwe, notMonster);

            if (winner == 1) Revive(notMonster);
            else Revive(qwe);

            Fight(notMonster, qwe);
        }
    }
}

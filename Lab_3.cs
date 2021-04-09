using System;
using System.Threading;

namespace Lab_3
{
    class MagicalСreature
    {
        private int _age;
        private double _defaultHealth;
        private double _inFightHealth;
        private double _defaultDamage;
        private double _inFightDamage;
        private string _name = "Creature " + _totalAmount;
        private static int _totalAmount = 1;
        //TO DO
        private string _colour = "Green";
        //private static int _hybridsAmount;

        public MagicalСreature(int age, double health, double damage)
        {
            ++_totalAmount;
            _age = age;
            _defaultHealth = health + age / 20;
            _inFightHealth = _defaultHealth;
            _defaultDamage = damage + age / 30;
            _inFightDamage = _defaultDamage;
        }

        public MagicalСreature(int age, double health, double damage,
                               string colour, string name) :
            this(age, health, damage)
        {
            _colour = colour;
            _name = name;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public double Health
        {
            get => _defaultHealth;
            set => _defaultHealth = value;
        }

        public double InFightHealth
        {
            get => _inFightHealth;
            set => _inFightHealth = value;
        }

        public double Damage
        {
            get => _defaultDamage;
            set => _defaultDamage = value;
        }

        public double InFightDamage
        {
            get => _inFightDamage;
            set => _inFightDamage = value;
        }

        public string Colour
        {
            get => _colour;
            set => _colour = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string this[int actionIndex]
        {
            get
            {
                switch (actionIndex)
                {
                    case 0: return $"{_age}";
                    case 1: return $"{_defaultHealth}";
                    case 2: return $"{_defaultDamage}";
                    case 3: return _colour;
                    case 4: return _name;
                    default: return "There is no such field";
                }
            }
        }

        public int Attack(MagicalСreature isAttacked)
        {
            if (_name == isAttacked._name)
            {
                Console.WriteLine($"\n{_name} is a little bit dump " +
                                  $"and tries to attack itself");
                return -1;
            }

            Console.WriteLine($"\n{_name} attacks {isAttacked._name}\n");

            if (isAttacked._inFightHealth <= 0)
            {
                Console.WriteLine(isAttacked._name +
                                  " is already defeated, no need to attack");
                return -1;
            }

            isAttacked._inFightHealth -= _defaultDamage;
            if (isAttacked._inFightHealth <= 0)
            {
                isAttacked._inFightHealth = 0;
                Console.WriteLine(isAttacked._name + " is defeated.");
            }
            return 1;
        }

        public void MakeSound(int temp)
        {
            Console.Write(_name + " says: ");

            switch (temp)
            {
                case 0:
                    Console.WriteLine("Roooooo-aaaar");
                    return;
                case 1:
                    Console.WriteLine("Aaaaaaaaaaa-rrrrrrrr");
                    return;
                case 2:
                    Console.WriteLine("U u u u u u u u");
                    return;
                case 3:
                    Console.WriteLine("Fksis-fksis-fksis-fksis");
                    return;
            }
        }

        public int PrintInFightInfo()
        {
            Console.WriteLine($"\n{_name} current characteristics");
            Console.WriteLine($"HP:\t {_inFightHealth}/{_defaultHealth}");
            Console.WriteLine($"Damage:\t {_inFightDamage}/{_defaultDamage}\n");
            return -1;
        }

        public void Revive()
        {
            _inFightHealth = _defaultHealth;
            _inFightDamage = _defaultDamage;
        }

        public int Healing()
        {
            if (_inFightDamage == 1)
            {
                Console.WriteLine($"\n{_name} attack point is 1, " +
                                  $"there is no opportunite to heal");
                return -1;
            }

            Console.WriteLine($"\n{_name} sacrifices 1 attack point " +
                              $"to get 10 HP ");
            --_inFightDamage;
            _inFightHealth += 10;
            return 1;
        }
    }

    class Lab_3
    {
        public static void FighterChoise(MagicalСreature activeFighter,
                                  MagicalСreature passiveFighter)
        {
            Console.WriteLine($"{activeFighter[4]} is acting");
            Console.WriteLine("Make chooise\n" +
                              "1) Attack\n2) Heal\n3) Print fight info");
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
                Console.WriteLine($"{firstFighter[4]} wins!");
                turn = 1;
            }
            else
            {
                Console.WriteLine($"{secondFighter[4]} wins!");
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
            Console.WriteLine($"\nSomething weired is happend: {deadMonster[4]} revived\n");
            deadMonster.Revive();
        }

        static void Main(string[] args)
        {
            Random temp = new Random();
            MagicalСreature monster = new MagicalСreature(123, 90, 10);
            monster.MakeSound(temp.Next(0,4));
            monster.PrintInFightInfo();

            MagicalСreature notMonster =
                        new MagicalСreature(100, 80, 11, "Red", "Kitty");
            notMonster.MakeSound(temp.Next(0, 4));
            notMonster.PrintInFightInfo();

            int winner = Fight(monster, notMonster);

            if (winner == 1) Revive(notMonster);
            else Revive(monster);

            Fight(notMonster, monster);
        }
    }
}

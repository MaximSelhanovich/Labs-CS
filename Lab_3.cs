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
        private double _inFightDamage   ;
        private string _colour = "Green";
        private string _name = "Creature " + _totalAmount;
        private static int _totalAmount = 1;
        //TO DO
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

        public void Attack(MagicalСreature isAttacked)
        {
            if (_name == isAttacked._name)
            {
                Console.WriteLine($"{_name} is a little bit dump " +
                                  $"and tries to attack itself");
                return;
            }

            Console.WriteLine($"{_name} attacks {isAttacked._name}");

            if (isAttacked._inFightHealth <= 0)
            {
                Console.WriteLine(isAttacked._name +
                                  " is already defeated, no need to attack");
                return;
            }


            isAttacked._inFightHealth -= _defaultDamage;
            if (isAttacked._inFightHealth <= 0)
            {
                isAttacked._inFightHealth = 0;
                Console.WriteLine(isAttacked._name + " is defeated.");
            }
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

        public void PrintInFightInfo()
        {
            Console.WriteLine($"{_name} infight characteristics");
            Console.WriteLine($"HP:\t {_inFightHealth}/{_defaultHealth}");
            Console.WriteLine($"Damage:\t {_inFightDamage}/{_defaultDamage}\n");
        }

        public void Revive()
        {
            _inFightHealth = _defaultHealth;
            _inFightDamage = _defaultDamage;
        }

        public void Healing()
        {
            if (_inFightDamage == 1)
            {
                Console.WriteLine($"{_name} attack point is 1, " +
                                  $"there is no opportunite to heal");
                return;
            }

            Console.WriteLine($"{_name} sacrifices 1 attack point " +
                              $"to get 10 HP ");
            --_inFightDamage;
            _inFightHealth += 10;
        }
    } 

    class Lab_3
    {
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

            monster.Attack(notMonster);
            notMonster.PrintInFightInfo();
            notMonster.Attack(monster);
            monster.PrintInFightInfo();

 
        }
    }
}

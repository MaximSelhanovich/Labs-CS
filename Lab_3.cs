using System;

namespace Lab_3
{
    class MagicalСreature
    {
        private int _age;
        private double _defaultHealth;
        private double _inFightHealth;
        private double _defaultDamage;
        private double _inFightDamage;
        private string _colour = "Green";
        private string _name = "Creature " + _totalAmount;
        private bool _isDefeated = false;
        private static int _totalAmount = 1;
        //TO DO
        //private static int _hybridsAmount;

        public MagicalСreature(int age, double health, double damage)
        {
            ++_totalAmount;
            _age = age;
            _defaultHealth = health + age / 20;
            _defaultDamage = damage + age / 30;
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

        public double Damage
        {
            get => _defaultDamage;
            set => _defaultDamage = value;
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

        public bool IsDefeated
        {
            get => _isDefeated;
            set => _isDefeated = value;
        }

        public void Attack(MagicalСreature isAttacked)
        {
            
            isAttacked._inFightHealth -= _defaultDamage;
            if (isAttacked._inFightHealth <= 0)
            {
                isAttacked._isDefeated = true;
                Console.WriteLine(isAttacked._name + " is defeated.");
            }
        }

        public void Revive()
        {
            _inFightHealth = _defaultHealth;
            _inFightDamage = _defaultDamage;
        }
    } 

    class Lab_3
    {
        static uint getValidUint()
        {
            uint tempUint;
            bool inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);

            while (!inputCheck)
            {
                Console.Clear();
                Console.WriteLine("Oops, wrong input, try again");
                inputCheck = uint.TryParse(Console.ReadLine(), out tempUint);
            }
            return tempUint;
        }

        static double getVlidUDouble()
        {
            double tempDouble;
            bool inputCheck = double.TryParse(Console.ReadLine(), out tempDouble);

            while (!inputCheck || tempDouble < 0)
            {
                Console.WriteLine("Oops, wrong input, try again");
                inputCheck = double.TryParse(Console.ReadLine(), out tempDouble);
                Console.Clear();
            }
            return tempDouble;
        }

        static void Main(string[] args)
        {
            MagicalСreature monster = new MagicalСreature(123, 123, 123);
            Console.WriteLine(monster.Name);
            MagicalСreature notMonster = new MagicalСreature(123, 123, 123);
            monster.Attack(notMonster);
            Console.WriteLine(notMonster.Name);




        }
    }
}

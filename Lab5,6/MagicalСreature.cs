using System;

namespace Lab_3
{
    public class MagicalСreature : IMagicalCreature
    {
        private int _age;
        private double _defaultHealth;
        protected double _inFightHealth;
        private double _defaultDamage;
        protected double _inFightDamage;
        private string _name = "Creature " + _totalAmount;
        private BaseWeapon Weapon { get; set; }
        protected static Random _randomValue = new Random();
        private static int _totalAmount = 0;

        public int Age {
            get
            {
                return _age;
            }
            protected set
            {
                _age = value <= 0 ? 30 : value;
            }
        }

        public double DefaultHealth
        {
            get
            {
                return _defaultHealth;
            }
            set
            {
                _defaultHealth = (value <= 0 ? 60 : value) + _age / 20;
            }
        }

        public virtual double InFightHealth {
            get
            {
                return _inFightHealth;
            }
            set
            {
                _inFightHealth = value <= 0 ? 0 : value;
            }
        }

        public double DefaultDamage
        {
            get
            {
                return _defaultDamage;
            }
            set
            {
                _defaultDamage = (value <= 0 ? 12 : value) + _age / 30;
            } 
        }

        public double InFightDamage
        {
            get
            {
                return _inFightDamage;
            }
            set
            {
                _inFightDamage = value <= 0 ? 0 : value;
            }
        }

        public string CreatureName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value == "" ? "Creature " + _totalAmount : value + _totalAmount;
            }
        }

        public MagicalСreature(int age, double health, double damage)
        {
            ++_totalAmount;
            Age = age;
            Weapon = new BaseWeapon((WeaponsName)_randomValue.Next(0, 4));
            DefaultHealth = health + Weapon.BonusWeaponHealth;
            _inFightHealth = DefaultHealth;
            DefaultDamage = damage + Weapon.BonusWeaponDamage;
            _inFightDamage = DefaultDamage;
            
        }

        public MagicalСreature(int age, double health,
                               double damage, string name)
            : this(age, health, damage)
        {
            CreatureName = name;
        }

        public MagicalСreature(MagicalСreature toCopyFrom)
            : this(toCopyFrom.Age, toCopyFrom.DefaultHealth,
                   toCopyFrom.DefaultDamage, toCopyFrom.CreatureName)
        { }

        public string this[int actionIndex]
        {
            get
            {
                switch (actionIndex)
                {
                    case 0: return $"{Age}";
                    case 1: return $"{DefaultHealth}";
                    case 2: return $"{DefaultDamage}";
                    case 3: return CreatureName;
                    default: return "There is no such field";
                }
            }
        }

        public virtual int Attack(IMagicalCreature isAttacked)
        {
            if (CreatureName == isAttacked.CreatureName)
            {
                Console.WriteLine($"\n{CreatureName} is a little bit dump and tries to attack itself");
                return -1;
            }

            if (isAttacked.InFightHealth == 0)
            {
                Console.WriteLine($"\n{isAttacked.CreatureName} is already defeated, no need to attack");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} attacks {isAttacked.CreatureName}");

            isAttacked.InFightHealth -= InFightDamage;

            if (isAttacked.InFightHealth <= 0) 
            {
                Console.WriteLine($"{isAttacked.CreatureName} is defeated.");
            }

            return 1;
        }

        public void MakeSound()
        {
            Console.Write($"{CreatureName} says: ");

            switch (_randomValue.Next(0, 4))
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
                    Console.WriteLine("Ksis-ksis-ksis-ksis");
                    return;
            }
        }

        public virtual int PrintInFightInfo()
        {
            Console.WriteLine($"\n{CreatureName} current characteristics");
            Console.WriteLine($"HP:\t {InFightHealth}/{DefaultHealth}");
            Console.WriteLine($"Damage:\t {InFightDamage}/{DefaultDamage}");
            Console.WriteLine($"Weapon:\t {Weapon}");

            return -1;
        }

        public virtual void Revive()
        {
            InFightHealth = DefaultHealth;
            InFightDamage = DefaultDamage;
        }

        public virtual int Healing()
        {
            if (InFightDamage == 1)
            {
                Console.WriteLine($"\n{CreatureName} attack point is 3, there is no opportunite to heal");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} sacrifices 3 attack point to get 10 HP ");
            InFightDamage -= 3;
            InFightHealth += 10;
            return 1;
        }

        public virtual object Clone()
        {
            return (IMagicalCreature) new MagicalСreature(this);
        }
    }
}

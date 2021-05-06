using System;

namespace Lab_3
{
    class Healer : MagicalСreature
    {
        private double _defaultMana;
        private double _inFightMana;
        private int _defaultNotToSpendManaChance;

        public double DefaultMana
        {
            get
            {
                return _defaultMana;
            }
            protected set
            {
                _defaultMana = value <= 0 ? 100 : value;
            }
        }

        public double InFightMana {
            get
            {
                return _inFightMana;
            }
            set
            {
                int temp = _randomValue.Next(0, InFightNotToSpendManaChance + 1);
                if (temp > 0 || temp <= InFightNotToSpendManaChance)
                {
                    Console.WriteLine($"{creatureName} doesn't spend mana");
                    return;
                }

                _inFightMana = value;
            }
                
        }

        public int DefaultNotToSpendManaChance
        {
            get
            {
                return _defaultNotToSpendManaChance;
            }

            private set
            {
                _defaultNotToSpendManaChance = (value < 0 || value > 10) ? 7 : value;
            }
        }

        public int InFightNotToSpendManaChance { get; set; }

        public Healer(int age, double health, double damage,
                        string name, double mana,
                        int notToSpendManaChance)
            : base(age, health, damage, name)
        {
            DefaultMana = mana;
            _inFightMana = DefaultMana;
            DefaultNotToSpendManaChance = notToSpendManaChance;
            InFightNotToSpendManaChance = DefaultNotToSpendManaChance;
        }

        public Healer(Healer toCopyFrom)
            : this(toCopyFrom.Age, toCopyFrom.DefaultHealth,
                    toCopyFrom.DefaultDamage, toCopyFrom.creatureName,
                    toCopyFrom.DefaultMana, toCopyFrom.DefaultNotToSpendManaChance)
        { }

        public override int Healing()
        {
            if (InFightMana <= 10)
            {
                Console.WriteLine($"\n{creatureName} mana points is {InFightMana}/10," +
                                  $" there is no opportunite to heal");
                return -1;
            }

            Console.WriteLine($"\n{creatureName} use 10 mana points to get 15 HP ");
            InFightMana -= 10;
            InFightHealth += 15;
            return 1;
        }

        public override int Attack(MagicalСreature isAttacked)
        {
            if (isAttacked.InFightHealth == 0)
            {
                Console.WriteLine($"{isAttacked.creatureName} is already defeated, no need to attack");
                return -1;
            }

            Console.WriteLine($"\n{creatureName} attacks {isAttacked.creatureName}\n");

            isAttacked.InFightHealth -= InFightDamage;

            if (InFightHealth < DefaultHealth - 5)
            {
                InFightMana -= 5;
                InFightHealth += 5;
            }

            if (isAttacked.InFightHealth <= 0)
            {
                Console.WriteLine(isAttacked.creatureName + " is defeated.");
            }

            return 1;
        }

        public override void Revive()
        {
            base.Revive();
            InFightMana = DefaultMana;
        }

        public override int PrintInFightInfo()
        {
            base.PrintInFightInfo();
            Console.WriteLine($"Mana:\t {InFightMana}/{DefaultMana}");
            Console.WriteLine($"Not spend mana chance: {InFightNotToSpendManaChance}%\n");

            return -1;
        }

        public override object Clone()
        {
            return new Healer(this);
        }
    }
}

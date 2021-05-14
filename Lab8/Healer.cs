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
                try
                {
                    if (value <= 0)
                    {
                        throw new IndexOutOfRangeException("Default Mana should be > 0.");
                    }
                    _defaultMana = value;
                }
                catch (IndexOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Set Default Mana to 100.");
                    _defaultMana = 100;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message} was catched.");
                    Console.WriteLine("Set Default Mana to 100.");
                    _defaultMana = 100;
                }
            }
        }

        public double InFightMana {
            get
            {
                return _inFightMana;
            }
            set
            {
                int temp = _randomValue.Next(0, 101);
                if (temp > 0 && temp <= InFightNotToSpendManaChance)
                {
                    Console.WriteLine($"{CreatureName} doesn't spend mana on healing");
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
                try
                {
                    if (value <= 0 || value > 10)
                    {
                        throw new IndexOutOfRangeException("Default Not To Spend Mana Chance should be > 0 <= 10.");
                    }
                    _defaultNotToSpendManaChance = value;
                }
                catch (IndexOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Set Default Critical Hit Chance to 5%.");
                    _defaultNotToSpendManaChance = 5;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message} was catched.");
                    Console.WriteLine("Set Default Critical Hit Chance to 5%.");
                    _defaultNotToSpendManaChance = 5;
                }
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
                    toCopyFrom.DefaultDamage, toCopyFrom.CreatureName,
                    toCopyFrom.DefaultMana, toCopyFrom.DefaultNotToSpendManaChance)
        { }

        public override int Healing()
        {
            if (InFightMana < 10)
            {
                Console.WriteLine($"\n{CreatureName} mana points is {InFightMana}/10," +
                                  $" there is no opportunite to heal");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} use 10 mana points to get 15 HP ");
            InFightMana -= 10;
            InFightHealth += 15;
            return 1;
        }

        public override int Attack(IMagicalCreature isAttacked)
        {
            if (isAttacked.InFightHealth == 0)
            {
                Console.WriteLine($"\n{isAttacked.CreatureName} is already defeated, no need to attack");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} attacks {isAttacked.CreatureName}\n");

            isAttacked.InFightHealth -= InFightDamage;

            if (InFightHealth < DefaultHealth - 5)
            {
                InFightMana -= 5;
                InFightHealth += 5;
            }

            if (isAttacked.InFightHealth <= 0)
            {
                Console.WriteLine(isAttacked.CreatureName + " is defeated.");
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

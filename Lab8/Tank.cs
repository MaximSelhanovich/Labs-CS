using System;

namespace Lab_3
{
    class Tank : MagicalСreature
    {
        private int _defaultChanceToDodge;
        private double _defaultShieldStrength;
        private double _inFightShieldStrength;

        public int DefaultChanceToDodge
        {
            get
            {
                return _defaultChanceToDodge;
            }
            protected set
            {
                try
                {
                    if (value <= 0 || value > 5)
                    {
                        throw new IndexOutOfRangeException("Default Chance To Dodge should be > 0 <= 5.");
                    }
                    _defaultChanceToDodge = value;
                }
                catch (IndexOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Set Default Chance To Dodge to 4%.");
                    _defaultChanceToDodge = 4;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message} was catched.");
                    Console.WriteLine("Set Default Chance To Dodge to 4%.");
                    _defaultChanceToDodge = 4;
                }
            }
        }

        public int InFightChanceToDodge { get; set; }

        public double DefaultShieldStrength
        {
            get
            {
                return _defaultShieldStrength;
            }
            protected set
            {
                try
                {
                    if (value <= 0)
                    {
                        throw new IndexOutOfRangeException("Default Shield Strength should be > 0.");
                    }
                    _defaultShieldStrength = value;
                }
                catch (IndexOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Set Default Shield Strength to 110.");
                    _defaultShieldStrength = 110;
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{exception.Message} was catched.");
                    Console.WriteLine("Set Default Shield Strength to 110.");
                    _defaultShieldStrength = 110;
                }
            }
        }

        public double InFightShieldStrength {
            get
            {
                return _inFightShieldStrength;
            }
            set
            {
                int temp = _randomValue.Next(1, 101);

                if (temp > 0 && temp <= InFightChanceToDodge)
                {
                    Console.WriteLine($"{CreatureName} avoids damage");
                    return;
                }

                _inFightShieldStrength = value <= 0 ? 0 : value;
            }
        }

        public override double InFightHealth
        {
            get
            {
                return _inFightHealth;
            }
            set
            {
                double temp = _randomValue.Next(1, 101);

                if (temp > 0 && temp <= InFightChanceToDodge)
                {
                    Console.WriteLine($"{CreatureName} avoids damage");
                    return;
                }

                temp = _inFightHealth - value;

                if (temp < 0 && temp < InFightHealth - DefaultHealth)
                {
                    _inFightHealth = DefaultHealth;
                    _inFightShieldStrength = InFightHealth - DefaultHealth - temp;

                    return;
                }
                else if (temp < 0)
                {
                    _inFightShieldStrength -= temp;
                    return;
                }

                if (temp < InFightShieldStrength)
                {
                    InFightShieldStrength -= temp;

                    return;
                }
                else if (temp > InFightShieldStrength && InFightShieldStrength > 0)
                {
                    _inFightHealth = temp - InFightShieldStrength;
                    InFightShieldStrength = 0;

                    return;
                }

                _inFightHealth = value <= 0 ? 0 : value;
            }
        }

        public Tank(int age, double health, double damage,
                    string name, int chanceToDodge,
                    double ShieldStrength)
            : base(age, health, damage, name)
        {
            DefaultChanceToDodge = chanceToDodge;
            InFightChanceToDodge = DefaultChanceToDodge;
            DefaultShieldStrength = ShieldStrength;
            _inFightShieldStrength = DefaultShieldStrength;
        }

        public Tank(Tank toCopyFrom)
            : this(toCopyFrom.Age, toCopyFrom.DefaultHealth,
                    toCopyFrom.DefaultDamage, toCopyFrom.CreatureName,
                    toCopyFrom.DefaultChanceToDodge,
                    toCopyFrom.DefaultShieldStrength)
        { }

        public override int Attack(IMagicalCreature isAttacked)
        {
            if (isAttacked.InFightHealth == 0)
            {
                Console.WriteLine($"{isAttacked.CreatureName} is already defeated, no need to attack");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} attacks {isAttacked.CreatureName}\n");

            isAttacked.InFightHealth -= InFightDamage;

            if (isAttacked.InFightHealth <= 0)
            {
                Console.WriteLine(isAttacked.CreatureName + " is defeated.");
            }

            return 1;
        }

        public override void Revive()
        {
            base.Revive();
            InFightChanceToDodge = DefaultChanceToDodge;
            InFightShieldStrength = DefaultShieldStrength;
        }

        public override int PrintInFightInfo()
        {
            base.PrintInFightInfo();
            Console.WriteLine($"Dodge chance: {InFightChanceToDodge}%");
            Console.WriteLine($"Shield:\t {InFightShieldStrength}/{DefaultShieldStrength}\n");

            return -1;
        }

        public override int Healing()
        {
            if (InFightDamage == 1)
            {
                Console.WriteLine($"\n{CreatureName} attack point is 3, there is no opportunite to heal");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} sacrifices 3 attack point to get 10 HP ");
            InFightDamage -= 3;

            if (InFightHealth + 10 > DefaultHealth && InFightShieldStrength + 10 < DefaultHealth)
            {
                InFightShieldStrength += 10;
            }
            else
            {
                InFightHealth += 10;
            }

            return 1;
        }

        public override object Clone()
        {
            return new Tank(this);
        }
    }
}

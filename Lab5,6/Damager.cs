using System;

namespace Lab_3
{
    public class Damager : MagicalСreature
    {
        private int _defaultCriticalHitChance;
        private double _defaultCriticalDamageMultiplier;

        public int DefaultCriticalHitChance
        {
            get
            {
                return _defaultCriticalHitChance;
            }
            private set
            {
                _defaultCriticalHitChance = (value < 0 || value > 10) ? 100 : value;
            }
        }

        public int InFightCriticalHitChance { get; set; }

        public double DefaultCriticalDamageMultiplier
        {
            get
            {
                return _defaultCriticalDamageMultiplier;
            }

            private set
            {
                _defaultCriticalDamageMultiplier = (value < 1 || value > 2) ? 1.4 : value;
            }
        }

        public double InFightCriticalDamageMultiplier { get; set; }

        public Damager (int age, double health, double damage,
                        string name, int criticalHitChance,
                        double criticalHitMultiplier)
            : base(age, health, damage, name)
        {
            DefaultCriticalHitChance = criticalHitChance;
            InFightCriticalHitChance = DefaultCriticalHitChance;
            DefaultCriticalDamageMultiplier = criticalHitMultiplier;
            InFightCriticalDamageMultiplier = DefaultCriticalDamageMultiplier;
        }


        public Damager (Damager toCopyFrom)
            : this (toCopyFrom.Age, toCopyFrom.DefaultHealth,
                    toCopyFrom.DefaultDamage, toCopyFrom.CreatureName,
                    toCopyFrom.DefaultCriticalHitChance,
                    toCopyFrom.DefaultCriticalDamageMultiplier)
        {}

        private double MakeCrit()
        {
            int tryToMakeCrit = _randomValue.Next(1, 101);

            if (tryToMakeCrit <= InFightCriticalHitChance)
            {
                Console.WriteLine($"{CreatureName} makes crit");
                return InFightCriticalDamageMultiplier;
            }

            return 1;
        }

        public override int Attack(IMagicalCreature isAttacked)
        {
            if (isAttacked.InFightHealth == 0)
            {
                Console.WriteLine($"{isAttacked.CreatureName} is already defeated, no need to attack");
                return -1;
            }

            Console.WriteLine($"\n{CreatureName} attacks {isAttacked.CreatureName}\n");

            isAttacked.InFightHealth -= InFightDamage * MakeCrit();

            if (isAttacked.InFightHealth <= 0)
            {
                Console.WriteLine(isAttacked.CreatureName + " is defeated.");
            }

            return 1;
        }

        public override void Revive()
        {
            base.Revive();
            InFightCriticalHitChance = DefaultCriticalHitChance;
            InFightCriticalDamageMultiplier = DefaultCriticalDamageMultiplier;
        }

        public override int PrintInFightInfo()
        {
            base.PrintInFightInfo();

            Console.WriteLine($"Critical hit chance: {InFightCriticalHitChance}%");
            Console.WriteLine($"Critical hit multiplier: {InFightCriticalDamageMultiplier}\n");

            return -1;
        }

        public override object Clone()
        {
            return new Damager(this);
        }
    }
}

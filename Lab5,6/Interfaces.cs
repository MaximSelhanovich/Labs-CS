using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public interface IMagicalCreature : ICloneable
    {
        int Attack(IMagicalCreature isAttacked);
        void MakeSound();
        int PrintInFightInfo();
        void Revive();
        int Healing();

        double DefaultHealth { get; set; }
        double InFightHealth { get; set; }

        double DefaultDamage { get; set; }
        double InFightDamage { get; set; }
        string CreatureName { get; set; }

    }
}

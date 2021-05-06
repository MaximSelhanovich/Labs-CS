using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    interface IMagicalCreature
    {
        int Attack(MagicalСreature isAttacked);
        void MakeSound();
        int PrintInFightInfo();
        void Revive();
        int Healing();
    }
}

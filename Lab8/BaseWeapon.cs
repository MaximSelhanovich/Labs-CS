using System;

namespace Lab_3
{
    public enum WeaponsName
    {
        Nothing,
        LongLongSword,
        BFG9000,
        NoisyCricket
    }


    public struct BaseWeapon
    {
        public double BonusWeaponHealth { get; }
        public double BonusWeaponDamage { get; }
        WeaponsName weaponName { get; }
        public BaseWeapon(WeaponsName name)
        {
            switch (name)
            {
                case WeaponsName.LongLongSword:
                    {
                        BonusWeaponHealth = 10;
                        BonusWeaponDamage = 15;
                        weaponName = WeaponsName.LongLongSword;
                        break;
                    }
                case WeaponsName.BFG9000:
                    {
                        BonusWeaponHealth = 5;
                        BonusWeaponDamage = 23;
                        weaponName = WeaponsName.BFG9000;
                        break;
                    }
                case WeaponsName.NoisyCricket:
                    {
                        BonusWeaponHealth = 0;
                        BonusWeaponDamage = 30;
                        weaponName = WeaponsName.NoisyCricket;
                        break;
                    }
                // WeaponsName.Nothing and default cases have the same meaning
                default:
                    {
                        BonusWeaponHealth = 0;
                        BonusWeaponDamage = 0;
                        weaponName = WeaponsName.Nothing;
                        break;
                    }
            }
        }

        public override string ToString()
        {
            return $"\"{weaponName}\" (Healt: +{BonusWeaponHealth}; Damage: +{BonusWeaponDamage})";
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal enum LootType
{
    Health, Ammo, Grenade
}
internal class LootFactory 
{
    public static LFInterface CreateLoot (LootType lootType)
    {
        LFInterface Loot;
        switch (lootType) { 
        case LootType.Health:
                Loot = new HealthLoot ();
                break;
        case LootType.Ammo:
                Loot = new AmmoLoot ();
                break;
        case LootType.Grenade:
                Loot = new UpgradeLoot ();
                break;

                default:
                Loot = new HealthLoot();
                break;
        }
        return Loot;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Assault, Engineer, Sniper
}
internal class ClassFactory
{
    public static ClassInterface SpawnClass(ClassType ClassType)
    {
        ClassInterface Loot;
        switch (ClassType)
        {
            case ClassType.Assault:
                Loot = new Assault();
                break;
            case ClassType.Engineer:
                Loot = new Engineer();
                break;
            case ClassType.Sniper:
                Loot = new Sniper();
                break;

            default:
                Loot = new Assault();
                break;
        }
        return Loot;

    }

}

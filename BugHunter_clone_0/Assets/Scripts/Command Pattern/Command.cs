using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    protected GameObject _entity;
    public Command(GameObject obj)
    {
        _entity = obj;
    }
    public abstract void Execute();
    public abstract void Undo();

}

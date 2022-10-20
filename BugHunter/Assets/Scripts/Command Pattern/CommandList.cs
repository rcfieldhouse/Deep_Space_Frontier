using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandList : MonoBehaviour
{

}

public class MoveCommand :Command
{
    private Vector3 _destination;
    private Vector3 _originalPosition;

    public MoveCommand( GameObject obj, Vector3 movePosition) : base(obj)
    {
        _destination = movePosition;
    }
    override public void Execute()
    {
        _originalPosition = _entity.transform.position;
        _entity.transform.position = _destination;
    }
    override public void Undo()
    {
        _entity.transform.position = _originalPosition;
    }

}
public class DestroyCommand : Command
{

    public DestroyCommand (GameObject obj) : base (obj)
    {

    }
    public override void Execute()
    {
        _entity.SetActive(false);
    }

    public override void Undo()
    {
        //TODO allow modification of CommandProcessor
    }
}
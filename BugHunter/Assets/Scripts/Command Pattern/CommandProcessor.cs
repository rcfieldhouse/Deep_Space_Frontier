using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : MonoBehaviour
{
    private List<Command> _commands = new List<Command>();
    private int _commandIndex;

    public void ExecuteCommand(Command command)
    {
        _commands.Add(command);
        command.Execute();
        _commandIndex= _commands.Count-1;
    }
    public void Undo()
    {
        if (_commands.Count < 1)
            return;
        _commands[_commandIndex].Undo();
        _commands.RemoveAt(_commandIndex);
        _commandIndex--;
    }
    public void Redo()
    {
        _commands[_commandIndex].Execute();
        _commandIndex++;
    }
}


using System;
using System.Windows.Input;
using UnityEngine;

namespace SirGames.Showcase.Managers
{   
    public interface IMoveCommand
    {
        void Execute();
    }
    public enum MoveDirection
    {
        Left, Right, Forward, Backward
    }
    public class MoveCommand : IMoveCommand
    {
        private Action _callback;
        public MoveCommand(Action callback)
        {
            _callback = callback;
        }

        public void Execute()
        {
            _callback?.Invoke();
        }
    }
}
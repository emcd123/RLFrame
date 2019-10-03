using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSystems
{
    public interface ICommand
    {
        void Execute(Actor actor);
    }
}

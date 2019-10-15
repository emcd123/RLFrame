using DataModels;
using DataModels.Entities;

namespace GameSystems.CommandSystem
{
    public interface ICommandItem
    {
        void Execute(Actor actor, Item item);
    }
}

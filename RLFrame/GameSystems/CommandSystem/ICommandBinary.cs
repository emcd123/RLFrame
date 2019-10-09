using DataModels.Entities;

namespace GameSystems.CommandSystem
{
    public interface ICommandBinary
    {
        void Execute(Actor primary_actor, Actor secondary_actor);
    }
}

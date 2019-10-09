using DataModels.Entities;

namespace GameSystems
{
    public interface ICommand
    {
        void Execute(Actor primary_actor);
    }
}

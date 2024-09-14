using Gibbon.AI.Models;

namespace Gibbon.AI.Interfaces.Purpose
{
    public interface IGeneral
    {
        AIModel[] GetAvailableModels();
        
        void SelectModel(AIModel model);
    }
}
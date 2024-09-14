using Gibbon.AI.Interfaces.Purpose;

namespace Gibbon.AI.Models
{
    public class AIModel
    {
        public string FriendlyName { get; }
        public string ApiName { get; }

        public AIModel(string friendlyName, string apiName)
        {
            FriendlyName = friendlyName;
            ApiName = apiName;
        }
    }
}
using Core.Config;

namespace Core.Common
{
    public class CGlobal
    {
        public static bool IsSimulateMonitor { get; set; } = false;

        public static string RecipeFileFormat = ConfigManager.Instance.GetConfig<string>("Recipe.RecipeFileFormat") ?? ".rp";
    }
}
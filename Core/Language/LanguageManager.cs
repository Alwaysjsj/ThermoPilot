using System.Globalization;
using System.Resources;

namespace Core.Language
{
    public class LanguageManager
    {
        private static string _languageType = "en";

        private static readonly ResourceManager? _resourceManagerPhrase = 
            new ResourceManager(
                "Core.Language.Lang-Phrase",
                typeof(LanguageManager).Assembly);

        private static readonly ResourceManager? _resourceManagerWord =
            new ResourceManager(
                "Core.Language.Lang-Word",
                typeof(LanguageManager).Assembly);

        public static event Action? LanguageChanged;

        private static string GetValue(string key)
        {
            key = key.Replace(" ", string.Empty);

            var retPhrase = _resourceManagerPhrase?.GetString(key, new CultureInfo(_languageType));

            if (!string.IsNullOrEmpty(retPhrase))
            {
                return retPhrase;
            }

            var retWord = _resourceManagerWord?.GetString(key, new CultureInfo(_languageType));

            if (!string.IsNullOrEmpty(retWord))
            {
                return retWord;
            }

            return key;
        }

        public static string GetString(string key)
        {
            if(_resourceManagerPhrase == null || _resourceManagerWord == null)
            {
                return key;
            }

            return key;
        }

        public static string GetString(string prefix,string key)
        {
            return GetValue($"{prefix}.{key}");
        }

        public static void ChangeLanguage(string cultureCode)
        {
            if(cultureCode.ToLower() == "en")
            {
                _languageType = "en";
            }

            if(cultureCode.ToLower() == "cn")
            {
                _languageType = "zh-CN";
            }

            LanguageChanged?.Invoke();
        }

        public static string GetLanguage()
        {
            return _languageType.ToUpper();
        }
    }
}
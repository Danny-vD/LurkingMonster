namespace Enums
{
    // ReSharper disable InconsistentNaming
    public enum Language : int
    {
        NL = 0,
        EN = 1,
        DE = 2,
    }

    public static class LanguageSettings
    {
        public static Language Language { get; set; } = 0;
    }
}
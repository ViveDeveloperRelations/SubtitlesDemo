using System;

public class Subtitles
{
    public enum Language {  ENGLISH = 0, FRENCH, NUMTYPES};
    public static int SelectedLanguage = (int)Language.ENGLISH;

    // index , selected language
    public static string[,] Files = new string[2, (int)Language.NUMTYPES] {
        {
            "part1_English.txt",
            "part1_French.txt"
        },
        {
            "bang_English.txt",
            "bang_French.txt"
        } 
    };
}

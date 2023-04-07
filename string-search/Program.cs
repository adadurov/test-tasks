using System.Text.RegularExpressions;

TestMatches("BcBACACBmmACBxxA");    // ==> should yield 3
TestMatches("BcBAC");               // should yield 1

int CountMatchesWithRegex(string text)
{
    return Regex.Matches(text, "(?=B..AC)").Count;
}

int CountMatches(string text)
{
    const int patternLength = 4;
    var matches = 0;
    for (int i = 0; i < text.Length - patternLength; i++)
    {
        if (text[i] == 'B' && text[i + 3] == 'A' && text[i + 4] == 'C')
        {
            matches++;
        }
    }
    return matches;
}

void TestMatches(string text)
{
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine($"Regex:   {text} => {CountMatchesWithRegex(text)}");
    Console.WriteLine($"Manual:  {text} => {CountMatches(text)}");
}

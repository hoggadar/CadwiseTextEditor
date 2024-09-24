using System.Text;
using System.Text.RegularExpressions;
using TextEditor.Ineterfases;

namespace TextEditor.Services
{
    public class ContentHandler : IContentHandler
    {
        public string DeleteWords(string str, int minLength)
        {
            StringBuilder sb = new StringBuilder();
            string pattern = @"[\wÀ-ÿ]+|[^\s\wÀ-ÿ]+|[\n]";
            MatchCollection matches = Regex.Matches(str, pattern);
            bool lastNewLine = true;

            foreach (Match match in matches)
            {
                string word = match.Value;
                if (word == "\n")
                {
                    if (!lastNewLine)
                    {
                        sb.Append(word);
                        lastNewLine = true;
                    } 
                }
                else if (word.Length >= minLength || Regex.IsMatch(word, @"[^\w\s]"))
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] != '\n') sb.Append(' ');
                    sb.Append(word);
                    lastNewLine = false;
                }
            }
            return sb.ToString();
        }

        public string DeletePunctuation(string str)
        {
            Regex rgx = new Regex(@"[^\w\s]");
            return rgx.Replace(str, "");
        }
    }
}

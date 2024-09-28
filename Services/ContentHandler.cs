using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TextEditor.Ineterfases;

namespace TextEditor.Services
{
    public class ContentHandler : IContentHandler
    {
        public async Task<StringBuilder> DeleteWordsAndPunctuation(string filePath, int minLength, bool removePunctuation)
        {
            StringBuilder sb = new StringBuilder();

            await Task.Run(() =>
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    string[] words;
                    bool lineHasContent;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (removePunctuation) line = Regex.Replace(line, @"[^\wÀ-ÿ\s]+", "");

                        words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        lineHasContent = false;

                        foreach (string word in words)
                        {
                            if (minLength == -1 || word.Length >= minLength)
                            {
                                if (lineHasContent) sb.Append(' ');
                                sb.Append(word);
                                lineHasContent = true;
                            }
                        }

                        if (lineHasContent) sb.Append('\n');
                    }
                }
            });

            return sb;
        }
    }
}

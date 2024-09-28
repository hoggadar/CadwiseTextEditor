using System.Text;

namespace TextEditor.Ineterfases
{
    public interface IContentHandler
    {
        Task<StringBuilder> DeleteWordsAndPunctuation(string filePath, int wordLen, bool punctuation);
    }
}

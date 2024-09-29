using System.Text;

namespace TextEditor.Interfaces
{
    public interface IContentService
    {
        Task<StringBuilder> DeleteWordsAndPunctuation(string filePath, int wordLen, bool punctuation);
    }
}

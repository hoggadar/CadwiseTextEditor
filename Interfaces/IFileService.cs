namespace TextEditor.Interfaces
{
    public interface IFileService
    {
        Task SaveContent(List<string> inputFileNames, bool isDeleteWords, int wordLength, bool isDeletePunctuation);
    }
}

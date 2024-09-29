using System.IO;
using System.Windows;
using TextEditor.Interfaces;

namespace TextEditor.Services
{
    public class FileService : IFileService
    {
        private readonly IContentService _contentService;

        public FileService(IContentService contentService)
        {
            _contentService = contentService;
        }

        public async Task SaveContent(List<string> inputFileNames, bool isDeleteWords, int wordLength, bool isDeletePunctuation)
        {
            foreach (string inputFileName in inputFileNames)
            {
                string outputFileName = Path.Combine(
                    Path.GetDirectoryName(inputFileName),
                    $"{Path.GetFileNameWithoutExtension(inputFileName)}_result{Path.GetExtension(inputFileName)}");

                var len = (isDeleteWords) ? wordLength : -1;
                var processedContent = await _contentService.DeleteWordsAndPunctuation(inputFileName, len, isDeletePunctuation);

                using StreamWriter writer = new StreamWriter(outputFileName);
                await writer.WriteAsync(processedContent.ToString());
            }
        }
    }
}

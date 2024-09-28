using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TextEditor.Ineterfases;
using TextEditor.MVVM.Commands;

namespace TextEditor.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IContentHandler _contentHandler;

        private int _wordLength;
        private bool _isDeleteWords;
        private bool _isDeletePunctuation;
        private List<string> _inputFileNames;

        public ICommand OpenInputFilesCommand { get; }
        public ICommand SaveContentCommand { get; }

        public HomeViewModel(IContentHandler contentHandler)
        {
            _contentHandler = contentHandler;

            _wordLength = 5;
            _isDeleteWords = false;
            _isDeletePunctuation = false;
            _inputFileNames = new List<string>();

            OpenInputFilesCommand = new RelayCommand(_ => OpenInputFiles(), _ => true);
            SaveContentCommand = new RelayCommand(async _ => await SaveContent(), _ => true);
        }

        private void OpenInputFiles()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Выберите файл",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                Multiselect = true
            };
            if (dialog.ShowDialog() == true) _inputFileNames = dialog.FileNames.ToList();
        }

        private async Task SaveContent()
        {
            try
            {
                foreach (var inputFileName in _inputFileNames)
                {
                    string outputFileName = Path.Combine(
                        Path.GetDirectoryName(inputFileName),
                        $"{Path.GetFileNameWithoutExtension(inputFileName)}_result{Path.GetExtension(inputFileName)}");

                    var wordLength = (_isDeleteWords) ? _wordLength : -1;
                    MessageBox.Show(_isDeletePunctuation.ToString());
                    var processedContent = await _contentHandler.DeleteWordsAndPunctuation(inputFileName, wordLength, _isDeletePunctuation);

                    using (StreamWriter writer = new StreamWriter(outputFileName))
                    {
                        await writer.WriteAsync(processedContent.ToString());
                    }

                    MessageBox.Show("Содержимое успешно сохранено.");
                }
            } catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}");
            }
        }

        public int WordLength
        {
            get => _wordLength;
            set
            {
                _wordLength = Convert.ToInt32(value);
                OnPropertyChanged(nameof(WordLength));
            }
        }

        public bool IsDeleteWords
        {
            get => _isDeleteWords;
            set
            {
                _isDeleteWords = value;
                OnPropertyChanged(nameof(IsDeleteWords));
            }
        }

        public bool IsDeletePunctuation
        {
            get => _isDeletePunctuation;
            set
            {
                _isDeletePunctuation = value;
                OnPropertyChanged(nameof(IsDeletePunctuation));
            }
        }
    }
}

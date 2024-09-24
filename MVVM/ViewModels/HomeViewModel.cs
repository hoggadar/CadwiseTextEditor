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
        private string _fileContent;

        private string _inputFileName;
        private string _outputFileName;

        public ICommand OpenInputFileCommand { get; }
        public ICommand OpenOutputFileCommand { get; }
        public ICommand GetOriginalContentCommand { get; }
        public ICommand GetChangedContentCommand { get; }
        public ICommand SaveContentCommand { get; }

        public HomeViewModel(IContentHandler contentHandler)
        {
            _contentHandler = contentHandler;

            _wordLength = 5;
            _isDeleteWords = false;
            _isDeletePunctuation = false;
            _fileContent = string.Empty;

            _inputFileName = string.Empty;
            _outputFileName = string.Empty;

            OpenInputFileCommand = new RelayCommand(_ => OpenInputFile(), _ => true);
            OpenOutputFileCommand = new RelayCommand(_ => OpenOutputFile(), _ => true);
            GetOriginalContentCommand = new RelayCommand(_ => GetOriginalContent(), _ => true);
            GetChangedContentCommand = new RelayCommand(_ => GetChangedContent(), _ => true);
            SaveContentCommand = new RelayCommand(_ => SaveContent(), _ => true);
        }

        private void OpenInputFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Выберите файл",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                _inputFileName = dialog.FileName;
                UploadFileContent();
            }
        }

        private void OpenOutputFile()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Выберите файл",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                _outputFileName = dialog.FileName;
            }
        }

        private void GetOriginalContent()
        {
            if (string.IsNullOrEmpty(_inputFileName)) return;
            UploadFileContent();
        }

        private void GetChangedContent()
        {
            GetOriginalContent();

            if (!_isDeleteWords && !_isDeletePunctuation) return;

            if (_isDeleteWords)
            {
                FileContent = _contentHandler.DeleteWords(_fileContent, _wordLength);
            }
            if (_isDeletePunctuation)
            {
                FileContent = _contentHandler.DeletePunctuation(_fileContent);
            }
        }

        private void SaveContent()
        {
            if (_inputFileName != string.Empty)
            {

            }
        }

        private void UploadFileContent()
        {
            try
            {
                string fileContent = File.ReadAllText(_inputFileName);
                FileContent = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
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

        public string FileContent
        {
            get => _fileContent;
            set
            {
                _fileContent = value;
                OnPropertyChanged(nameof(FileContent));
            }
        }
    }
}

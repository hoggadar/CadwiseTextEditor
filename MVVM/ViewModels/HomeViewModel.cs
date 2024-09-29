using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using TextEditor.Interfaces;
using TextEditor.MVVM.Commands;

namespace TextEditor.MVVM.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IFileService _fileService;

        private string _wordLength;
        private bool _isDeleteWords;
        private bool _isDeletePunctuation;
        private List<string> _inputFileNames;

        public ICommand OpenInputFilesCommand { get; }
        public ICommand SaveContentCommand { get; }

        public HomeViewModel(IFileService fileService)
        {
            _fileService = fileService;

            _wordLength = string.Empty;
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
            if (dialog.ShowDialog() == true)
            {
                _inputFileNames.Clear();
                foreach (var fileName in dialog.FileNames) _inputFileNames.Add(fileName);
                OnPropertyChanged(nameof(InputFileNames));
            }
        }

        private async Task SaveContent()
        {
            try
            {
                if (_inputFileNames.Count == 0)
                    throw new InvalidOperationException("Выберите файл");

                if (!_isDeleteWords && !_isDeletePunctuation)
                    throw new InvalidOperationException("Выберите хотя бы одну опцию");

                int length = (_isDeleteWords) ? int.Parse(_wordLength) : 0;
                await _fileService.SaveContent(_inputFileNames, _isDeleteWords, length, _isDeletePunctuation);

                WordLength = string.Empty;
                IsDeleteWords = false;
                IsDeletePunctuation = false;

                MessageBox.Show("Результат успешно записан");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Некорректная длина слова");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string InputFileNames => string.Join(Environment.NewLine, _inputFileNames);

        public string WordLength
        {
            get => _wordLength;
            set
            {
                _wordLength = value;
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

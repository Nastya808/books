using System;
using System.ComponentModel;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

public class BookFormViewModel : INotifyPropertyChanged
{
    private Book _newBook;

    public Book NewBook
    {
        get { return _newBook; }
        set
        {
            _newBook = value;
            OnPropertyChanged(nameof(NewBook));
        }
    }

    public ICommand AddCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public BookFormViewModel()
    {
        NewBook = new Book();

        AddCommand = new RelayCommand(AddBook);
        CancelCommand = new RelayCommand(Cancel);
    }

    private void AddBook(object parameter)
    {
        var bookViewModel = (BookViewModel)Application.Current.MainWindow.DataContext;
        bookViewModel.Books.Add(NewBook);

        Close();
    }

    private void Cancel(object parameter)
    {
        Close();
    }

    private void Close()
    {
        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        window?.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

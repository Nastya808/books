using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

public class BookViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Book> _books;
    private Book _selectedBook;
    private string _searchText;
    private int _itemsPerPage = 5;
    private int _currentPage = 1;

    public ObservableCollection<Book> Books
    {
        get { return _books; }
        set
        {
            _books = value;
            OnPropertyChanged(nameof(Books));
        }
    }

    public Book SelectedBook
    {
        get { return _selectedBook; }
        set
        {
            _selectedBook = value;
            OnPropertyChanged(nameof(SelectedBook));
        }
    }

    public string SearchText
    {
        get { return _searchText; }
        set
        {
            _searchText = value;
            FilterBooks();
            OnPropertyChanged(nameof(SearchText));
        }
    }

    public int CurrentPage
    {
        get { return _currentPage; }
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
            LoadBooks();
        }
    }

    public int TotalPages
    {
        get { return (int)Math.Ceiling((double)Books.Count / _itemsPerPage); }
    }

    public ICommand AddCommand { get; set; }
    public ICommand EditCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand OpenAddBookFormCommand { get; set; }

    public BookViewModel()
    {
        AddCommand = new RelayCommand(AddBook);
        EditCommand = new RelayCommand(EditBook, CanEditOrDelete);
        DeleteCommand = new RelayCommand(DeleteBook, CanEditOrDelete);
        OpenAddBookFormCommand = new RelayCommand(OpenAddBookForm);

        LoadBooks();
    }

    private void LoadBooks()
    {
        Books = new ObservableCollection<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Genre = "Genre 1", Year = 2020 },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Genre = "Genre 2", Year = 2021 },
            new Book { Id = 3, Title = "Book 3", Author = "Author 3", Genre = "Genre 3", Year = 2019 },
        };
    }

    private void FilterBooks()
    {
        var filteredBooks = _books.Where(b => b.Title.Contains(_searchText, StringComparison.OrdinalIgnoreCase)).ToList();
        Books = new ObservableCollection<Book>(filteredBooks);
    }

    private void AddBook(object parameter)
    {
        var newBookForm = new BookForm();
        newBookForm.ShowDialog();
        LoadBooks(); 
    }

    private void EditBook(object parameter)
    {
        if (SelectedBook != null)
        {
            var editBookForm = new BookForm();
            editBookForm.ShowDialog();
            LoadBooks(); 
        }
    }

    private void DeleteBook(object parameter)
    {
        Books.Remove(SelectedBook);
        SelectedBook = null;
    }

    private bool CanEditOrDelete(object parameter)
    {
        return SelectedBook != null;
    }

    private void OpenAddBookForm(object parameter)
    {
        var addBookForm = new BookForm();
        addBookForm.ShowDialog();
        LoadBooks(); 
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

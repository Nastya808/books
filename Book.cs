using System.ComponentModel;

public class Book : INotifyPropertyChanged
{
    private int _id;
    private string _title;
    private string _author;
    private string _genre;
    private int _year;

    public int Id
    {
        get { return _id; }
        set
        {
            _id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Author
    {
        get { return _author; }
        set
        {
            _author = value;
            OnPropertyChanged(nameof(Author));
        }
    }

    public string Genre
    {
        get { return _genre; }
        set
        {
            _genre = value;
            OnPropertyChanged(nameof(Genre));
        }
    }

    public int Year
    {
        get { return _year; }
        set
        {
            _year = value;
            OnPropertyChanged(nameof(Year));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

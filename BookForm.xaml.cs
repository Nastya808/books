using System.Windows;

public partial class BookForm : Window
{
    public BookForm()
    {
        InitializeComponent();
        DataContext = new BookFormViewModel();
    }
}

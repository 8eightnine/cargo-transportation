using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using cargo_transportation.Hash;

#nullable disable

namespace cargo_transportation
{
/// <summary>
/// Логика взаимодействия для Authorization.xaml
/// </summary>
public partial class Authorization : Window
{
    // Data needed for authorization
    private string _login;
    private string _password;
    private bool _isLoggedIn;
    private bool _isAuthenticated;

    public Authorization()
    {
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _login = login_box.Text;
        _password = password_box.Password;
        _password = Hash.Hash.hashPassword(_password);

        // TODO: check if password is correct
        // For now just a simple check
    }
}
}
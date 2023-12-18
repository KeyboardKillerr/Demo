using ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CaptchaGen.SkiaSharp;
using SkiaSharp;

namespace Demo.WpfApp.Pages;

/// <summary>
/// Логика взаимодействия для LoginPage.xaml
/// </summary>
public partial class LoginPage : Page
{
    private Presentation VM { get; init; }
    public LoginPage()
    {
        InitializeComponent();
        DataContext = App.ViewModel;
        if (DataContext is Presentation viewmodel) VM = viewmodel;
        VM.LoginVM.Captcha.PropertyChanged += DisplayCaptcha;
        VM.LoginVM.Captcha.Update();
    }

    private void DisplayCaptcha(object sender, EventArgs e)
    {
        var imageSource = new BitmapImage();
        imageSource.BeginInit();
        imageSource.StreamSource = new CaptchaGenerator().GenerateImageAsStream(VM.LoginVM.Captcha.Text);
        imageSource.EndInit();
        CaptchaImage.Source = imageSource;
    }

    private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        VM.LoginVM.Password = PassBox.Password;
    }
}

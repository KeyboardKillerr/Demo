using ViewModel;
using System;
using System.Windows.Controls;
using Demo.WpfApp.Pages;
using Demo.WpfApp.Windows;

namespace Demo.WpfApp;

internal class SetHelper
{   
    internal static MainWindow MainWin { get; set; }
    internal static EditingWindow EditWin { get; set; }
    private static Page LoginPage { get; set; }
    private static Page MainPage { get; set; }
    private static Page EditingPage { get; set; }

    static SetHelper()
    {
        Helper.ErrorHandler = new ErrorHandler();
        Helper.LoginToMain = NaviToMain;
        Helper.MainToLogin = NaviToLogin;
        Helper.MainToEdit = NaviToEdit;
        Helper.EditToMain = NaviToMainFromEdit;

        LoginPage = new LoginPage();
        MainPage = new MainPage();
        EditingPage = new EditingPage();
    }

    internal static void NaviToLogin(object obj) => MainWin.MainFrame.Navigate(LoginPage);
    internal static void NaviToMain(object obj) => MainWin.MainFrame.Navigate(MainPage);
    internal static void NaviToEdit(object obj)
    {
        EditWin = EditingWindow.Get();
        EditWin.MainFrame.Navigate(EditingPage);
        EditWin.Show();
    }
    internal static void NaviToMainFromEdit(object obj) => EditWin.Close();
}

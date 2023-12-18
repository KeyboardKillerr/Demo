using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;
using ViewModel.Exceptions;
using ViewModelBase.Commands.ErrorHandlers;

namespace Demo.WpfApp;

internal class ErrorHandler : IErrorHandler
{
    public void ErrorHandle(Exception e)
    {
        if (e is BadAttemptException)
        {
            MessageBox.Show("Логин или пароль введены неправильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (e is CaptchaException)
        {
            MessageBox.Show("Капча введена неправильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (e is InvalidImagePathException)
        {
            MessageBox.Show("Выбран файл неподходящего формата", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (e is BadImageException)
        {
            MessageBox.Show("Картинка должна быть рзамера 300x200", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

using DataModel;
using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Exceptions;
using ViewModel.Other;
using ViewModelBase.Commands.AsyncCommands;
using ViewModelBase.Commands.QuickCommands;

namespace ViewModel.ViewModels;

public class EditingViewModel : ViewModelBase.ViewModelBase
{
    private readonly DataManager data = Helper.DataModel;
    private Presentation OuterContext { get; init; }

    public Command DeleteImage { get; private init; }
    public Command SaveChangesAndExit { get; private init; }
    public Command Cancel { get; private init; }

    public EditingViewModel(Presentation outerContext)
    {
        DeleteImage = new(DeleteImageFunc, CanExecute);
        Cancel = new(CancelFunc, CanExecute);
        SaveChangesAndExit = new(SaveChangesAndExitFunc, CanExecute);
        OuterContext = outerContext;
    }

    private void Refresh()
    {
        ImagePath = Product.ImagePath;
    }

    private async void SaveChangesAndExitFunc()
    {
        Product? searchResult = null;
        Product product;

        do
        {
            product = new()
            {
                Type = "",
                MeasurmentUnit = "",
                Manufacturer = "",
                Dealer = "",
                Category = "",
                Description = "",
                ImagePath = ImagePath
            };
            searchResult = data.Products.Items.FirstOrDefault(x => x.Id == product.Id);
        }
        while (searchResult is not null);

        await data.Products.UpdateAsync(product);
        OuterContext.MainVM.Refresh();

        Helper.EditToMain?.Invoke(null);
    }

    private void CancelFunc() => Helper.EditToMain?.Invoke(null);
    private void DeleteImageFunc() => ImagePath = null;
    public void OnWindowClosing(object sender, CancelEventArgs e) => Refresh();

    public void SetImage(string imagePath, string saveDirectoryPath)
    {
        if (!Directory.Exists(saveDirectoryPath))
        {
            Helper.ErrorHandler?.ErrorHandle(new InvalidDirectoryPathException(saveDirectoryPath));
            return;
        }
        if (!Path.GetExtension(imagePath).In(".png", ".jpg", ".jpeg"))
        {
            Helper.ErrorHandler?.ErrorHandle(new InvalidImagePathException(imagePath));
            return;
        }

        //var img = Image.FromFile(imagePath);
        //if (!(img.Height == 300 && img.Width == 200))
        //{
        //    Helper.ErrorHandler?.ErrorHandle(new BadImageException(imagePath));
        //    return;
        //}

        if (!File.Exists(saveDirectoryPath + "\\" + Path.GetFileName(imagePath)))
            File.Copy(imagePath, saveDirectoryPath + "\\" + Path.GetFileName(imagePath));

        ImagePath = Path.GetFileName(imagePath);
    }
    private bool CanExecute() => true;

    private string? _imagePath;
    public string? ImagePath
    {
        get { return _imagePath; }
        private set { Set(ref _imagePath, value); }
    }

    private Product _product = Defaults.DefaultProduct;
    public Product? Product
    {
        get { return _product; }
        set { if (Set(ref _product, value is null ? Defaults.DefaultProduct : value)) Refresh(); }
    }
}

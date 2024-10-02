using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace DungeonsOfInfinityTrainer;

public partial class CheatExportDonePrompt : Window
{
    public CheatExportDonePrompt()
    {
        InitializeComponent();
    }

    private void OnCloseClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
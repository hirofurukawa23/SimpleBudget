using Prism.Ioc;
using Prism.Unity;
using SB.Infrastructures;
using SB.Presentation.ViewModels.Dialogs;
using SB.Presentation.Views;
using SB.Presentation.Views.Dialogs;
using System.Windows;

namespace SimpleBudget
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            SqliteCore.InitDb();

            var w = Container.Resolve<ExpensesListView>();
            return w;
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ExpenseInputDialogView, ExpenseInputDialogViewModel>();
        }
    }
}

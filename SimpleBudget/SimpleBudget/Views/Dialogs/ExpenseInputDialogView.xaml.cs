using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SB.Presentation.Views.Dialogs
{
    /// <summary>
    /// ExpenseInputDialogView.xaml の相互作用ロジック
    /// </summary>
    public partial class ExpenseInputDialogView : UserControl
    {
        public ExpenseInputDialogView()
        {
            InitializeComponent();
        }

        #region 金額テキストボックス

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 0-9のみ
            e.Handled = !new Regex("[0-9]").IsMatch(e.Text);
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}

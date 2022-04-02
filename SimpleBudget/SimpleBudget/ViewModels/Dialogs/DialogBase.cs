using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace SB.Presentation.ViewModels.Dialogs
{
    /// <summary>
    /// ダイアログ用のベースクラス（IDialogAwareをラップしただけ）
    /// </summary>
    public class DialogBase : BindableBase, IDialogAware
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="title"></param>
        public DialogBase(string title)
        {
            Title = title;
        }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; }


        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        /// 画面閉設時
        /// </summary>
        public void OnDialogClosed() { }

        /// <summary>
        /// 画面開設時
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}

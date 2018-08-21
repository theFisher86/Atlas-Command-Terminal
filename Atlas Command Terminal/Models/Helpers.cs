using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Atlas_Command_Terminal.Models
{
    public class Helpers
    {
        public static async void BasicDialogBox(string title, string content, string buttonText = "Ok")
        {
            ContentDialog dialogBox = new ContentDialog();
            dialogBox.Title = title;
            dialogBox.Content = content;
            dialogBox.CloseButtonText = buttonText;

            await dialogBox.ShowAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Therapist
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message()
        {
            InitializeComponent();
        }
        public Message(string content): this()
        {
            Content.Text = content;
            
        }
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        
    }
    //public static class WindowExtension
    //{
    //    public static Task<bool?> ShowDialogAsync(this Window window, CancellationToken cancellationToken = new CancellationToken())
    //    {
    //        var completionSource = new TaskCompletionSource<bool?>();

    //        window.Dispatcher.BeginInvoke(new Action(() =>
    //        {
    //            var result = window.ShowDialog();
    //            // When dialog is closed, set the result to complete the returned task. If the task is already cancelled, it will be discarded.
    //            completionSource.TrySetResult(result);
    //        }));

    //        if (cancellationToken.CanBeCanceled)
    //        {
    //            // Gets notified when cancellation is requested so that we can close window and cancel the returned task 
    //            cancellationToken.Register(() => window.Dispatcher.BeginInvoke(new Action(() =>
    //            {
    //                completionSource.TrySetCanceled();
    //                window.Close();
    //            })));
    //        }

    //        return completionSource.Task;
    //    }
    //}
}

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
using System.Windows.Shapes;

namespace ClientRegistry
{
    /// <summary>
    /// Interaction logic for ContactFormWindow.xaml
    /// </summary>
    public partial class ContactFormWindow : Window
    {
        public ContactFormWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            string message;
            var formVM = DataContext as ContactFormVM;
            if (!formVM.ChosenContactValidate(out message))
                MessageBox.Show(message);
            else
            {
                formVM.SavePartner();
                Close();
            }
                
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var formVM = DataContext as ContactFormVM;
            if (formVM.IsEdit && formVM.IsModified())
            {
                var result = MessageBox.Show("Nem mentett változások vannak.\n\rSzeretné menteni?", "Figyelmeztetés", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    formVM.SavePartner();
                else
                    formVM.RestoreContact();
            }
                
        }
    }
}

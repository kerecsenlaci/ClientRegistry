using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for PartnerFormWindow.xaml
    /// </summary>
    public partial class PartnerFormWindow : Window
    {
        public PartnerFormWindow()
        {
            InitializeComponent();
        }

        private void SavePartnerForm(object sender, RoutedEventArgs e)
        {
            string message;
            var formVM = DataContext as PartnerFormVM;
            if (!formVM.PartnerValidate(out message))
                MessageBox.Show(message);
            else
            {
                formVM.SavePartner();
                Close();
            }
                
        }
    }
}

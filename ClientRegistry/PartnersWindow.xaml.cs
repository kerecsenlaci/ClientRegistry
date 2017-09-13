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
    /// Interaction logic for PartnersWindow.xaml
    /// </summary>
    public partial class PartnersWindow : Window
    {
        public PartnersWindow()
        {
            InitializeComponent();
        }

        private void PartnerFormClick(object sender, MouseButtonEventArgs e)
        {
            PartnerFormVM partnerForm = new PartnerFormVM();
            partnerForm.Partner = (partners)((ListBox)sender).SelectedItem;
            PartnerFormWindow formWindow = new PartnerFormWindow { DataContext = partnerForm  };
            formWindow.ShowDialog();
        }
    }
}

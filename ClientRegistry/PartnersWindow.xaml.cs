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
            PartnersVM partnersVM = DataContext as PartnersVM;
            if (partnersVM != null && partnersVM.SelectedParameter!=null)
            {
                PartnerFormVM partnerForm = new PartnerFormVM { ChosenPartner = partnersVM.SelectedParameter };
                PartnerFormWindow formWindow = new PartnerFormWindow { DataContext = partnerForm };
                partnerForm.ValuesTransmission();
                formWindow.ShowDialog();
            }
        }
    }
}

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
using Microsoft.Reporting.WinForms;
using System.Collections.ObjectModel;

namespace ClientRegistry
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(Partner partner, string OwnerName, ObservableCollection<Contact> list )
        {
            InitializeComponent();
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("Name", partner.Name);
            parameters[1] = new ReportParameter("Address", partner.Address);
            parameters[2] = new ReportParameter("Type", partner.City);
            parameters[3] = new ReportParameter("Owner", OwnerName);
            parameters[4] = new ReportParameter("Timestamp", DateTime.Now.ToString("f"));


            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "ContactList";
            reportDataSource.Value = list;
            reportViewer.LocalReport.ReportPath = "..\\..\\Rdlc\\PartnerForm.rdlc";
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.LocalReport.SetParameters(parameters);
            reportViewer.RefreshReport();
        }

        private void reportViewer_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {
        }
    }
}

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

namespace WODIA_Reserach_Guide
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ExperienceQuestionnaire1 : Window
    {
        public ExperienceQuestionnaire1()
        {
            InitializeComponent();
        }


        private void SecondPage_Click(object sender, RoutedEventArgs e)
        {
            var participant = ((MainWindow)Application.Current.MainWindow).CurrentParticipant;
            participant.agree1 = agree1.Text;
            participant.agree2 = agree2.Text;
            participant.agree3 = agree3.Text;
            participant.agree4 = agree4.Text;
            participant.agree5 = agree5.Text;

            ((MainWindow)Application.Current.MainWindow).SaveData();
            Close();
            var form = new ExperienceQuestionnaire2();
            form.ShowDialog();
        }
    }
}

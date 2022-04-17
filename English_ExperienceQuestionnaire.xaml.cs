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
    public partial class English_ExperienceQuestionnaire : Window
    {
        public English_ExperienceQuestionnaire()
        {
            InitializeComponent();
        }


        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            var participant = ((MainWindow)Application.Current.MainWindow).CurrentParticipant;
            participant.Page1_1 = Page1_1.Text;
            participant.Page1_2 = Page1_2.Text;
            participant.Page1_3 = Page1_3.Text;
            participant.Page1_4 = Page1_4.Text;
            participant.Page1_5 = Page1_5.Text;

            ((MainWindow)Application.Current.MainWindow).SaveData();
            Close();
            var form = new English_ExperienceQuestionnaire1();
            form.ShowDialog();
        }
    }
}

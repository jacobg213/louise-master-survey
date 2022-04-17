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
    /// Interaction logic for MaternalCharacteristicsQuestionnaire.xaml
    /// </summary>
    public partial class EnglishMaternalCharacteristicsQuestionnaire : Window
    {
        public EnglishMaternalCharacteristicsQuestionnaire()
        {
            InitializeComponent();
        }

        private void Age_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            { 
                Int32.Parse(((TextBox)sender).Text);
            } catch 
            { ((TextBox)sender).Text = "";  }
        }

        private void EndQuistionnaire(object sender, RoutedEventArgs e)
        {
            try
            {
                var participant = ((MainWindow)Application.Current.MainWindow).CurrentParticipant;
                participant.Age = Int32.Parse(Age.Text);
                participant.Height = Int32.Parse(PersonHeight.Text);
                participant.Weight = Int32.Parse(Weight.Text);
                participant.GA = Int32.Parse(GA.Text);
                participant.Race = Race.Text;
                participant.PregnancyConception = PregnancyConception.Text;
                participant.Smoker = Smoker.Text;
                participant.ChronicBP = ChronicBP.Text;
                participant.SLE = SLE.Text;
                participant.AntifosSyndrome = AntifosSyndrome.Text;
                participant.DM = DM.Text;
                participant.FormerBirth = FormerBirth.Text;
                participant.GenericDisposed = GenericDisposed.Text;
                participant.Interval = Int32.Parse(Interval.Text);
                participant.PastChildAge = Int32.Parse(PastChildAge.Text);
                participant.EndQuistionnaire = DateTime.Now;
                Close();
            }
            catch
            {
                MessageBox.Show("You are missing one");
            }
            }

            }
}


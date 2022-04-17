using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class English_ExperienceQuestionnaire2 : Window
    {
        public English_ExperienceQuestionnaire2()
        {
            InitializeComponent();
        }

        private void SaveQuestionnaire_Click(object sender, RoutedEventArgs e)
        {
            var participant = ((MainWindow)Application.Current.MainWindow).CurrentParticipant;
            participant.Choose1 = Choose1.Text;
            participant.Choose1Elaborate = Choose1Elaborate.Text;

            participant.Choose2 = Choose2.Text;
            participant.Choose2Elaborate = Choose2Elaborate.Text;

            participant.Choose3 = Choose3.Text;
            participant.Choose3Elaborate = Choose3Elaborate.Text;

            participant.Choose4 = Choose4.Text;
            participant.Choose4Elaborate = Choose4Elaborate.Text;

            participant.Choose5 = Choose5.Text;
            participant.Choose5Elaborate = Choose5Elaborate.Text;

            participant.Choose6 = Choose6.Text;
            participant.Choose6Elaborate = Choose6Elaborate.Text;

            SaveQuestions();
            Close();
        }

        public void SaveQuestions()
        {
            var participant = ((MainWindow)Application.Current.MainWindow).CurrentParticipant;
            if (participant == null)
                return;

            //Here  we construct the new row (e.g. all data must arrive here)
            var newAnswer = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21}", participant.Page1_1, participant.Page1_2, participant.Page1_3, participant.Page1_4, participant.Page1_5, participant.agree1, participant.agree2, participant.agree3, participant.agree4, participant.agree5, participant.Choose1, participant.Choose1Elaborate, participant.Choose2, participant.Choose2Elaborate, participant.Choose3, participant.Choose3Elaborate, participant.Choose4, participant.Choose4Elaborate, participant.Choose5, participant.Choose5Elaborate, participant.Choose6, participant.Choose6Elaborate);
            
            //First - we read ALL lines - in order to check for a previous version
            string[] lines = null;

            if (File.Exists("QuestionAnswers.csv"))
                lines = File.ReadAllLines("QuestionAnswers.csv");

            //If there are no lines - or no file - this will null or 0
            if (lines != null && lines.Length > 0)
            {
                //There are lines already - now lets find the research number - we need to split the data
                var columns = lines[lines.Length - 1].Split(";");
                //If there is at least one column - then get the research number
                if (columns.Length > 0)
                {
                    //If the research number - of the last line - equals the current research number - then update
                    if (columns[0] == participant.ResearchNumber)
                    {
                        lines[lines.Length - 1] = newAnswer;
                        File.Delete("QuestionAnswers.csv");
                        File.WriteAllLines("QuestionAnswers.csv", lines);
                    }
                    else
                    {
                        var list = new List<string>();
                        list.Add(newAnswer);
                        File.AppendAllLines("QuestionAnswers.csv", list);
                    }
                }
            }
            else
            {
                var QuestionHeaders = "Q1;Q2;Q3;Q4;Q5;Q6;Q7;Q8;Q9;Q10;Q11;Q12;Q13;Q14;Q15;Q16;Q17";
                QuestionHeaders += Environment.NewLine;
                QuestionHeaders += newAnswer;
                File.AppendAllText("QuestionAnswers.csv", QuestionHeaders);
            }
        }
    }

    
}

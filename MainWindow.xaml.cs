using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WODIA_Reserach_Guide
{

    public class ResearchParticipant
    {
        public string ResearchNumber { get; set; }
        public DateTime ToUltrasound { get; set; }
        public DateTime ArrivedUltrasound { get; set; }
        public DateTime FinishedUltrasound { get; set; }
        public DateTime WalkFromUltrasound { get; set; }
        public DateTime ArrivedBT { get; set; }
        public DateTime IntroductionStart { get; set; }
        public DateTime IntroductionEnd { get; set; }
        public DateTime FirstCuffStart { get; set; }
        public DateTime FirstCuffEnd { get; set; }
        public DateTime SecondCuffStart { get; set; }
        public DateTime SecondCuffEnd { get; set; }
        public DateTime NurseCuffStart { get; set; }
        public DateTime NurseCuffEnd { get; set; }
        public DateTime CuffRemovalStart { get; set; }
        public DateTime CuffRemovalEnd { get; set; }
        public DateTime ReadyToLeave { get; set; }
        public DateTime RoomLeft { get; set; }
        public string ArmMeasure { get; set; }
        public bool WasRightArmFirst { get; set; }
        public bool IsFirstCuffPlacingCorrect { get; set; }
        public bool IsSecondCuffPlacingCorrect { get; set; }
        public string ArrivedAtWaitingRoom { get; set; }
        public int Age { get; internal set; }
        public int Height { get; internal set; }
        public int Weight { get; internal set; }
        public int GA { get; internal set; }
        public string Race { get; internal set; }
        public string PregnancyConception { get; internal set; }
        public string Smoker { get; internal set; }
        public string ChronicBP { get; internal set; }
        public string SLE { get; internal set; }
        public string AntifosSyndrome { get; internal set; }
        public string DM { get; internal set; }
        public string FormerBirth { get; internal set; }
        public string GenericDisposed { get; internal set; }
        public int Interval { get; internal set; }
        public int PastChildAge { get; internal set; }
        public DateTime EndQuistionnaire { get; internal set; }
        public DateTime StartQuestionnaire { get; internal set; }
        public string Page1_1 { get; internal set; }
        public string Page1_2 { get; internal set; }
        public string Page1_3 { get; internal set; }
        public string Page1_4 { get; internal set; }
        public string Page1_5 { get; internal set; }
        public string agree1 { get; internal set; }
        public string agree2 { get; internal set; }
        public string agree3 { get; internal set; }
        public string agree4 { get; internal set; }
        public string agree5 { get; internal set; }
        public string Choose1 { get; internal set; }
        public string Choose2 { get; internal set; }
        public string Choose3 { get; internal set; }
        public string Choose4 { get; internal set; }
        public string Choose5 { get; internal set; }
        public string Choose6 { get; internal set; }
        public string Choose1Elaborate { get; internal set; }
        public string Choose2Elaborate { get; internal set; }
        public string Choose3Elaborate { get; internal set; }
        public string Choose4Elaborate { get; internal set; }
        public string Choose5Elaborate { get; internal set; }
        public string Choose6Elaborate { get; internal set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
        List<Control> DisabledList = new List<Control>();
        private string CurrentLang = "DK";
        private int ResearchNumber;

        public MainWindow()
        {
            InitializeComponent();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            if (File.Exists("wodia.config.txt"))
            {
                var configContent = File.ReadAllText("wodia.config.txt");
                var hasLatestNumber = int.TryParse(configContent, out int latestResearchNumber);

                if (hasLatestNumber)
                {
                    ResearchNumber = latestResearchNumber + 1;
                }
                else
                {
                    ResearchNumber = 1;
                    File.WriteAllText("wodia.config.txt", "0");
                }
            }
            else
            {
                ResearchNumber = 1;
                File.WriteAllText("wodia.config.txt", "0");
            }
        }

        public ResearchParticipant CurrentParticipant { get; private set; }

        private void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            Clock.Content = d.ToString("HH:mm:ss");
        }

        private void Disable(object sender)
        {
            ((Control)sender).IsEnabled = false;
            DisabledList.Add((Control)sender);
        }

        private void NewParticipant_Click(object sender, RoutedEventArgs e)
        {
            CurrentParticipant = new ResearchParticipant();
            CurrentParticipant.ResearchNumber = ResearchNumber.ToString();
            ResearchID.Content = $"Forskningsnummer: {CurrentParticipant.ResearchNumber}";

            Disable(sender);
        }

        public void SaveData()
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.ArmMeasure = ArmMeasure.Text;
            CurrentParticipant.ArrivedAtWaitingRoom = ArrivedAtWaitingRoom.Text;
            CurrentParticipant.WasRightArmFirst = FirstArm.IsChecked.HasValue ? FirstArm.IsChecked.Value : false;
            CurrentParticipant.IsFirstCuffPlacingCorrect = FirstCuffPlacing.IsChecked.HasValue ? FirstCuffPlacing.IsChecked.Value : false;
            CurrentParticipant.IsSecondCuffPlacingCorrect = SecondCuffPlacing.IsChecked.HasValue ? SecondCuffPlacing.IsChecked.Value : false;

            //Here  we construct the new row (e.g. all data must arrive here)
            var newLine = String.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26}", CurrentParticipant.ResearchNumber, CurrentParticipant.ArrivedAtWaitingRoom, CurrentParticipant.ToUltrasound, CurrentParticipant.ArrivedUltrasound, CurrentParticipant.FinishedUltrasound, CurrentParticipant.WalkFromUltrasound, CurrentParticipant.ArrivedBT, CurrentParticipant.IntroductionStart, CurrentParticipant.IntroductionEnd, CurrentParticipant.WasRightArmFirst, CurrentParticipant.IsFirstCuffPlacingCorrect, CurrentParticipant.IsSecondCuffPlacingCorrect, CurrentParticipant.FirstCuffStart, CurrentParticipant.FirstCuffEnd, CurrentParticipant.SecondCuffStart, CurrentParticipant.SecondCuffEnd, CurrentParticipant.NurseCuffStart, CurrentParticipant.NurseCuffEnd, CurrentParticipant.CuffRemovalStart, CurrentParticipant.CuffRemovalEnd, CurrentParticipant.ArmMeasure, CurrentParticipant.Age, CurrentParticipant.FormerBirth, CurrentParticipant.StartQuestionnaire, CurrentParticipant.EndQuistionnaire, CurrentParticipant.ReadyToLeave, CurrentParticipant.RoomLeft);

            //First - we read ALL lines - in order to check for a previous version
            string[] lines = null;

            if (File.Exists("data.csv"))
                lines = File.ReadAllLines("data.csv");

            //If there are no lines - or no file - this will null or 0
            if (lines != null && lines.Length > 0)
            {
                //There are lines already - now lets find the research number - we need to split the data
                var columns = lines[lines.Length - 1].Split(";");
                //If there is at least one column - then get the research number
                if (columns.Length > 0)
                {
                    //If the research number - of the last line - equals the current research number - then update
                    if (columns[0] == CurrentParticipant.ResearchNumber)
                    {
                        lines[lines.Length - 1] = newLine;
                        File.Delete("data.csv");
                        File.WriteAllLines("data.csv", lines);
                    }
                    else
                    {
                        var list = new List<string>();
                        list.Add(newLine);
                        File.AppendAllLines("data.csv", list);
                    }
                }
            }
            else
            {
                var lineWithHeaders = "ResearchNumber;ArrivedAtWaitingRoom;ToUltrasound;ArrivedUltrasound;FinishedUltrasound;WalkFromUltrasound;ArrivedBT;IntroductionStart;IntroductionEnd;WasRightArmFirst;IsFirstCuffPlacingCorrect;IsSecondCuffPlacingCorrect;FirstCuffStart;FirstCuffEnd;SecondCuffStart;SecondCuffEnd;NurseCuffStart;NurseCuffEnd;CuffRemovalStart;CuffRemovalEnd;ArmMeasure;Age;FormerBirth;StartQuestionnaire;EndQuistionnaire;ReadyToLeave;RoomLeft";
                lineWithHeaders += Environment.NewLine;
                lineWithHeaders += newLine;
                File.AppendAllText("data.csv", lineWithHeaders);
            }
        }

        private void StartQuestionnaire_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant != null)
            {
                CurrentParticipant.StartQuestionnaire = DateTime.Now;

                switch (CurrentLang)
                {
                    case "DK":
                        {
                            var form = new MaternalCharacteristicsQuestionnaire();
                            form.ShowDialog();
                            break;
                        }
                    case "EN":
                        {
                            var form = new EnglishMaternalCharacteristicsQuestionnaire();
                            form.ShowDialog();
                            break;
                        }
                }

                SaveData();
            }
        }

        private void Age_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                Int64.Parse(((TextBox)sender).Text);
            }
            catch
            { ((TextBox)sender).Text = ""; }
        }

        private void StartExperinceQuestionnaire_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant != null)
            {
                switch (CurrentLang)
                {
                    case "DK":
                        {
                            var form = new ExperienceQuestionnaire();
                            form.ShowDialog();
                            break;
                        }
                    case "EN":
                        {
                            var form = new English_ExperienceQuestionnaire();
                            form.ShowDialog();
                            break;
                        }
                }

                SaveData();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            SaveData();
            DisabledList.ForEach(control => control.IsEnabled = true);

            File.WriteAllText("wodia.config.txt", ResearchNumber.ToString());
            ResearchNumber = ResearchNumber + 1;


            CurrentLang = "DK";
            Lang.Content = "English";
            ArrivedAtWaitingRoom.Text = "Ankomst tid";
            ResearchID.Content = "Forskningsnummer: ";
            ArmMeasure.Text = "Arm omkreds";
            StartExperienceQuestionnaire.Content = "UNDERSØGELSESSPØRGSMÅL";
            StarQuestionnaire.Content = "START SPØRGESKEMA";
            FirstArm.IsChecked = false;
            FirstCuffPlacing.IsChecked = false;
            SecondCuffPlacing.IsChecked = false;

            CurrentParticipant = null;
        }


        private void ReadyToLeave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.ReadyToLeave = DateTime.Now;

            Disable(sender);

            SaveData();
        }
    
        private void TimeToUltrasound_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.ToUltrasound = DateTime.Now;

            Disable(sender);

            SaveData();

        }

        private void ArrivedToUltrasound_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.ArrivedUltrasound = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void DoneWithUltrasound_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.FinishedUltrasound = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void WalkFromUltrasound_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.WalkFromUltrasound = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void ArrivedToBT_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.ArrivedBT = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void StartIntroduction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.IntroductionStart = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void EndIntroduction_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.IntroductionEnd = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void FirstCuffStart_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.FirstCuffStart = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void FirstCuffEnd_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.FirstCuffEnd = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void SecondCuffStart_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.SecondCuffStart = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void SecondCuffEnd_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.SecondCuffEnd = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void StartNurse_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.NurseCuffStart = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void EndNurse_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.NurseCuffEnd = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void CuffRemovalStart_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.CuffRemovalStart = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void CuffRemovalEnd_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.CuffRemovalEnd = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void RoomLeft_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParticipant == null)
                return;

            CurrentParticipant.RoomLeft = DateTime.Now;

            Disable(sender);

            SaveData();
        }

        private void Lang_Click(object sender, RoutedEventArgs e)
        {
            switch (CurrentLang)
            {
                case "DK":
                    CurrentLang = "EN";
                    Lang.Content = "Dansk";
                    break;
                case "EN":
                    CurrentLang = "DK";
                    Lang.Content = "English";
                    break;
            }
        }

        private void GetDataClick(object sender, RoutedEventArgs e)
        {
            var dataWindow = new Data();
            dataWindow.Show();
        }
    }
}

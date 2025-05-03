using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.Xml;

namespace Insurances
{
    public partial class MainWindow : Window
    {

        public string InsuranceName;
        public int IsPartnered;
        public int NeedReferral;
        public int Accept;
        public string Other;
        public MainWindow()
        {



            InitializeComponent();
            string[] lines = System.IO.File.ReadAllLines("InsuranceData.txt");
            int size = Convert.ToInt32(lines[0]);
            InsuranceDefine[] insurances = new InsuranceDefine[size];
            for (int index = 0; index < size; index++)
            {
                string[] tokens = lines[index + 1].Split('_');
                insurances[index] = new InsuranceDefine();
                {
                    InsuranceName = tokens[0];
                    IsPartnered = Convert.ToInt32(tokens[1]);
                    NeedReferral = Convert.ToInt32(tokens[2]);
                    Accept = Convert.ToInt32(tokens[3]);
                    Other = tokens[4];
                }
                InsuranceDefine insuranceDefine = new InsuranceDefine { insuranceName = InsuranceName, isPartnered = IsPartnered, needReferral = NeedReferral, accept = Accept, otherRequire = Other };

                ComboCompany.Items.Add(insuranceDefine);
                ComboX.Items.Add(insuranceDefine);

            }


            ComboCompany.SelectionChanged += ComboBox_SelectionChanged;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Confirm1st.Visibility = Visibility.Hidden;
            Confirm2nd.Visibility = Visibility.Hidden;
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
            Confirm2nd.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
            Confirm2nd.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
            Confirm2nd.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
            Confirm2nd.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InsuranceDefine selectedDefiner = ComboCompany.SelectedItem as InsuranceDefine;

            InsuranceDefine selectedDefine = ComboX.SelectedItem as InsuranceDefine;

            if (selectedDefiner != null)
            {
                switch (selectedDefiner.isPartnered)
                {
                    case 0:
                        Confirm1st.Visibility = Visibility.Hidden;
                        MessageBox.Show("I am not sure if we do accept this. Please speak to your Billing Department or Office Administrator for more details!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Question);
                        break;
                    case 1:
                        Confirm1st.Visibility = Visibility.Visible;
                        Confirm1st.Text = "We accept this Insurance!";
                        Confirm1st.Foreground = Brushes.Green;
                        break;
                    case 2:
                        Confirm1st.Visibility = Visibility.Visible;
                        Confirm1st.Text = "We cannot accept this Insurance...";
                        Confirm1st.Foreground = Brushes.Red;
                        break;
                    case 3:
                        if (MessageBox.Show("Does the patient have any Out Of Network benefits?", $"{selectedDefiner.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We accept this Insurance!";
                            Confirm1st.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We cannot accept this Insurance...";
                            Confirm1st.Foreground = Brushes.Red;
                        }
                        break;
                    case 4:
                        MessageBox.Show("This insurance must be a secondary only...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                        Confirm1st.Visibility = Visibility.Visible;
                        Confirm1st.Text = "We cannot accept this Insurance...";
                        Confirm1st.Foreground = Brushes.Red;
                        break;
                    case 5:
                        MessageBox.Show("This insurance must be a secondary only to Medicare...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                        Confirm1st.Visibility = Visibility.Visible;
                        Confirm1st.Text = "We cannot accept this Insurance...";
                        Confirm1st.Foreground = Brushes.Red;
                        break;
                    case 6:
                        if (MessageBox.Show("Is this a pediatric patient(20 or younger)?", $"{selectedDefiner.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We accept this Insurance!";
                            Confirm1st.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient, but you can also ask the doctor if it's okay for the patient to be seen regardless of their age. He does not prefer to do thyroids, so keep that in mind.", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We cannot accept this Insurance...";
                            Confirm1st.Foreground = Brushes.Red;
                        }
                        break;
                    case 7:
                        if (MessageBox.Show("Insurance needs to be a PA Plan. Does this insurance have an insurance plan?", $"{selectedDefiner.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We accept this Insurance!";
                            Confirm1st.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We cannot accept this Insurance...";
                            Confirm1st.Foreground = Brushes.Red;
                        }
                        break;
                    case 8:
                        if (MessageBox.Show("This is a secondary only insurance, but it may be accepted if it has OON benefits. Does it have OON benefits?", $"{selectedDefiner.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We accept this Insurance!";
                            Confirm1st.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "We cannot accept this Insurance...";
                            Confirm1st.Foreground = Brushes.Red;
                        }
                        break;

                    default:
                        Confirm1st.Visibility = Visibility.Hidden;
                        break;
                }


                switch (selectedDefiner.needReferral)
                {
                    case 1:
                        MessageBox.Show("Patient needs a referral from his insurance in order for them to be seen. Please inform them that they need to speak with their PCP so that they can receive a referral.", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Confirm1st.Foreground = Brushes.Yellow;
                        break;
                }

                switch(selectedDefiner.accept)
                {
                    case 1:
                        if (Dropcho.IsChecked == true)
                        {
                            MessageBox.Show("Dropcho cannot accept this insurance unless Dr.Watson or Dr.Mueller can cosign. Make sure to schedule the patient when either doctor is present.", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "Dropcho needs a cosign!";
                            Confirm1st.Foreground = Brushes.Orange;
                        }
                        break;
                    case 2:
                        if (Kao.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Kao only patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "Please switch to Dr.Kao!";
                            Confirm1st.Foreground = Brushes.Orange;
                        }
                        break;
                    case 3:
                        if (Watson.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Watson only patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "Please switch to Dr.Watson!";
                            Confirm1st.Foreground = Brushes.Orange;
                        }
                        break;
                    case 4:
                        if (Mueller.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Mueller only patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "Please switch to Dr.Mueller!";
                            Confirm1st.Foreground = Brushes.Orange;
                        }
                        break;
                    case 5:
                        if (Dropcho.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a PA Dropcho only patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm1st.Visibility = Visibility.Visible;
                            Confirm1st.Text = "Please switch to PA Dropcho!";
                            Confirm1st.Foreground = Brushes.Orange;
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("You must choose a primary insurance!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (selectedDefine != null)
            {
                switch (selectedDefine.isPartnered)
                {
                    case 0:
                        Confirm2nd.Visibility = Visibility.Hidden;
                        MessageBox.Show("I am not sure if we do accept this. Please speak to your Billing Department or Office Administrator for more details!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Question);
                        break;
                    case 1:
                        Confirm2nd.Visibility = Visibility.Visible;
                        Confirm2nd.Text = "We accept this Insurance!";
                        Confirm2nd.Foreground = Brushes.Green;
                        break;
                    case 2:
                        Confirm2nd.Visibility = Visibility.Visible;
                        Confirm2nd.Text = "We cannot accept this Insurance...";
                        Confirm2nd.Foreground = Brushes.Red;
                        break;
                    case 3:
                        if (MessageBox.Show("Does the patient have any Out Of Network benefits?", $"{selectedDefine.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We accept this Insurance!";
                            Confirm2nd.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We cannot accept this Insurance...";
                            Confirm2nd.Foreground = Brushes.Red;
                        }
                        break;
                    case 4:
                        Confirm2nd.Visibility = Visibility.Visible;
                        Confirm2nd.Text = "We accept this Insurance!";
                        Confirm2nd.Foreground = Brushes.Green;
                        break;
                    case 5:
                        if(selectedDefiner.insuranceName == "Medicare")
                        {
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We accept this Insurance!";
                            Confirm2nd.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("This insurance must be a secondary only to Medicare...", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We cannot accept this insurance...";
                            Confirm2nd.Foreground = Brushes.Red;
                        }
                        break;
                    case 6:
                        if (MessageBox.Show("Is this a pediatric patient(20 or younger)?", $"{selectedDefine.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We accept this Insurance!";
                            Confirm2nd.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient, but you can also ask the doctor if it's okay for the patient to be seen regardless of their age. He does not prefer to do thyroids, so keep that in mind.", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We cannot accept this Insurance...";
                            Confirm2nd.Foreground = Brushes.Red;
                        }
                        break;
                    case 7:
                        if (MessageBox.Show("Insurance needs to be a PA Plan. Does this insurance have an insurance plan?", $"{selectedDefine.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We accept this Insurance!";
                            Confirm2nd.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We cannot accept this Insurance...";
                            Confirm2nd.Foreground = Brushes.Red;
                        }
                        break;
                    case 8:
                        if (MessageBox.Show("This is a secondary only insurance, but it may be accepted if it has OON benefits. Does it have OON benefits?", $"{selectedDefiner.insuranceName}", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("We can see the patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Information);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We accept this Insurance!";
                            Confirm2nd.Foreground = Brushes.Green;
                        }
                        else
                        {
                            MessageBox.Show("We cannot see this patient...", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "We cannot accept this Insurance...";
                            Confirm2nd.Foreground = Brushes.Red;
                        }
                        break;

                    default:
                        Confirm2nd.Visibility = Visibility.Hidden;
                        break;
                }


                switch (selectedDefine.needReferral)
                {
                    case 1:
                        MessageBox.Show("Patient needs a referral from his insurance in order for them to be seen. Please inform them that they need to speak with their PCP so that they can receive a referral.", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        Confirm2nd.Foreground = Brushes.Yellow;
                        break;
                }

                switch (selectedDefine.accept)
                {
                    case 1:
                        if (Dropcho.IsChecked == true)
                        {
                            MessageBox.Show("Dropcho cannot accept this insurance unless Dr.Watson or Dr.Mueller can cosign. Make sure to schedule the patient when either doctor is present.", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "Dropcho needs a cosign!";
                            Confirm2nd.Foreground = Brushes.Orange;
                        }
                        break;
                    case 2:
                        if (Kao.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Kao only patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "Please switch to Dr.Kao!";
                            Confirm2nd.Foreground = Brushes.Orange;
                        }
                        break;
                    case 3:
                        if (Watson.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Watson only patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "Please switch to Dr.Watson!";
                            Confirm2nd.Foreground = Brushes.Orange;
                        }
                        break;
                    case 4:
                        if (Mueller.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a Dr.Mueller only patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "Please switch to Dr.Mueller!";
                            Confirm2nd.Foreground = Brushes.Orange;
                        }
                        break;
                    case 5:
                        if (Dropcho.IsChecked == false)
                        {
                            MessageBox.Show("This insurance must be a PA Dropcho only patient!", $"{selectedDefine.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
                            Confirm2nd.Visibility = Visibility.Visible;
                            Confirm2nd.Text = "Please switch to PA Dropcho!";
                            Confirm2nd.Foreground = Brushes.Orange;
                        }
                        break;
                }
            }
        }
    }
}

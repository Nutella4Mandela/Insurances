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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string InsuranceName;
        public int IsPartnered;
        public int NeedReferral;
        public int Plans;
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
                    Plans = Convert.ToInt32(tokens[3]);
                    Other = tokens[4];
                }
                InsuranceDefine insuranceDefine = new InsuranceDefine { insuranceName = InsuranceName, isPartnered = IsPartnered, needReferral = NeedReferral, hasPlans = Plans, otherRequire = Other };

                ComboCompany.Items.Add(insuranceDefine);
                ComboX.Items.Add(insuranceDefine);

            }


            ComboCompany.SelectionChanged += ComboBox_SelectionChanged;

            ComboX.SelectionChanged += ComboXML_SelectionChanged;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Confirm1st.Visibility = Visibility.Hidden;
        }

        private void ComboXML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
        }

        private void RadioButton_Checked_3(object sender, RoutedEventArgs e)
        {
            Confirm.Visibility = Visibility.Visible;
            Confirm1st.Visibility = Visibility.Hidden;
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
                            MessageBox.Show("We cannot see the patient...", $"{selectedDefiner.insuranceName}", MessageBoxButton.OK, MessageBoxImage.Error);
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


                    default:
                        Confirm1st.Visibility = Visibility.Hidden;
                        break;
                }
            }
            else
            {
                MessageBox.Show("You must choose a primary insurance!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            switch(selectedDefiner.needReferral)
            {
                case 0:

                    break;
                case 1:
                    MessageBox.Show("Patient needs a referral from his insurance in order for them to be seen. Please inform them that they need to speak with their PCP so that they can receive a referral.", "Attention!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Confirm1st.Foreground = Brushes.Yellow;
                    break;
                case 2:
                    if (selectedDefiner.isPartnered == 5 || selectedDefiner.isPartnered == 4 || selectedDefiner.isPartnered == 2)
                    {

                    }
                    else
                    {
                        Confirm1st.Foreground = Brushes.Green;
                    }
                    break;
            }


            if (selectedDefine != null)
            {
                MessageBox.Show($"Number: {selectedDefine.isPartnered}", "Your Mom", MessageBoxButton.OK);
            }
        }
    }
}

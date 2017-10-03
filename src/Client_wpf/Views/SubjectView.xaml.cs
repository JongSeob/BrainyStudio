using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using RestSharp;
using Sdk.Models;

namespace HamburgerMenuApp.V3.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class SubjectView : UserControl
    {
        //List of logged user Subjects
        List<Subject> subjectList = new List<Subject>();

        public SubjectView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fetch logged user Repository list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fetch Repositories from API
                listBox.Items.Clear();
                string subjects = ((MainWindow)Application.Current.MainWindow).Api.Request("/api/Subject", Method.GET);
                subjectList = JsonConvert.DeserializeObject<List<Subject>>(subjects);

                foreach (Subject subject in subjectList)
                {
                    listBox.Items.Add(subject.Name);
                }
            }
            catch (Exception exception)
            {
                ((MainWindow)Application.Current.MainWindow).Logged_Label.Content = exception;
                ((MainWindow)Application.Current.MainWindow).ToggleFlyout(5);
            }
           
        }

        /// <summary>
        /// Load selected repository metadata
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //Subject info
                SubjectName.Text = subjectList[listBox.SelectedIndex].Name;
                SubjectDescription.Text = subjectList[listBox.SelectedIndex].Description;
                SubjectAge.Text = subjectList[listBox.SelectedIndex].Age.ToString();
                //Gender
                if (Int32.Parse(subjectList[listBox.SelectedIndex].Gender) == 1) SubjectIsMale.IsChecked = true;
                else if (Int32.Parse(subjectList[listBox.SelectedIndex].Gender) == 2) SubjectIsFemale.IsChecked = true;
            }
            catch (Exception exception)
            {
               
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
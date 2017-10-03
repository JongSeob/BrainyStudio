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
    public partial class SettingsView : UserControl
    {

        //List of logged user Repositories
        List<Repository> reposlist = new List<Repository>();

        //List of logged user Subjects
        List<Subject> subjectList = new List<Subject>();

        public SettingsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start Recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Set up and start Recording
            ((MainWindow)Application.Current.MainWindow).StartNewRecording(RecordingName.Text, RecordingDescription.Text, 
                subjectList[RecordingSubject.SelectedIndex], reposlist[RecordingRepository.SelectedIndex].Id);
            //Disable controls during the recording
            //RecordButton.IsEnabled = false;
            //SubjectProfile.IsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Fetch Repositories from API
                RecordingRepository.Items.Clear();

                string repos = ((MainWindow)Application.Current.MainWindow).Api.Request("/api/Repository", Method.GET);
                reposlist = JsonConvert.DeserializeObject<List<Repository>>(repos);

                foreach (Repository repo in reposlist)
                {
                    RecordingRepository.Items.Add(repo.Name);
                }


                //Fetch Subjects from API
                RecordingSubject.Items.Clear();
                string subjects = ((MainWindow)Application.Current.MainWindow).Api.Request("/api/Subject", Method.GET);
                subjectList = JsonConvert.DeserializeObject<List<Subject>>(subjects);

                foreach (Subject subject in subjectList)
                {
                    RecordingSubject.Items.Add(subject.Name);
                }

            }
            catch (Exception exception)
            {
                ((MainWindow)Application.Current.MainWindow).Logged_Label.Content = exception;
                ((MainWindow)Application.Current.MainWindow).ToggleFlyout(5);
            }
        }
    }
}
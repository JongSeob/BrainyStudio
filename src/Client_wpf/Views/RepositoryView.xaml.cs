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
    public partial class RepositoryView : UserControl
    {
        //List of logged user Repositories
        List<Repository> reposlist = new List<Repository>();

        public RepositoryView()
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
                string repos = ((MainWindow)Application.Current.MainWindow).Api.Request("/api/Repository", Method.GET);
                reposlist = JsonConvert.DeserializeObject<List<Repository>>(repos);

                foreach (Repository repo in reposlist)
                {
                    listBox.Items.Add(repo.Name);
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
            RepositoryName.Text = reposlist[listBox.SelectedIndex].Name;
            RepositoryDescription.Text = reposlist[listBox.SelectedIndex].Description;
            RepositoryId.Text = reposlist[listBox.SelectedIndex].Id.ToString();
            }
            catch (Exception exception)
            {
               
            }
        }
    }
}
﻿using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Views;
using SGAM.Elfec.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SGAM.Elfec
{
    /// <summary>
    /// Interaction logic for ShowAllApps.xaml
    /// </summary>
    public partial class ShowApplications : UserControl, IShowApplicationsView
    {
        private IndeterminateLoading _indeterminateLoading;
        private ErrorMessage _errorMessage;

        public ShowApplications()
        {
            InitializeComponent();
            _indeterminateLoading = new IndeterminateLoading();
            _indeterminateLoading.Margin = new Thickness(40, 40, 0, 0);
            _errorMessage = new ErrorMessage();
            _errorMessage.Margin = new Thickness(40, 40, 0, 0);
            DataContext = new ShowApplicationsPresenter(this);
        }

        public void ViewAppDetail(int appId)
        {
            throw new NotImplementedException();
        }

        #region Interface Methods

        public void OnLoadingData(bool isRefresh = false)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                _indeterminateLoading.TxtLoadingMessage.Text = Properties.Resources.MsgLoadingApps;
                MainWindowService.Instance.MainWindowView.SetStatusBar(Properties.Resources.MsgLoadingApps);
                Transitioning.Content = _indeterminateLoading;
            }));
        }

        public void OnLoadingErrors(bool isRefresh = false, params Exception[] errors)
        {
            if (errors.Length > 0)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                    _errorMessage.TxtErrorMessage.Text = MessageListFormatter.FormatFromErrorList(errors);
                    _errorMessage.BtnOk.Click += (s, e) => { Transitioning.Content = null; };
                    Transitioning.Content = _errorMessage;
                }));
            }
        }

        public void OnDataLoaded()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindowService.Instance.MainWindowView.SetStatusBarDefault();
                if (Transitioning.Content != ListViewApplications)
                    Transitioning.Content = ListViewApplications;
            }));
        }

        #endregion
    }
}
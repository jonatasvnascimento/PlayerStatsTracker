﻿using PlayerStatsTracker.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace PlayerStatsTracker.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}

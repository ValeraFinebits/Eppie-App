﻿using System;
using Tuvi.App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tuvi.App.Shared.Controls
{
    public sealed partial class AccountSettingsControl : UserControl
    {
        public AccountSettingsModel AccountSettingsModel
        {
            get { return (AccountSettingsModel)GetValue(AccountSettingsModelProperty); }
            set { SetValue(AccountSettingsModelProperty, value); }
        }
        public static readonly DependencyProperty AccountSettingsModelProperty =
            DependencyProperty.Register(nameof(AccountSettingsModel), typeof(AccountSettingsModel), typeof(AccountSettingsControl), new PropertyMetadata(null, OnModelChanged));

        private static void OnModelChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if(dependencyObject is AccountSettingsControl control)
            {
                control.BasicAccountSettingsModel = control.AccountSettingsModel as BasicAccountSettingsModel;
            }
        }

        public BasicAccountSettingsModel BasicAccountSettingsModel
        {
            get { return (BasicAccountSettingsModel)GetValue(BasicAccountSettingsModelProperty); }
            private set { SetValue(BasicAccountSettingsModelProperty, value); }
        }
        public static readonly DependencyProperty BasicAccountSettingsModelProperty =
            DependencyProperty.Register(nameof(BasicAccountSettingsModel), typeof(BasicAccountSettingsModel), typeof(AccountSettingsControl), new PropertyMetadata(null));

        public bool InCompactMode
        {
            get { return (bool)GetValue(InCompactModeProperty); }
            set { SetValue(InCompactModeProperty, value); }
        }
        public static readonly DependencyProperty InCompactModeProperty =
            DependencyProperty.Register(nameof(InCompactMode), typeof(bool), typeof(AccountSettingsControl), new PropertyMetadata(false));

        public bool IsEmailReadOnly
        {
            get { return (bool)GetValue(IsEmailReadOnlyProperty); }
            set { SetValue(IsEmailReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsEmailReadOnlyProperty =
            DependencyProperty.Register(nameof(IsEmailReadOnly), typeof(bool), typeof(AccountSettingsControl), new PropertyMetadata(false));

        public Array IncomingProtocolTypes
        {
            get { return (Array)GetValue(IncomingProtocolTypesProperty); }
            set { SetValue(IncomingProtocolTypesProperty, value); }
        }
        public static readonly DependencyProperty IncomingProtocolTypesProperty =
            DependencyProperty.Register(nameof(IncomingProtocolTypes), typeof(Array), typeof(AccountSettingsControl), new PropertyMetadata(null));

        public bool IsLocked
        {
            get { return (bool)GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, value); }
        }
        public static readonly DependencyProperty IsLockedProperty =
            DependencyProperty.Register(nameof(IsLocked), typeof(bool), typeof(AccountSettingsControl), new PropertyMetadata(false));

        public AccountSettingsControl()
        {
            this.InitializeComponent();
        }
    }
}

﻿// ---------------------------------------------------------------------------- //
//                                                                              //
//   Copyright 2025 Eppie (https://eppie.io)                                    //
//                                                                              //
//   Licensed under the Apache License, Version 2.0 (the "License"),            //
//   you may not use this file except in compliance with the License.           //
//   You may obtain a copy of the License at                                    //
//                                                                              //
//       http://www.apache.org/licenses/LICENSE-2.0                             //
//                                                                              //
//   Unless required by applicable law or agreed to in writing, software        //
//   distributed under the License is distributed on an "AS IS" BASIS,          //
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.   //
//   See the License for the specific language governing permissions and        //
//   limitations under the License.                                             //
//                                                                              //
// ---------------------------------------------------------------------------- //

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tuvi.App.Shared.Models;
using Tuvi.App.Shared.Services;
using Tuvi.App.ViewModels;
using Tuvi.Core.Entities;
using Eppie.App.Shared.Services;
using System;
using System.Collections.Generic;

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Input;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Input;
#endif

namespace Tuvi.App.Shared.Views
{
    public partial class BasePage<TViewModel, TViewModelBase> : Page, INotifyPropertyChanged
                 where TViewModel : TViewModelBase
                 where TViewModelBase : BaseViewModel
    {
        public string AppName
        {
            get
            {
                var brand = new BrandLoader();
                return brand.GetName();
            }
        }

        public TViewModel ViewModel { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public BasePage()
        {
#if __ANDROID__ || __IOS__
            // TODO: investigate a leak here. DependancyPropertyChanged handler retains whole page (TVM-551)
            // Uno.UI.Toolkit.VisibleBoundsPadding.SetPaddingMask(this, Uno.UI.Toolkit.VisibleBoundsPadding.PaddingMask.All);
#endif

            DataContextChanged += BasePage_DataContextChanged;
        }

        private void DataContextChangedImpl()
        {
            var app = Application.Current as Eppie.App.Shared.App;

            ViewModel = DataContext as TViewModel;
            ViewModel.SetCore(() => app.Core);
            ViewModel.SetNavigationService(app.NavigationService);
            ViewModel.SetLocalSettingsService(app.LocalSettingsService);
            ViewModel.SetAIService(app.AIService);
            ViewModel.SetLocalizationService(new LocalizationService(app.Host?.Services));
            ViewModel.SetMessageService(new MessageService(() => app.XamlRoot));
            ViewModel.SetErrorHandler(new ErrorHandler());
            ViewModel.SetDispatcherService(new DispatcherService());
            ViewModel.SetBrandService(new BrandLoader());
            ViewModel.SetLauncherService(new LauncherService());
            ViewModel.SetAppStoreService(new AppStoreService());
            ViewModel.SetDragAndDropService(new DragAndDropService());
            AfterDataContextChanged();
        }

        // UWP
        public void BasePage_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            DataContextChangedImpl();
        }

        // Android, iOS
        public void BasePage_DataContextChanged(DependencyObject sender, DataContextChangedEventArgs args)
        {
            DataContextChangedImpl();
        }

        public virtual void AfterDataContextChanged()
        {
        }

        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            if (Frame != null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected virtual void GoForward(object sender, RoutedEventArgs e)
        {
            if (Frame != null && Frame.CanGoForward)
            {
                Frame.GoForward();
            }
        }

        protected virtual bool CanGoBack()
        {
            return Frame.CanGoBack;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel?.OnNavigatedTo(e?.Parameter);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel?.OnNavigatedFrom();
        }

        protected virtual void OnExceptionOccurred(object sender, ExceptionEventArgs e)
        {
            ViewModel.OnError(e.Exception);
        }

        #region INotifyPropertyChanged implementation
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected void ListViewSwipeContainer_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse || e.Pointer.PointerDeviceType == PointerDeviceType.Pen)
            {
                VisualStateManager.GoToState(sender as Control, "HoverButtonsShown", true);
            }
        }

        protected void ListViewSwipeContainer_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "HoverButtonsHidden", true);
        }

        protected async void InitAIAgentButton(AppBarButton agentButton, Controls.MessageListControl messageListControl)
        {
            try
            {
                var menuFlyout = new MenuFlyout();

                await ViewModel.CreateAIAgentsMenuAsync((string text, Action<IList<object>> command) =>
                {
                    var item = new MenuFlyoutItem { Text = text };
                    item.Click += (s, e) => command.Invoke(messageListControl.SelectedItems);
                    item.IsEnabled = messageListControl.SelectedItems.Count > 0;
                    messageListControl.SelectionChanged += (s, e) => item.IsEnabled = messageListControl.SelectedItems.Count > 0;
                    menuFlyout.Items.Add(item);
                });

                if (menuFlyout.Items.Count > 0)
                {
                    agentButton.Flyout = menuFlyout;
                }
            }
            catch (Exception ex)
            {
                ViewModel.OnError(ex);
            }
        }

        protected async void InitAIAgentButton(AppBarButton agentButton)
        {
            try
            {
                var menuFlyout = new MenuFlyout();

                await ViewModel.CreateAIAgentsMenuAsync((string text, Action command) =>
                {
                    var item = new MenuFlyoutItem { Text = text };
                    item.Click += (s, e) => command.Invoke();
                    menuFlyout.Items.Add(item);
                });

                if (menuFlyout.Items.Count > 0)
                {
                    agentButton.Flyout = menuFlyout;
                }
            }
            catch (Exception ex)
            {
                ViewModel.OnError(ex);
            }
        }
    }
}

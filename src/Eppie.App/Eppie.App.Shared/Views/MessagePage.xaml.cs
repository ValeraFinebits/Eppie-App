using System;
using Tuvi.App.ViewModels;

#if WINDOWS_UWP
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
#else
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif

namespace Tuvi.App.Shared.Views
{
    public partial class MessagePageBase : BasePage<MessagePageViewModel, BaseViewModel>
    {
    }

    public sealed partial class MessagePage : MessagePageBase
    {
        public MessagePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitAIAgentButton();
        }

        async void InitAIAgentButton()
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
                    AIAgentButton.Flyout = menuFlyout;
                }
            }
            catch (Exception ex)
            {
                ViewModel.OnError(ex);
            }
        }
    }
}

using System;

using Xamarin.Forms;

namespace Questy
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    itemsPage = new NavigationPage(new ItemsPage());
                    itemsPage.Icon = "tab_feed.png";
                    break;
                default:
                    itemsPage = new ItemsPage();
                    break;
            }

            Children.Add(itemsPage);

            Title = "Questification";
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}

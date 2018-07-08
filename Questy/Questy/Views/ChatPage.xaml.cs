using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Questy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        YandexCloud yandexCloud;

        public ChatPage()
        {
            InitializeComponent();
            yandexCloud = new YandexCloud();
        }

        async void SendItem_Clicked(object sender, EventArgs e)
        {
            string request = Input.Text;

            Input.Text = "";

            Output.Text += "\n" +
                "User /" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "/: " + request;

            await yandexCloud.Send(request, Output);
        }
    }
}
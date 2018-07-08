using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Questy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AvatarPage : ContentPage
    {
        private Stream imageStream;
        private ImageSource avatarSource;
        public Avatarizer avatarizer;

        public ImageSource AvatarSource { get => avatarSource; set => avatarSource = value; }

        public AvatarPage()
        {
            InitializeComponent();

            avatarizer = new Avatarizer();
            if (avatarizer.AvatarPhotoPath != null)
            {
                AvatarSource = ImageSource.FromFile(avatarizer.AvatarPhotoPath);
            }
        }

        public void SetAvatarImage(String imagePath)
        {
            avatarizer.AvatarPhotoPath = imagePath;

            Device.BeginInvokeOnMainThread(() => { ImagePath.Text = imagePath; });

            if (avatarizer.AvatarPhotoPath != null)
            {
                AvatarSource = ImageSource.FromFile(avatarizer.AvatarPhotoPath);
            }
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            //IStreamImageSource photoStream = DependencyService.Get<IStreamImageSource>();

            //imageStream = await photoStream.GetStreamAsync();
            //AvatarSource = ImageSource.FromStream(() => imageStream);

            //AvatarImage.Source = AvatarSource;

            await Navigation.PopToRootAsync();
        }
    }
}
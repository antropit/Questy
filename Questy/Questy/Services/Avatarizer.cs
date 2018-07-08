using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Questy
{
    public class AvatarizerView : View
    {
    }

    public class Avatarizer
    {
        Stream streamImage;
        String avatarPhotoPath;

        public Stream StreamImage { get => streamImage; set => streamImage = value; }
        public string AvatarPhotoPath { get => avatarPhotoPath; set => avatarPhotoPath = value; }

        public Avatarizer() {}
    }
}

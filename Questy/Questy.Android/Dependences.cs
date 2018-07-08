using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Android.Graphics;
using Android.Provider;

//[assembly: Dependency(typeof(Questy.Droid.PhotoSream))]
//[assembly: ExportRenderer(typeof(Questy.AvatarizerView), typeof(CaptureElement))]

namespace Questy.Droid
{
    //public class PhotoSream : IStreamImageSource
    //{
        //public Task<Stream> GetStreamAsync(CancellationToken userToken = default(CancellationToken))
        //{
            //PackageManager packageManager = PackageManager.GetObject<PackageManager>;
            //Stream photoStream;

            //if (packageManager.HasSystemFeature(PackageManager.FeatureCamera) != true) throw new NotImplementedException();

            //Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
            //if (takePictureIntent.ResolveActivity(packageManager) = null) throw new NotImplementedException();

            //return Task.Factory.StartNew(() =>
            //{
                //StartActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);

                //takePictureIntent.PutExtra(MediaStore.ExtraOutput, photoStream);
                //StartActivityForResult(takePictureIntent, REQUEST_TAKE_PHOTO);

                //return photoStream;
            //});
        //}
    //}
}
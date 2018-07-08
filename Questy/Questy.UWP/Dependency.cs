using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

using Questy;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.Foundation;

[assembly: Dependency(typeof(Questy.UWP.PhotoSream))]
//[assembly: ExportRenderer(typeof(Questy.AvatarizerView), typeof(CaptureElement))]

namespace Questy.UWP
{
    public class PhotoSream: IStreamImageSource
    {
        MediaCapture _mediaCapture;
        StorageFile _photo;
        bool _isPreviewing;

        async public Task<Stream> GetStreamAsync(CancellationToken userToken = default(CancellationToken))
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                throw new NotImplementedException();
            }

            _photo = photo;

            Task<Stream> task = photo.OpenStreamForReadAsync();
            task.Wait();

            return await task;
        }
    }
}

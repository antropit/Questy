using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;

namespace Questy
{
    public class YandexCloud
    {
        WebHeaderCollection headers = new WebHeaderCollection
        {
            ["Content-Type"] = "application/json",
            ["Authorization"] = "OAuth AQAAAAAAmjczAASVLKSSxSWnTkeKm9kVX2QjVcc",
        };

        String fileName = "Guest.xml";
        String fileInfoUriString = "https://cloud-api.yandex.net:443/v1/disk/resources?path=app:/{0}&fields=modified";
        String uploadUriString = "https://cloud-api.yandex.net/v1/disk/resources/upload?path=app:/{0}&overwrite=true";
        String infoDataUriString = "https://cloud-api.yandex.net/v1/disk/";
        String resource_url = "/v1/disk/resources?path={path}";

        public WebHeaderCollection Headers { get => headers; set => headers = value; }
        public string FileInfoUriString { get => fileInfoUriString; set => fileInfoUriString = value; }
        public string UploadUriString { get => uploadUriString; set => uploadUriString = value; }
        public string InfoDataUriString { get => infoDataUriString; set => infoDataUriString = value; }
        public string Resource_url { get => resource_url; set => resource_url = value; }

        //fix -- needed block when used
        object answerJSON = null;

        public YandexCloud()
        {

        }

        [DataContract]
        public class ErrorAnswerJSON
        {
            [DataMember]
            public string message { get; set; }

            [DataMember]
            public string description { get; set; }

            [DataMember]
            public string error { get; set; }
        }

        [DataContract]
        public class FileInfoAnswerJSON
        {
            [DataMember]
            public string modified { get; set; }
        }

        [DataContract]
        public class UploadReqAnswerJSON
        {
            [DataMember]
            public string operation_id { get; set; }

            [DataMember]
            public string href { get; set; }

            [DataMember]
            public string method { get; set; }

            [DataMember]
            public bool templated { get; set; }
        }

        [DataContract]
        public class DataFileTaskTegXML
        {
            [DataMember]
            public string Guid { get; set; }

            [DataMember]
            public string Created { get; set; }

            [DataMember]
            public string Request { get; set; }

            [DataMember]
            public string Finished { get; set; }

            [DataMember]
            public string Answer { get; set; }
        }

        [DataContract]
        public class DataFileBodyXML
        {
            [DataMember]
            ObservableCollection<DataFileTaskTegXML> Tasks { get; set; }
        }

        public Task<bool> Send(string Text, Label Output)
        {
            return Task<bool>.Factory.StartNew(() =>
            {
                FileInfoAnswerJSON fileInfoAnswerJSON = null;
                UploadReqAnswerJSON uploadReqAnswerJSON = null;

                if (DoWebRequest(FileInfoUriString, Output, typeof(FileInfoAnswerJSON)))
                {
                    fileInfoAnswerJSON = (FileInfoAnswerJSON)answerJSON;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Output.Text += "\n" + fileInfoAnswerJSON.modified;
                    });

                }

                if (DoWebRequest(UploadUriString, Output, typeof(UploadReqAnswerJSON)) == false) return false;

                uploadReqAnswerJSON = (UploadReqAnswerJSON)answerJSON;

                Device.BeginInvokeOnMainThread(() =>
                {
                    Output.Text += "\n" + uploadReqAnswerJSON.href;
                });




                return true;
            });
        }

        public bool DoWebRequest(string UriString, Label Output, Type typeJSON)
        {
            bool result = false; Task task = null;
            HttpWebRequest request = WebRequest.CreateHttp(String.Format(UriString, fileName));

            request.Headers = Headers;
            request.BeginGetResponse((arg) =>
            {
                task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeJSON);

                        Stream stream = request.EndGetResponse(arg).GetResponseStream();
                        answerJSON = serializer.ReadObject(stream);

                        result = true;
                    }
                    catch (WebException e)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Output.Text += "\nWeb Exception raised!";
                            Output.Text += String.Format("\nMessage:{0}", e.Message);
                            Output.Text += String.Format("\nStatus:{0}", e.Status);
                        });
                    }
                    catch (Exception e)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Output.Text += "\nMain Exception raised!";
                            Output.Text += String.Format("Source :{0} ", e.Source);
                            Output.Text += String.Format("Message :{0} ", e.Message);
                        });
                    }
                });
            }, null).AsyncWaitHandle.WaitOne();

            while (task == null) { Task.Delay(100); }
            task.Wait();

            return result;
        }
    }
}
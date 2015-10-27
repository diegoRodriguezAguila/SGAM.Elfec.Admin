using System.Net;

namespace SGAM.Elfec.Model.Callbacks
{
    public class UploadResultCallback<TResult> : ResultCallback<TResult>
    {
        public event UploadProgressChangedEventHandler UploadProgressChanged;
        public void OnUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
                UploadProgressChanged(sender, e);
        }
    }
}

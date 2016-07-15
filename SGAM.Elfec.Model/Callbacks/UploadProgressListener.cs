using System.Net;

namespace SGAM.Elfec.Model.Callbacks
{
    public class UploadProgressListener
    {
        public event UploadProgressChangedEventHandler ProgressChanged;
        public void OnUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }
    }
}

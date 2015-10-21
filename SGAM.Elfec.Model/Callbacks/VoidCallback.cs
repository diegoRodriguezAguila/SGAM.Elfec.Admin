namespace SGAM.Elfec.Model.Callbacks
{
    public delegate void SuccessEventHandler(object sender);
    public class VoidCallback : Callback
    {
        public event SuccessEventHandler Success;
        // Invoke the Success event; called whenever list changes
        public void OnSuccess(object sender)
        {
            if (Success != null)
                Success(sender);
        }
    }
}

namespace SGAM.Elfec.Model.Callbacks
{
    public delegate void SuccessEventHandler<TResult>(object sender, TResult result);
    public class ResultCallback<TResult> : Callback
    {
        public event SuccessEventHandler<TResult> Success;
        // Invoke the Success event; called whenever list changes
        public virtual void OnSuccess(object sender, TResult result)
        {
            if (Success != null)
                Success(sender, result);
        }
    }
}

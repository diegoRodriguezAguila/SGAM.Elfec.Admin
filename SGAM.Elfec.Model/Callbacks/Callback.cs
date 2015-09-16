using System;

namespace SGAM.Elfec.Model.Callbacks
{
    public delegate void FailureEventHandler(object sender, params Exception[] errors);
    public class Callback
    {
        public event FailureEventHandler Failure;
        // Invoke the Failure event; called whenever list changes
        public virtual void OnFailure(object sender, params Exception[] errors)
        {
            if (Failure != null)
                Failure(sender, errors);
        }
    }
}

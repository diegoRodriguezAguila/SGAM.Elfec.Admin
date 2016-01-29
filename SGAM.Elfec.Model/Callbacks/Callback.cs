using System;
using System.Collections.Generic;
using System.Linq;

namespace SGAM.Elfec.Model.Callbacks
{
    public delegate void FailureEventHandler(object sender, params Exception[] errors);
    public class Callback
    {
        public event FailureEventHandler Failure;
        public IList<Exception> Errors { get; set; } = new List<Exception>();
        public bool HasErrors { get { return Errors.Count > 0; } }
        // Invoke the Failure event; called whenever list changes
        public void OnFailure(object sender)
        {
            if (Failure != null)
                Failure(sender, Errors.ToArray());
        }

        /// <summary>
        /// Adds An error
        /// </summary>
        /// <param name="errors"></param>
        public void AddErrors(params Exception[] errors)
        {
            ((List<Exception>)Errors).AddRange(errors);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAM.Elfec.Presenters.Views
{
    public interface IShowApplicationsView : ILoadingContentErrorView
    {
        void ViewAppDetail(int appId);
    }
}
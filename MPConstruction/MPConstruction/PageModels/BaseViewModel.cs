using System;
using System.Collections.Generic;
using System.Text;

namespace MPConstruction.PageModels
{
    public class BaseViewModel : FreshMvvm.FreshBasePageModel
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
    }
}

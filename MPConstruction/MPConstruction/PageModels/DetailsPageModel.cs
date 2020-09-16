using MPConstruction.Models;
using MPConstruction.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MPConstruction.PageModels
{
    public class DetailsPageModel : BaseViewModel
    {
        public DetailsPageModel()
        {
        }

        public Equipment Equipment { get; set; }
        public ICommand ExecuteEdit { get; set; }
        public ICommand ExecuteBack { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData != null)
            {
                Equipment = (Equipment)initData;
            }
            else
            {
                Equipment = new Equipment();
            }

            ExecuteEdit = new Command(() => Edit());
            ExecuteBack = new Command(() => Back());
        }

        private async void Edit()
        {
            await CoreMethods.PushPageModel<EquipmentPageModel>(Equipment);
        }

        private async void Back()
        {
            await CoreMethods.PopPageModel();
        }
    }
}

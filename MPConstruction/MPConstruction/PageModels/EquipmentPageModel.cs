using MPConstruction.Models;
using MPConstruction.Services;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MPConstruction.PageModels
{
    public class EquipmentPageModel : BaseViewModel
    {
        private readonly IDataStore<Equipment> _dataStore;

        public EquipmentPageModel(IDataStore<Equipment> dataStore)
        {
            _dataStore = dataStore;
        }

        public Equipment Equipment { get; set; }
        public ICommand ExecuteSave { get; set; }
        public ICommand ExecuteBack { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData != null)
            {
                Title = "Edit";
                Equipment = (Equipment)initData;                
            }
            else
            {
                Title = "New";
                Equipment = new Equipment();
            }

            ExecuteSave = new Command(() => Save());
            ExecuteBack = new Command(() => Back());
        }

        private async void Save()
        {
            if (Equipment.Id != null && Equipment.Name != null)
            {
                if (Title == "Edit")
                {
                    await _dataStore.UpdateItemAsync(Equipment);
                }
                else
                {
                    await _dataStore.AddItemAsync(Equipment);
                }

                await CoreMethods.PopToRoot(true);
            }
        }

        private async void Back()
        {
            await CoreMethods.PopPageModel();
        }
    }
}

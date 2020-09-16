using MPConstruction.Models;
using MPConstruction.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MPConstruction.PageModels
{
    public class MainMenuPageModel : BaseViewModel
    {
        private readonly IDataStore<Equipment> _dataStore;

        public MainMenuPageModel(IDataStore<Equipment> dataStore)
        {
            _dataStore = dataStore;
        }

        public Command LoadEquipmentsCommand { get; set; }
        public ICommand ExecuteNew { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);

            ExecuteNew = new Command(() => New());
            LoadEquipmentsCommand = new Command(async () => await ExecuteLoadEquipmentsCommand());
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await ExecuteLoadEquipmentsCommand();
        }

        private async Task ExecuteLoadEquipmentsCommand()
        {
            IsBusy = true;

            try
            {
                Equipments.Clear();
                var items = await _dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Equipments.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private ObservableCollection<Equipment> _equipments;
        public ObservableCollection<Equipment> Equipments
        {
            get
            {
                return _equipments ?? (_equipments = new ObservableCollection<Equipment>());
            }
            set
            {
                _equipments = value;
                RaisePropertyChanged(nameof(Equipments));
            }
        }

        public Equipment SelectedEquipment
        {
            get { return null; }
            set
            {
                CoreMethods.PushPageModel<DetailsPageModel>(value);
                RaisePropertyChanged(nameof(SelectedEquipment));
            }
        }

        private async void New()
        {
            await CoreMethods.PushPageModel<EquipmentPageModel>();
        }

    }
}

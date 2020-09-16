using MPConstruction.Models;
using MPConstruction.PageModels;
using MPConstruction.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPConstruction
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Plugin.Media.CrossMedia.Current.Initialize();

            SetupIOC();
            SetupBasicNav();
        }

        void SetupIOC()
        {
            FreshMvvm.FreshIOC.Container.Register<IDataStore<Equipment>, MockDataStore>();
        }

        void SetupBasicNav()
        {
            var mainPage = FreshMvvm.FreshPageModelResolver.ResolvePageModel<MainMenuPageModel>();

            var basicNavContainer = new FreshMvvm.FreshNavigationContainer(mainPage)
            {
                BackgroundColor = Color.FromHex("#FBFBFE")
            };

            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using System.Threading;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.XamlTypeInfo;

namespace WPF_XAML_Islands_WinUI3
{
    internal class Program : Microsoft.UI.Xaml.Application, IXamlMetadataProvider
    {
        private static XamlControlsXamlMetaDataProvider? xamlMetaDataProvider = null;
 
        public Program()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        { 
            XamlControlsXamlMetaDataProvider.Initialize();
            xamlMetaDataProvider = new();
            this.Resources.MergedDictionaries.Add(new Microsoft.UI.Xaml.Controls.XamlControlsResources());
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Microsoft.UI.Xaml.Application.Start((p) =>
            {
                var syncContext = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                SynchronizationContext.SetSynchronizationContext(syncContext);

                new Program();

                var currentApp = Microsoft.UI.Xaml.Application.Current;
                if (currentApp is not null)
                    currentApp.Exit();
            });
        }

        public IXamlType GetXamlType(Type type)
        {
            return xamlMetaDataProvider.GetXamlType(type);
        }

        public IXamlType GetXamlType(string fullName)
        {
            return xamlMetaDataProvider.GetXamlType(fullName);
        }

        public XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return xamlMetaDataProvider.GetXmlnsDefinitions();
        }
    }
}
using Client.Common.DI;
using Client.Domain.Theme;
using Client.Extensions;
using DevExpress.Xpf.Core;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Client
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IState<ThemeState> ThemeState;
        public IStore Store;
        public static event EventHandler<bool> ThemeChanged;


        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.Office2016WhiteName;

            ServiceProvider = ConfigureServices();

            DISource.Resolver = Resolve;

            var mainView = ServiceProvider.GetRequiredService<MainWindow>();
            mainView.Show();

            // Store를 초기화합니다.
            Store = ServiceProvider.GetRequiredService<IStore>();
            Store.InitializeAsync().Wait();

            ThemeState = ServiceProvider.GetRequiredService<IState<ThemeState>>();
            ThemeState.StateChanged += (sender, e) =>
            {
                ApplicationThemeHelper.ApplicationThemeName = ThemeState.Value.IsDark ? Theme.Office2016BlackName : Theme.Office2016WhiteName;
            };
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.Configure();
            services.AddFluxor(o => o.ScanAssemblies(
                typeof(App).Assembly,
                typeof(Domain.Count.CountState).Assembly,
                typeof(Domain.Theme.ThemeState).Assembly
            ));

            ServiceConfigurator.Configure(services);

            return services.BuildServiceProvider();
        }

        object Resolve(Type type, object key, string name) => type == null ? null : ServiceProvider.GetService(type);
    }
}

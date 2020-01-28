﻿using Microsoft.Extensions.DependencyInjection;
using SqlQueryStressGUI.DbProviders;
using SqlQueryStressGUI.QueryStressTests;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SqlQueryStressGUI.TestEnvironment
{
    public class TestEnvironmentViewModel : ViewModel
    {
        private const string _connectionManagerText = "Connection Manager...";
        private readonly IConnectionProvider _connectionProvider;
        private readonly IViewFactory _viewFactory;

        public TestEnvironmentViewModel(
            IViewFactory viewFactory,
            IConnectionProvider connectionProvider)
        {
            _viewFactory = viewFactory;
            _connectionProvider = connectionProvider;
            _connectionProvider.ConnectionsChanged += (sender, args) =>
            {
                Connections = BuildConnectionList(args.Connections);
                ActiveTest.SelectedConnection = Connections.First();
            };

            Connections = BuildConnectionList(_connectionProvider.GetConnections());
            Tests = new ObservableCollection<QueryStressTestViewModel>();
            
            //OnConnectionChanged();

            AddNewQueryStressTest();

            ExecuteCommandHandler = new CommandHandler((_) => {
                ActiveTest.StartQueryStressTest();
                }, canExecute: (_) => IsConnectionValid());

            NewQueryStressTestCommandHandler = new CommandHandler((_) => AddNewQueryStressTest());

            ConnectionDropdownClosedCommand = new CommandHandler((_) => OnConnectionDropdownClosed());
        }

        public CommandHandler ExecuteCommandHandler { get; }

        public CommandHandler NewQueryStressTestCommandHandler { get; }

        public CommandHandler ConnectionDropdownClosedCommand { get; }

        private QueryStressTestViewModel _activeTest;
        public QueryStressTestViewModel ActiveTest
        {
            get => _activeTest;
            set => SetProperty(value, ref _activeTest);
        }

        private ObservableCollection<QueryStressTestViewModel> _tests;
        public ObservableCollection<QueryStressTestViewModel> Tests
        {
            get => _tests;
            set => SetProperty(value, ref _tests);
        }

        private ObservableCollection<DatabaseConnection> _connections;
        public ObservableCollection<DatabaseConnection> Connections
        {
            get => _connections;
            set => SetProperty(value, ref _connections);
        }

        private ObservableCollection<DatabaseConnection> BuildConnectionList(IEnumerable<DatabaseConnection> connections)
        {
            return new ObservableCollection<DatabaseConnection>(connections.Append(new DatabaseConnection
            {
                Name = _connectionManagerText,
                DbProvider = DbProvider.NotSpecified
            }));
        }

        private bool IsConnectionValid()
        {
            return ActiveTest.SelectedConnection != null && ActiveTest.SelectedConnection.DbProvider != DbProvider.NotSpecified;
        }

        private void AddNewQueryStressTest()
        {
            var newTest = DiContainer.Instance.ServiceProvider.GetRequiredService<QueryStressTestViewModel>();
            newTest.SelectedConnection = Connections.First();
            Tests.Add(newTest);
            ActiveTest = newTest;
        }

        private void OnConnectionDropdownClosed()
        {
            if (ActiveTest.SelectedConnection.Name == _connectionManagerText)
            {
                ActiveTest.SelectedConnection = Connections.First();

                _viewFactory.ShowDialog<ConnectionManagerViewModel>();
            }
        }
    }
}

﻿using SqlQueryStressGUI.DbProviders;
using SqlQueryStressGUI.Parameters;
using SqlQueryStressGUI.TestEnvironment;
using System;

namespace SqlQueryStressGUI.DesignTimeContexts
{
    public static class DesignContexts
    {
        private static IConnectionProvider _connectionProvider = new FakeConnectionProvider();

        private static DbProviderFactory _dbProviderFactory = new DbProviderFactory();

        private static DbCommandProvider _dbCommandProvider = new DbCommandProvider(_dbProviderFactory);

        private static ParameterViewModelBuilder _queryParameterViewModelBuilder => new ParameterViewModelBuilder(null);

        public static QueryStressTestViewModel QueryStressTestContext =>
            new QueryStressTestViewModel(_dbProviderFactory);

        public static ConnectionManagerViewModel ConnectionManagerContext =>
            new ConnectionManagerViewModel(_connectionProvider, null);

        public static AddEditConnectionViewModel AddEditConnectionContext =>
            new AddEditConnectionViewModel(_connectionProvider)
            {
                Name = "Test"
            };

        public static ParameterManagerViewModel ParameterManagerContext => new ParameterManagerViewModel(Array.Empty<ParameterViewModel>(), null);
    }
}

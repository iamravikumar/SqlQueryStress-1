﻿using SqlQueryStressGUI.DbProviders;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SqlQueryStressGUI.TestEnvironment.Views
{
    /// <summary>
    /// Interaction logic for Toolbar.xaml
    /// </summary>
    public partial class Toolbar : UserControl
    {
        public static readonly DependencyProperty ConnectionsProperty;
        public static readonly DependencyProperty SelectedConnectionProperty;
        public static readonly DependencyProperty IterationsProperty;
        public static readonly DependencyProperty ThreadCountProperty;
        public static readonly DependencyProperty ExecuteCommandProperty;
        public static readonly DependencyProperty StopCommandProperty;
        public static readonly DependencyProperty NewQueryStressTestCommandProperty;
        public static readonly DependencyProperty OpenParameterSettingsCommandProperty;

        public Toolbar()
        {
            InitializeComponent();
        }

        static Toolbar()
        {
            ConnectionsProperty = DependencyProperty.Register(nameof(Connections), typeof(ObservableCollection<DatabaseConnection>), typeof(Toolbar));

            SelectedConnectionProperty = DependencyProperty.Register(
                nameof(SelectedConnection),
                typeof(DatabaseConnection),
                typeof(Toolbar),
                new FrameworkPropertyMetadata(default(DatabaseConnection), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

            IterationsProperty = DependencyProperty.Register(
                nameof(Iterations),
                typeof(int),
                typeof(Toolbar),
                new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

            ThreadCountProperty = DependencyProperty.Register(
                nameof(ThreadCount),
                typeof(int),
                typeof(Toolbar),
                new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

            ExecuteCommandProperty = DependencyProperty.Register(nameof(ExecuteCommand), typeof(ICommand), typeof(Toolbar));
            StopCommandProperty = DependencyProperty.Register(nameof(StopCommand), typeof(ICommand), typeof(Toolbar));
            NewQueryStressTestCommandProperty = DependencyProperty.Register(nameof(NewQueryStressTestCommand), typeof(ICommand), typeof(Toolbar));
            OpenParameterSettingsCommandProperty = DependencyProperty.Register(nameof(OpenParameterSettingsCommand), typeof(ICommand), typeof(Toolbar));
        }

        public ObservableCollection<DatabaseConnection> Connections
        {
            get => (ObservableCollection<DatabaseConnection>)GetValue(ConnectionsProperty);
            set => SetValue(ConnectionsProperty, value);
        }

        public DatabaseConnection SelectedConnection
        {
            get => (DatabaseConnection)GetValue(SelectedConnectionProperty);
            set => SetValue(SelectedConnectionProperty, value);
        }

        public int ThreadCount
        {
            get => (int)GetValue(ThreadCountProperty);
            set => SetValue(ThreadCountProperty, value);
        }

        public int Iterations
        {
            get => (int)GetValue(IterationsProperty);
            set => SetValue(IterationsProperty, value);
        }

        public ICommand ExecuteCommand
        {
            get => (ICommand)GetValue(ExecuteCommandProperty);
            set => SetValue(ExecuteCommandProperty, value);
        }

        public ICommand StopCommand
        {
            get => (ICommand)GetValue(StopCommandProperty);
            set => SetValue(StopCommandProperty, value);
        }

        public ICommand NewQueryStressTestCommand
        {
            get => (ICommand)GetValue(NewQueryStressTestCommandProperty);
            set => SetValue(NewQueryStressTestCommandProperty, value);
        }

        public ICommand OpenParameterSettingsCommand
        {
            get => (ICommand)GetValue(OpenParameterSettingsCommandProperty);
            set => SetValue(OpenParameterSettingsCommandProperty, value);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int _);
        }
    }
}

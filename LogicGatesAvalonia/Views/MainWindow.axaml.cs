using Avalonia.Controls;
using System;

using LogicGatesAvalonia.ViewModels;
using LogicGatesAvinternal.Controls;
using LogicGatesAvalonia.Controls;
using Avalonia.Input;

namespace LogicGatesAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel vm;
        private BaseLogicalGateControl selectedControl = null;
        public MainWindow(MainWindowViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;


            vm.NewLogicGateEvent += SpawnOnCanvasEventHandler;
            vm.DeleteConnectorEvent += DeleteConnectorFromCanvas;

            MyCanvas.PointerPressed += OnCanvasPointerPressed;
            ContextMenu.Closed += ContextMenuClosed;
            ContextMenu.IsVisible = false;
        }

        private void ContextMenuClosed(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ContextMenu.IsVisible = false;
            selectedControl = null;
        }

        private void SpawnOnCanvasEventHandler(object? sender, Control controlForSwapn)
        {
            if (controlForSwapn is BaseLogicalGateControl control)
            {
                MyCanvas.Children.Add(control);
            }

            if(controlForSwapn is Connector connector)
            {
                MyCanvas.Children.Add(connector);
            }
        }

        private void GenerateCanvasEventHandler(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var width = Convert.ToDouble(CanvasX.Text);
            var height = Convert.ToDouble(CanvasY.Text);

            
            MyCanvas.Height = height;
            MyCanvas.Width = width;
        }

        private void RemoveControlContextMenuHandler(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            if(selectedControl != null)
            {
                vm.RemoveLogicalGates(selectedControl);
                MyCanvas.Children.Remove(selectedControl);
            }

        }
        private void OnCanvasPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                if (e.Source is BaseLogicalGateControl control)
                {
                    ContextMenu.IsVisible = true;
                    ContextMenu.PlacementTarget = control;
                    selectedControl = control;
                }
            }

            this.vm.CanvasTapped(sender, e);
        }

        private void DeleteConnectorFromCanvas(object? sender, Control connector)
        {
            this.MyCanvas.Children.Remove(connector);
        }
    }
}
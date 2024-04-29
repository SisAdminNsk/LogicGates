using Avalonia.Controls;
using LogicGatesAvinternal.Controls;
using LogicGatesAvalonia.Models.ElectricCircuit;
using ReactiveUI;
using Avalonia.Input;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Models.LogicGates;

namespace LogicGatesAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Circuit circuit;

        public void RemoveLogicalGates(BaseLogicalGateControl control)
        {
            this.circuit.DeleteGate(control);
        }

        public delegate void SpawnNewLogicGate(object? sender,Control controlForSwapn);
        public event SpawnNewLogicGate? NewLogicGateEvent;


        public delegate void DeleteConnector(object? sender, Control connector);
        public event DeleteConnector? DeleteConnectorEvent;

        public void NotifyViewAboutConnectorDeletion(object? sender,Control connector) // view Должна подписаться на это
        {
            DeleteConnectorEvent?.Invoke(sender,connector);
        }

        private double canvasWidth = 1600;
        private double canvasHeight = 900;
        public double CanvasWidth
        {
            get => canvasWidth;
            set => this.RaiseAndSetIfChanged(ref canvasWidth, value);
        }

        public double CanvasHeight
        {
            get => canvasHeight;
            set => this.RaiseAndSetIfChanged(ref canvasHeight, value);
        }

        private ViewModelBase logicGatesPannel;
        public ViewModelBase LogicGatesPannel
        {
            get => logicGatesPannel;
            set => this.RaiseAndSetIfChanged(ref logicGatesPannel, value);
        }
        public MainWindowViewModel()
        {
            this.circuit = new Circuit(this);
            this.LogicGatesPannel = new LogicGatesPannelViewModel(this);

            circuit.DeleteConnectorEvent += NotifyViewAboutConnectorDeletion;
        }

        public void SpawnOnCanvas(object control)
        {
            if(control is BaseLogicalGateControl anyControl)
            {
                var clone = anyControl.Clone() as Control;
                this.circuit.AddGate(clone as BaseLogicalGateControl);

                Canvas.SetLeft(clone, 10);
                Canvas.SetTop(clone, 10);

                NewLogicGateEvent?.Invoke(this, clone);
            }
        }

        public void SpawnConnector(object control)
        {
            NewLogicGateEvent?.Invoke(this, control as Control);
        }

        public void CanvasTapped(object sender, PointerPressedEventArgs e)
        {
            circuit.CheckOnDisconnect(sender, e);
        }
    }
}

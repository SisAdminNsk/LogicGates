using Avalonia.Controls;
using System.Collections.Generic;

using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.ViewModels;
using LogicGatesAvinternal.Controls;
using Avalonia.Input;

namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class Circuit
    {
        public delegate void DeleteConnector(object? sender, Control connectorForDelete);
        public event DeleteConnector? DeleteConnectorEvent = null;

        public BaseLogicalGateControl outputSocket = null;
        private List<BaseLogicalGateControl> gates = new List<BaseLogicalGateControl>();
        private List<Connector> connectors = new List<Connector>();

        private BaseCircuitState circuitState;
        private MainWindowViewModel vm;

        public Circuit(MainWindowViewModel vm)
        {
            this.vm = vm;
            this.circuitState = new DefaultCircuitState(this);
        }

        public void SpawnConnector(Connector connector)
        {
            this.vm.SpawnConnector(connector);
        }

        public void AddConnector(Connector connector)
        {
            this.connectors.Add(connector);
        }
        public void AddGate(BaseLogicalGateControl gateControl)
        {
            gateControl.
                connectorManipulator.
                OutputConnectionHappendEvent += OutputConnectionHappend;

            gateControl.
                connectorManipulator.
                InputConnectionHappendEvent += InputConnectionHappend;

            gates.Add(gateControl);
        }

        public void DeleteGate(BaseLogicalGateControl gateControl)
        {
            if (gateControl is BaseOneChannelLogicalContorl oneChannelLogicalControl)
            {
                if (oneChannelLogicalControl.outputConenctor != null)
                {
                    oneChannelLogicalControl.outputConenctor.SetInput(false);
                    DeleteConnectorEvent?.Invoke(this, oneChannelLogicalControl.outputConenctor);
                    this.connectors.Remove(oneChannelLogicalControl.outputConenctor);
                }

                if (oneChannelLogicalControl.inputConnector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, oneChannelLogicalControl.inputConnector);
                    this.connectors.Remove(oneChannelLogicalControl.inputConnector);
                }
            }

            if (gateControl is BaseTwoChannelLogicalControl twoChannelLogicalControl)
            {
                if (twoChannelLogicalControl.outputConenctor != null)
                {
                    twoChannelLogicalControl.outputConenctor.SetInput(false);
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.outputConenctor);
                    this.connectors.Remove(twoChannelLogicalControl.outputConenctor);
                }

                if (twoChannelLogicalControl.input1Connector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.input1Connector);
                    this.connectors.Remove(twoChannelLogicalControl.input1Connector);
                }

                if (twoChannelLogicalControl.input2Connector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.input2Connector);
                    this.connectors.Remove(twoChannelLogicalControl.input2Connector);
                }
            }

            gates.Remove(gateControl);
        }

        public void OutputConnectionHappend(object? sender, OutputConnectionEventArgs e)
        {
            circuitState.ConnectWithOutput(sender,e);
        }

        public void InputConnectionHappend(object? sender, InputConnectionEventArgs e)
        {
            circuitState.ConnectWithInput(sender,e);
        }

        public void CheckOnDisconnect(object? sender,PointerPressedEventArgs e)
        {
            circuitState.CheckOnDisconnectWithOutput(sender,e);
        }

        public void SetState(CiruitStateType state)
        {
            switch(state)
            {
                case CiruitStateType.DefaultState:
                    this.circuitState = new DefaultCircuitState(this);
                    break;

                case CiruitStateType.WaitingInputConnectionState:
                    this.circuitState = new WaitingInputConnectionState(this);
                    break;

                default:
                    break;
            }
        }

        public CiruitStateType GetCiruitStateType()
        {
            if (circuitState.GetType() == typeof(DefaultCircuitState))
            {
                return CiruitStateType.DefaultState;
            }

            if (circuitState.GetType() == typeof(WaitingInputConnectionState))
            {
                return CiruitStateType.WaitingInputConnectionState;
            }

            return CiruitStateType.DefaultState;
        }
    }
}

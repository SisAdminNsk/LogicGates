using Avalonia.Input;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using LogicGatesAvinternal.Controls;


namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class WaitingInputConnectionState : BaseCircuitState
    {
        public WaitingInputConnectionState(Circuit circuit) : base(circuit)
        {

        }

        public override void ConnectWithInput(object? sender, InputConnectionEventArgs e)
        {
            var connector = new Connector(
                    circuit.outputSocket,
                    e.socket,
                    e.socketId);

            circuit.outputSocket.outputConenctor = connector;// задаем коннектор для выходного подключения

            if (e.socket is BaseOneChannelLogicalContorl oneChannelControl)
            {
                    oneChannelControl.inputConnector = connector;
            }

            if (e.socket is BaseTwoChannelLogicalControl twoChannelControl) // задаем коннекторы для входного подключения
            {
                if (e.socketId == InputSockets.firstSocket)// воткнули в первый сокет
                {
                    twoChannelControl.input1Connector = connector;
                }

                if (e.socketId == InputSockets.secondSocket)// воткнули во второй сокет
                {
                    twoChannelControl.input2Connector = connector;
                }
            }

            circuit.AddConnector(connector);
            circuit.SpawnConnector(connector);

            this.circuit.SetState(CiruitStateType.DefaultState);
        }

        public override void ConnectWithOutput(object? sender, OutputConnectionEventArgs e)
        {

        }

        public override void CheckOnDisconnectWithOutput(object? sender, PointerPressedEventArgs e)
        {
            if(e.Source is BaseLogicalGateControl control)
            {
                var clickPosX = e.GetPosition(control).X;
                var clickPosY = e.GetPosition(control).Y;

                if (control is BaseOneChannelLogicalContorl oneChannelLogicalControl)
                {
                    if (!oneChannelLogicalControl.IsInputHitted(new Avalonia.Point(clickPosX, clickPosY)))
                    {
                        this.circuit.SetState(CiruitStateType.DefaultState);
                        return;
                    }
                }

                if(control is BaseTwoChannelLogicalControl twoChannelControl)
                {
                    if (!twoChannelControl.IsInput1Hitted(new Avalonia.Point(clickPosX,clickPosY)))
                    {
                        if(!twoChannelControl.IsInput2Hitted(new Avalonia.Point(clickPosX, clickPosY)))
                        {
                            this.circuit.SetState(CiruitStateType.DefaultState);
                        }
                    }
                }
            }
            else
            {
                this.circuit.SetState(CiruitStateType.DefaultState);
            }
        }
    }
}

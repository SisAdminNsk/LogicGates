﻿using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalANDNOTCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalANDNOTCommand(string elementName, BaseDesiginationSymbol desiginationSymbol) :
            base(elementName, desiginationSymbol)
        {

        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalANDNOT(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}

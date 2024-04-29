﻿using Avalonia.Media;
using LogicGatesAvalonia.Models.DesiginationSymbols;
using System;

namespace LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates
{
    public class LogicalInvertor : OneChannelLogicalGate
    {
        public LogicalInvertor(string elementName, 
            string userElementName, 
            BaseDesiginationSymbol desiginationSymbol) : 
            base(elementName, userElementName, desiginationSymbol)
        {
        }

        protected override bool DoLogic()
        {
            if (this.input.Value is bool value)
            {
                return !value;
            }
            else
            {
                throw new ArgumentException($"value need to be boolean, value {this.input.Value} isn't boolean");
            }
        }
    }
}

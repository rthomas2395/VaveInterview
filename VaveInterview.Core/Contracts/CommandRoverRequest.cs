using System;
using System.Collections.Generic;
using System.Text;

namespace VaveInterview.Core.Contracts
{
    public class CommandRoverRequest
    {
        public Guid RoverId { get; set; }
        public string Commands { get; set; }
    }
}

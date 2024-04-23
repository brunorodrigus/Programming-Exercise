using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programming_Exercise.Model
{
    public class EndPoint : Meter
    {
        public string SerialNumber { get; set; }
        public int SwitchState { get; set; }
    }
}

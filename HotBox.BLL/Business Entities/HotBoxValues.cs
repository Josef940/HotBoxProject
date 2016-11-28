﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Entities
{
    public class HotBoxValues
    {
       [Browsable(false)]
       public string Module { get; set; }
       public string Label { get; set; }
       public double Value { get; set; }
       public string Unit { get; set; }
       public double? valueDifference { get; set; }
    }
}

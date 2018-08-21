﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Atlas_Command_Terminal.Models
{
    public class MBIN
    {
        public string filePath { get; set; }
        public string Name { get; set; }
        public string vanillaFilePath { get; set; }     // For comparison purposes.  Might not ever use this and end up removing it.
        public string vanillaName { get; set; }         // Used when creating new files that are based on vanilla (custom models, etc)
    }

    public class MBINField
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string NMSType { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Data.Context.Entities
{
    public partial class USER
    {
        public long id { get; set; }
        public string name { get; set; }
        public string groups { get; set; }
    }
}

using id.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace id.common.Entities
{
    public interface IXEntityBase
    {
        string XErrorMessage { get; set; }
        Exception XException { get; set; }
        GlobalStatus XStatus { get; set; }

        int XCount { get; set; }
    }
}

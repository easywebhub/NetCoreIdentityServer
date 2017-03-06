using id.common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace id.common.Entities
{
    public abstract class XEntityBase : IXEntityBase
    {
        public Exception XException { get; set; }
        public string XErrorMessage { get; set; }
        public GlobalStatus XStatus { get; set; }
        public ICollection<ValidationResult> ValidateResults = null;

        public int XCount { get; set; }


        protected void SyncStatus(XEntityBase d, XEntityBase s)
        {
            d.XStatus = s.XStatus;
            d.XErrorMessage = s.XErrorMessage;
            d.XException = s.XException;
            d.ValidateResults = s.ValidateResults;

        }
    }
}

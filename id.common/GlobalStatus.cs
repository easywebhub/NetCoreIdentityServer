using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace id.common
{
    public enum GlobalStatus
    {
        Success,
        UnSuccess,
        Failed,
        Invalid,
        InvalidData,
        Maximum_Limited, // bị giới hạn
        Access_Denied,
        NotFound,
        IsLocked,
        AlreadyExists,
    }
}

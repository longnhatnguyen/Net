using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Enum
{
    public class CybersEnum
    {
        public enum CybersStatus
        {
            [Description("Cyber đang chờ duyệt")]
            Pending = 1,
            [Description("Cyber đang hoạt động")]
            Active = 2,
            [Description("Cyber ngưng hoạt động")]
            Deactivate = 3,
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Enum
{
    public class UsersEnum
    {
        public enum UserRole
        {
            /// <summary>
            /// Admin có quyền cao nhất
            /// </summary>
            [Description("Admin")]
            Admin = 1,
            /// <summary>
            /// Cyber Manager là admin của Cyber, có quyền duyệt các Order
            /// </summary>
            [Description("CyberManager")]
            CyberManager = 2,
            /// <summary>
            /// UserNormal là người dùng, có quyền đặt chỗ
            /// </summary>
            [Description("User")]
            UserNormal = 3
        }
    }
}

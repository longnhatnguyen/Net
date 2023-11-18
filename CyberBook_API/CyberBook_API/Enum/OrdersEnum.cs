using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Enum
{
    public class OrdersEnum
    {
        public enum StatusOrder
        {
            [Description("Order được chủ quán duyệt")]
            Approve = 1,
            [Description("Order đang chờ chủ quán duyệt")]
            Pending = 2,
            [Description("Order bị chủ quán reject")]
            Reject = 3,
            [Description("Người dùng hủy đơn đặt chỗ")]
            Cancel = 4,
        }
    }
}

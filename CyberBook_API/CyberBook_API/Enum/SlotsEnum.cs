using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CyberBook_API.Enum
{
    public class SlotsEnum
    {
        public enum SlotStatus
        {    
            /// <summary>
            /// Trạng thái đang có người dùng
            /// </summary>
            [Description("Bận")]
            NotReady = 1,
            /// <summary>
            /// Trạng thái máy rảnh có thể book
            /// </summary>
            [Description("Rảnh")]
            Ready = 2,
            /// <summary>
            /// Trạng thái máy đang được sửa chữa hoặc update
            /// </summary>
            [Description("Đang sửa chữa")]
            Fixing = 3,
            /// <summary>
            /// Trạng thái máy đã có người đặt trước
            /// </summary>
            [Description("Đã được đặt trước")]
            Booked = 4,
            /// <summary>
            /// Khi chủ quán thay đổi kích cỡ phòng nhỏ đi, các máy ở vị trí lớn hơn kích cơ mới của phòng sẽ trở thành unload
            /// </summary>
            [Description("Không hợp lệ khi thay đổi kích thước phòng")]
            Unload = 5
        }

    }
}

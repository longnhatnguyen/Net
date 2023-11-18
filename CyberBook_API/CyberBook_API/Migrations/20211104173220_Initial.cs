using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberBook_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cyber",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CyberName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CyberDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RatingPoint = table.Column<int>(type: "int", nullable: true),
                    BossCyberName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BossCyberID = table.Column<int>(type: "int", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    lng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cyber", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CyberAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CyberID = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CyberName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyberAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlotOrderID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    StatusOrder = table.Column<int>(type: "int", nullable: true),
                    CyberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RatingCyber",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    RatePoint = table.Column<int>(type: "int", nullable: true),
                    CommentContent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CyberID = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Edited = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCyber", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RatingUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CyberID = table.Column<int>(type: "int", nullable: true),
                    RatePoint = table.Column<int>(type: "int", nullable: true),
                    CommentContent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UsersID = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Edited = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CyberID = table.Column<int>(type: "int", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoomType = table.Column<int>(type: "int", nullable: true),
                    RoomPosition = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaxX = table.Column<int>(type: "int", nullable: false),
                    MaxY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoomType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoomDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: true),
                    SlotName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SlotHardwareConfigID = table.Column<int>(type: "int", nullable: true),
                    SlotHardwareName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    SlotDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SlotPositionX = table.Column<int>(type: "int", nullable: true),
                    SlotPositionY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SlotHardwareConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monitor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GPU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CPU = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RAM = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NameHardware = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CyberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotHardwareConfig", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusOrder",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusOrderDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrder", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StatusSlot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusSlotDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusSlot", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime", nullable: true),
                    RatingPoint = table.Column<int>(type: "int", nullable: true),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    ComfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "ICy5YqxZB1uWSwcVLSNLcA==", "linhphung" },
                    { 2, "kAFQmDzST7DWlj99KOF/cg==", "huynhnd53" },
                    { 3, "kAFQmDzST7DWlj99KOF/cg==", "dungba" }
                });

            migrationBuilder.InsertData(
                table: "Cyber",
                columns: new[] { "ID", "Address", "BossCyberID", "BossCyberName", "CyberDescription", "CyberName", "PhoneNumber", "RatingPoint", "image", "lat", "lng", "status" },
                values: new object[,]
                {
                    { 1, "King Cyber CC2 Linh Đàm, CC2, Nguyễn Hữu Thọ, Dai Kim, Hoàng Mai, Hanoi", 2, "Phùng Đức Linh", "KINGCYBER xin trân trọng công bố giải đấu đầu tiên trong hệ thống giải mùa hè 2020 - KINGCYBER SUNDAY, khởi tranh vào ngày Chủ Nhật 07/06 với nội dung thi đấu LMHT. Thông tin và thể lệ đăng ký như sau:", "King Cyber Game", "0934808373", 5, null, "20.9684231", "105.8250523", true },
                    { 2, "Cyber Legend QL13, Quốc lộ 13, Hiệp Bình Phước, Thủ Đức, Thành phố Hồ Chí Minh", 1, "Nguyễn Đình Huynh", "Điểm cung cấp trò chơi điện tử", "Cyber Legend", "0908404054", 7, null, "10.8397686", "106.7139693", true }
                });

            migrationBuilder.InsertData(
                table: "CyberAccount",
                columns: new[] { "Id", "CyberID", "CyberName", "Password", "PhoneNumber", "UserID", "Username" },
                values: new object[,]
                {
                    { 1, 1, "King Cyber Game", "12345", "0987432666", 1, "pikachu01" },
                    { 2, 2, "Cyber Legend", "1234", "0934637895", 1, "legend02" },
                    { 3, 1, "King Cyber Game", "1234", "0934637895", 2, "legend02" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "ID", "CreatedBy", "CreatedDate", "CyberId", "EndAt", "SlotOrderID", "StartAt", "StatusOrder" },
                values: new object[,]
                {
                    { 4, 2, new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), "6", new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 2, new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), "9", new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), 2 },
                    { 1, 1, new DateTime(2021, 8, 10, 13, 23, 44, 0, DateTimeKind.Unspecified), 1, new DateTime(2021, 9, 11, 10, 23, 44, 0, DateTimeKind.Unspecified), "1", new DateTime(2021, 8, 11, 13, 23, 44, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified), 2, new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), "2", new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "RatingCyber",
                columns: new[] { "ID", "CommentContent", "CreatedDate", "CyberID", "Edited", "RatePoint", "UpdatedDate", "UserID" },
                values: new object[,]
                {
                    { 1, "Chất lượng phục vụ tốt", new DateTime(2020, 10, 29, 14, 56, 59, 0, DateTimeKind.Unspecified), 1, true, 5, new DateTime(2021, 10, 29, 20, 0, 59, 0, DateTimeKind.Unspecified), 1 },
                    { 2, "Có nhiều máy giá rẻ", new DateTime(2020, 8, 2, 4, 21, 59, 0, DateTimeKind.Unspecified), 2, false, 7, new DateTime(2021, 10, 21, 17, 21, 59, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "RoleName" },
                values: new object[,]
                {
                    { 3, "User" },
                    { 1, "Admin" },
                    { 2, "Cyber Manager" }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "ID", "CyberID", "MaxX", "MaxY", "RoomName", "RoomPosition", "RoomType" },
                values: new object[,]
                {
                    { 2, 2, 0, 0, "Phòng cao cấp", "tầng 2", 2 },
                    { 3, 3, 0, 0, "Phòng chuyên nghiệp", "tầng 3", 3 },
                    { 1, 1, 0, 0, "Phòng thường", "tầng 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Slot",
                columns: new[] { "ID", "RoomID", "SlotDescription", "SlotHardwareConfigID", "SlotHardwareName", "SlotName", "SlotPositionX", "SlotPositionY", "StatusID" },
                values: new object[,]
                {
                    { 1, 1, "Máy dùng để chơi những game online nhẹ nhàng", 1, "Máy thường", "Máy 1", 2, 5, 1 },
                    { 2, 1, "Máy dùng stream", 2, "Máy stream", "Máy 4", 7, 5, 2 },
                    { 3, 1, "Máy dùng để thi đấu", 3, "Máy thi đấu", "Máy 8", 1, 5, 3 },
                    { 4, 1, "Máy dùng để chơi những game online nhẹ nhàng", 1, "Máy thường", "Máy 19", 1, 1, 1 },
                    { 5, 1, "Máy dùng để chơi những game online nhẹ nhàng", 2, "Máy stream", "Máy 7", 2, 3, 2 },
                    { 6, 1, "Máy dùng để chơi những game online nhẹ nhàng", 1, "Máy thường", "Máy 9", 4, 8, 1 },
                    { 7, 1, "Máy dùng để chơi những game online nhẹ nhàng", 3, "Máy thi đấu", "Máy 5", 2, 4, 3 }
                });

            migrationBuilder.InsertData(
                table: "SlotHardwareConfig",
                columns: new[] { "ID", "CPU", "CreatedDate", "CyberID", "GPU", "Monitor", "NameHardware", "RAM", "UpdateDate" },
                values: new object[,]
                {
                    { 2, "Intel Core i9-10900K Processor", new DateTime(2021, 3, 1, 13, 23, 44, 0, DateTimeKind.Unspecified), 2, "NVIDIA GeForce RTX 3080 Ti", "BenQ EX2780Q", "Cấu hình Nâng cấp", "G.Skill Trident Z RGB", new DateTime(2021, 4, 11, 10, 23, 44, 0, DateTimeKind.Unspecified) },
                    { 3, "AMD Ryzen 9 5900X", new DateTime(2021, 6, 11, 13, 23, 44, 0, DateTimeKind.Unspecified), 1, "AMD Radeon 6900 XT", "Acer Nitro XV252Q F", "Cấu hình Cao cấp", "Kingston HyperX Predator", new DateTime(2021, 8, 27, 10, 23, 44, 0, DateTimeKind.Unspecified) },
                    { 1, "AMD Ryzen 9 5950X", new DateTime(2021, 8, 11, 13, 23, 44, 0, DateTimeKind.Unspecified), 1, "NVIDIA GeForce RTX 3090", "ViewSonic XG2705 27 inch FHD 144Hz", "Cấu hình chung", "Corsair Vengeance LED", new DateTime(2021, 9, 11, 10, 23, 44, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "StatusOrder",
                columns: new[] { "ID", "StatusOrderDescription" },
                values: new object[,]
                {
                    { 1, "Accepted" },
                    { 2, "Pending" },
                    { 3, "Reject" }
                });

            migrationBuilder.InsertData(
                table: "StatusSlot",
                columns: new[] { "ID", "StatusSlotDescription" },
                values: new object[,]
                {
                    { 1, "Bận" },
                    { 2, "Rảnh" },
                    { 3, "Đang sửa chữa" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "AccountID", "Address", "Bio", "ComfirmPassword", "Dob", "Email", "Fullname", "Image", "PhoneNumber", "RatingPoint", "RoleID" },
                values: new object[,]
                {
                    { 2, 2, "Chung cư Tứ Hiệp Plaza, Tứ Hiệp, Ha Nội, Hà Nội", "lorem lipsom", null, new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(4695), "linhpdhe130432@fpt.edu.vn", "Phùng Đức Linh", ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", "0962515234", 7, 2 },
                    { 1, 1, "Thường tín", "Đẹp trai", null, new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(965), "HuynhNDHE130390@fpt.edu.vn", "Nguyễn Đình Huynh", ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", "0387706669", 5, 1 },
                    { 3, 3, "Hà Nội", "lorem lipsom", null, new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(4707), "dungbahe130372@fpt.edu.vn", "Bùi Anh Dũng", ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", "0832229283", 7, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cyber");

            migrationBuilder.DropTable(
                name: "CyberAccount");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "RatingCyber");

            migrationBuilder.DropTable(
                name: "RatingUser");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "RoomType");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "SlotHardwareConfig");

            migrationBuilder.DropTable(
                name: "StatusOrder");

            migrationBuilder.DropTable(
                name: "StatusSlot");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

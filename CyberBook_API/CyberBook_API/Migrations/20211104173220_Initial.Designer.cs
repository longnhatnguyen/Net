﻿// <auto-generated />
using System;
using CyberBook_API.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CyberBook_API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211104173220_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CyberBook_API.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "ICy5YqxZB1uWSwcVLSNLcA==",
                            Username = "linhphung"
                        },
                        new
                        {
                            Id = 2,
                            Password = "kAFQmDzST7DWlj99KOF/cg==",
                            Username = "huynhnd53"
                        },
                        new
                        {
                            Id = 3,
                            Password = "kAFQmDzST7DWlj99KOF/cg==",
                            Username = "dungba"
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.Cyber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("BossCyberID")
                        .HasColumnType("int")
                        .HasColumnName("BossCyberID");

                    b.Property<string>("BossCyberName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CyberDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CyberName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("RatingPoint")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("lat")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("lat");

                    b.Property<string>("lng")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("lng");

                    b.Property<bool>("status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("Cyber");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "King Cyber CC2 Linh Đàm, CC2, Nguyễn Hữu Thọ, Dai Kim, Hoàng Mai, Hanoi",
                            BossCyberID = 2,
                            BossCyberName = "Phùng Đức Linh",
                            CyberDescription = "KINGCYBER xin trân trọng công bố giải đấu đầu tiên trong hệ thống giải mùa hè 2020 - KINGCYBER SUNDAY, khởi tranh vào ngày Chủ Nhật 07/06 với nội dung thi đấu LMHT. Thông tin và thể lệ đăng ký như sau:",
                            CyberName = "King Cyber Game",
                            PhoneNumber = "0934808373",
                            RatingPoint = 5,
                            lat = "20.9684231",
                            lng = "105.8250523",
                            status = true
                        },
                        new
                        {
                            Id = 2,
                            Address = "Cyber Legend QL13, Quốc lộ 13, Hiệp Bình Phước, Thủ Đức, Thành phố Hồ Chí Minh",
                            BossCyberID = 1,
                            BossCyberName = "Nguyễn Đình Huynh",
                            CyberDescription = "Điểm cung cấp trò chơi điện tử",
                            CyberName = "Cyber Legend",
                            PhoneNumber = "0908404054",
                            RatingPoint = 7,
                            lat = "10.8397686",
                            lng = "106.7139693",
                            status = true
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.CyberAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CyberId")
                        .HasColumnType("int")
                        .HasColumnName("CyberID");

                    b.Property<string>("CyberName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Username");

                    b.HasKey("ID");

                    b.ToTable("CyberAccount");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CyberId = 1,
                            CyberName = "King Cyber Game",
                            Password = "12345",
                            PhoneNumber = "0987432666",
                            UserId = 1,
                            Username = "pikachu01"
                        },
                        new
                        {
                            ID = 2,
                            CyberId = 2,
                            CyberName = "Cyber Legend",
                            Password = "1234",
                            PhoneNumber = "0934637895",
                            UserId = 1,
                            Username = "legend02"
                        },
                        new
                        {
                            ID = 3,
                            CyberId = 1,
                            CyberName = "King Cyber Game",
                            Password = "1234",
                            PhoneNumber = "0934637895",
                            UserId = 2,
                            Username = "legend02"
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("CyberId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("datetime");

                    b.Property<string>("SlotOrderId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("SlotOrderID");

                    b.Property<DateTime?>("StartAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("StatusOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = 1,
                            CreatedDate = new DateTime(2021, 8, 10, 13, 23, 44, 0, DateTimeKind.Unspecified),
                            CyberId = 1,
                            EndAt = new DateTime(2021, 9, 11, 10, 23, 44, 0, DateTimeKind.Unspecified),
                            SlotOrderId = "1",
                            StartAt = new DateTime(2021, 8, 11, 13, 23, 44, 0, DateTimeKind.Unspecified),
                            StatusOrder = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = 2,
                            CreatedDate = new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            CyberId = 2,
                            EndAt = new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            SlotOrderId = "2",
                            StartAt = new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            StatusOrder = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = 2,
                            CreatedDate = new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            CyberId = 2,
                            EndAt = new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            SlotOrderId = "9",
                            StartAt = new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            StatusOrder = 2
                        },
                        new
                        {
                            Id = 4,
                            CreatedBy = 2,
                            CreatedDate = new DateTime(2021, 6, 28, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            CyberId = 1,
                            EndAt = new DateTime(2021, 9, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            SlotOrderId = "6",
                            StartAt = new DateTime(2021, 6, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            StatusOrder = 2
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.RatingCyber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CyberId")
                        .HasColumnType("int")
                        .HasColumnName("CyberID");

                    b.Property<bool?>("Edited")
                        .HasColumnType("bit");

                    b.Property<int?>("RatePoint")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.ToTable("RatingCyber");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CommentContent = "Chất lượng phục vụ tốt",
                            CreatedDate = new DateTime(2020, 10, 29, 14, 56, 59, 0, DateTimeKind.Unspecified),
                            CyberId = 1,
                            Edited = true,
                            RatePoint = 5,
                            UpdatedDate = new DateTime(2021, 10, 29, 20, 0, 59, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CommentContent = "Có nhiều máy giá rẻ",
                            CreatedDate = new DateTime(2020, 8, 2, 4, 21, 59, 0, DateTimeKind.Unspecified),
                            CyberId = 2,
                            Edited = false,
                            RatePoint = 7,
                            UpdatedDate = new DateTime(2021, 10, 21, 17, 21, 59, 0, DateTimeKind.Unspecified),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.RatingUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CyberId")
                        .HasColumnType("int")
                        .HasColumnName("CyberID");

                    b.Property<bool?>("Edited")
                        .HasColumnType("bit");

                    b.Property<int?>("RatePoint")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("UsersId")
                        .HasColumnType("int")
                        .HasColumnName("UsersID");

                    b.HasKey("Id");

                    b.ToTable("RatingUser");
                });

            modelBuilder.Entity("CyberBook_API.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Cyber Manager"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CyberId")
                        .HasColumnType("int")
                        .HasColumnName("CyberID");

                    b.Property<int>("MaxX")
                        .HasColumnType("int");

                    b.Property<int>("MaxY")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RoomPosition")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("RoomType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Room");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CyberId = 1,
                            MaxX = 0,
                            MaxY = 0,
                            RoomName = "Phòng thường",
                            RoomPosition = "tầng 1",
                            RoomType = 1
                        },
                        new
                        {
                            Id = 2,
                            CyberId = 2,
                            MaxX = 0,
                            MaxY = 0,
                            RoomName = "Phòng cao cấp",
                            RoomPosition = "tầng 2",
                            RoomType = 2
                        },
                        new
                        {
                            Id = 3,
                            CyberId = 3,
                            MaxX = 0,
                            MaxY = 0,
                            RoomName = "Phòng chuyên nghiệp",
                            RoomPosition = "tầng 3",
                            RoomType = 3
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoomDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RoomTypeName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("RoomType");
                });

            modelBuilder.Entity("CyberBook_API.Models.Slot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("RoomId")
                        .HasColumnType("int")
                        .HasColumnName("RoomID");

                    b.Property<string>("SlotDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("SlotHardwareConfigId")
                        .HasColumnType("int")
                        .HasColumnName("SlotHardwareConfigID");

                    b.Property<string>("SlotHardwareName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SlotName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("SlotPositionX")
                        .HasColumnType("int");

                    b.Property<int?>("SlotPositionY")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("Id");

                    b.ToTable("Slot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để chơi những game online nhẹ nhàng",
                            SlotHardwareConfigId = 1,
                            SlotHardwareName = "Máy thường",
                            SlotName = "Máy 1",
                            SlotPositionX = 2,
                            SlotPositionY = 5,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoomId = 1,
                            SlotDescription = "Máy dùng stream",
                            SlotHardwareConfigId = 2,
                            SlotHardwareName = "Máy stream",
                            SlotName = "Máy 4",
                            SlotPositionX = 7,
                            SlotPositionY = 5,
                            StatusId = 2
                        },
                        new
                        {
                            Id = 3,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để thi đấu",
                            SlotHardwareConfigId = 3,
                            SlotHardwareName = "Máy thi đấu",
                            SlotName = "Máy 8",
                            SlotPositionX = 1,
                            SlotPositionY = 5,
                            StatusId = 3
                        },
                        new
                        {
                            Id = 4,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để chơi những game online nhẹ nhàng",
                            SlotHardwareConfigId = 1,
                            SlotHardwareName = "Máy thường",
                            SlotName = "Máy 19",
                            SlotPositionX = 1,
                            SlotPositionY = 1,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 5,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để chơi những game online nhẹ nhàng",
                            SlotHardwareConfigId = 2,
                            SlotHardwareName = "Máy stream",
                            SlotName = "Máy 7",
                            SlotPositionX = 2,
                            SlotPositionY = 3,
                            StatusId = 2
                        },
                        new
                        {
                            Id = 6,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để chơi những game online nhẹ nhàng",
                            SlotHardwareConfigId = 1,
                            SlotHardwareName = "Máy thường",
                            SlotName = "Máy 9",
                            SlotPositionX = 4,
                            SlotPositionY = 8,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 7,
                            RoomId = 1,
                            SlotDescription = "Máy dùng để chơi những game online nhẹ nhàng",
                            SlotHardwareConfigId = 3,
                            SlotHardwareName = "Máy thi đấu",
                            SlotName = "Máy 5",
                            SlotPositionX = 2,
                            SlotPositionY = 4,
                            StatusId = 3
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.SlotHardwareConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("CPU");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("CyberID")
                        .HasColumnType("int")
                        .HasColumnName("CyberID");

                    b.Property<string>("Gpu")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("GPU");

                    b.Property<string>("Monitor")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameHardware")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ram")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("RAM");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("SlotHardwareConfig");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cpu = "AMD Ryzen 9 5950X",
                            CreatedDate = new DateTime(2021, 8, 11, 13, 23, 44, 0, DateTimeKind.Unspecified),
                            CyberID = 1,
                            Gpu = "NVIDIA GeForce RTX 3090",
                            Monitor = "ViewSonic XG2705 27 inch FHD 144Hz",
                            NameHardware = "Cấu hình chung",
                            Ram = "Corsair Vengeance LED",
                            UpdateDate = new DateTime(2021, 9, 11, 10, 23, 44, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Cpu = "Intel Core i9-10900K Processor",
                            CreatedDate = new DateTime(2021, 3, 1, 13, 23, 44, 0, DateTimeKind.Unspecified),
                            CyberID = 2,
                            Gpu = "NVIDIA GeForce RTX 3080 Ti",
                            Monitor = "BenQ EX2780Q",
                            NameHardware = "Cấu hình Nâng cấp",
                            Ram = "G.Skill Trident Z RGB",
                            UpdateDate = new DateTime(2021, 4, 11, 10, 23, 44, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Cpu = "AMD Ryzen 9 5900X",
                            CreatedDate = new DateTime(2021, 6, 11, 13, 23, 44, 0, DateTimeKind.Unspecified),
                            CyberID = 1,
                            Gpu = "AMD Radeon 6900 XT",
                            Monitor = "Acer Nitro XV252Q F",
                            NameHardware = "Cấu hình Cao cấp",
                            Ram = "Kingston HyperX Predator",
                            UpdateDate = new DateTime(2021, 8, 27, 10, 23, 44, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.StatusOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusOrderDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("StatusOrder");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusOrderDescription = "Accepted"
                        },
                        new
                        {
                            Id = 2,
                            StatusOrderDescription = "Pending"
                        },
                        new
                        {
                            Id = 3,
                            StatusOrderDescription = "Reject"
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.StatusSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusSlotDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("StatusSlot");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusSlotDescription = "Bận"
                        },
                        new
                        {
                            Id = 2,
                            StatusSlotDescription = "Rảnh"
                        },
                        new
                        {
                            Id = 3,
                            StatusSlotDescription = "Đang sửa chữa"
                        });
                });

            modelBuilder.Entity("CyberBook_API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ComfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Fullname")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("RatingPoint")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountID = 1,
                            Address = "Thường tín",
                            Bio = "Đẹp trai",
                            Dob = new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(965),
                            Email = "HuynhNDHE130390@fpt.edu.vn",
                            Fullname = "Nguyễn Đình Huynh",
                            Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG",
                            PhoneNumber = "0387706669",
                            RatingPoint = 5,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountID = 2,
                            Address = "Chung cư Tứ Hiệp Plaza, Tứ Hiệp, Ha Nội, Hà Nội",
                            Bio = "lorem lipsom",
                            Dob = new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(4695),
                            Email = "linhpdhe130432@fpt.edu.vn",
                            Fullname = "Phùng Đức Linh",
                            Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG",
                            PhoneNumber = "0962515234",
                            RatingPoint = 7,
                            RoleId = 2
                        },
                        new
                        {
                            Id = 3,
                            AccountID = 3,
                            Address = "Hà Nội",
                            Bio = "lorem lipsom",
                            Dob = new DateTime(2021, 11, 5, 0, 32, 20, 334, DateTimeKind.Local).AddTicks(4707),
                            Email = "dungbahe130372@fpt.edu.vn",
                            Fullname = "Bùi Anh Dũng",
                            Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG",
                            PhoneNumber = "0832229283",
                            RatingPoint = 7,
                            RoleId = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

using System;
using System.Threading.Tasks;
using CyberBook_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CyberBook_API.Dal
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Cyber> Cybers { get; set; }
        public virtual DbSet<CyberAccount> CyberAccounts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<RatingCyber> RatingCybers { get; set; }
        public virtual DbSet<RatingUser> RatingUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<SlotHardwareConfig> SlotHardwareConfigs { get; set; }
        public virtual DbSet<StatusOrder> StatusOrders { get; set; }
        public virtual DbSet<StatusSlot> StatusSlots { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CyberBooking;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=tcp:cyberbookingcheck-db-server.database.windows.net,1433;Initial Catalog=CyberBookingCheck_Db;Persist Security Info=False;User ID=huynhnd;Password=Dinhhuynh533;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Cyber>(entity =>
            {
                entity.ToTable("Cyber");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.lat)
                    .HasMaxLength(200)
                    .HasColumnName("lat");
                entity.Property(e => e.lng)
                 .HasMaxLength(200)
                 .HasColumnName("lng");

                entity.Property(e => e.BossCyberName).HasMaxLength(255);

                entity.Property(e => e.BossCyberID).HasColumnName("BossCyberID");

                entity.Property(e => e.RatingPoint).HasColumnName("RatingPoint");

                entity.Property(e => e.CyberDescription).HasMaxLength(255);

                entity.Property(e => e.CyberName).HasMaxLength(255);

                entity.Property(e => e.image).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<CyberAccount>(entity =>
            {
                entity.ToTable("CyberAccount");
                entity.Property(e => e.ID).HasColumnName("Id");

                entity.Property(e => e.CyberId).HasColumnName("CyberID");

                entity.Property(e => e.CyberName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Username).HasColumnName("Username");
                entity.Property(e => e.Password).HasColumnName("Password");

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndAt).HasColumnType("datetime");

                entity.Property(e => e.SlotOrderId)
                    .HasMaxLength(255)
                    .HasColumnName("SlotOrderID");

                entity.Property(e => e.StartAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<RatingCyber>(entity =>
            {
                entity.ToTable("RatingCyber");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentContent).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CyberId).HasColumnName("CyberID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.RatePoint).HasColumnName("RatePoint");

                entity.Property(e => e.OrderId).HasColumnName("OrderId");
            });

            modelBuilder.Entity<RatingUser>(entity =>
            {
                entity.ToTable("RatingUser");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CommentContent).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CyberId).HasColumnName("CyberID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UsersId).HasColumnName("UsersID");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName).HasMaxLength(255);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CyberId).HasColumnName("CyberID");

                entity.Property(e => e.RoomName).HasMaxLength(255);
                entity.Property(e => e.RoomPosition).HasMaxLength(255);
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoomDescription).HasMaxLength(255);

                entity.Property(e => e.RoomTypeName).HasMaxLength(255);
                entity.Property(e => e.CyberID).HasColumnName("CyberID");

            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.SlotDescription).HasMaxLength(255);
                entity.Property(e => e.SlotName).HasMaxLength(255);

                entity.Property(e => e.SlotHardwareConfigId).HasColumnName("SlotHardwareConfigID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<SlotHardwareConfig>(entity =>
            {
                entity.ToTable("SlotHardwareConfig");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cpu)
                    .HasMaxLength(255)
                    .HasColumnName("CPU");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Gpu)
                    .HasMaxLength(255)
                    .HasColumnName("GPU");

                entity.Property(e => e.Monitor).HasMaxLength(255);

                entity.Property(e => e.NameHardware).HasMaxLength(255);

                entity.Property(e => e.Ram)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("RAM");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
                entity.Property(e => e.CyberID).HasColumnName("CyberID");
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.ToTable("StatusOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusOrderDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<StatusSlot>(entity =>
            {
                entity.ToTable("StatusSlot");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatusSlotDescription).HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Bio).HasMaxLength(255);

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.RatingPoint).HasColumnName("RatingPoint");
                entity.Property(e => e.AccountID).HasColumnName("AccountID");
            });


            //Seed Data
            modelBuilder.Entity<Account>().HasData(
               new Account() { Id = 1, Username = "linhphung", Password = "ICy5YqxZB1uWSwcVLSNLcA==" },
               new Account() { Id = 2, Username = "huynhnd53", Password = "kAFQmDzST7DWlj99KOF/cg==" },
                new Account() { Id = 3, Username = "dungba", Password = "kAFQmDzST7DWlj99KOF/cg==" }
               );

            modelBuilder.Entity<Cyber>().HasData(
                new Cyber() { Id = 1, CyberName = "King Cyber Game", Address = "King Cyber CC2 Linh Đàm, CC2, Nguyễn Hữu Thọ, Dai Kim, Hoàng Mai, Hanoi", PhoneNumber = "0934808373", CyberDescription = "KINGCYBER xin trân trọng công bố giải đấu đầu tiên trong hệ thống giải mùa hè 2020 - KINGCYBER SUNDAY, khởi tranh vào ngày Chủ Nhật 07/06 với nội dung thi đấu LMHT. Thông tin và thể lệ đăng ký như sau:", RatingPoint = 5, BossCyberName = "Phùng Đức Linh", BossCyberID = 2, lat = "20.9684231", lng = "105.8250523", status = 2 },
                new Cyber() { Id = 2, CyberName = "Cyber Legend", Address = "Cyber Legend QL13, Quốc lộ 13, Hiệp Bình Phước, Thủ Đức, Thành phố Hồ Chí Minh", PhoneNumber = "0908404054", CyberDescription = "Điểm cung cấp trò chơi điện tử", RatingPoint = 7, BossCyberName = "Nguyễn Đình Huynh", BossCyberID = 1, lat = "10.8397686", lng = "106.7139693", status = 2 }
                );

            modelBuilder.Entity<CyberAccount>().HasData(
                new CyberAccount() { ID = 1, UserId = 1, CyberId = 1, PhoneNumber = "0987432666", CyberName = "King Cyber Game", Username = "pikachu01", Password = "12345" },
                new CyberAccount() { ID = 2, UserId = 1, CyberId = 2, PhoneNumber = "0934637895", CyberName = "Cyber Legend", Username = "legend02", Password = "1234" },
                new CyberAccount() { ID = 3, UserId = 2, CyberId = 1, PhoneNumber = "0934637895", CyberName = "King Cyber Game", Username = "legend02", Password = "1234" }
                 );

            modelBuilder.Entity<Order>().HasData(
                new Order() { Id = 1, SlotOrderId = "1", StartAt = DateTime.Parse("2021-08-11 13:23:44.000"), EndAt = DateTime.Parse("2021-09-11 10:23:44.000"), CreatedDate = DateTime.Parse("2021-08-10 13:23:44.000"), CreatedBy = 1, StatusOrder = 1, CyberId = 1 },
                new Order() { Id = 2, SlotOrderId = "2", StartAt = DateTime.Parse("2021-06-29 14:56:59.000"), EndAt = DateTime.Parse("2021-09-29 14:56:59.000"), CreatedDate = DateTime.Parse("2021-06-28 14:56:59.000"), CreatedBy = 2, StatusOrder = 2, CyberId = 2 },
                new Order() { Id = 3, SlotOrderId = "9", StartAt = DateTime.Parse("2021-06-29 14:56:59.000"), EndAt = DateTime.Parse("2021-09-29 14:56:59.000"), CreatedDate = DateTime.Parse("2021-06-28 14:56:59.000"), CreatedBy = 2, StatusOrder = 2, CyberId = 2 },
                new Order() { Id = 4, SlotOrderId = "6", StartAt = DateTime.Parse("2021-06-29 14:56:59.000"), EndAt = DateTime.Parse("2021-09-29 14:56:59.000"), CreatedDate = DateTime.Parse("2021-06-28 14:56:59.000"), CreatedBy = 2, StatusOrder = 2, CyberId = 1 }
                );

            modelBuilder.Entity<RatingCyber>().HasData(
                new RatingCyber() { Id = 1, UserId = 1, RatePoint = 5, CommentContent = "Chất lượng phục vụ tốt", CyberId = 1, CreatedDate = DateTime.Parse("2020-10-29 14:56:59.000"), UpdatedDate = DateTime.Parse("2021-10-29 20:00:59.000"), Edited = true },
                new RatingCyber() { Id = 2, UserId = 2, RatePoint = 7, CommentContent = "Có nhiều máy giá rẻ", CyberId = 2, CreatedDate = DateTime.Parse("2020-08-02 04:21:59.000"), UpdatedDate = DateTime.Parse("2021-10-21 17:21:59.000"), Edited = false }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, RoleName = "Admin" },
                new Role() { Id = 2, RoleName = "Cyber Manager" },
                new Role() { Id = 3, RoleName = "User" }
                );

            modelBuilder.Entity<SlotHardwareConfig>().HasData(
                new SlotHardwareConfig() { Id = 1, Monitor = "ViewSonic XG2705 27 inch FHD 144Hz", Gpu = "NVIDIA GeForce RTX 3090", Cpu = "AMD Ryzen 9 5950X", Ram = "Corsair Vengeance LED", CreatedDate = DateTime.Parse("2021-08-11 13:23:44.000"), UpdateDate = DateTime.Parse("2021-09-11 10:23:44.000"), NameHardware = "Cấu hình chung", CyberID = 1 },
                new SlotHardwareConfig() { Id = 2, Monitor = "BenQ EX2780Q", Gpu = "NVIDIA GeForce RTX 3080 Ti", Cpu = "Intel Core i9-10900K Processor", Ram = "G.Skill Trident Z RGB", CreatedDate = DateTime.Parse("2021-03-01 13:23:44.000"), UpdateDate = DateTime.Parse("2021-04-11 10:23:44.000"), NameHardware = "Cấu hình Nâng cấp", CyberID = 2 },
                new SlotHardwareConfig() { Id = 3, Monitor = "Acer Nitro XV252Q F", Gpu = "AMD Radeon 6900 XT", Cpu = "AMD Ryzen 9 5900X", Ram = "Kingston HyperX Predator", CreatedDate = DateTime.Parse("2021-06-11 13:23:44.000"), UpdateDate = DateTime.Parse("2021-08-27 10:23:44.000"), NameHardware = "Cấu hình Cao cấp", CyberID = 1 }
                );

            modelBuilder.Entity<StatusOrder>().HasData(
                new StatusOrder() { Id = 1, StatusOrderDescription = "Accepted" },
                new StatusOrder() { Id = 2, StatusOrderDescription = "Pending" },
                new StatusOrder() { Id = 3, StatusOrderDescription = "Reject" }
                );

            modelBuilder.Entity<StatusSlot>().HasData(
                new StatusSlot() { Id = 1, StatusSlotDescription = "Bận" },
                new StatusSlot() { Id = 2, StatusSlotDescription = "Rảnh" },
                new StatusSlot() { Id = 3, StatusSlotDescription = "Đang sửa chữa" }
                );

            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Fullname = "Nguyễn Đình Huynh", Address = "Thường tín", Email = "HuynhNDHE130390@fpt.edu.vn", PhoneNumber = "0387706669", Bio = "Đẹp trai", RoleId = 1, Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", Dob = DateTime.Now, RatingPoint = 5, AccountID = 1 },
                new User() { Id = 2, Fullname = "Phùng Đức Linh", Address = "Chung cư Tứ Hiệp Plaza, Tứ Hiệp, Ha Nội, Hà Nội", Email = "linhpdhe130432@fpt.edu.vn", PhoneNumber = "0962515234", Bio = "lorem lipsom", RoleId = 2, Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", Dob = DateTime.Now, RatingPoint = 7, AccountID = 2 },
                new User() { Id = 3, Fullname = "Bùi Anh Dũng", Address = "Hà Nội", Email = "dungbahe130372@fpt.edu.vn", PhoneNumber = "0832229283", Bio = "lorem lipsom", RoleId = 3, Image = ".\\booking-cyber-backend\\CyberBook_API\\CyberBook_API\\CyberBook_API\\Content\\Image\\hoangthuong01.PNG", Dob = DateTime.Now, RatingPoint = 7, AccountID = 3 }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, CyberId = 1, RoomName = "Phòng thường", RoomPosition = "tầng 1", RoomType = 1, MaxX =10, MaxY=10, PriceRoom=10000 },
                new Room() { Id = 2, CyberId = 2, RoomName = "Phòng cao cấp", RoomPosition = "tầng 2", RoomType = 2, MaxX = 10, MaxY = 10, PriceRoom = 15000 },
                new Room() { Id = 3, CyberId = 3, RoomName = "Phòng chuyên nghiệp", RoomPosition = "tầng 3", RoomType = 3, MaxX = 10, MaxY = 10, PriceRoom = 17000 }
                );

            OnModelCreatingPartial(modelBuilder);
        }



        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

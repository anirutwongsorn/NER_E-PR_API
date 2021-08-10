using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ner_api.Entities;

#nullable disable

namespace ner_api.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TableDailyIssueMain> TableDailyIssueMains { get; set; }
        public virtual DbSet<TableDailyKeeping> TableDailyKeepings { get; set; }
        public virtual DbSet<TableDailyProduct> TableDailyProducts { get; set; }
        public virtual DbSet<TableDailyStockCard> TableDailyStockCards { get; set; }
        public virtual DbSet<TableDailyStockMain> TableDailyStockMains { get; set; }
        public virtual DbSet<TableHrCvMain> TableHrCvMains { get; set; }
        public virtual DbSet<TableHrLeave> TableHrLeaves { get; set; }
        public virtual DbSet<TableHrNotify> TableHrNotifies { get; set; }
        public virtual DbSet<TableItRequestDomain> TableItRequestDomains { get; set; }
        public virtual DbSet<TableVehDriver> TableVehDrivers { get; set; }
        public virtual DbSet<TableVehOrder> TableVehOrders { get; set; }
        public virtual DbSet<TableVehTruckKind> TableVehTruckKinds { get; set; }
        public virtual DbSet<TableVehVehicle> TableVehVehicles { get; set; }
        public virtual DbSet<TbBdgList> TbBdgLists { get; set; }
        public virtual DbSet<TbBdgRequest> TbBdgRequests { get; set; }
        public virtual DbSet<TbBdgRequestMore> TbBdgRequestMores { get; set; }
        public virtual DbSet<TbBdgSetting> TbBdgSettings { get; set; }
        public virtual DbSet<TbBdgType> TbBdgTypes { get; set; }
        public virtual DbSet<TbBdgUsage> TbBdgUsages { get; set; }
        public virtual DbSet<TbBdgWaitForApprove> TbBdgWaitForApproves { get; set; }
        public virtual DbSet<TbCompany> TbCompanies { get; set; }
        public virtual DbSet<TbDepartment> TbDepartments { get; set; }
        public virtual DbSet<TbMenuList> TbMenuLists { get; set; }
        public virtual DbSet<TbOpinion> TbOpinions { get; set; }
        public virtual DbSet<TbPrMain> TbPrMains { get; set; }
        public virtual DbSet<TbPrMainDetail> TbPrMainDetails { get; set; }
        public virtual DbSet<TbPrTimeSetting> TbPrTimeSettings { get; set; }
        public virtual DbSet<TbPurPcode> TbPurPcodes { get; set; }
        public virtual DbSet<TbUser> TbUsers { get; set; }
        public virtual DbSet<TbUsersLogin> TbUsersLogins { get; set; }
        public virtual DbSet<TbUsersMenuAccess> TbUsersMenuAccesses { get; set; }
        public virtual DbSet<ViewPdBalance> ViewPdBalances { get; set; }
        public virtual DbSet<ViewPr> ViewPrs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<TableDailyIssueMain>(entity =>
            {
                entity.HasKey(e => e.IssueItems);

                entity.ToTable("Table_DailyIssueMain");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IssueDetail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableDailyKeeping>(entity =>
            {
                entity.HasKey(e => e.KeepId);

                entity.ToTable("Table_DailyKeeping");

                entity.Property(e => e.KeepId).HasColumnName("KeepID");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Keeping)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Multiplier).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableDailyProduct>(entity =>
            {
                entity.HasKey(e => e.Pcode);

                entity.ToTable("Table_DailyProduct");

                entity.Property(e => e.Pcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Pname)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableDailyStockCard>(entity =>
            {
                entity.HasKey(e => e.StkId);

                entity.ToTable("Table_DailyStockCard");

                entity.Property(e => e.StkId).HasColumnName("StkID");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.IssueQty).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.OlderQty).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Pcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RetrieveQty).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.RptId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RptID");

                entity.HasOne(d => d.PcodeNavigation)
                    .WithMany(p => p.TableDailyStockCards)
                    .HasForeignKey(d => d.Pcode)
                    .HasConstraintName("FK_Table_DailyStockCard_Table_DailyProduct");

                entity.HasOne(d => d.Rpt)
                    .WithMany(p => p.TableDailyStockCards)
                    .HasForeignKey(d => d.RptId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Table_DailyStockCard_Table_DailyStockMain");
            });

            modelBuilder.Entity<TableDailyStockMain>(entity =>
            {
                entity.HasKey(e => e.RptId);

                entity.ToTable("Table_DailyStockMain");

                entity.Property(e => e.RptId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RptID");

                entity.Property(e => e.CheckedDate).HasColumnType("date");

                entity.Property(e => e.CheckedName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReportDate).HasColumnType("date");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<TableHrCvMain>(entity =>
            {
                entity.HasKey(e => e.CvId);

                entity.ToTable("Table_HR_CV_Main");

                entity.Property(e => e.CvId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CvID");

                entity.Property(e => e.Checker)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DateRequest)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Detail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Room)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeEnd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStart)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<TableHrLeave>(entity =>
            {
                entity.HasKey(e => e.DocNo);

                entity.ToTable("Table_HR_Leave");

                entity.Property(e => e.DocNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateLeaveEnd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateLeaveFrom)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateNow)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Qty).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Reason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeLeave)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<TableHrNotify>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("Table_HR_Notify");

                entity.Property(e => e.Item).HasColumnName("item");

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Date_");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("fullName");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableItRequestDomain>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Table_IT_RequestDomain");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.CheckedName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateRequest)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Domain)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FullNameEn)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FullNameEN");

                entity.Property(e => e.FullNameTh)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("FullNameTH");

                entity.Property(e => e.Pass)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.UserPosition)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableVehDriver>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Table_VEH_Drivers");

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableVehOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Table_VEH_Order");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.Checker)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateApprove)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateNow)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Driver)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Position)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Position_");

                entity.Property(e => e.Priority)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reason)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.Vehicle)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableVehTruckKind>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("Table_VEH_TruckKind");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.VehType)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TableVehVehicle>(entity =>
            {
                entity.HasKey(e => e.VehId);

                entity.ToTable("Table_VEH_Vehicle");

                entity.Property(e => e.VehId).HasColumnName("VehID");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Vehicle)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbBdgList>(entity =>
            {
                entity.HasKey(e => e.BdgCode);

                entity.ToTable("Tb_BdgList");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BdgDetail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbBdgRequest>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_BdgRequest");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateRequest)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MoneyRequest).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RequestID");

                entity.Property(e => e.Yearly)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbBdgRequestMore>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_BdgRequestMore");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateRequest)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MoneyRequestMore).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RequestID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<TbBdgSetting>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_BdgSetting");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.TitleMessage)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Yearly)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbBdgType>(entity =>
            {
                entity.HasKey(e => e.TypeCode);

                entity.ToTable("Tb_BdgType");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BdgType)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");
            });

            modelBuilder.Entity<TbBdgUsage>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_BdgUsage");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DatePaid)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateRecord)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Paid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ref)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RequestID");
            });

            modelBuilder.Entity<TbBdgWaitForApprove>(entity =>
            {
                entity.ToTable("Tb_BdgWaitForApprove");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApproveName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateApprove)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateSend)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RequestID");

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<TbCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyCode);

                entity.ToTable("Tb_Company");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TaxId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("TaxID");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbDepartment>(entity =>
            {
                entity.HasKey(e => e.DeptCode);

                entity.ToTable("Tb_Department");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DeptSymbol)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<TbMenuList>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("Tb_MenuList");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.MenuGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MenuLink)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.MenuName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TbOpinion>(entity =>
            {
                entity.ToTable("Tb_Opinions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("date")
                    .HasColumnName("created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Isactive)
                    .HasColumnName("isactive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(350)
                    .HasColumnName("note");

                entity.Property(e => e.OrderId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OrderID");

                entity.Property(e => e.Scored).HasColumnName("scored");
            });

            modelBuilder.Entity<TbPrMain>(entity =>
            {
                entity.HasKey(e => e.PrNo);

                entity.ToTable("Tb_PrMain");

                entity.Property(e => e.PrNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AcknDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AcknName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveRemark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ApproveTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BudgetsStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateNeed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateNow)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.IsAckn).HasDefaultValueSql("((0))");

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.OperateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OperatelName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.OperatelRemark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PrType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TimeCreated)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbPrMains)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Tb_PrMain_Tb_Users");
            });

            modelBuilder.Entity<TbPrMainDetail>(entity =>
            {
                entity.HasKey(e => e.Item);

                entity.ToTable("Tb_PrMainDetail");

                entity.Property(e => e.ApproveStatus).HasColumnName("approveStatus");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ItemsStatus).HasColumnName("itemsStatus");

                entity.Property(e => e.Keeping)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Objective)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PdDetail)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Po)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PO");

                entity.Property(e => e.Podate)
                    .HasColumnType("date")
                    .HasColumnName("POdate");

                entity.Property(e => e.PrNo)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Qty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PrNoNavigation)
                    .WithMany(p => p.TbPrMainDetails)
                    .HasForeignKey(d => d.PrNo)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Tb_PrMainDetail_Tb_PrMain");
            });

            modelBuilder.Entity<TbPrTimeSetting>(entity =>
            {
                entity.ToTable("Tb_PrTimeSetting");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.InitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbPurPcode>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_PurPcode");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.PDetail)
                    .HasMaxLength(350)
                    .HasColumnName("pDetail");

                entity.Property(e => e.Pcode)
                    .HasMaxLength(50)
                    .HasColumnName("pcode");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Tb_Users");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EmailBoss)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("emailBoss");

                entity.Property(e => e.EncryptPass)
                    .HasMaxLength(250)
                    .HasColumnName("encryptPass");

                entity.Property(e => e.FullName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Items)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("items");

                entity.Property(e => e.Pass)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.Position)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Position_");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<TbUsersLogin>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_UsersLogin");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.DeptCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<TbUsersMenuAccess>(entity =>
            {
                entity.HasKey(e => e.Items);

                entity.ToTable("Tb_UsersMenuAccess");

                entity.Property(e => e.Items).HasColumnName("items");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<ViewPdBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_pdBalance");

                entity.Property(e => e.Expr1).HasColumnType("decimal(38, 2)");

                entity.Property(e => e.Pcode)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewPr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_PR");

                entity.Property(e => e.BdgCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeptSymbol)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Division)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Keeping)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Objective)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PdCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PdDetail)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Po)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PO");

                entity.Property(e => e.PrNo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Qty)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

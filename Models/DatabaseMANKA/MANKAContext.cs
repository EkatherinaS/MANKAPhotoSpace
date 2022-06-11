using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PracticalTraining.Models.DatabaseMANKA
{
    public partial class MANKAContext : DbContext
    {
        public MANKAContext()
        {
        }

        public MANKAContext(DbContextOptions<MANKAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaseInfo> BaseInfo { get; set; }
        public virtual DbSet<BuildingInfo> BuildingInfo { get; set; }
        public virtual DbSet<CityInfo> CityInfo { get; set; }
        public virtual DbSet<FinalReservations> FinalReservations { get; set; }
        public virtual DbSet<GuestInfo> GuestInfo { get; set; }
        public virtual DbSet<PaymentInfo> PaymentInfo { get; set; }
        public virtual DbSet<PlaceInfo> PlaceInfo { get; set; }
        public virtual DbSet<ReservationInfo> ReservationInfo { get; set; }
        public virtual DbSet<ServiceInfo> ServiceInfo { get; set; }
        public virtual DbSet<StaffInfo> StaffInfo { get; set; }
        public virtual DbSet<StreetInfo> StreetInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
            }*/
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MANKA;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseInfo>(entity =>
            {
                entity.HasKey(e => e.OwnerLogin)
                    .HasName("PK_BaseInfo_1");

                entity.Property(e => e.OwnerLogin)
                    .HasColumnName("ownerLogin")
                    .HasMaxLength(17);

                entity.Property(e => e.BasicText)
                    .IsRequired()
                    .HasColumnName("basicText")
                    .IsUnicode(false);

                entity.Property(e => e.OwnerPassword)
                    .IsRequired()
                    .HasColumnName("ownerPassword")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BuildingInfo>(entity =>
            {
                entity.HasKey(e => e.AddressCode);

                entity.Property(e => e.AddressCode).HasColumnName("addressCode");

                entity.Property(e => e.BuildingNumber)
                    .IsRequired()
                    .HasColumnName("buildingNumber")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StreetCode).HasColumnName("streetCode");

                entity.HasOne(d => d.StreetCodeNavigation)
                    .WithMany(p => p.BuildingInfo)
                    .HasForeignKey(d => d.StreetCode)
                    .HasConstraintName("FK_BuildingInfo_StreetInfo");
            });

            modelBuilder.Entity<CityInfo>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.Property(e => e.CityCode).HasColumnName("cityCode");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("cityName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FinalReservations>(entity =>
            {
                entity.HasKey(e => e.ReservationCode);

                entity.Property(e => e.ReservationCode)
                    .HasColumnName("reservationCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.StaffPhone)
                    .HasColumnName("staffPhone")
                    .HasMaxLength(17);

                entity.HasOne(d => d.ReservationCodeNavigation)
                    .WithOne(p => p.FinalReservations)
                    .HasForeignKey<FinalReservations>(d => d.ReservationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinalReservations_ReservationInfo");

                entity.HasOne(d => d.StaffPhoneNavigation)
                    .WithMany(p => p.FinalReservations)
                    .HasForeignKey(d => d.StaffPhone)
                    .HasConstraintName("FK_FinalReservations_StaffInfo");
            });

            modelBuilder.Entity<GuestInfo>(entity =>
            {
                entity.HasKey(e => e.GuestPhone)
                    .HasName("PK_GuestInfo_1");

                entity.Property(e => e.GuestPhone)
                    .HasColumnName("guestPhone")
                    .HasMaxLength(17);

                entity.Property(e => e.GuestEmail)
                    .HasColumnName("guestEmail")
                    .IsUnicode(false);

                entity.Property(e => e.GuestFamilyName)
                    .IsRequired()
                    .HasColumnName("guestFamilyName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuestName)
                    .IsRequired()
                    .HasColumnName("guestName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GuestPassword)
                    .IsRequired()
                    .HasColumnName("guestPassword")
                    .HasMaxLength(50);

                entity.Property(e => e.GuestSocialNetwork)
                    .HasColumnName("guestSocialNetwork")
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnName("registrationDate")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<PaymentInfo>(entity =>
            {
                entity.HasKey(e => e.PaymentCode);

                entity.Property(e => e.PaymentCode).HasColumnName("paymentCode");

                entity.Property(e => e.DateExploration)
                    .HasColumnName("dateExploration")
                    .HasColumnType("date");

                entity.Property(e => e.PaymentName)
                    .IsRequired()
                    .HasColumnName("paymentName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlaceInfo>(entity =>
            {
                entity.HasKey(e => e.PlaceCode);

                entity.Property(e => e.PlaceCode).HasColumnName("placeCode");

                entity.Property(e => e.AddressCode).HasColumnName("addressCode");

                entity.Property(e => e.MaxPeopleNumber).HasColumnName("maxPeopleNumber");

                entity.Property(e => e.PlaceCloseDate)
                    .HasColumnName("placeCloseDate")
                    .HasColumnType("date");

                entity.Property(e => e.PlaceName)
                    .IsRequired()
                    .HasColumnName("placeName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddressCodeNavigation)
                    .WithMany(p => p.PlaceInfo)
                    .HasForeignKey(d => d.AddressCode)
                    .HasConstraintName("FK_PlaceInfo_BuildingInfo");
            });

            modelBuilder.Entity<ReservationInfo>(entity =>
            {
                entity.HasKey(e => e.ReservationCode);

                entity.Property(e => e.ReservationCode).HasColumnName("reservationCode");

                entity.Property(e => e.CancelDate)
                    .HasColumnName("cancelDate")
                    .HasColumnType("date");

                entity.Property(e => e.FinishTime)
                    .HasColumnName("finishTime")
                    .HasColumnType("time(0)");

                entity.Property(e => e.GuestPhone)
                    .IsRequired()
                    .HasColumnName("guestPhone")
                    .HasMaxLength(17);

                entity.Property(e => e.PaymentCode).HasColumnName("paymentCode");

                entity.Property(e => e.PeopleNumber).HasColumnName("peopleNumber");

                entity.Property(e => e.PlaceCode).HasColumnName("placeCode");

                entity.Property(e => e.ReservationComment)
                    .HasColumnName("reservationComment")
                    .IsUnicode(false);

                entity.Property(e => e.ReservationDate)
                    .HasColumnName("reservationDate")
                    .HasColumnType("date");

                entity.Property(e => e.ServiceCode).HasColumnName("serviceCode");

                entity.Property(e => e.StartTime)
                    .HasColumnName("startTime")
                    .HasColumnType("time(0)");

                entity.HasOne(d => d.GuestPhoneNavigation)
                    .WithMany(p => p.ReservationInfo)
                    .HasForeignKey(d => d.GuestPhone)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationInfo_GuestInfo");

                entity.HasOne(d => d.PaymentCodeNavigation)
                    .WithMany(p => p.ReservationInfo)
                    .HasForeignKey(d => d.PaymentCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationInfo_PaymentInfo");

                entity.HasOne(d => d.PlaceCodeNavigation)
                    .WithMany(p => p.ReservationInfo)
                    .HasForeignKey(d => d.PlaceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationInfo_PlaceInfo");

                entity.HasOne(d => d.ServiceCodeNavigation)
                    .WithMany(p => p.ReservationInfo)
                    .HasForeignKey(d => d.ServiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationInfo_Service");
            });

            modelBuilder.Entity<ServiceInfo>(entity =>
            {
                entity.HasKey(e => e.ServiceCode)
                    .HasName("PK_Service");

                entity.Property(e => e.ServiceCode).HasColumnName("serviceCode");

                entity.Property(e => e.DateExploration)
                    .HasColumnName("dateExploration")
                    .HasColumnType("date");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasColumnName("serviceName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServicePrice)
                    .HasColumnName("servicePrice")
                    .HasColumnType("smallmoney");
            });

            modelBuilder.Entity<StaffInfo>(entity =>
            {
                entity.HasKey(e => e.StaffPhone)
                    .HasName("PK_StaffInfo_1");

                entity.Property(e => e.StaffPhone)
                    .HasColumnName("staffPhone")
                    .HasMaxLength(17);

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasColumnName("passportNumber")
                    .HasMaxLength(6)
                    .IsFixedLength();

                entity.Property(e => e.PassportSeries)
                    .IsRequired()
                    .HasColumnName("passportSeries")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.PaymentPerHour)
                    .HasColumnName("paymentPerHour")
                    .HasColumnType("smallmoney");

                entity.Property(e => e.ResignationDate)
                    .HasColumnName("resignationDate")
                    .HasColumnType("date");

                entity.Property(e => e.Snils)
                    .IsRequired()
                    .HasColumnName("SNILS")
                    .HasMaxLength(11)
                    .IsFixedLength();

                entity.Property(e => e.StaffFamilyName)
                    .IsRequired()
                    .HasColumnName("staffFamilyName")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffName)
                    .IsRequired()
                    .HasColumnName("staffName")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffPassword)
                    .IsRequired()
                    .HasColumnName("staffPassword")
                    .HasMaxLength(50);

                entity.Property(e => e.StaffSurname)
                    .HasColumnName("staffSurname")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StreetInfo>(entity =>
            {
                entity.HasKey(e => e.StreetCode);

                entity.Property(e => e.StreetCode).HasColumnName("streetCode");

                entity.Property(e => e.CityCode).HasColumnName("cityCode");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasColumnName("streetName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CityCodeNavigation)
                    .WithMany(p => p.StreetInfo)
                    .HasForeignKey(d => d.CityCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_StreetInfo_CityInfo2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

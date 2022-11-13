using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SPNewApi2.Models
{
    public partial class FinalPlugDBContext : DbContext
    {
        public FinalPlugDBContext()
        {
        }

        public FinalPlugDBContext(DbContextOptions<FinalPlugDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Merchant> Merchants { get; set; } = null!;
        public virtual DbSet<Quotation> Quotations { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SQL8001.site4now.net;Initial Catalog=db_a8d94e_frenkie;User Id=db_a8d94e_frenkie_admin;Password=Frenkie21"
);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.ToTable("Booking");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.BookDate)
                    .HasColumnType("date")
                    .HasColumnName("book_date");

                entity.Property(e => e.BookMessage)
                    .IsUnicode(false)
                    .HasColumnName("book_message");

                entity.Property(e => e.BookStatus)
                    .IsUnicode(false)
                    .HasColumnName("book_status");

                entity.Property(e => e.BookTime).HasColumnName("book_time");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Merch)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.MerchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Merchant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Client");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Client");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserAddress)
                    .IsUnicode(false)
                    .HasColumnName("user_address");

                entity.Property(e => e.UserCity)
                    .IsUnicode(false)
                    .HasColumnName("user_city");

                entity.Property(e => e.UserContactdetails)
                    .IsUnicode(false)
                    .HasColumnName("user_contactdetails");

                entity.Property(e => e.UserEmail)
                    .IsUnicode(false)
                    .HasColumnName("user_email");

                entity.Property(e => e.UserName)
                    .IsUnicode(false)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .IsUnicode(false)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserProvince)
                    .IsUnicode(false)
                    .HasColumnName("user_province");

                entity.Property(e => e.UserStatus)
                    .IsUnicode(false)
                    .HasColumnName("user_status");

                entity.Property(e => e.UserSurname)
                    .IsUnicode(false)
                    .HasColumnName("user_surname");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.JobDateend)
                    .HasColumnType("date")
                    .HasColumnName("job_dateend");

                entity.Property(e => e.JobDatestart)
                    .HasColumnType("date")
                    .HasColumnName("job_datestart");

                entity.Property(e => e.JobStatus)
                    .IsUnicode(false)
                    .HasColumnName("job_status");

                entity.Property(e => e.JobTimeend).HasColumnName("job_timeend");

                entity.Property(e => e.JobTimestart).HasColumnName("job_timestart");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Booking");

                entity.HasOne(d => d.Merch)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.MerchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Merchant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Job_Client");
            });

            modelBuilder.Entity<Merchant>(entity =>
            {
                entity.HasKey(e => e.MerchId);

                entity.ToTable("Merchant");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.MerchAddress)
                    .IsUnicode(false)
                    .HasColumnName("merch_address");

                entity.Property(e => e.MerchCity)
                    .IsUnicode(false)
                    .HasColumnName("merch_city");

                entity.Property(e => e.MerchContactdetails)
                    .IsUnicode(false)
                    .HasColumnName("merch_contactdetails");

                entity.Property(e => e.MerchEmail)
                    .IsUnicode(false)
                    .HasColumnName("merch_email");

                entity.Property(e => e.MerchFile)
                    .IsUnicode(false)
                    .HasColumnName("merch_file");

                entity.Property(e => e.MerchIdnumber)
                    .IsUnicode(false)
                    .HasColumnName("merch_idnumber");

                entity.Property(e => e.MerchName)
                    .IsUnicode(false)
                    .HasColumnName("merch_name");

                entity.Property(e => e.MerchPassword)
                    .IsUnicode(false)
                    .HasColumnName("merch_password");

                entity.Property(e => e.MerchPictures1)
                    .IsUnicode(false)
                    .HasColumnName("merch_pictures1");

                entity.Property(e => e.MerchPictures2)
                    .IsUnicode(false)
                    .HasColumnName("merch_pictures2");

                entity.Property(e => e.MerchPictures3)
                    .IsUnicode(false)
                    .HasColumnName("merch_pictures3");

                entity.Property(e => e.MerchProfilepicture)
                    .IsUnicode(false)
                    .HasColumnName("merch_profilepicture");

                entity.Property(e => e.MerchProvince)
                    .IsUnicode(false)
                    .HasColumnName("merch_province");

                entity.Property(e => e.MerchStatus)
                    .IsUnicode(false)
                    .HasColumnName("merch_status");

                entity.Property(e => e.MerchSurname)
                    .IsUnicode(false)
                    .HasColumnName("merch_surname");

                entity.Property(e => e.MerchTaxnumber)
                    .IsUnicode(false)
                    .HasColumnName("merch_taxnumber");

                entity.Property(e => e.MerchType)
                    .IsUnicode(false)
                    .HasColumnName("merch_type");

                entity.Property(e => e.MerchVerify)
                    .IsUnicode(false)
                    .HasColumnName("merch_verify");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.HasKey(e => e.QuotId);

                entity.ToTable("Quotation");

                entity.Property(e => e.QuotId).HasColumnName("quot_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.QuotAmount)
                    .IsUnicode(false)
                    .HasColumnName("quot_amount");

                entity.Property(e => e.QuotDescription)
                    .IsUnicode(false)
                    .HasColumnName("quot_description");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quotation_Booking");

                entity.HasOne(d => d.Merch)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.MerchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quotation_Merchant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quotation_Client");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.MerchId).HasColumnName("merch_id");

                entity.Property(e => e.ReviewMessage)
                    .IsUnicode(false)
                    .HasColumnName("review_message");

                entity.Property(e => e.ReviewRating)
                    .IsUnicode(false)
                    .HasColumnName("review_rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Job");

                entity.HasOne(d => d.Merch)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MerchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Merchant");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Review_Client");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

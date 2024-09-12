using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using NewsAgregator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintStatus> ComplaintStatuses { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<NewsStatus> NewsStatuses { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePolicy> RolePolicies { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<NotificationMessage> NotificationMessages { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WeatherStatus> WeatherStatuses { get; set; }
        public DbSet<Source> Sources { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            modelBuilder.Entity<Account>()
                    .HasIndex(a => a.Login)
                    .IsUnique();

            modelBuilder.Entity<NotificationMessage>()
                      .HasOne(nm => nm.User)
                      .WithMany(nm => nm.RecipientUsers)
                      .HasForeignKey(nm => nm.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NotificationMessage>()
                      .HasOne(nm => nm.Administrator)
                      .WithMany(nm => nm.SenderAdministrators)
                      .HasForeignKey(nm => nm.AdministratorId)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                       .HasOne(c => c.User)
                       .WithMany(c => c.UserComplaints)
                       .HasForeignKey(c => c.UserId)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Complaint>()
                       .HasOne(c => c.Administrator)
                       .WithMany(c => c.AdministratorComplaints)
                       .HasForeignKey(c => c.AdministratorId)
                       .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Weather>()
                        .HasOne(w => w.WeatherStatusMorning)
                        .WithMany(w => w.MorningWeathers)
                        .HasForeignKey(w => w.WeatherStatusMorningId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Weather>()
                        .HasOne(w => w.WeatherStatusDay)
                        .WithMany(w => w.DayWeathers)
                        .HasForeignKey(w => w.WeatherStatusDayId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Weather>()
                        .HasOne(w => w.WeatherStatusEvening)
                        .WithMany(w => w.EveningWeathers)
                        .HasForeignKey(w => w.WeatherStatusEveningId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Weather>()
                        .HasOne(w => w.WeatherStatusNight)
                        .WithMany(w => w.NightWeathers)
                        .HasForeignKey(w => w.WeatherStatusNightId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Weather>()
                        .HasOne(w => w.WeatherStatusCommon)
                        .WithMany(w => w.CommonWeathers)
                        .HasForeignKey(w => w.WeatherStatusCommonId)
                        .OnDelete(DeleteBehavior.Restrict);

            // DeleteBehavior Cascade -> Restrict
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
    }
}

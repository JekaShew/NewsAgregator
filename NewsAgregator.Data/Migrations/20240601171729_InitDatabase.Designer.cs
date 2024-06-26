﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewsAgregator.Data;

#nullable disable

namespace NewsAgregator.Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240601171729_InitDatabase")]
    partial class InitDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NewsAgregator.Data.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("DesiredNewsRating")
                        .HasColumnType("real");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountStatusId");

                    b.HasIndex("RoleId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.AccountStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccountStatuses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NewsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("NewsId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Complaint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdministratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComplaintStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComplaintTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("NewsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("CommentId");

                    b.HasIndex("ComplaintStatusId");

                    b.HasIndex("ComplaintTypeId");

                    b.HasIndex("NewsId");

                    b.HasIndex("UserId");

                    b.ToTable("Complaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.ComplaintStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComplaintStatuses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.ComplaintType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ComplaintTypes");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.News", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("NewsStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("PositiveRating")
                        .HasColumnType("real");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsStatusId");

                    b.ToTable("Newses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.NewsStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NewsStatuses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.NotificationMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AdministratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("UserId");

                    b.ToTable("NotificationMessages");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Policy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.RolePolicy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PolicyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PolicyId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePolicies");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Humidity")
                        .HasColumnType("int");

                    b.Property<string>("Percipittaion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Pressure")
                        .HasColumnType("int");

                    b.Property<int?>("TemperatureCommon")
                        .HasColumnType("int");

                    b.Property<int?>("TemperatureDay")
                        .HasColumnType("int");

                    b.Property<int?>("TemperatureEvening")
                        .HasColumnType("int");

                    b.Property<int?>("TemperatureMorning")
                        .HasColumnType("int");

                    b.Property<int?>("TemperatureNight")
                        .HasColumnType("int");

                    b.Property<Guid?>("WeatherStatusCommonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WeatherStatusDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WeatherStatusEveningId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WeatherStatusMorningId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WeatherStatusNightId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float?>("Wind")
                        .HasColumnType("real");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("WeatherStatusCommonId");

                    b.HasIndex("WeatherStatusDayId");

                    b.HasIndex("WeatherStatusEveningId");

                    b.HasIndex("WeatherStatusMorningId");

                    b.HasIndex("WeatherStatusNightId");

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.WeatherStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WeatherStatuses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Account", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.AccountStatus", "AccountStatus")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountStatusId");

                    b.HasOne("NewsAgregator.Data.Models.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId");

                    b.Navigation("AccountStatus");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Comment", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.Account", "Account")
                        .WithMany("Comments")
                        .HasForeignKey("AccountId");

                    b.HasOne("NewsAgregator.Data.Models.News", "News")
                        .WithMany("Comments")
                        .HasForeignKey("NewsId");

                    b.Navigation("Account");

                    b.Navigation("News");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Complaint", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.Account", "Administrator")
                        .WithMany("AdministratorComplaints")
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.Comment", "Comment")
                        .WithMany("Complaints")
                        .HasForeignKey("CommentId");

                    b.HasOne("NewsAgregator.Data.Models.ComplaintStatus", "ComplaintStatus")
                        .WithMany("Complaints")
                        .HasForeignKey("ComplaintStatusId");

                    b.HasOne("NewsAgregator.Data.Models.ComplaintType", "ComplaintType")
                        .WithMany("Complaints")
                        .HasForeignKey("ComplaintTypeId");

                    b.HasOne("NewsAgregator.Data.Models.News", "News")
                        .WithMany("Complaints")
                        .HasForeignKey("NewsId");

                    b.HasOne("NewsAgregator.Data.Models.Account", "User")
                        .WithMany("UserComplaints")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Administrator");

                    b.Navigation("Comment");

                    b.Navigation("ComplaintStatus");

                    b.Navigation("ComplaintType");

                    b.Navigation("News");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.News", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.NewsStatus", "NewsStatus")
                        .WithMany("Newses")
                        .HasForeignKey("NewsStatusId");

                    b.Navigation("NewsStatus");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.NotificationMessage", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.Account", "Administrator")
                        .WithMany("SenderAdministrators")
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.Account", "User")
                        .WithMany("RecipientUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Administrator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.RolePolicy", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.Policy", "Policy")
                        .WithMany("RolePolicies")
                        .HasForeignKey("PolicyId");

                    b.HasOne("NewsAgregator.Data.Models.Role", "Role")
                        .WithMany("RolePolicies")
                        .HasForeignKey("RoleId");

                    b.Navigation("Policy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Weather", b =>
                {
                    b.HasOne("NewsAgregator.Data.Models.WeatherStatus", "WeatherStatusCommon")
                        .WithMany("CommonWeathers")
                        .HasForeignKey("WeatherStatusCommonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.WeatherStatus", "WeatherStatusDay")
                        .WithMany("DayWeathers")
                        .HasForeignKey("WeatherStatusDayId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.WeatherStatus", "WeatherStatusEvening")
                        .WithMany("EveningWeathers")
                        .HasForeignKey("WeatherStatusEveningId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.WeatherStatus", "WeatherStatusMorning")
                        .WithMany("MorningWeathers")
                        .HasForeignKey("WeatherStatusMorningId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NewsAgregator.Data.Models.WeatherStatus", "WeatherStatusNight")
                        .WithMany("NightWeathers")
                        .HasForeignKey("WeatherStatusNightId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("WeatherStatusCommon");

                    b.Navigation("WeatherStatusDay");

                    b.Navigation("WeatherStatusEvening");

                    b.Navigation("WeatherStatusMorning");

                    b.Navigation("WeatherStatusNight");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Account", b =>
                {
                    b.Navigation("AdministratorComplaints");

                    b.Navigation("Comments");

                    b.Navigation("RecipientUsers");

                    b.Navigation("SenderAdministrators");

                    b.Navigation("UserComplaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.AccountStatus", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Comment", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.ComplaintStatus", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.ComplaintType", b =>
                {
                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.News", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Complaints");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.NewsStatus", b =>
                {
                    b.Navigation("Newses");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Policy", b =>
                {
                    b.Navigation("RolePolicies");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.Role", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("RolePolicies");
                });

            modelBuilder.Entity("NewsAgregator.Data.Models.WeatherStatus", b =>
                {
                    b.Navigation("CommonWeathers");

                    b.Navigation("DayWeathers");

                    b.Navigation("EveningWeathers");

                    b.Navigation("MorningWeathers");

                    b.Navigation("NightWeathers");
                });
#pragma warning restore 612, 618
        }
    }
}

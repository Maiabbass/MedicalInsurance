﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240618125503_InitialCreat")]
    partial class InitialCreat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("api.Entities.AgeSegments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EnduranceRatio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FromYear")
                        .HasColumnType("int");

                    b.Property<decimal>("TheAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ToYear")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgeSegments");
                });

            modelBuilder.Entity("api.Entities.AnnualData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EngineereId")
                        .HasColumnType("int");

                    b.Property<int?>("EngineeringUnitsId")
                        .HasColumnType("int");

                    b.Property<decimal>("ExAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("HisDic")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PayMethodId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WorkPlaceId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineereId");

                    b.HasIndex("EngineeringUnitsId");

                    b.HasIndex("PayMethodId");

                    b.HasIndex("WorkPlaceId");

                    b.ToTable("AnnualDatas");
                });

            modelBuilder.Entity("api.Entities.AnnualDataDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("AnnualDataId")
                        .HasColumnType("int");

                    b.Property<bool>("IsEngineer")
                        .HasColumnType("bit");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnnualDataId");

                    b.HasIndex("PersonId");

                    b.ToTable("AnnualDataDetails");
                });

            modelBuilder.Entity("api.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("api.Entities.Claims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("ApprovedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Company_fees")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EnduranceRatio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EnsuranceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExitDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LoginDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgicalProceduresId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Trust")
                        .HasColumnType("bit");

                    b.Property<decimal>("non_Add")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("non_AddForPerson")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("PersonId");

                    b.HasIndex("SurgicalProceduresId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("api.Entities.Engineere", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("EngNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("SubNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkPlaceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("StatusId");

                    b.HasIndex("WorkPlaceId");

                    b.ToTable("Engineeres");
                });

            modelBuilder.Entity("api.Entities.EngineeringeDepar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EngineeringeDepars");
                });

            modelBuilder.Entity("api.Entities.EngineeringUnits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Emailpresident")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Namepresident")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phonepresident")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EngineeringUnits");
                });

            modelBuilder.Entity("api.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("api.Entities.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("Inside")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("api.Entities.PayMethod", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("NameMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PayMethods");
                });

            modelBuilder.Entity("api.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Affiliate")
                        .HasColumnType("bit");

                    b.Property<bool>("Beneficiary")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineereId")
                        .HasColumnType("int");

                    b.Property<string>("EnsuranceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Subscrib")
                        .HasColumnType("bit");

                    b.Property<int?>("statusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineereId");

                    b.HasIndex("GenderId");

                    b.HasIndex("statusId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("api.Entities.Relation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EngineereId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("RelationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineereId");

                    b.HasIndex("PersonId");

                    b.HasIndex("RelationTypeId");

                    b.ToTable("Relations");
                });

            modelBuilder.Entity("api.Entities.RelationType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RelationTypes");
                });

            modelBuilder.Entity("api.Entities.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EngineeringeDeparId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EngineeringeDeparId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("api.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("api.Entities.SurgicalProcedures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SurgicalProcedures");
                });

            modelBuilder.Entity("api.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("api.Entities.WorkPlace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("EngineeringUnitsId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EngineeringUnitsId");

                    b.ToTable("WorkPlaces");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("api.Entities.AnnualData", b =>
                {
                    b.HasOne("api.Entities.Engineere", "Engineere")
                        .WithMany("AnnualDatas")
                        .HasForeignKey("EngineereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.EngineeringUnits", "EngineeringUnits")
                        .WithMany("AnnualDatas")
                        .HasForeignKey("EngineeringUnitsId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("api.Entities.PayMethod", "PayMethod")
                        .WithMany("AnnualDatas")
                        .HasForeignKey("PayMethodId");

                    b.HasOne("api.Entities.WorkPlace", "WorkPlace")
                        .WithMany("AnnualDatas")
                        .HasForeignKey("WorkPlaceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Engineere");

                    b.Navigation("EngineeringUnits");

                    b.Navigation("PayMethod");

                    b.Navigation("WorkPlace");
                });

            modelBuilder.Entity("api.Entities.AnnualDataDetail", b =>
                {
                    b.HasOne("api.Entities.AnnualData", "AnnualData")
                        .WithMany("AnnualDataDetails")
                        .HasForeignKey("AnnualDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Person", "Person")
                        .WithMany("AnnualDataDetails")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AnnualData");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("api.Entities.Claims", b =>
                {
                    b.HasOne("api.Entities.Hospital", "Hospital")
                        .WithMany("Claims")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Person", "Person")
                        .WithMany("Claims")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.SurgicalProcedures", "SurgicalProcedures")
                        .WithMany("Claims")
                        .HasForeignKey("SurgicalProceduresId");

                    b.Navigation("Hospital");

                    b.Navigation("Person");

                    b.Navigation("SurgicalProcedures");
                });

            modelBuilder.Entity("api.Entities.Engineere", b =>
                {
                    b.HasOne("api.Entities.Specialization", "Specialization")
                        .WithMany("Engineeres")
                        .HasForeignKey("SpecializationId");

                    b.HasOne("api.Entities.Status", null)
                        .WithMany("Engineeres")
                        .HasForeignKey("StatusId");

                    b.HasOne("api.Entities.WorkPlace", "WorkPlace")
                        .WithMany("Engineeres")
                        .HasForeignKey("WorkPlaceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Specialization");

                    b.Navigation("WorkPlace");
                });

            modelBuilder.Entity("api.Entities.Hospital", b =>
                {
                    b.HasOne("api.Entities.City", "City")
                        .WithMany("Hospitals")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("api.Entities.Person", b =>
                {
                    b.HasOne("api.Entities.Engineere", "Engineere")
                        .WithMany()
                        .HasForeignKey("EngineereId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Gender", "Gender")
                        .WithMany("Persons")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId");

                    b.Navigation("Engineere");

                    b.Navigation("Gender");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("api.Entities.Relation", b =>
                {
                    b.HasOne("api.Entities.Engineere", "Engineere")
                        .WithMany("Relations")
                        .HasForeignKey("EngineereId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("api.Entities.Person", "Person")
                        .WithMany("Relations")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("api.Entities.RelationType", "RelationType")
                        .WithMany("Relations")
                        .HasForeignKey("RelationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineere");

                    b.Navigation("Person");

                    b.Navigation("RelationType");
                });

            modelBuilder.Entity("api.Entities.Specialization", b =>
                {
                    b.HasOne("api.Entities.EngineeringeDepar", "EngineeringeDepar")
                        .WithMany("Specializations")
                        .HasForeignKey("EngineeringeDeparId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineeringeDepar");
                });

            modelBuilder.Entity("api.Entities.WorkPlace", b =>
                {
                    b.HasOne("api.Entities.EngineeringUnits", "EngineeringUnits")
                        .WithMany("WorkPlaces")
                        .HasForeignKey("EngineeringUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineeringUnits");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("api.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("api.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("api.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Entities.AnnualData", b =>
                {
                    b.Navigation("AnnualDataDetails");
                });

            modelBuilder.Entity("api.Entities.City", b =>
                {
                    b.Navigation("Hospitals");
                });

            modelBuilder.Entity("api.Entities.Engineere", b =>
                {
                    b.Navigation("AnnualDatas");

                    b.Navigation("Relations");
                });

            modelBuilder.Entity("api.Entities.EngineeringeDepar", b =>
                {
                    b.Navigation("Specializations");
                });

            modelBuilder.Entity("api.Entities.EngineeringUnits", b =>
                {
                    b.Navigation("AnnualDatas");

                    b.Navigation("WorkPlaces");
                });

            modelBuilder.Entity("api.Entities.Gender", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("api.Entities.Hospital", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("api.Entities.PayMethod", b =>
                {
                    b.Navigation("AnnualDatas");
                });

            modelBuilder.Entity("api.Entities.Person", b =>
                {
                    b.Navigation("AnnualDataDetails");

                    b.Navigation("Claims");

                    b.Navigation("Relations");
                });

            modelBuilder.Entity("api.Entities.RelationType", b =>
                {
                    b.Navigation("Relations");
                });

            modelBuilder.Entity("api.Entities.Specialization", b =>
                {
                    b.Navigation("Engineeres");
                });

            modelBuilder.Entity("api.Entities.Status", b =>
                {
                    b.Navigation("Engineeres");
                });

            modelBuilder.Entity("api.Entities.SurgicalProcedures", b =>
                {
                    b.Navigation("Claims");
                });

            modelBuilder.Entity("api.Entities.WorkPlace", b =>
                {
                    b.Navigation("AnnualDatas");

                    b.Navigation("Engineeres");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20240521111512_modified9")]
    partial class modified9
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

                    b.Property<int>("PayMethodId")
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

                    b.Property<decimal>("AdditionalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ApprovedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EnduranceRatio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngId")
                        .HasColumnType("int");

                    b.Property<int>("EngineereeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExitDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LoginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurgicalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurgicalType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Trust")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EngineereeId");

                    b.HasIndex("HospitalId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("api.Entities.Engineere", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("EngNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecializationId")
                        .HasColumnType("int");

                    b.Property<string>("SubNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WorkPlaceId")
                        .HasColumnType("int");

                    b.Property<int>("statusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("WorkPlaceId");

                    b.HasIndex("statusId");

                    b.ToTable("Engineeres");
                });

            modelBuilder.Entity("api.Entities.EngineeringUnits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<bool>("Inside")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
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

                    b.Property<string>("EnsuranceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Subscrib")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.HasKey("Id");

                    b.HasIndex("EngineeringUnitsId");

                    b.ToTable("WorkPlaces");
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
                        .HasForeignKey("PayMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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
                    b.HasOne("api.Entities.Engineere", "Engineeree")
                        .WithMany("Claims")
                        .HasForeignKey("EngineereeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Hospital", "Hospital")
                        .WithMany("Claims")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engineeree");

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("api.Entities.Engineere", b =>
                {
                    b.HasOne("api.Entities.Person", "Person")
                        .WithOne("Engineere")
                        .HasForeignKey("api.Entities.Engineere", "Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("api.Entities.Specialization", "Specialization")
                        .WithMany("Engineeres")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.WorkPlace", "WorkPlace")
                        .WithMany("Engineeres")
                        .HasForeignKey("WorkPlaceId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("api.Entities.Status", "Status")
                        .WithMany("Engineeres")
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Specialization");

                    b.Navigation("Status");

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
                    b.HasOne("api.Entities.Gender", "Gender")
                        .WithMany("Persons")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
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

            modelBuilder.Entity("api.Entities.WorkPlace", b =>
                {
                    b.HasOne("api.Entities.EngineeringUnits", "EngineeringUnits")
                        .WithMany("WorkPlaces")
                        .HasForeignKey("EngineeringUnitsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineeringUnits");
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

                    b.Navigation("Claims");

                    b.Navigation("Relations");
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

                    b.Navigation("Engineere");

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

            modelBuilder.Entity("api.Entities.WorkPlace", b =>
                {
                    b.Navigation("AnnualDatas");

                    b.Navigation("Engineeres");
                });
#pragma warning restore 612, 618
        }
    }
}

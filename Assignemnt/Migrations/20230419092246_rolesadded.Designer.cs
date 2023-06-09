﻿// <auto-generated />
using System;
using Assignemnt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignemnt.Migrations
{
    [DbContext(typeof(ITIContext))]
    [Migration("20230419092246_rolesadded")]
    partial class rolesadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignemnt.Models.Course", b =>
                {
                    b.Property<int>("CrsId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CrsId"), 1L, 1);

                    b.Property<string>("CrsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabMinutes")
                        .HasColumnType("int");

                    b.Property<int>("LectHours")
                        .HasColumnType("int");

                    b.HasKey("CrsId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Assignemnt.Models.CourseStudent", b =>
                {
                    b.Property<int>("StdId")
                        .HasColumnType("int");

                    b.Property<int>("CrsId")
                        .HasColumnType("int");

                    b.Property<int>("Degrees")
                        .HasColumnType("int");

                    b.HasKey("StdId", "CrsId");

                    b.HasIndex("CrsId");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("Assignemnt.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeptId"), 1L, 1);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("DeptId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Assignemnt.Models.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Assignemnt.Models.Student", b =>
                {
                    b.Property<int>("StdId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StdId"), 1L, 1);

                    b.Property<int?>("DepartmentDeptId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StdAge")
                        .HasColumnType("int");

                    b.Property<string>("StdName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StdId");

                    b.HasIndex("DepartmentDeptId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.Property<int>("CoursesCrsId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentsDeptId")
                        .HasColumnType("int");

                    b.HasKey("CoursesCrsId", "DepartmentsDeptId");

                    b.HasIndex("DepartmentsDeptId");

                    b.ToTable("CourseDepartment");
                });

            modelBuilder.Entity("RolesStudent", b =>
                {
                    b.Property<int>("RolesRoleId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStdId")
                        .HasColumnType("int");

                    b.HasKey("RolesRoleId", "StudentsStdId");

                    b.HasIndex("StudentsStdId");

                    b.ToTable("RolesStudent");
                });

            modelBuilder.Entity("Assignemnt.Models.CourseStudent", b =>
                {
                    b.HasOne("Assignemnt.Models.Course", "Course")
                        .WithMany("CourseStudents")
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignemnt.Models.Student", "Student")
                        .WithMany("CourseStudents")
                        .HasForeignKey("StdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Assignemnt.Models.Student", b =>
                {
                    b.HasOne("Assignemnt.Models.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentDeptId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CourseDepartment", b =>
                {
                    b.HasOne("Assignemnt.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesCrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignemnt.Models.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolesStudent", b =>
                {
                    b.HasOne("Assignemnt.Models.Roles", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignemnt.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assignemnt.Models.Course", b =>
                {
                    b.Navigation("CourseStudents");
                });

            modelBuilder.Entity("Assignemnt.Models.Department", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Assignemnt.Models.Student", b =>
                {
                    b.Navigation("CourseStudents");
                });
#pragma warning restore 612, 618
        }
    }
}

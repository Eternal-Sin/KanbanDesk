﻿// <auto-generated />
using System;
using KanbanBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KanbanBackend.Migrations
{
    [DbContext(typeof(KanbanContext))]
    partial class KanbanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KanbanBackend.Models.Column", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("idProject")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("order")
                        .HasColumnType("int");

                    b.Property<int>("projectid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("projectid");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("KanbanBackend.Models.DeskProject", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("DeskProjects");
                });

            modelBuilder.Entity("KanbanBackend.Models.Task", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("columnid")
                        .HasColumnType("int");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idColumn")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("updateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("columnid");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("KanbanBackend.Models.TaskLog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("idTask")
                        .HasColumnType("int");

                    b.Property<int>("taskid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("taskid");

                    b.ToTable("TaskLogs");
                });

            modelBuilder.Entity("KanbanBackend.Models.Column", b =>
                {
                    b.HasOne("KanbanBackend.Models.DeskProject", "project")
                        .WithMany("Columns")
                        .HasForeignKey("projectid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("project");
                });

            modelBuilder.Entity("KanbanBackend.Models.Task", b =>
                {
                    b.HasOne("KanbanBackend.Models.Column", "column")
                        .WithMany("Tasks")
                        .HasForeignKey("columnid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("column");
                });

            modelBuilder.Entity("KanbanBackend.Models.TaskLog", b =>
                {
                    b.HasOne("KanbanBackend.Models.Task", "task")
                        .WithMany("Logs")
                        .HasForeignKey("taskid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("task");
                });

            modelBuilder.Entity("KanbanBackend.Models.Column", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("KanbanBackend.Models.DeskProject", b =>
                {
                    b.Navigation("Columns");
                });

            modelBuilder.Entity("KanbanBackend.Models.Task", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}

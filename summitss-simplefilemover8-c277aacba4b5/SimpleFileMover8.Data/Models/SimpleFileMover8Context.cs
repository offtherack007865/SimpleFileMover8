using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimpleFileMover8.Data.Models;

public partial class SimpleFileMover8Context : DbContext
{
    public SimpleFileMover8Context(DbContextOptions<SimpleFileMover8Context> options)
        : base(options)
    {
        string projectPath = AppDomain.CurrentDomain.BaseDirectory;
        IConfigurationRoot configuration =
            new ConfigurationBuilder()
                .SetBasePath(projectPath)
        .AddJsonFile(MyConstants.AppSettingsFile)
        .Build();
        MyConnectionString =
            configuration.GetConnectionString(MyConstants.SimpleFileMover8ConnectionString);
    }

    public string MyConnectionString { get; set; }

    public virtual DbSet<SimpleFileMover8Config> SimpleFileMover8Configs { get; set; }
    public virtual DbSet<spGetMySimpleFileMover8ConfigOutputColumns> spGetMySimpleFileMover8ConfigOutputColumnsList { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<spGetMySimpleFileMover8ConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<SimpleFileMover8Config>(entity =>
        {
            entity.HasKey(e => e.Pk);

            entity.ToTable("SimpleFileMover8_Config");

            entity.Property(e => e.ConfigWebApiUrl).HasMaxLength(300);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedTimestamp).HasColumnType("datetime");
            entity.Property(e => e.RequiredFilePrefix).HasMaxLength(300);
            entity.Property(e => e.SourceDirectory).HasMaxLength(300);
            entity.Property(e => e.SystemName).HasMaxLength(300);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedTimestamp).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

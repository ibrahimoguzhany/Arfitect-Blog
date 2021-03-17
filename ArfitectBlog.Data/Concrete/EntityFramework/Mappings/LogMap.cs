﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArfitectBlog.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArfitectBlog.Data.Concrete.EntityFramework.Mappings
{
    public class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id).ValueGeneratedOnAdd();

            builder.Property(l => l.MachineName).IsRequired();
            builder.Property(l => l.MachineName).HasMaxLength(50);

            builder.Property(l => l.Logged).IsRequired();
            builder.Property(l => l.Level).IsRequired().HasMaxLength(50);

            builder.Property(l => l.Message).IsRequired().HasColumnType("NVARCHAR(MAX)");

            builder.Property(l => l.Logger).IsRequired(false).HasMaxLength(250);

            builder.Property(l => l.Callsite).IsRequired(false);
            builder.Property(l => l.Callsite).HasColumnType("NVARCHAR(MAX)");

            builder.Property(l => l.Exception).IsRequired(false);
            builder.Property(l => l.Exception).HasColumnType("NVARCHAR(MAX)");

            builder.ToTable("Logs");

        }
    }
}

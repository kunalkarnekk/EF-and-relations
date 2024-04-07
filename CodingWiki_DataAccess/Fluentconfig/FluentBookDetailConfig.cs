using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.Fluentconfig
{
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("Fluent_BookDetails");

            //name of columns
            modelBuilder.Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters");


            //Other validation
            modelBuilder.Property(u => u.NumberOfChapters).IsRequired();
            //primary key 
            modelBuilder.HasKey(u => u.BookDetail_Id);

            //relation
            modelBuilder.HasOne(b => b.Book).WithOne(b => b.BookDetail)
                .HasForeignKey<Fluent_BookDetail>(b => b.Book_Id);
        }

    }
}

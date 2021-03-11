using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;

namespace MyBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(1000);
            builder.HasOne<Post>(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
            builder.Property(x => x.CreatedByName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ModifiedByName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.ModifiedByName).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.ToTable("Comments");

            //builder.HasData(new Comment
            //{
            //    Id = 1,
            //    PostId = 1,
            //    Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C# Makale Yorumu",

            //},

            //    new Comment()
            //    {
            //        Id = 2,
            //        PostId = 2,
            //        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "C++ Makale Yorumu",
            //    },
            //    new Comment()
            //    {
            //        Id = 3,
            //        PostId = 3,
            //        Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu ex non diam luctus pellentesque vitae malesuada dolor. Mauris sed fermentum ipsum, ut interdum tortor. Etiam pretium turpis et metus lacinia fringilla. Fusce vel leo quis felis molestie luctus eget sed nunc. In hac habitasse platea dictumst. Integer ex sem, egestas in dapibus nec, ullamcorper mattis nulla. Cras sodales nunc dignissim ligula euismod, in gravida tellus sollicitudin. Vivamus suscipit malesuada eleifend. Phasellus sodales augue risus, at fringilla est tincidunt non. Donec ut justo sed ligula iaculis vestibulum vitae sed massa. Pellentesque tortor quam, maximus in vehicula sed, dignissim vel leo. Quisque iaculis diam et est pulvinar rutrum. In eu hendrerit neque, non condimentum mi. Ut a egestas turpis",
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "InitialCreate",
            //        CreatedDate = DateTime.Now,
            //        ModifiedByName = "InitialCreate",
            //        ModifiedDate = DateTime.Now,
            //        Note = "JavaScript Makale Yorumu",
            //    }
            //    );
        }
    }
}

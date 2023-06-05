using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class CommentReplyConfiguration : IEntityTypeConfiguration<CommentReply>
    {
        public void Configure(EntityTypeBuilder<CommentReply> builder)
        {
            builder.HasKey(cr => cr.Id);

            builder.Property(cr => cr.CommentId)
                .IsRequired();

            builder.Property(cr => cr.ReplyId)
                .IsRequired();

            builder.HasOne(cr => cr.Comment)
                .WithMany(c => c.CommentsReplies)
                .HasForeignKey(cr => cr.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cr => cr.Reply)
                .WithMany(r => r.CommentsReplies)
                .HasForeignKey(cr => cr.ReplyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("CommentsReplies", "gamecatalog");

            builder.HasData(DataSeeder.CommentsReplies);
        }
    }
}
using FirstApi.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Configuration
{
    

    public class Majorconfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder.HasKey(c => c.Id);


            builder.HasMany(v=>v.Students).WithOne(v=>v.Major).HasForeignKey(b=>b.MajorID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(v => v.Name).HasMaxLength(200).IsRequired();



        }

    }

}

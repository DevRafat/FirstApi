using FirstApi.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApi.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {

      
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Email).IsRequired().HasMaxLength(500);
            builder.HasIndex(v => v.Email).IsUnique();
            builder.Property(v => v.FullName).IsRequired().HasMaxLength(100);
            builder.Property(v => v.Password).IsRequired().HasMaxLength(2000);

        }
    }
}

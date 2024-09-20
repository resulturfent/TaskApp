using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using TaskManagement.Domain.Entity;

namespace TaskManagement.DataLayer.Configurations
{
    public  class AppUserConfiguration :IEntityTypeConfiguration<AppUser>
    {
     

        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Password).IsRequired().HasMaxLength(255);


        }
    }

}

using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Service.ModelConfigs
{
    public class UserConfig : EntityTypeConfiguration<UserEntity>
    {
        public UserConfig()
        {
            this.ToTable("Summer_User");
            this.Property(a => a.WxID).HasMaxLength(255);
        }
    }
}

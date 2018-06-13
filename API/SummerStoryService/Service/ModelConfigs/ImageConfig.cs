using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ModelConfigs
{
    public class ImageConfig: EntityTypeConfiguration<ImageEntity>
    {
        public ImageConfig()
        {
            this.ToTable("Summer_Image");
            this.Property(p => p.ImageName).IsRequired().HasMaxLength(255).IsUnicode(true);
            this.HasRequired(p => p.Record).WithMany(p => p.Images).HasForeignKey(p => p.RecordID).WillCascadeOnDelete(false);
        }
    }
}

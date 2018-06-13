using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ModelConfigs
{
    public class TextConfig : EntityTypeConfiguration<TextEntity>
    {
        public TextConfig()
        {
            this.ToTable("Summer_Text");
            this.Property(p => p.Content).IsOptional().HasMaxLength(1024).IsUnicode(true);
            this.HasRequired(p => p.Record).WithMany(p => p.Texts).HasForeignKey(p => p.RecordID).WillCascadeOnDelete(false);
        }
    }
}

using System;
using System.Collections.Generic;
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
            this.HasRequired(p => p.User).WithMany(p => p.Records).HasForeignKey(p => p.UserID).WillCascadeOnDelete(false);
        }
    }
}

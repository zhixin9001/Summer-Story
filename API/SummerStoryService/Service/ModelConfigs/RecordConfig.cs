using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace Service.ModelConfigs
{
    public class RecordConfig : EntityTypeConfiguration<RecordEntity>
    {
        public RecordConfig()
        {
            this.ToTable("Summer_Record");

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("IFsInit")]
    internal partial class ParameterBind: IParameter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Variable")]
        public string Name { get; set; }
        [Column("Value")]
        public string Value { get; set; }
    }

    [Table("LoadFull")]
    internal partial class ParameterBind2 : IParameter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Variable")]
        public string Name { get; set; }
        [Column("Value")]
        public string Value { get; set; }
    }

    [Table("ProjectDataCountryLoad")]
    public partial class ProjectDataCountryLoad
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }

    }

    internal partial class IFsInitContext
    {

        protected virtual DbSet<ParameterBind> IFsInit { get; set; }
        protected virtual DbSet<ParameterBind2> LoadFull { get; set; }
        internal virtual DbSet<ProjectDataCountryLoad> ProjectDataCountryLoad { get; set; }
    }

}

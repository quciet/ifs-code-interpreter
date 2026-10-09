using System;
using System.Collections;
using System.Collections.Generic;

namespace IFs.Data
{

    // maps to records in IFS.grp
    internal partial class DimensionBind: IDimension
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SortedList<short, string> Buckets { get; set; }

        IReadOnlyDictionary<short, string> IDimension.Buckets {
            get => this.Buckets;
        }

    }

    internal partial class IFsGrpContext
    {
        protected SortedList<int, IDimension>  IFsGrp { get; set; }
    }

}

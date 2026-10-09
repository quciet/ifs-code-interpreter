using System;
using System.Collections.Generic;

namespace IFs.Data
{

    public interface IBasecase: IDisposable
    {
        // time dimension
        IEnumerable<TimeBucket> TimeBuckets { get; }

        // region dimension
        IEnumerable<RegionBucket> RegionBuckets { get; }
        
        // cohort dimension
        IEnumerable<CohortBucket> CohortBuckets { get; }

        // gender dimension
        IEnumerable<GenderBucket> GenderBuckets { get; }

        // save POP basecase data
        void Add(PopBasecase record);

        //jrs 2019/07/26
        // save FERT basecase data
        void Add(FerBasecase record);
        // save MORT basecase data
        void Add(MorBasecase record);
        void Add(MorDetBaseCase record);
        void Add(MorbDetBaseCase record);
        void Add(MorDetFactors record);
        void Add(MorDetNormFactors record);
        void Add(StuntingByAge record);
        // save Education Graduates
        void Add(EdnGradsBaseCase record);
        void Add(EdnYears25BaseCase record);
        void Add(EdnYears15BaseCase record);
        void Add(HLPROXIMATEDRIVERS record);

        // clear all results
        void Clear(Type modelType);

        // sync changes to disk
        void Flush();
    }

}

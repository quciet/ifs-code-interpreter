using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{

    [Table("RegionalizationFull")]
    internal partial class RegionalizationFull
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("NewRegion")]
        public int RegionId { get; set; }
    }

    [Table("RegionIDNewFull")]
    internal partial class RegionIdNewFull
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionNum")]
        public int Id { get; set; }
        [Column("RegionName")]
        public string RegionName { get; set; }
        //jrs 2022/06/15 removed for compatibility with older versions 7.06 for example
        //[Column("RegionAlias")]
        //public string RegionAlias { get; set; }
    }

    //2018/10/25
    [Table("GroupIDNewFull")]
    public partial class GroupIDNewFull
    {
        [Column("GroupName")]
        public string GroupName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GroupNum")]
        public int GroupNum { get; set; }
        [Column("LongGroupName")]
        public string LongGroupName { get; set; }
        [Column("Alias")]
        public string Alias { get; set; }
        [ForeignKey("GroupId")]
        public virtual ISet<GroupingFull> Regions { get; set; }
    }

    [Table("GroupingFull")]
    public partial class GroupingFull
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionNo")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GroupNo")]
        public int GroupId { get; set; }
    }

    [Table("GList")]
    public partial class GList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GListCode")]
        public int GListCode { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region")]
        public string RegionName { get; set; }
        [Column("Group")]
        public int GroupId { get; set; }
        [Column("Decomposable")]
        public int Decomposable { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MemberOrder")]
        public int MemberOrder { get; set; }
        [Column("LineFeed")]
        public int LineFeed { get; set; }
        [Column("GroupTitle")]
        public string GroupTitle { get; set; }
        [Column("ColorValue")]
        public int ColorValue { get; set; }
        [Column("LightColorValue")]
        public int LightColorValue { get; set; }

    }

    [Table("GListID")]
    public partial class GListID
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Code")]
        public int Code { get; set; }
        [Column("Description")]
        public string Description { get; set; }

    }

    public abstract partial class CohortCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("Cohort0")]
        public double? Cohort0 { get; set; }
        [Column("Cohort1")]
        public double? Cohort1 { get; set; }
        [Column("Cohort2")]
        public double? Cohort2 { get; set; }
        [Column("Cohort3")]
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("Cohort0", Cohort0);
            yield return new KeyValuePair<string, double?>("Cohort1", Cohort1);
            yield return new KeyValuePair<string, double?>("Cohort2", Cohort2);
            yield return new KeyValuePair<string, double?>("Cohort3", Cohort3);
            yield return new KeyValuePair<string, double?>("Cohort4", Cohort4);
            yield return new KeyValuePair<string, double?>("Cohort5", Cohort5);
            yield return new KeyValuePair<string, double?>("Cohort6", Cohort6);
            yield return new KeyValuePair<string, double?>("Cohort7", Cohort7);
            yield return new KeyValuePair<string, double?>("Cohort8", Cohort8);
            yield return new KeyValuePair<string, double?>("Cohort9", Cohort9);
            yield return new KeyValuePair<string, double?>("Cohort10", Cohort10);
            yield return new KeyValuePair<string, double?>("Cohort11", Cohort11);
            yield return new KeyValuePair<string, double?>("Cohort12", Cohort12);
            yield return new KeyValuePair<string, double?>("Cohort13", Cohort13);
            yield return new KeyValuePair<string, double?>("Cohort14", Cohort14);
            yield return new KeyValuePair<string, double?>("Cohort15", Cohort15);
            yield return new KeyValuePair<string, double?>("Cohort16", Cohort16);
            yield return new KeyValuePair<string, double?>("Cohort17", Cohort17);
            yield return new KeyValuePair<string, double?>("Cohort18", Cohort18);
            yield return new KeyValuePair<string, double?>("Cohort19", Cohort19);
            yield return new KeyValuePair<string, double?>("Cohort20", Cohort20);
            yield return new KeyValuePair<string, double?>("Cohort21", Cohort21);
            yield return new KeyValuePair<string, double?>("Cohort22", Cohort22);
            yield return new KeyValuePair<string, double?>("Cohort23", Cohort23);
            yield return new KeyValuePair<string, double?>("Cohort24", Cohort24);
            yield return new KeyValuePair<string, double?>("Cohort25", Cohort25);
            yield return new KeyValuePair<string, double?>("Cohort26", Cohort26);
            yield return new KeyValuePair<string, double?>("Cohort27", Cohort27);
            yield return new KeyValuePair<string, double?>("Cohort28", Cohort28);
            yield return new KeyValuePair<string, double?>("Cohort29", Cohort29);
            yield return new KeyValuePair<string, double?>("Cohort30", Cohort30);
            yield return new KeyValuePair<string, double?>("Cohort31", Cohort31);
            yield return new KeyValuePair<string, double?>("Cohort32", Cohort32);
            yield return new KeyValuePair<string, double?>("Cohort33", Cohort33);
            yield return new KeyValuePair<string, double?>("Cohort34", Cohort34);
            yield return new KeyValuePair<string, double?>("Cohort35", Cohort35);
            yield return new KeyValuePair<string, double?>("Cohort36", Cohort36);
            yield return new KeyValuePair<string, double?>("Cohort37", Cohort37);
            yield return new KeyValuePair<string, double?>("Cohort38", Cohort38);
            yield return new KeyValuePair<string, double?>("Cohort39", Cohort39);
            yield return new KeyValuePair<string, double?>("Cohort40", Cohort40);
            yield return new KeyValuePair<string, double?>("Cohort41", Cohort41);
        }
    }

    public abstract partial class AgeSingleCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("Age0")]
        public double? Age0 { get; set; }
        [Column("Age1")]
        public double? Age1 { get; set; }
        [Column("Age2")]
        public double? Age2 { get; set; }
        [Column("Age3")]
        public double? Age3 { get; set; }
        [Column("Age4")]
        public double? Age4 { get; set; }
        [Column("Age5")]
        public double? Age5 { get; set; }
        [Column("Age6")]
        public double? Age6 { get; set; }
        [Column("Age7")]
        public double? Age7 { get; set; }
        [Column("Age8")]
        public double? Age8 { get; set; }
        [Column("Age9")]
        public double? Age9 { get; set; }
        [Column("Age10")]
        public double? Age10 { get; set; }
        [Column("Age11")]
        public double? Age11 { get; set; }
        [Column("Age12")]
        public double? Age12 { get; set; }
        [Column("Age13")]
        public double? Age13 { get; set; }
        [Column("Age14")]
        public double? Age14 { get; set; }
        [Column("Age15")]
        public double? Age15 { get; set; }
        [Column("Age16")]
        public double? Age16 { get; set; }
        [Column("Age17")]
        public double? Age17 { get; set; }
        [Column("Age18")]
        public double? Age18 { get; set; }
        [Column("Age19")]
        public double? Age19 { get; set; }
        [Column("Age20")]
        public double? Age20 { get; set; }
        [Column("Age21")]
        public double? Age21 { get; set; }
        [Column("Age22")]
        public double? Age22 { get; set; }
        [Column("Age23")]
        public double? Age23 { get; set; }
        [Column("Age24")]
        public double? Age24 { get; set; }
        [Column("Age25")]
        public double? Age25 { get; set; }
        [Column("Age26")]
        public double? Age26 { get; set; }
        [Column("Age27")]
        public double? Age27 { get; set; }
        [Column("Age28")]
        public double? Age28 { get; set; }
        [Column("Age29")]
        public double? Age29 { get; set; }
        [Column("Age30")]
        public double? Age30 { get; set; }
        [Column("Age31")]
        public double? Age31 { get; set; }
        [Column("Age32")]
        public double? Age32 { get; set; }
        [Column("Age33")]
        public double? Age33 { get; set; }
        [Column("Age34")]
        public double? Age34 { get; set; }
        [Column("Age35")]
        public double? Age35 { get; set; }
        [Column("Age36")]
        public double? Age36 { get; set; }
        [Column("Age37")]
        public double? Age37 { get; set; }
        [Column("Age38")]
        public double? Age38 { get; set; }
        [Column("Age39")]
        public double? Age39 { get; set; }
        [Column("Age40")]
        public double? Age40 { get; set; }
        [Column("Age41")]
        public double? Age41 { get; set; }
        [Column("Age42")]
        public double? Age42 { get; set; }
        [Column("Age43")]
        public double? Age43 { get; set; }
        [Column("Age44")]
        public double? Age44 { get; set; }
        [Column("Age45")]
        public double? Age45 { get; set; }
        [Column("Age46")]
        public double? Age46 { get; set; }
        [Column("Age47")]
        public double? Age47 { get; set; }
        [Column("Age48")]
        public double? Age48 { get; set; }
        [Column("Age49")]
        public double? Age49 { get; set; }
        [Column("Age50")]
        public double? Age50 { get; set; }
        [Column("Age51")]
        public double? Age51 { get; set; }
        [Column("Age52")]
        public double? Age52 { get; set; }
        [Column("Age53")]
        public double? Age53 { get; set; }
        [Column("Age54")]
        public double? Age54 { get; set; }
        [Column("Age55")]
        public double? Age55 { get; set; }
        [Column("Age56")]
        public double? Age56 { get; set; }
        [Column("Age57")]
        public double? Age57 { get; set; }
        [Column("Age58")]
        public double? Age58 { get; set; }
        [Column("Age59")]
        public double? Age59 { get; set; }
        [Column("Age60")]
        public double? Age60 { get; set; }
        [Column("Age61")]
        public double? Age61 { get; set; }
        [Column("Age62")]
        public double? Age62 { get; set; }
        [Column("Age63")]
        public double? Age63 { get; set; }
        [Column("Age64")]
        public double? Age64 { get; set; }
        [Column("Age65")]
        public double? Age65 { get; set; }
        [Column("Age66")]
        public double? Age66 { get; set; }
        [Column("Age67")]
        public double? Age67 { get; set; }
        [Column("Age68")]
        public double? Age68 { get; set; }
        [Column("Age69")]
        public double? Age69 { get; set; }
        [Column("Age70")]
        public double? Age70 { get; set; }
        [Column("Age71")]
        public double? Age71 { get; set; }
        [Column("Age72")]
        public double? Age72 { get; set; }
        [Column("Age73")]
        public double? Age73 { get; set; }
        [Column("Age74")]
        public double? Age74 { get; set; }
        [Column("Age75")]
        public double? Age75 { get; set; }
        [Column("Age76")]
        public double? Age76 { get; set; }
        [Column("Age77")]
        public double? Age77 { get; set; }
        [Column("Age78")]
        public double? Age78 { get; set; }
        [Column("Age79")]
        public double? Age79 { get; set; }
        [Column("Age80")]
        public double? Age80 { get; set; }
        [Column("Age81")]
        public double? Age81 { get; set; }
        [Column("Age82")]
        public double? Age82 { get; set; }
        [Column("Age83")]
        public double? Age83 { get; set; }
        [Column("Age84")]
        public double? Age84 { get; set; }
        [Column("Age85")]
        public double? Age85 { get; set; }
        [Column("Age86")]
        public double? Age86 { get; set; }
        [Column("Age87")]
        public double? Age87 { get; set; }
        [Column("Age88")]
        public double? Age88 { get; set; }
        [Column("Age89")]
        public double? Age89 { get; set; }
        [Column("Age90")]
        public double? Age90 { get; set; }
        [Column("Age91")]
        public double? Age91 { get; set; }
        [Column("Age92")]
        public double? Age92 { get; set; }
        [Column("Age93")]
        public double? Age93 { get; set; }
        [Column("Age94")]
        public double? Age94 { get; set; }
        [Column("Age95")]
        public double? Age95 { get; set; }
        [Column("Age96")]
        public double? Age96 { get; set; }
        [Column("Age97")]
        public double? Age97 { get; set; }
        [Column("Age98")]
        public double? Age98 { get; set; }
        [Column("Age99")]
        public double? Age99 { get; set; }
        [Column("Age100")]
        public double? Age100 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Ages
        {
            get
            {
                return allAges().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allAges()
        {
            yield return new KeyValuePair<string, double?>("Age0", Age0);
            yield return new KeyValuePair<string, double?>("Age1", Age1);
            yield return new KeyValuePair<string, double?>("Age2", Age2);
            yield return new KeyValuePair<string, double?>("Age3", Age3);
            yield return new KeyValuePair<string, double?>("Age4", Age4);
            yield return new KeyValuePair<string, double?>("Age5", Age5);
            yield return new KeyValuePair<string, double?>("Age6", Age6);
            yield return new KeyValuePair<string, double?>("Age7", Age7);
            yield return new KeyValuePair<string, double?>("Age8", Age8);
            yield return new KeyValuePair<string, double?>("Age9", Age9);
            yield return new KeyValuePair<string, double?>("Age10", Age10);
            yield return new KeyValuePair<string, double?>("Age11", Age11);
            yield return new KeyValuePair<string, double?>("Age12", Age12);
            yield return new KeyValuePair<string, double?>("Age13", Age13);
            yield return new KeyValuePair<string, double?>("Age14", Age14);
            yield return new KeyValuePair<string, double?>("Age15", Age15);
            yield return new KeyValuePair<string, double?>("Age16", Age16);
            yield return new KeyValuePair<string, double?>("Age17", Age17);
            yield return new KeyValuePair<string, double?>("Age18", Age18);
            yield return new KeyValuePair<string, double?>("Age19", Age19);
            yield return new KeyValuePair<string, double?>("Age20", Age20);
            yield return new KeyValuePair<string, double?>("Age21", Age21);
            yield return new KeyValuePair<string, double?>("Age22", Age22);
            yield return new KeyValuePair<string, double?>("Age23", Age23);
            yield return new KeyValuePair<string, double?>("Age24", Age24);
            yield return new KeyValuePair<string, double?>("Age25", Age25);
            yield return new KeyValuePair<string, double?>("Age26", Age26);
            yield return new KeyValuePair<string, double?>("Age27", Age27);
            yield return new KeyValuePair<string, double?>("Age28", Age28);
            yield return new KeyValuePair<string, double?>("Age29", Age29);
            yield return new KeyValuePair<string, double?>("Age30", Age30);
            yield return new KeyValuePair<string, double?>("Age31", Age31);
            yield return new KeyValuePair<string, double?>("Age32", Age32);
            yield return new KeyValuePair<string, double?>("Age33", Age33);
            yield return new KeyValuePair<string, double?>("Age34", Age34);
            yield return new KeyValuePair<string, double?>("Age35", Age35);
            yield return new KeyValuePair<string, double?>("Age36", Age36);
            yield return new KeyValuePair<string, double?>("Age37", Age37);
            yield return new KeyValuePair<string, double?>("Age38", Age38);
            yield return new KeyValuePair<string, double?>("Age39", Age39);
            yield return new KeyValuePair<string, double?>("Age40", Age40);
            yield return new KeyValuePair<string, double?>("Age41", Age41);
            yield return new KeyValuePair<string, double?>("Age42", Age42);
            yield return new KeyValuePair<string, double?>("Age43", Age43);
            yield return new KeyValuePair<string, double?>("Age44", Age44);
            yield return new KeyValuePair<string, double?>("Age45", Age45);
            yield return new KeyValuePair<string, double?>("Age46", Age46);
            yield return new KeyValuePair<string, double?>("Age47", Age47);
            yield return new KeyValuePair<string, double?>("Age48", Age48);
            yield return new KeyValuePair<string, double?>("Age49", Age49);
            yield return new KeyValuePair<string, double?>("Age50", Age50);
            yield return new KeyValuePair<string, double?>("Age51", Age51);
            yield return new KeyValuePair<string, double?>("Age52", Age52);
            yield return new KeyValuePair<string, double?>("Age53", Age53);
            yield return new KeyValuePair<string, double?>("Age54", Age54);
            yield return new KeyValuePair<string, double?>("Age55", Age55);
            yield return new KeyValuePair<string, double?>("Age56", Age56);
            yield return new KeyValuePair<string, double?>("Age57", Age57);
            yield return new KeyValuePair<string, double?>("Age58", Age58);
            yield return new KeyValuePair<string, double?>("Age59", Age59);
            yield return new KeyValuePair<string, double?>("Age60", Age60);
            yield return new KeyValuePair<string, double?>("Age61", Age61);
            yield return new KeyValuePair<string, double?>("Age62", Age62);
            yield return new KeyValuePair<string, double?>("Age63", Age63);
            yield return new KeyValuePair<string, double?>("Age64", Age64);
            yield return new KeyValuePair<string, double?>("Age65", Age65);
            yield return new KeyValuePair<string, double?>("Age66", Age66);
            yield return new KeyValuePair<string, double?>("Age67", Age67);
            yield return new KeyValuePair<string, double?>("Age68", Age68);
            yield return new KeyValuePair<string, double?>("Age69", Age69);
            yield return new KeyValuePair<string, double?>("Age70", Age70);
            yield return new KeyValuePair<string, double?>("Age71", Age71);
            yield return new KeyValuePair<string, double?>("Age72", Age72);
            yield return new KeyValuePair<string, double?>("Age73", Age73);
            yield return new KeyValuePair<string, double?>("Age74", Age74);
            yield return new KeyValuePair<string, double?>("Age75", Age75);
            yield return new KeyValuePair<string, double?>("Age76", Age76);
            yield return new KeyValuePair<string, double?>("Age77", Age77);
            yield return new KeyValuePair<string, double?>("Age78", Age78);
            yield return new KeyValuePair<string, double?>("Age79", Age79);
            yield return new KeyValuePair<string, double?>("Age80", Age80);
            yield return new KeyValuePair<string, double?>("Age81", Age81);
            yield return new KeyValuePair<string, double?>("Age82", Age82);
            yield return new KeyValuePair<string, double?>("Age83", Age83);
            yield return new KeyValuePair<string, double?>("Age84", Age84);
            yield return new KeyValuePair<string, double?>("Age85", Age85);
            yield return new KeyValuePair<string, double?>("Age86", Age86);
            yield return new KeyValuePair<string, double?>("Age87", Age87);
            yield return new KeyValuePair<string, double?>("Age88", Age88);
            yield return new KeyValuePair<string, double?>("Age89", Age89);
            yield return new KeyValuePair<string, double?>("Age90", Age90);
            yield return new KeyValuePair<string, double?>("Age91", Age91);
            yield return new KeyValuePair<string, double?>("Age92", Age92);
            yield return new KeyValuePair<string, double?>("Age93", Age93);
            yield return new KeyValuePair<string, double?>("Age94", Age94);
            yield return new KeyValuePair<string, double?>("Age95", Age95);
            yield return new KeyValuePair<string, double?>("Age96", Age96);
            yield return new KeyValuePair<string, double?>("Age97", Age97);
            yield return new KeyValuePair<string, double?>("Age98", Age98);
            yield return new KeyValuePair<string, double?>("Age99", Age99);
            yield return new KeyValuePair<string, double?>("Age100", Age100);
        }
    }

    public abstract partial class FertCohortCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("Cohort3")] //jrs 2023/12/05
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("Cohort3", Cohort3); //jrs 2023/12/05
            yield return new KeyValuePair<string, double?>("Cohort4", Cohort4);
            yield return new KeyValuePair<string, double?>("Cohort5", Cohort5);
            yield return new KeyValuePair<string, double?>("Cohort6", Cohort6);
            yield return new KeyValuePair<string, double?>("Cohort7", Cohort7);
            yield return new KeyValuePair<string, double?>("Cohort8", Cohort8);
            yield return new KeyValuePair<string, double?>("Cohort9", Cohort9);
            yield return new KeyValuePair<string, double?>("Cohort10", Cohort10);
            yield return new KeyValuePair<string, double?>("Cohort11", Cohort11);
            yield return new KeyValuePair<string, double?>("Cohort12", Cohort12);
            yield return new KeyValuePair<string, double?>("Cohort13", Cohort13);
            yield return new KeyValuePair<string, double?>("Cohort14", Cohort14);
            yield return new KeyValuePair<string, double?>("Cohort15", Cohort15);
            yield return new KeyValuePair<string, double?>("Cohort16", Cohort16);
            yield return new KeyValuePair<string, double?>("Cohort17", Cohort17);
            yield return new KeyValuePair<string, double?>("Cohort18", Cohort18);
            yield return new KeyValuePair<string, double?>("Cohort19", Cohort19);
            yield return new KeyValuePair<string, double?>("Cohort20", Cohort20);
            yield return new KeyValuePair<string, double?>("Cohort21", Cohort21);
            yield return new KeyValuePair<string, double?>("Cohort22", Cohort22);
            yield return new KeyValuePair<string, double?>("Cohort23", Cohort23);
            yield return new KeyValuePair<string, double?>("Cohort24", Cohort24);
            yield return new KeyValuePair<string, double?>("Cohort25", Cohort25);
            yield return new KeyValuePair<string, double?>("Cohort26", Cohort26);
            yield return new KeyValuePair<string, double?>("Cohort27", Cohort27);
            yield return new KeyValuePair<string, double?>("Cohort28", Cohort28);
            yield return new KeyValuePair<string, double?>("Cohort29", Cohort29);
            yield return new KeyValuePair<string, double?>("Cohort30", Cohort30);
            yield return new KeyValuePair<string, double?>("Cohort31", Cohort31);
            yield return new KeyValuePair<string, double?>("Cohort32", Cohort32);
            yield return new KeyValuePair<string, double?>("Cohort33", Cohort33);
            yield return new KeyValuePair<string, double?>("Cohort34", Cohort34);
            yield return new KeyValuePair<string, double?>("Cohort35", Cohort35);
            yield return new KeyValuePair<string, double?>("Cohort36", Cohort36);
            yield return new KeyValuePair<string, double?>("Cohort37", Cohort37);
            yield return new KeyValuePair<string, double?>("Cohort38", Cohort38);
            yield return new KeyValuePair<string, double?>("Cohort39", Cohort39);
            yield return new KeyValuePair<string, double?>("Cohort40", Cohort40);
            yield return new KeyValuePair<string, double?>("Cohort41", Cohort41);
        }
    }

    public abstract partial class EduCohortCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string CountryName { get; set; }
        [Column("FIPS_CODE")]
        public string FipsCode { get; set; }
        [Column("AgeGroup4")]
        public double? Cohort4 { get; set; }
        [Column("AgeGroup5")]
        public double? Cohort5 { get; set; }
        [Column("AgeGroup6")]
        public double? Cohort6 { get; set; }
        [Column("AgeGroup7")]
        public double? Cohort7 { get; set; }
        [Column("AgeGroup8")]
        public double? Cohort8 { get; set; }
        [Column("AgeGroup9")]
        public double? Cohort9 { get; set; }
        [Column("AgeGroup10")]
        public double? Cohort10 { get; set; }
        [Column("AgeGroup11")]
        public double? Cohort11 { get; set; }
        [Column("AgeGroup12")]
        public double? Cohort12 { get; set; }
        [Column("AgeGroup13")]
        public double? Cohort13 { get; set; }
        [Column("AgeGroup14")]
        public double? Cohort14 { get; set; }
        [Column("AgeGroup15")]
        public double? Cohort15 { get; set; }
        [Column("AgeGroup16")]
        public double? Cohort16 { get; set; }
        [Column("AgeGroup17")]
        public double? Cohort17 { get; set; }
        [Column("AgeGroup18")]
        public double? Cohort18 { get; set; }
        [Column("AgeGroup19")]
        public double? Cohort19 { get; set; }
        [Column("AgeGroup20")]
        public double? Cohort20 { get; set; }
        [Column("AgeGroup21")]
        public double? Cohort21 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("AgeGroup4", Cohort4);
            yield return new KeyValuePair<string, double?>("AgeGroup5", Cohort5);
            yield return new KeyValuePair<string, double?>("AgeGroup6", Cohort6);
            yield return new KeyValuePair<string, double?>("AgeGroup7", Cohort7);
            yield return new KeyValuePair<string, double?>("AgeGroup8", Cohort8);
            yield return new KeyValuePair<string, double?>("AgeGroup9", Cohort9);
            yield return new KeyValuePair<string, double?>("AgeGroup10", Cohort10);
            yield return new KeyValuePair<string, double?>("AgeGroup11", Cohort11);
            yield return new KeyValuePair<string, double?>("AgeGroup12", Cohort12);
            yield return new KeyValuePair<string, double?>("AgeGroup13", Cohort13);
            yield return new KeyValuePair<string, double?>("AgeGroup14", Cohort14);
            yield return new KeyValuePair<string, double?>("AgeGroup15", Cohort15);
            yield return new KeyValuePair<string, double?>("AgeGroup16", Cohort16);
            yield return new KeyValuePair<string, double?>("AgeGroup17", Cohort17);
            yield return new KeyValuePair<string, double?>("AgeGroup18", Cohort18);
            yield return new KeyValuePair<string, double?>("AgeGroup19", Cohort19);
            yield return new KeyValuePair<string, double?>("AgeGroup20", Cohort20);
            yield return new KeyValuePair<string, double?>("AgeGroup21", Cohort21);
        }
    }

    [Table("PopAgeCohortCountryMale")]
    public partial class PopAgeCohortCountryMale : CohortCountry
    {
    }

    [Table("PopAgeCohortCountryMaleHist")]
    public partial class PopAgeCohortCountryMaleHist : CohortCountry
    {
    }

    [Table("PopAgeCohortCountryFemale")]
    public partial class PopAgeCohortCountryFemale : CohortCountry
    {
    }

    [Table("PopAgeCohortCountryFemaleHist")]
    public partial class PopAgeCohortCountryFemaleHist : CohortCountry
    {
    }

    [Table("PopAgeSingleCountryMale")]
    public partial class PopAgeSingleCountryMale : AgeSingleCountry
    {
    }

    [Table("PopAgeSingleCountryMaleHist")]
    public partial class PopAgeSingleCountryMaleHist : AgeSingleCountry
    {
    }

    [Table("PopAgeSingleCountryFemale")]
    public partial class PopAgeSingleCountryFemale : AgeSingleCountry
    {
    }

    [Table("PopAgeSingleCountryFemaleHist")]
    public partial class PopAgeSingleCountryFemaleHist : AgeSingleCountry
    {
    }

    [Table("PopFertilityCohortCountryFemale")]
    public partial class PopFertilityCohortCountryFemale : FertCohortCountry
    {
    }

    [Table("PopFertilityCohortCountryFemaleHist")]
    public partial class PopFertilityCohortCountryFemaleHist : FertCohortCountry
    {
    }
    
    [Table("PopMortalityCohortCountryMale")]
    public partial class PopMortalityCohortCountryMale : CohortCountry
    {
    }

    [Table("PopMortalityCohortCountryMaleHist")]
    public partial class PopMortalityCohortCountryMaleHist : FertCohortCountry
    {
    }

    [Table("PopMortalityCohortCountryFemale")]
    public partial class PopMortalityCohortCountryFemale : CohortCountry
    {
    }

    [Table("PopMortalityCohortCountryFemaleHist")]
    public partial class PopMortalityCohortCountryFemaleHist : FertCohortCountry
    {
    }

    [Table("HealthYLDComMatPerCohortCountryMale")]
    public partial class HealthYLDComMatPerCohortCountryMale : CohortCountry
    {
    }

    [Table("HealthYLDComMatPerCohortCountryFemale")]
    public partial class HealthYLDComMatPerCohortCountryFemale : CohortCountry
    {
    }

    [Table("HealthYLDNonComDisCohortCountryMale")]
    public partial class HealthYLDNonComDisCohortCountryMale : CohortCountry
    {
    }

    [Table("HealthYLDNonComDisCohortCountryFemale")]
    public partial class HealthYLDNonComDisCohortCountryFemale : CohortCountry
    {
    }

    [Table("HealthYLDInjuriesCohortCountryMale")]
    public partial class HealthYLDInjuriesCohortCountryMale : CohortCountry
    {
    }

    [Table("HealthYLDInjuriesCohortCountryFemale")]
    public partial class HealthYLDInjuriesCohortCountryFemale : CohortCountry
    {
    }

    //mti 2024/05/23 start

    [Table("EducAttainPrimaryFemaleWICCountry")]
    public partial class EducAttainPrimaryFemaleWICCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPrimaryMaleWICCountry")]
    public partial class EducAttainPrimaryMaleWICCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecUpperFemaleWICCountry")]
    public partial class EducAttainSecUpperFemaleWICCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecUpperMaleWICCountry")]
    public partial class EducAttainSecUpperMaleWICCountry : EduCohortCountry
    {
    }

    [Table("EducAttainTertiaryFemaleWICCountry")]
    public partial class EducAttainTertiaryFemaleWICCountry : EduCohortCountry
    {
    }

    [Table("EducAttainTertiaryMaleWICCountry")]
    public partial class EducAttainTertiaryMaleWICCountry : EduCohortCountry
    {
    }

    //mti 2024/05/23 end

    [Table("EducAttainNoEducMaleCountry")]
    public partial class EducAttainNoEducMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainNoEducFemaleCountry")]
    public partial class EducAttainNoEducFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainNoEducTotalCountry")]
    public partial class EducAttainNoEducTotalCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriIncompleteMaleCountry")]
    public partial class EducAttainPriIncompleteMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriIncompleteFemaleCountry")]
    public partial class EducAttainPriIncompleteFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriIncompleteTotalCountry")]
    public partial class EducAttainPriIncompleteTotalCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriCompleteMaleCountry")]
    public partial class EducAttainPriCompleteMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriCompleteFemaleCountry")]
    public partial class EducAttainPriCompleteFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainPriCompleteTotalCountry")]
    public partial class EducAttainPriCompleteTotalCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecLowerMaleCountry")]
    public partial class EducAttainSecLowerMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecLowerFemaleCountry")]
    public partial class EducAttainSecLowerFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecLowerTotalCountry")]
    public partial class EducAttainSecLowerTotalCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecUpperMaleCountry")]
    public partial class EducAttainSecUpperMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecUpperFemaleCountry")]
    public partial class EducAttainSecUpperFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainSecUpperTotalCountry")]
    public partial class EducAttainSecUpperTotalCountry : EduCohortCountry
    {
    }

    [Table("EducAttainTertiaryMaleCountry")]
    public partial class EducAttainTertiaryMaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainTertiaryFemaleCountry")]
    public partial class EducAttainTertiaryFemaleCountry : EduCohortCountry
    {
    }

    [Table("EducAttainTertiaryTotalCountry")]
    public partial class EducAttainTertiaryTotalCountry : EduCohortCountry
    {
    }

    //jrs 2018/10/29
    [Table("AnnualGrowthRates")]
    public partial class AnnualGrowthRates
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VarName")]
        public string VarName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegName")]
        public string RegName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Column("GrowthRate")]
        public float? GrowthRate { get; set; }
    }

    [Table("AnnualGrowthRatesHist")]
    public partial class AnnualGrowthRatesHist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VarName")]
        public string VarName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegName")]
        public string RegName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Column("GrowthRate")]
        public float? GrowthRate { get; set; }
    }

    [Table("AnnualGrowthRatesExt")]
    public partial class AnnualGrowthRatesExt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VarName")]
        public string VarName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegName")]
        public string RegName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Column("InitVal")]
        public double? InitVal { get; set; }
        [Column("GrowthRate")]
        public double? GrowthRate { get; set; }
        [Column("InitYear")]
        public double? InitYear { get; set; }
        [Column("FinalYear")]
        public double? FinalYear { get; set; }
    }

    [Table("GlobalParameters")]
    public partial class GlobalParameters
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension")]
        public int Dimension { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension2")]
        public int Dimension2 { get; set; }
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension3")]
        public int Dimension3 { get; set; }
        [Column("Value")]
        public double? Value { get; set; }
        [Column("Comments/Source")]
        public string CommentsSource { get; set; }
    }

    [Table("CountryParameters")]
    public partial class CountryParameters
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("CultReg")]
        public float? CultReg { get; set; }
        [Column("ENPCOALMAX")]
        public float? ENPCOALMAX { get; set; }
        [Column("ENPGASMAX")]
        public float? ENPGASMAX { get; set; }
        [Column("ENPOILMAX")]
        public float? ENPOILMAX { get; set; }
        [Column("ENRESUNCE")]
        public float? ENRESUNCE { get; set; }
    }

    [Table("GlobalParametersMult")]
    public partial class GlobalParametersMult
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension")]
        public int Dimension { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension2")]
        public int Dimension2 { get; set; }
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension3")]
        public int Dimension3 { get; set; }
        [Column("Value")]
        public double? Value { get; set; }
        [Column("Comments/Source")]
        public string CommentsSource { get; set; }
    }

    [Table("GlobalParametersMultHist")]
    public partial class GlobalParametersMultHist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension")]
        public int Dimension { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension2")]
        public int Dimension2 { get; set; }
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension3")]
        public int Dimension3 { get; set; }
        [Column("Value")]
        public double? Value { get; set; }
        [Column("Comments/Source")]
        public string CommentsSource { get; set; }
    }

    [Table("GlobalParametersMultiple")]
    public partial class GlobalParametersMultiple
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WAP1")]
        public double? WAP1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WAP2")]
        public double? WAP2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WAP3")]
        public double? WAP3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WORLDAP1")]
        public double? WORLDAP1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WORLDAP2")]
        public double? WORLDAP2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI1")]
        public double? RDI1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI2")]
        public double? RDI2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI3")]
        public double? RDI3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI4")]
        public double? RDI4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR1")]
        public double? RDINR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR2")]
        public double? RDINR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR3")]
        public double? RDINR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR4")]
        public double? RDINR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE1")]
        public double? LKE1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE2")]
        public double? LKE2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE3")]
        public double? LKE3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE4")]
        public double? LKE4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE5")]
        public double? LKE5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE6")]
        public double? LKE6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS1")]
        public double? LKS1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS2")]
        public double? LKS2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS3")]
        public double? LKS3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS4")]
        public double? LKS4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS5")]
        public double? LKS5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS6")]
        public double? LKS6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV1")]
        public double? ETECHADV1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV2")]
        public double? ETECHADV2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV3")]
        public double? ETECHADV3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV4")]
        public double? ETECHADV4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV5")]
        public double? ETECHADV5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV6")]
        public double? ETECHADV6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV7")]
        public double? ETECHADV7 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR1")]
        public double? ELASTRPR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR2")]
        public double? ELASTRPR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR3")]
        public double? ELASTRPR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR4")]
        public double? ELASTRPR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR5")]
        public double? ELASTRPR5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR6")]
        public double? ELASTRPR6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR1")]
        public double? MFPLEADR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR2")]
        public double? MFPLEADR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR3")]
        public double? MFPLEADR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR4")]
        public double? MFPLEADR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR5")]
        public double? MFPLEADR5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR6")]
        public double? MFPLEADR6 { get; set; }
    }

    [Table("GlobalParametersMultipleHist")]
    public partial class GlobalParametersMultipleHist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WAP1")]
        public double? WAP1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WAP2")]
        public double? WAP2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WORLDAP1")]
        public double? WORLDAP1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WORLDAP2")]
        public double? WORLDAP2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI1")]
        public double? RDI1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI2")]
        public double? RDI2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI3")]
        public double? RDI3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDI4")]
        public double? RDI4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR1")]
        public double? RDINR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR2")]
        public double? RDINR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR3")]
        public double? RDINR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RDINR4")]
        public double? RDINR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE1")]
        public double? LKE1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE2")]
        public double? LKE2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE3")]
        public double? LKE3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE4")]
        public double? LKE4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE5")]
        public double? LKE5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKE6")]
        public double? LKE6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS1")]
        public double? LKS1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS2")]
        public double? LKS2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS3")]
        public double? LKS3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS4")]
        public double? LKS4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS5")]
        public double? LKS5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("LKS6")]
        public double? LKS6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV1")]
        public double? ETECHADV1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV2")]
        public double? ETECHADV2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV3")]
        public double? ETECHADV3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV4")]
        public double? ETECHADV4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV5")]
        public double? ETECHADV5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV6")]
        public double? ETECHADV6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ETECHADV7")]
        public double? ETECHADV7 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR1")]
        public double? ELASTRPR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR2")]
        public double? ELASTRPR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR3")]
        public double? ELASTRPR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR4")]
        public double? ELASTRPR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR5")]
        public double? ELASTRPR5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ELASTRPR6")]
        public double? ELASTRPR6 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR1")]
        public double? MFPLEADR1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR2")]
        public double? MFPLEADR2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR3")]
        public double? MFPLEADR3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR4")]
        public double? MFPLEADR4 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR5")]
        public double? MFPLEADR5 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("MFPLEADR6")]
        public double? MFPLEADR6 { get; set; }
    }

    [Table("GlobalParametersMultipleCols")]
    public partial class GlobalParametersMultipleCols
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Position")]
        public int Position { get; set; }
        [Column("WPWGHTPOW")]
        public double? WPWGHTPOW { get; set; }
        [Column("XWBSECTAR")]
        public double? XWBSECTAR { get; set; }
        [Column("QE")]
        public double? QE { get; set; }
    }

    [Table("GlobalParametersHist")]
    public partial class GlobalParametersHist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension")]
        public int Dimension { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Dimension2")]
        public int Dimension2 { get; set; }
        [Column("Value")]
        public double? Value { get; set; }
    }

    [Table("GlobalParametersPolityRegions")]
    public partial class GlobalParametersPolityRegions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SwingStateRegion")]
        public int SwingStateRegion { get; set; }
        [Column("GlobalAverage")]
        public float? GlobalAverage { get; set; }
        [Column("SwingState")]
        public float? SwingState { get; set; }
        [Column("RegionalAverage")]
        public float? RegionalAverage { get; set; }
    }

    [Table("GlobalParametersTimeSeriesLong")]
    public partial class GlobalParametersTimeSeriesLong
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DataYear")]
        public int DataYear { get; set; }
        [Column("AtmosphericCO2PPM")]
        public double? AtmosphericCO2PPM { get; set; }
        [Column("Source")]
        public string Source { get; set; }
        [Column("AnnWorldEmsC")]
        public double? AnnWorldEmsC { get; set; }
    }

    //jrs 2022/05/02
    [Table("EnergyNoProductionCountries")]
    public partial class EnergyNoProductionCountries
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CountryName")]
        public string CountryName { get; set; }
    }

    public abstract partial class CohortRegion : ICohortRegion
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")]
        public int RegionId { get; set; }
        [Column("Cohort0")]
        public double? Cohort0 { get; set; }
        [Column("Cohort1")]
        public double? Cohort1 { get; set; }
        [Column("Cohort2")]
        public double? Cohort2 { get; set; }
        [Column("Cohort3")]
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("Cohort0", Cohort0);
            yield return new KeyValuePair<string, double?>("Cohort1", Cohort1);
            yield return new KeyValuePair<string, double?>("Cohort2", Cohort2);
            yield return new KeyValuePair<string, double?>("Cohort3", Cohort3);
            yield return new KeyValuePair<string, double?>("Cohort4", Cohort4);
            yield return new KeyValuePair<string, double?>("Cohort5", Cohort5);
            yield return new KeyValuePair<string, double?>("Cohort6", Cohort6);
            yield return new KeyValuePair<string, double?>("Cohort7", Cohort7);
            yield return new KeyValuePair<string, double?>("Cohort8", Cohort8);
            yield return new KeyValuePair<string, double?>("Cohort9", Cohort9);
            yield return new KeyValuePair<string, double?>("Cohort10", Cohort10);
            yield return new KeyValuePair<string, double?>("Cohort11", Cohort11);
            yield return new KeyValuePair<string, double?>("Cohort12", Cohort12);
            yield return new KeyValuePair<string, double?>("Cohort13", Cohort13);
            yield return new KeyValuePair<string, double?>("Cohort14", Cohort14);
            yield return new KeyValuePair<string, double?>("Cohort15", Cohort15);
            yield return new KeyValuePair<string, double?>("Cohort16", Cohort16);
            yield return new KeyValuePair<string, double?>("Cohort17", Cohort17);
            yield return new KeyValuePair<string, double?>("Cohort18", Cohort18);
            yield return new KeyValuePair<string, double?>("Cohort19", Cohort19);
            yield return new KeyValuePair<string, double?>("Cohort20", Cohort20);
            yield return new KeyValuePair<string, double?>("Cohort21", Cohort21);
            yield return new KeyValuePair<string, double?>("Cohort22", Cohort22);
            yield return new KeyValuePair<string, double?>("Cohort23", Cohort23);
            yield return new KeyValuePair<string, double?>("Cohort24", Cohort24);
            yield return new KeyValuePair<string, double?>("Cohort25", Cohort25);
            yield return new KeyValuePair<string, double?>("Cohort26", Cohort26);
            yield return new KeyValuePair<string, double?>("Cohort27", Cohort27);
            yield return new KeyValuePair<string, double?>("Cohort28", Cohort28);
            yield return new KeyValuePair<string, double?>("Cohort29", Cohort29);
            yield return new KeyValuePair<string, double?>("Cohort30", Cohort30);
            yield return new KeyValuePair<string, double?>("Cohort31", Cohort31);
            yield return new KeyValuePair<string, double?>("Cohort32", Cohort32);
            yield return new KeyValuePair<string, double?>("Cohort33", Cohort33);
            yield return new KeyValuePair<string, double?>("Cohort34", Cohort34);
            yield return new KeyValuePair<string, double?>("Cohort35", Cohort35);
            yield return new KeyValuePair<string, double?>("Cohort36", Cohort36);
            yield return new KeyValuePair<string, double?>("Cohort37", Cohort37);
            yield return new KeyValuePair<string, double?>("Cohort38", Cohort38);
            yield return new KeyValuePair<string, double?>("Cohort39", Cohort39);
            yield return new KeyValuePair<string, double?>("Cohort40", Cohort40);
            yield return new KeyValuePair<string, double?>("Cohort41", Cohort41);
        }
    }

    public interface ICohortRegion
    {
        string RegionName { get; set; }
        int RegionId { get; set; }
        double? Cohort0 { get; set; }
        double? Cohort1 { get; set; }
        double? Cohort2 { get; set; }
        double? Cohort3 { get; set; }
        double? Cohort4 { get; set; }
        double? Cohort5 { get; set; }
        double? Cohort6 { get; set; }
        double? Cohort7 { get; set; }
        double? Cohort8 { get; set; }
        double? Cohort9 { get; set; }
        double? Cohort10 { get; set; }
        double? Cohort11 { get; set; }
        double? Cohort12 { get; set; }
        double? Cohort13 { get; set; }
        double? Cohort14 { get; set; }
        double? Cohort15 { get; set; }
        double? Cohort16 { get; set; }
        double? Cohort17 { get; set; }
        double? Cohort18 { get; set; }
        double? Cohort19 { get; set; }
        double? Cohort20 { get; set; }
        double? Cohort21 { get; set; }
        double? Cohort22 { get; set; }
        double? Cohort23 { get; set; }
        double? Cohort24 { get; set; }
        double? Cohort25 { get; set; }
        double? Cohort26 { get; set; }
        double? Cohort27 { get; set; }
        double? Cohort28 { get; set; }
        double? Cohort29 { get; set; }
        double? Cohort30 { get; set; }
        double? Cohort31 { get; set; }
        double? Cohort32 { get; set; }
        double? Cohort33 { get; set; }
        double? Cohort34 { get; set; }
        double? Cohort35 { get; set; }
        double? Cohort36 { get; set; }
        double? Cohort37 { get; set; }
        double? Cohort38 { get; set; }
        double? Cohort39 { get; set; }
        double? Cohort40 { get; set; }
        double? Cohort41 { get; set; }

    }

    public abstract partial class CohortRegion4 : ICohortRegion4
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")]
        public int RegionId { get; set; }
        [Column("Cohort3")] //jrs 2023/12/05
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("Cohort3", Cohort3); //jrs 2023/12/05
            yield return new KeyValuePair<string, double?>("Cohort4", Cohort4);
            yield return new KeyValuePair<string, double?>("Cohort5", Cohort5);
            yield return new KeyValuePair<string, double?>("Cohort6", Cohort6);
            yield return new KeyValuePair<string, double?>("Cohort7", Cohort7);
            yield return new KeyValuePair<string, double?>("Cohort8", Cohort8);
            yield return new KeyValuePair<string, double?>("Cohort9", Cohort9);
            yield return new KeyValuePair<string, double?>("Cohort10", Cohort10);
            yield return new KeyValuePair<string, double?>("Cohort11", Cohort11);
            yield return new KeyValuePair<string, double?>("Cohort12", Cohort12);
            yield return new KeyValuePair<string, double?>("Cohort13", Cohort13);
            yield return new KeyValuePair<string, double?>("Cohort14", Cohort14);
            yield return new KeyValuePair<string, double?>("Cohort15", Cohort15);
            yield return new KeyValuePair<string, double?>("Cohort16", Cohort16);
            yield return new KeyValuePair<string, double?>("Cohort17", Cohort17);
            yield return new KeyValuePair<string, double?>("Cohort18", Cohort18);
            yield return new KeyValuePair<string, double?>("Cohort19", Cohort19);
            yield return new KeyValuePair<string, double?>("Cohort20", Cohort20);
            yield return new KeyValuePair<string, double?>("Cohort21", Cohort21);
            yield return new KeyValuePair<string, double?>("Cohort22", Cohort22);
            yield return new KeyValuePair<string, double?>("Cohort23", Cohort23);
            yield return new KeyValuePair<string, double?>("Cohort24", Cohort24);
            yield return new KeyValuePair<string, double?>("Cohort25", Cohort25);
            yield return new KeyValuePair<string, double?>("Cohort26", Cohort26);
            yield return new KeyValuePair<string, double?>("Cohort27", Cohort27);
            yield return new KeyValuePair<string, double?>("Cohort28", Cohort28);
            yield return new KeyValuePair<string, double?>("Cohort29", Cohort29);
            yield return new KeyValuePair<string, double?>("Cohort30", Cohort30);
            yield return new KeyValuePair<string, double?>("Cohort31", Cohort31);
            yield return new KeyValuePair<string, double?>("Cohort32", Cohort32);
            yield return new KeyValuePair<string, double?>("Cohort33", Cohort33);
            yield return new KeyValuePair<string, double?>("Cohort34", Cohort34);
            yield return new KeyValuePair<string, double?>("Cohort35", Cohort35);
            yield return new KeyValuePair<string, double?>("Cohort36", Cohort36);
            yield return new KeyValuePair<string, double?>("Cohort37", Cohort37);
            yield return new KeyValuePair<string, double?>("Cohort38", Cohort38);
            yield return new KeyValuePair<string, double?>("Cohort39", Cohort39);
            yield return new KeyValuePair<string, double?>("Cohort40", Cohort40);
            yield return new KeyValuePair<string, double?>("Cohort41", Cohort41);
        }
    }

    public interface ICohortRegion4
    {
        string RegionName { get; set; }
        int RegionId { get; set; }
        double? Cohort3 { get; set; } //jrs 2023/12/05
        double? Cohort4 { get; set; }
        double? Cohort5 { get; set; }
        double? Cohort6 { get; set; }
        double? Cohort7 { get; set; }
        double? Cohort8 { get; set; }
        double? Cohort9 { get; set; }
        double? Cohort10 { get; set; }
        double? Cohort11 { get; set; }
        double? Cohort12 { get; set; }
        double? Cohort13 { get; set; }
        double? Cohort14 { get; set; }
        double? Cohort15 { get; set; }
        double? Cohort16 { get; set; }
        double? Cohort17 { get; set; }
        double? Cohort18 { get; set; }
        double? Cohort19 { get; set; }
        double? Cohort20 { get; set; }
        double? Cohort21 { get; set; }
        double? Cohort22 { get; set; }
        double? Cohort23 { get; set; }
        double? Cohort24 { get; set; }
        double? Cohort25 { get; set; }
        double? Cohort26 { get; set; }
        double? Cohort27 { get; set; }
        double? Cohort28 { get; set; }
        double? Cohort29 { get; set; }
        double? Cohort30 { get; set; }
        double? Cohort31 { get; set; }
        double? Cohort32 { get; set; }
        double? Cohort33 { get; set; }
        double? Cohort34 { get; set; }
        double? Cohort35 { get; set; }
        double? Cohort36 { get; set; }
        double? Cohort37 { get; set; }
        double? Cohort38 { get; set; }
        double? Cohort39 { get; set; }
        double? Cohort40 { get; set; }
        double? Cohort41 { get; set; }

    }
    public abstract partial class AgeSingleRegion : IAgeSingleRegion
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")] //jrs 2022/08/15
        public int RegionId { get; set; }
        [Column("Age0")]
        public double? Age0 { get; set; }
        [Column("Age1")]
        public double? Age1 { get; set; }
        [Column("Age2")]
        public double? Age2 { get; set; }
        [Column("Age3")]
        public double? Age3 { get; set; }
        [Column("Age4")]
        public double? Age4 { get; set; }
        [Column("Age5")]
        public double? Age5 { get; set; }
        [Column("Age6")]
        public double? Age6 { get; set; }
        [Column("Age7")]
        public double? Age7 { get; set; }
        [Column("Age8")]
        public double? Age8 { get; set; }
        [Column("Age9")]
        public double? Age9 { get; set; }
        [Column("Age10")]
        public double? Age10 { get; set; }
        [Column("Age11")]
        public double? Age11 { get; set; }
        [Column("Age12")]
        public double? Age12 { get; set; }
        [Column("Age13")]
        public double? Age13 { get; set; }
        [Column("Age14")]
        public double? Age14 { get; set; }
        [Column("Age15")]
        public double? Age15 { get; set; }
        [Column("Age16")]
        public double? Age16 { get; set; }
        [Column("Age17")]
        public double? Age17 { get; set; }
        [Column("Age18")]
        public double? Age18 { get; set; }
        [Column("Age19")]
        public double? Age19 { get; set; }
        [Column("Age20")]
        public double? Age20 { get; set; }
        [Column("Age21")]
        public double? Age21 { get; set; }
        [Column("Age22")]
        public double? Age22 { get; set; }
        [Column("Age23")]
        public double? Age23 { get; set; }
        [Column("Age24")]
        public double? Age24 { get; set; }
        [Column("Age25")]
        public double? Age25 { get; set; }
        [Column("Age26")]
        public double? Age26 { get; set; }
        [Column("Age27")]
        public double? Age27 { get; set; }
        [Column("Age28")]
        public double? Age28 { get; set; }
        [Column("Age29")]
        public double? Age29 { get; set; }
        [Column("Age30")]
        public double? Age30 { get; set; }
        [Column("Age31")]
        public double? Age31 { get; set; }
        [Column("Age32")]
        public double? Age32 { get; set; }
        [Column("Age33")]
        public double? Age33 { get; set; }
        [Column("Age34")]
        public double? Age34 { get; set; }
        [Column("Age35")]
        public double? Age35 { get; set; }
        [Column("Age36")]
        public double? Age36 { get; set; }
        [Column("Age37")]
        public double? Age37 { get; set; }
        [Column("Age38")]
        public double? Age38 { get; set; }
        [Column("Age39")]
        public double? Age39 { get; set; }
        [Column("Age40")]
        public double? Age40 { get; set; }
        [Column("Age41")]
        public double? Age41 { get; set; }
        [Column("Age42")]
        public double? Age42 { get; set; }
        [Column("Age43")]
        public double? Age43 { get; set; }
        [Column("Age44")]
        public double? Age44 { get; set; }
        [Column("Age45")]
        public double? Age45 { get; set; }
        [Column("Age46")]
        public double? Age46 { get; set; }
        [Column("Age47")]
        public double? Age47 { get; set; }
        [Column("Age48")]
        public double? Age48 { get; set; }
        [Column("Age49")]
        public double? Age49 { get; set; }
        [Column("Age50")]
        public double? Age50 { get; set; }
        [Column("Age51")]
        public double? Age51 { get; set; }
        [Column("Age52")]
        public double? Age52 { get; set; }
        [Column("Age53")]
        public double? Age53 { get; set; }
        [Column("Age54")]
        public double? Age54 { get; set; }
        [Column("Age55")]
        public double? Age55 { get; set; }
        [Column("Age56")]
        public double? Age56 { get; set; }
        [Column("Age57")]
        public double? Age57 { get; set; }
        [Column("Age58")]
        public double? Age58 { get; set; }
        [Column("Age59")]
        public double? Age59 { get; set; }
        [Column("Age60")]
        public double? Age60 { get; set; }
        [Column("Age61")]
        public double? Age61 { get; set; }
        [Column("Age62")]
        public double? Age62 { get; set; }
        [Column("Age63")]
        public double? Age63 { get; set; }
        [Column("Age64")]
        public double? Age64 { get; set; }
        [Column("Age65")]
        public double? Age65 { get; set; }
        [Column("Age66")]
        public double? Age66 { get; set; }
        [Column("Age67")]
        public double? Age67 { get; set; }
        [Column("Age68")]
        public double? Age68 { get; set; }
        [Column("Age69")]
        public double? Age69 { get; set; }
        [Column("Age70")]
        public double? Age70 { get; set; }
        [Column("Age71")]
        public double? Age71 { get; set; }
        [Column("Age72")]
        public double? Age72 { get; set; }
        [Column("Age73")]
        public double? Age73 { get; set; }
        [Column("Age74")]
        public double? Age74 { get; set; }
        [Column("Age75")]
        public double? Age75 { get; set; }
        [Column("Age76")]
        public double? Age76 { get; set; }
        [Column("Age77")]
        public double? Age77 { get; set; }
        [Column("Age78")]
        public double? Age78 { get; set; }
        [Column("Age79")]
        public double? Age79 { get; set; }
        [Column("Age80")]
        public double? Age80 { get; set; }
        [Column("Age81")]
        public double? Age81 { get; set; }
        [Column("Age82")]
        public double? Age82 { get; set; }
        [Column("Age83")]
        public double? Age83 { get; set; }
        [Column("Age84")]
        public double? Age84 { get; set; }
        [Column("Age85")]
        public double? Age85 { get; set; }
        [Column("Age86")]
        public double? Age86 { get; set; }
        [Column("Age87")]
        public double? Age87 { get; set; }
        [Column("Age88")]
        public double? Age88 { get; set; }
        [Column("Age89")]
        public double? Age89 { get; set; }
        [Column("Age90")]
        public double? Age90 { get; set; }
        [Column("Age91")]
        public double? Age91 { get; set; }
        [Column("Age92")]
        public double? Age92 { get; set; }
        [Column("Age93")]
        public double? Age93 { get; set; }
        [Column("Age94")]
        public double? Age94 { get; set; }
        [Column("Age95")]
        public double? Age95 { get; set; }
        [Column("Age96")]
        public double? Age96 { get; set; }
        [Column("Age97")]
        public double? Age97 { get; set; }
        [Column("Age98")]
        public double? Age98 { get; set; }
        [Column("Age99")]
        public double? Age99 { get; set; }
        [Column("Age100")]
        public double? Age100 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Ages
        {
            get
            {
                return allAges().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allAges()
        {
            yield return new KeyValuePair<string, double?>("Age0", Age0);
            yield return new KeyValuePair<string, double?>("Age1", Age1);
            yield return new KeyValuePair<string, double?>("Age2", Age2);
            yield return new KeyValuePair<string, double?>("Age3", Age3);
            yield return new KeyValuePair<string, double?>("Age4", Age4);
            yield return new KeyValuePair<string, double?>("Age5", Age5);
            yield return new KeyValuePair<string, double?>("Age6", Age6);
            yield return new KeyValuePair<string, double?>("Age7", Age7);
            yield return new KeyValuePair<string, double?>("Age8", Age8);
            yield return new KeyValuePair<string, double?>("Age9", Age9);
            yield return new KeyValuePair<string, double?>("Age10", Age10);
            yield return new KeyValuePair<string, double?>("Age11", Age11);
            yield return new KeyValuePair<string, double?>("Age12", Age12);
            yield return new KeyValuePair<string, double?>("Age13", Age13);
            yield return new KeyValuePair<string, double?>("Age14", Age14);
            yield return new KeyValuePair<string, double?>("Age15", Age15);
            yield return new KeyValuePair<string, double?>("Age16", Age16);
            yield return new KeyValuePair<string, double?>("Age17", Age17);
            yield return new KeyValuePair<string, double?>("Age18", Age18);
            yield return new KeyValuePair<string, double?>("Age19", Age19);
            yield return new KeyValuePair<string, double?>("Age20", Age20);
            yield return new KeyValuePair<string, double?>("Age21", Age21);
            yield return new KeyValuePair<string, double?>("Age22", Age22);
            yield return new KeyValuePair<string, double?>("Age23", Age23);
            yield return new KeyValuePair<string, double?>("Age24", Age24);
            yield return new KeyValuePair<string, double?>("Age25", Age25);
            yield return new KeyValuePair<string, double?>("Age26", Age26);
            yield return new KeyValuePair<string, double?>("Age27", Age27);
            yield return new KeyValuePair<string, double?>("Age28", Age28);
            yield return new KeyValuePair<string, double?>("Age29", Age29);
            yield return new KeyValuePair<string, double?>("Age30", Age30);
            yield return new KeyValuePair<string, double?>("Age31", Age31);
            yield return new KeyValuePair<string, double?>("Age32", Age32);
            yield return new KeyValuePair<string, double?>("Age33", Age33);
            yield return new KeyValuePair<string, double?>("Age34", Age34);
            yield return new KeyValuePair<string, double?>("Age35", Age35);
            yield return new KeyValuePair<string, double?>("Age36", Age36);
            yield return new KeyValuePair<string, double?>("Age37", Age37);
            yield return new KeyValuePair<string, double?>("Age38", Age38);
            yield return new KeyValuePair<string, double?>("Age39", Age39);
            yield return new KeyValuePair<string, double?>("Age40", Age40);
            yield return new KeyValuePair<string, double?>("Age41", Age41);
            yield return new KeyValuePair<string, double?>("Age42", Age42);
            yield return new KeyValuePair<string, double?>("Age43", Age43);
            yield return new KeyValuePair<string, double?>("Age44", Age44);
            yield return new KeyValuePair<string, double?>("Age45", Age45);
            yield return new KeyValuePair<string, double?>("Age46", Age46);
            yield return new KeyValuePair<string, double?>("Age47", Age47);
            yield return new KeyValuePair<string, double?>("Age48", Age48);
            yield return new KeyValuePair<string, double?>("Age49", Age49);
            yield return new KeyValuePair<string, double?>("Age50", Age50);
            yield return new KeyValuePair<string, double?>("Age51", Age51);
            yield return new KeyValuePair<string, double?>("Age52", Age52);
            yield return new KeyValuePair<string, double?>("Age53", Age53);
            yield return new KeyValuePair<string, double?>("Age54", Age54);
            yield return new KeyValuePair<string, double?>("Age55", Age55);
            yield return new KeyValuePair<string, double?>("Age56", Age56);
            yield return new KeyValuePair<string, double?>("Age57", Age57);
            yield return new KeyValuePair<string, double?>("Age58", Age58);
            yield return new KeyValuePair<string, double?>("Age59", Age59);
            yield return new KeyValuePair<string, double?>("Age60", Age60);
            yield return new KeyValuePair<string, double?>("Age61", Age61);
            yield return new KeyValuePair<string, double?>("Age62", Age62);
            yield return new KeyValuePair<string, double?>("Age63", Age63);
            yield return new KeyValuePair<string, double?>("Age64", Age64);
            yield return new KeyValuePair<string, double?>("Age65", Age65);
            yield return new KeyValuePair<string, double?>("Age66", Age66);
            yield return new KeyValuePair<string, double?>("Age67", Age67);
            yield return new KeyValuePair<string, double?>("Age68", Age68);
            yield return new KeyValuePair<string, double?>("Age69", Age69);
            yield return new KeyValuePair<string, double?>("Age70", Age70);
            yield return new KeyValuePair<string, double?>("Age71", Age71);
            yield return new KeyValuePair<string, double?>("Age72", Age72);
            yield return new KeyValuePair<string, double?>("Age73", Age73);
            yield return new KeyValuePair<string, double?>("Age74", Age74);
            yield return new KeyValuePair<string, double?>("Age75", Age75);
            yield return new KeyValuePair<string, double?>("Age76", Age76);
            yield return new KeyValuePair<string, double?>("Age77", Age77);
            yield return new KeyValuePair<string, double?>("Age78", Age78);
            yield return new KeyValuePair<string, double?>("Age79", Age79);
            yield return new KeyValuePair<string, double?>("Age80", Age80);
            yield return new KeyValuePair<string, double?>("Age81", Age81);
            yield return new KeyValuePair<string, double?>("Age82", Age82);
            yield return new KeyValuePair<string, double?>("Age83", Age83);
            yield return new KeyValuePair<string, double?>("Age84", Age84);
            yield return new KeyValuePair<string, double?>("Age85", Age85);
            yield return new KeyValuePair<string, double?>("Age86", Age86);
            yield return new KeyValuePair<string, double?>("Age87", Age87);
            yield return new KeyValuePair<string, double?>("Age88", Age88);
            yield return new KeyValuePair<string, double?>("Age89", Age89);
            yield return new KeyValuePair<string, double?>("Age90", Age90);
            yield return new KeyValuePair<string, double?>("Age91", Age91);
            yield return new KeyValuePair<string, double?>("Age92", Age92);
            yield return new KeyValuePair<string, double?>("Age93", Age93);
            yield return new KeyValuePair<string, double?>("Age94", Age94);
            yield return new KeyValuePair<string, double?>("Age95", Age95);
            yield return new KeyValuePair<string, double?>("Age96", Age96);
            yield return new KeyValuePair<string, double?>("Age97", Age97);
            yield return new KeyValuePair<string, double?>("Age98", Age98);
            yield return new KeyValuePair<string, double?>("Age99", Age99);
            yield return new KeyValuePair<string, double?>("Age100", Age100);
        }
    }

    public interface IAgeSingleRegion
    {
        string RegionName { get; set; }
        int RegionId { get; set; }
        double? Age0 { get; set; }
        double? Age1 { get; set; }
        double? Age2 { get; set; }
        double? Age3 { get; set; }
        double? Age4 { get; set; }
        double? Age5 { get; set; }
        double? Age6 { get; set; }
        double? Age7 { get; set; }
        double? Age8 { get; set; }
        double? Age9 { get; set; }
        double? Age10 { get; set; }
        double? Age11 { get; set; }
        double? Age12 { get; set; }
        double? Age13 { get; set; }
        double? Age14 { get; set; }
        double? Age15 { get; set; }
        double? Age16 { get; set; }
        double? Age17 { get; set; }
        double? Age18 { get; set; }
        double? Age19 { get; set; }
        double? Age20 { get; set; }
        double? Age21 { get; set; }
        double? Age22 { get; set; }
        double? Age23 { get; set; }
        double? Age24 { get; set; }
        double? Age25 { get; set; }
        double? Age26 { get; set; }
        double? Age27 { get; set; }
        double? Age28 { get; set; }
        double? Age29 { get; set; }
        double? Age30 { get; set; }
        double? Age31 { get; set; }
        double? Age32 { get; set; }
        double? Age33 { get; set; }
        double? Age34 { get; set; }
        double? Age35 { get; set; }
        double? Age36 { get; set; }
        double? Age37 { get; set; }
        double? Age38 { get; set; }
        double? Age39 { get; set; }
        double? Age40 { get; set; }
        double? Age41 { get; set; }
        double? Age42 { get; set; }
        double? Age43 { get; set; }
        double? Age44 { get; set; }
        double? Age45 { get; set; }
        double? Age46 { get; set; }
        double? Age47 { get; set; }
        double? Age48 { get; set; }
        double? Age49 { get; set; }
        double? Age50 { get; set; }
        double? Age51 { get; set; }
        double? Age52 { get; set; }
        double? Age53 { get; set; }
        double? Age54 { get; set; }
        double? Age55 { get; set; }
        double? Age56 { get; set; }
        double? Age57 { get; set; }
        double? Age58 { get; set; }
        double? Age59 { get; set; }
        double? Age60 { get; set; }
        double? Age61 { get; set; }
        double? Age62 { get; set; }
        double? Age63 { get; set; }
        double? Age64 { get; set; }
        double? Age65 { get; set; }
        double? Age66 { get; set; }
        double? Age67 { get; set; }
        double? Age68 { get; set; }
        double? Age69 { get; set; }
        double? Age70 { get; set; }
        double? Age71 { get; set; }
        double? Age72 { get; set; }
        double? Age73 { get; set; }
        double? Age74 { get; set; }
        double? Age75 { get; set; }
        double? Age76 { get; set; }
        double? Age77 { get; set; }
        double? Age78 { get; set; }
        double? Age79 { get; set; }
        double? Age80 { get; set; }
        double? Age81 { get; set; }
        double? Age82 { get; set; }
        double? Age83 { get; set; }
        double? Age84 { get; set; }
        double? Age85 { get; set; }
        double? Age86 { get; set; }
        double? Age87 { get; set; }
        double? Age88 { get; set; }
        double? Age89 { get; set; }
        double? Age90 { get; set; }
        double? Age91 { get; set; }
        double? Age92 { get; set; }
        double? Age93 { get; set; }
        double? Age94 { get; set; }
        double? Age95 { get; set; }
        double? Age96 { get; set; }
        double? Age97 { get; set; }
        double? Age98 { get; set; }
        double? Age99 { get; set; }
        double? Age100 { get; set; }
    }

    [Table("PopAgeCohortRegionMale")]
    public partial class PopAgeCohortRegionMale : CohortRegion
    {
    }

    [Table("PopAgeSingleRegionMale")]
    public partial class PopAgeSingleRegionMale : AgeSingleRegion
    {
    }

    [Table("PopAgeCohortRegionMaleHist")]
    public partial class PopAgeCohortRegionMaleHist : CohortRegion
    {
    }

    [Table("PopAgeCohortRegionFemale")]
    public partial class PopAgeCohortRegionFemale : CohortRegion
    {
    }

    [Table("PopAgeSingleRegionFemale")]
    public partial class PopAgeSingleRegionFemale : AgeSingleRegion
    {
    }

    [Table("PopAgeCohortRegionFemaleHist")]
    public partial class PopAgeCohortRegionFemaleHist : CohortRegion
    {
    }

    [Table("PopFertilityCohortRegionFemale")]
    public partial class PopFertilityCohortRegionFemale : CohortRegion4
    {
    }

    [Table("PopFertilityCohortRegionFemaleHist")]
    public partial class PopFertilityCohortRegionFemaleHist : CohortRegion4
    {
    }

    [Table("PopMortalityCohortRegionMale")]
    public partial class PopMortalityCohortRegionMale : CohortRegion
    {
    }

    [Table("PopMortalityCohortRegionMaleHist")]
    public partial class PopMortalityCohortRegionMaleHist : CohortRegion
    {
    }

    [Table("PopMortalityCohortRegionFemale")]
    public partial class PopMortalityCohortRegionFemale : CohortRegion
    {
    }

    [Table("PopMortalityCohortRegionFemaleHist")]
    public partial class PopMortalityCohortRegionFemaleHist : CohortRegion
    {
    }

    [Table("HealthYLDComMatPerCohortRegionMale")]
    public partial class HealthYLDComMatPerCohortRegionMale : CohortRegion
    {
    }

    [Table("HealthYLDComMatPerCohortRegionFemale")]
    public partial class HealthYLDComMatPerCohortRegionFemale : CohortRegion
    {
    }

    [Table("HealthYLDNonComDisCohortRegionMale")]
    public partial class HealthYLDNonComDisCohortRegionMale : CohortRegion
    {
    }

    [Table("HealthYLDNonComDisCohortRegionFemale")]
    public partial class HealthYLDNonComDisCohortRegionFemale : CohortRegion
    {
    }

    [Table("HealthYLDInjuriesCohortRegionMale")]
    public partial class HealthYLDInjuriesCohortRegionMale : CohortRegion
    {
    }

    [Table("HealthYLDInjuriesCohortRegionFemale")]
    public partial class HealthYLDInjuriesCohortRegionFemale : CohortRegion
    {
    }

    public abstract partial class EduCohortRegion : IEduCohortRegion
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")]
        public int RegionId { get; set; }
        [Column("AgeGroup4")]
        public double? Cohort4 { get; set; }
        [Column("AgeGroup5")]
        public double? Cohort5 { get; set; }
        [Column("AgeGroup6")]
        public double? Cohort6 { get; set; }
        [Column("AgeGroup7")]
        public double? Cohort7 { get; set; }
        [Column("AgeGroup8")]
        public double? Cohort8 { get; set; }
        [Column("AgeGroup9")]
        public double? Cohort9 { get; set; }
        [Column("AgeGroup10")]
        public double? Cohort10 { get; set; }
        [Column("AgeGroup11")]
        public double? Cohort11 { get; set; }
        [Column("AgeGroup12")]
        public double? Cohort12 { get; set; }
        [Column("AgeGroup13")]
        public double? Cohort13 { get; set; }
        [Column("AgeGroup14")]
        public double? Cohort14 { get; set; }
        [Column("AgeGroup15")]
        public double? Cohort15 { get; set; }
        [Column("AgeGroup16")]
        public double? Cohort16 { get; set; }
        [Column("AgeGroup17")]
        public double? Cohort17 { get; set; }
        [Column("AgeGroup18")]
        public double? Cohort18 { get; set; }
        [Column("AgeGroup19")]
        public double? Cohort19 { get; set; }
        [Column("AgeGroup20")]
        public double? Cohort20 { get; set; }
        [Column("AgeGroup21")]
        public double? Cohort21 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("AgeGroup4", Cohort4);
            yield return new KeyValuePair<string, double?>("AgeGroup5", Cohort5);
            yield return new KeyValuePair<string, double?>("AgeGroup6", Cohort6);
            yield return new KeyValuePair<string, double?>("AgeGroup7", Cohort7);
            yield return new KeyValuePair<string, double?>("AgeGroup8", Cohort8);
            yield return new KeyValuePair<string, double?>("AgeGroup9", Cohort9);
            yield return new KeyValuePair<string, double?>("AgeGroup10", Cohort10);
            yield return new KeyValuePair<string, double?>("AgeGroup11", Cohort11);
            yield return new KeyValuePair<string, double?>("AgeGroup12", Cohort12);
            yield return new KeyValuePair<string, double?>("AgeGroup13", Cohort13);
            yield return new KeyValuePair<string, double?>("AgeGroup14", Cohort14);
            yield return new KeyValuePair<string, double?>("AgeGroup15", Cohort15);
            yield return new KeyValuePair<string, double?>("AgeGroup16", Cohort16);
            yield return new KeyValuePair<string, double?>("AgeGroup17", Cohort17);
            yield return new KeyValuePair<string, double?>("AgeGroup18", Cohort18);
            yield return new KeyValuePair<string, double?>("AgeGroup19", Cohort19);
            yield return new KeyValuePair<string, double?>("AgeGroup20", Cohort20);
            yield return new KeyValuePair<string, double?>("AgeGroup21", Cohort21);
        }
    }

    public interface IEduCohortRegion
    {
        string RegionName { get; set; }
        int RegionId { get; set; }
        double? Cohort4 { get; set; }
        double? Cohort5 { get; set; }
        double? Cohort6 { get; set; }
        double? Cohort7 { get; set; }
        double? Cohort8 { get; set; }
        double? Cohort9 { get; set; }
        double? Cohort10 { get; set; }
        double? Cohort11 { get; set; }
        double? Cohort12 { get; set; }
        double? Cohort13 { get; set; }
        double? Cohort14 { get; set; }
        double? Cohort15 { get; set; }
        double? Cohort16 { get; set; }
        double? Cohort17 { get; set; }
        double? Cohort18 { get; set; }
        double? Cohort19 { get; set; }
        double? Cohort20 { get; set; }
        double? Cohort21 { get; set; }
    }

    //mti 2024/05/23 start    
    [Table("EducAttainPrimaryFemaleWICRegion")]
    public partial class EducAttainPrimaryFemaleWICRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPrimaryMaleWICRegion")]
    public partial class EducAttainPrimaryMaleWICRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecUpperFemaleWICRegion")]
    public partial class EducAttainSecUpperFemaleWICRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecUpperMaleWICRegion")]
    public partial class EducAttainSecUpperMaleWICRegion : EduCohortRegion
    {
    }

    [Table("EducAttainTertiaryFemaleWICRegion")]
    public partial class EducAttainTertiaryFemaleWICRegion : EduCohortRegion
    {
    }

    [Table("EducAttainTertiaryMaleWICRegion")]
    public partial class EducAttainTertiaryMaleWICRegion : EduCohortRegion
    {
    }
    //mti 2024/05/23 end

    [Table("EducAttainNoEducMaleRegion")]
    public partial class EducAttainNoEducMaleRegion : EduCohortRegion
    {
    }



    [Table("EducAttainNoEducFemaleRegion")]
    public partial class EducAttainNoEducFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainNoEducTotalRegion")]
    public partial class EducAttainNoEducTotalRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriIncompleteMaleRegion")]
    public partial class EducAttainPriIncompleteMaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriIncompleteFemaleRegion")]
    public partial class EducAttainPriIncompleteFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriIncompleteTotalRegion")]
    public partial class EducAttainPriIncompleteTotalRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriCompleteMaleRegion")]
    public partial class EducAttainPriCompleteMaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriCompleteFemaleRegion")]
    public partial class EducAttainPriCompleteFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainPriCompleteTotalRegion")]
    public partial class EducAttainPriCompleteTotalRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecLowerMaleRegion")]
    public partial class EducAttainSecLowerMaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecLowerFemaleRegion")]
    public partial class EducAttainSecLowerFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecLowerTotalRegion")]
    public partial class EducAttainSecLowerTotalRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecUpperMaleRegion")]
    public partial class EducAttainSecUpperMaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecUpperFemaleRegion")]
    public partial class EducAttainSecUpperFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainSecUpperTotalRegion")]
    public partial class EducAttainSecUpperTotalRegion : EduCohortRegion
    {
    }

    [Table("EducAttainTertiaryMaleRegion")]
    public partial class EducAttainTertiaryMaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainTertiaryFemaleRegion")]
    public partial class EducAttainTertiaryFemaleRegion : EduCohortRegion
    {
    }

    [Table("EducAttainTertiaryTotalRegion")]
    public partial class EducAttainTertiaryTotalRegion : EduCohortRegion
    {
    }

    [Table("SeriesGDP2011")]
    public partial class SeriesRegGDP2011
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Column("1960")]
        public float? Val1960 { get; set; }
        [Column("1961")]
        public float? Val1961 { get; set; }
        [Column("1962")]
        public float? Val1962 { get; set; }
        [Column("1963")]
        public float? Val1963 { get; set; }
        [Column("1964")]
        public float? Val1964 { get; set; }
        [Column("1965")]
        public float? Val1965 { get; set; }
        [Column("1966")]
        public float? Val1966 { get; set; }
        [Column("1967")]
        public float? Val1967 { get; set; }
        [Column("1968")]
        public float? Val1968 { get; set; }
        [Column("1969")]
        public float? Val1969 { get; set; }
        [Column("1970")]
        public float? Val1970 { get; set; }
        [Column("1971")]
        public float? Val1971 { get; set; }
        [Column("1972")]
        public float? Val1972 { get; set; }
        [Column("1973")]
        public float? Val1973 { get; set; }
        [Column("1974")]
        public float? Val1974 { get; set; }
        [Column("1975")]
        public float? Val1975 { get; set; }
        [Column("1976")]
        public float? Val1976 { get; set; }
        [Column("1977")]
        public float? Val1977 { get; set; }
        [Column("1978")]
        public float? Val1978 { get; set; }
        [Column("1979")]
        public float? Val1979 { get; set; }
        [Column("1980")]
        public float? Val1980 { get; set; }
        [Column("1981")]
        public float? Val1981 { get; set; }
        [Column("1982")]
        public float? Val1982 { get; set; }
        [Column("1983")]
        public float? Val1983 { get; set; }
        [Column("1984")]
        public float? Val1984 { get; set; }
        [Column("1985")]
        public float? Val1985 { get; set; }
        [Column("1986")]
        public float? Val1986 { get; set; }
        [Column("1987")]
        public float? Val1987 { get; set; }
        [Column("1988")]
        public float? Val1988 { get; set; }
        [Column("1989")]
        public float? Val1989 { get; set; }
        [Column("1990")]
        public float? Val1990 { get; set; }
        [Column("1991")]
        public float? Val1991 { get; set; }
        [Column("1992")]
        public float? Val1992 { get; set; }
        [Column("1993")]
        public float? Val1993 { get; set; }
        [Column("1994")]
        public float? Val1994 { get; set; }
        [Column("1995")]
        public float? Val1995 { get; set; }
        [Column("1996")]
        public float? Val1996 { get; set; }
        [Column("1997")]
        public float? Val1997 { get; set; }
        [Column("1998")]
        public float? Val1998 { get; set; }
        [Column("1999")]
        public float? Val1999 { get; set; }
        [Column("2000")]
        public float? Val2000 { get; set; }
        [Column("2001")]
        public float? Val2001 { get; set; }
        [Column("2002")]
        public float? Val2002 { get; set; }
        [Column("2003")]
        public float? Val2003 { get; set; }
        [Column("2004")]
        public float? Val2004 { get; set; }
        [Column("2005")]
        public float? Val2005 { get; set; }
        [Column("2006")]
        public float? Val2006 { get; set; }
        [Column("2007")]
        public float? Val2007 { get; set; }
        [Column("2008")]
        public float? Val2008 { get; set; }
        [Column("2009")]
        public float? Val2009 { get; set; }
        [Column("2010")]
        public float? Val2010 { get; set; }
        [Column("2011")]
        public float? Val2011 { get; set; }
        [Column("2012")]
        public float? Val2012 { get; set; }
        [Column("2013")]
        public float? Val2013 { get; set; }
        [Column("2014")]
        public float? Val2014 { get; set; }
        [Column("2015")]
        public float? Val2015 { get; set; }
        [Column("2016")]
        public float? Val2016 { get; set; }
        [Column("2017")]
        public float? Val2017 { get; set; }
        [Column("2018")]
        public float? Val2018 { get; set; }
        [Column("2019")]
        public float? Val2019 { get; set; }
        [Column("2020")]
        public float? Val2020 { get; set; }
        [Column("2021")]
        public float? Val2021 { get; set; }
        [Column("2022")]
        public float? Val2022 { get; set; }
        [Column("2023")]
        public float? Val2023 { get; set; }
        [Column("2024")]
        public float? Val2024 { get; set; }
        [Column("2025")]
        public float? Val2025 { get; set; }
        [Column("Earliest")]
        public float? Earliest { get; set; }
        [Column("MostRecent")]
        public float? MostRecent { get; set; }
    }

    [Table("AgGlobalFeedtoDomSuppRatios")]
    public partial class AgGlobalFeedtoDomSuppRatios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("FishCategory")]
        public string FishCategory { get; set; }
        [Column("1961")]
        public float? Val1961 { get; set; }
        [Column("1962")]
        public float? Val1962 { get; set; }
        [Column("1963")]
        public float? Val1963 { get; set; }
        [Column("1964")]
        public float? Val1964 { get; set; }
        [Column("1965")]
        public float? Val1965 { get; set; }
        [Column("1966")]
        public float? Val1966 { get; set; }
        [Column("1967")]
        public float? Val1967 { get; set; }
        [Column("1968")]
        public float? Val1968 { get; set; }
        [Column("1969")]
        public float? Val1969 { get; set; }
        [Column("1970")]
        public float? Val1970 { get; set; }
        [Column("1971")]
        public float? Val1971 { get; set; }
        [Column("1972")]
        public float? Val1972 { get; set; }
        [Column("1973")]
        public float? Val1973 { get; set; }
        [Column("1974")]
        public float? Val1974 { get; set; }
        [Column("1975")]
        public float? Val1975 { get; set; }
        [Column("1976")]
        public float? Val1976 { get; set; }
        [Column("1977")]
        public float? Val1977 { get; set; }
        [Column("1978")]
        public float? Val1978 { get; set; }
        [Column("1979")]
        public float? Val1979 { get; set; }
        [Column("1980")]
        public float? Val1980 { get; set; }
        [Column("1981")]
        public float? Val1981 { get; set; }
        [Column("1982")]
        public float? Val1982 { get; set; }
        [Column("1983")]
        public float? Val1983 { get; set; }
        [Column("1984")]
        public float? Val1984 { get; set; }
        [Column("1985")]
        public float? Val1985 { get; set; }
        [Column("1986")]
        public float? Val1986 { get; set; }
        [Column("1987")]
        public float? Val1987 { get; set; }
        [Column("1988")]
        public float? Val1988 { get; set; }
        [Column("1989")]
        public float? Val1989 { get; set; }
        [Column("1990")]
        public float? Val1990 { get; set; }
        [Column("1991")]
        public float? Val1991 { get; set; }
        [Column("1992")]
        public float? Val1992 { get; set; }
        [Column("1993")]
        public float? Val1993 { get; set; }
        [Column("1994")]
        public float? Val1994 { get; set; }
        [Column("1995")]
        public float? Val1995 { get; set; }
        [Column("1996")]
        public float? Val1996 { get; set; }
        [Column("1997")]
        public float? Val1997 { get; set; }
        [Column("1998")]
        public float? Val1998 { get; set; }
        [Column("1999")]
        public float? Val1999 { get; set; }
        [Column("2000")]
        public float? Val2000 { get; set; }
        [Column("2001")]
        public float? Val2001 { get; set; }
        [Column("2002")]
        public float? Val2002 { get; set; }
        [Column("2003")]
        public float? Val2003 { get; set; }
        [Column("2004")]
        public float? Val2004 { get; set; }
        [Column("2005")]
        public float? Val2005 { get; set; }
        [Column("2006")]
        public float? Val2006 { get; set; }
        [Column("2007")]
        public float? Val2007 { get; set; }
        [Column("2008")]
        public float? Val2008 { get; set; }
        [Column("2009")]
        public float? Val2009 { get; set; }
        [Column("2010")]
        public float? Val2010 { get; set; }
        [Column("2011")]
        public float? Val2011 { get; set; }
        [Column("Earliest")]
        public float? Earliest { get; set; }
        [Column("MostRecent")]
        public float? MostRecent { get; set; }
    }

    [Table("GTAPSectorConversion")]
    public partial class GTAPSectorConversion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SectorNumber")]
        public int SectorNumber { get; set; }
        [Column("SectorAbbreviation")]
        public string SectorAbbreviation { get; set; }
        [Column("SectorName")]
        public string SectorName { get; set; }
        [Column("IFsSector")]
        public string IFsSector { get; set; }
    }

    public abstract partial class IOThrhld : IIOThrhld
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Threshold")]
        public int Threshold { get; set; }
    }

    public interface IIOThrhld
    {
        int Threshold { get; set; }
    }

    [Table("IOThresholds")]
    public partial class IOThresholds : IOThrhld
    {
    }

    [Table("IOThresholdsHist")]
    public partial class IOThresholdsHist : IOThrhld
    {
    }

    public abstract partial class GTAPIOAgg : IGTAPIOAgg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("Ints11")]
        public double? Ints11 { get; set; }
        [Column("Ints12")]
        public double? Ints12 { get; set; }
        [Column("Ints13")]
        public double? Ints13 { get; set; }
        [Column("Ints14")]
        public double? Ints14 { get; set; }
        [Column("Ints15")]
        public double? Ints15 { get; set; }
        [Column("Ints16")]
        public double? Ints16 { get; set; }
        [Column("Ints21")]
        public double? Ints21 { get; set; }
        [Column("Ints22")]
        public double? Ints22 { get; set; }
        [Column("Ints23")]
        public double? Ints23 { get; set; }
        [Column("Ints24")]
        public double? Ints24 { get; set; }
        [Column("Ints25")]
        public double? Ints25 { get; set; }
        [Column("Ints26")]
        public double? Ints26 { get; set; }
        [Column("Ints31")]
        public double? Ints31 { get; set; }
        [Column("Ints32")]
        public double? Ints32 { get; set; }
        [Column("Ints33")]
        public double? Ints33 { get; set; }
        [Column("Ints34")]
        public double? Ints34 { get; set; }
        [Column("Ints35")]
        public double? Ints35 { get; set; }
        [Column("Ints36")]
        public double? Ints36 { get; set; }
        [Column("Ints41")]
        public double? Ints41 { get; set; }
        [Column("Ints42")]
        public double? Ints42 { get; set; }
        [Column("Ints43")]
        public double? Ints43 { get; set; }
        [Column("Ints44")]
        public double? Ints44 { get; set; }
        [Column("Ints45")]
        public double? Ints45 { get; set; }
        [Column("Ints46")]
        public double? Ints46 { get; set; }
        [Column("Ints51")]
        public double? Ints51 { get; set; }
        [Column("Ints52")]
        public double? Ints52 { get; set; }
        [Column("Ints53")]
        public double? Ints53 { get; set; }
        [Column("Ints54")]
        public double? Ints54 { get; set; }
        [Column("Ints55")]
        public double? Ints55 { get; set; }
        [Column("Ints56")]
        public double? Ints56 { get; set; }
        [Column("Ints61")]
        public double? Ints61 { get; set; }
        [Column("Ints62")]
        public double? Ints62 { get; set; }
        [Column("Ints63")]
        public double? Ints63 { get; set; }
        [Column("Ints64")]
        public double? Ints64 { get; set; }
        [Column("Ints65")]
        public double? Ints65 { get; set; }
        [Column("Ints66")]
        public double? Ints66 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Ints
        {
            get
            {
                return allInts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allInts()
        {
            yield return new KeyValuePair<string, double?>("Ints11", Ints11);
            yield return new KeyValuePair<string, double?>("Ints12", Ints12);
            yield return new KeyValuePair<string, double?>("Ints13", Ints13);
            yield return new KeyValuePair<string, double?>("Ints14", Ints14);
            yield return new KeyValuePair<string, double?>("Ints15", Ints15);
            yield return new KeyValuePair<string, double?>("Ints16", Ints16);
            yield return new KeyValuePair<string, double?>("Ints21", Ints21);
            yield return new KeyValuePair<string, double?>("Ints22", Ints22);
            yield return new KeyValuePair<string, double?>("Ints23", Ints23);
            yield return new KeyValuePair<string, double?>("Ints24", Ints24);
            yield return new KeyValuePair<string, double?>("Ints25", Ints25);
            yield return new KeyValuePair<string, double?>("Ints26", Ints26);
            yield return new KeyValuePair<string, double?>("Ints31", Ints31);
            yield return new KeyValuePair<string, double?>("Ints32", Ints32);
            yield return new KeyValuePair<string, double?>("Ints33", Ints33);
            yield return new KeyValuePair<string, double?>("Ints34", Ints34);
            yield return new KeyValuePair<string, double?>("Ints35", Ints35);
            yield return new KeyValuePair<string, double?>("Ints36", Ints36);
            yield return new KeyValuePair<string, double?>("Ints41", Ints41);
            yield return new KeyValuePair<string, double?>("Ints42", Ints42);
            yield return new KeyValuePair<string, double?>("Ints43", Ints43);
            yield return new KeyValuePair<string, double?>("Ints44", Ints44);
            yield return new KeyValuePair<string, double?>("Ints45", Ints45);
            yield return new KeyValuePair<string, double?>("Ints46", Ints46);
            yield return new KeyValuePair<string, double?>("Ints51", Ints51);
            yield return new KeyValuePair<string, double?>("Ints52", Ints52);
            yield return new KeyValuePair<string, double?>("Ints53", Ints53);
            yield return new KeyValuePair<string, double?>("Ints54", Ints54);
            yield return new KeyValuePair<string, double?>("Ints55", Ints55);
            yield return new KeyValuePair<string, double?>("Ints56", Ints56);
            yield return new KeyValuePair<string, double?>("Ints61", Ints61);
            yield return new KeyValuePair<string, double?>("Ints62", Ints62);
            yield return new KeyValuePair<string, double?>("Ints63", Ints63);
            yield return new KeyValuePair<string, double?>("Ints64", Ints64);
            yield return new KeyValuePair<string, double?>("Ints65", Ints65);
            yield return new KeyValuePair<string, double?>("Ints66", Ints66);
        }
    }

    public interface IGTAPIOAgg
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        double? Ints11 { get; set; }
        double? Ints12 { get; set; }
        double? Ints13 { get; set; }
        double? Ints14 { get; set; }
        double? Ints15 { get; set; }
        double? Ints16 { get; set; }
        double? Ints21 { get; set; }
        double? Ints22 { get; set; }
        double? Ints23 { get; set; }
        double? Ints24 { get; set; }
        double? Ints25 { get; set; }
        double? Ints26 { get; set; }
        double? Ints31 { get; set; }
        double? Ints32 { get; set; }
        double? Ints33 { get; set; }
        double? Ints34 { get; set; }
        double? Ints35 { get; set; }
        double? Ints36 { get; set; }
        double? Ints41 { get; set; }
        double? Ints42 { get; set; }
        double? Ints43 { get; set; }
        double? Ints44 { get; set; }
        double? Ints45 { get; set; }
        double? Ints46 { get; set; }
        double? Ints51 { get; set; }
        double? Ints52 { get; set; }
        double? Ints53 { get; set; }
        double? Ints54 { get; set; }
        double? Ints55 { get; set; }
        double? Ints56 { get; set; }
        double? Ints61 { get; set; }
        double? Ints62 { get; set; }
        double? Ints63 { get; set; }
        double? Ints64 { get; set; }
        double? Ints65 { get; set; }
        double? Ints66 { get; set; }

    }

    [Table("GTAPIOAggregationCountry")]
    public partial class GTAPIOAggregationCountry : GTAPIOAgg
    {
    }

    [Table("GTAPIOAggregationCountryHist")]
    public partial class GTAPIOAggregationCountryHist : GTAPIOAgg
    {
    }

    public abstract partial class GTAPIOAggReg : IGTAPIOAggReg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")]
        public int Number { get; set; }
        [Column("Region")]
        public string Region { get; set; }
        [Column("Ints11")]
        public double? Ints11 { get; set; }
        [Column("Ints12")]
        public double? Ints12 { get; set; }
        [Column("Ints13")]
        public double? Ints13 { get; set; }
        [Column("Ints14")]
        public double? Ints14 { get; set; }
        [Column("Ints15")]
        public double? Ints15 { get; set; }
        [Column("Ints16")]
        public double? Ints16 { get; set; }
        [Column("Ints21")]
        public double? Ints21 { get; set; }
        [Column("Ints22")]
        public double? Ints22 { get; set; }
        [Column("Ints23")]
        public double? Ints23 { get; set; }
        [Column("Ints24")]
        public double? Ints24 { get; set; }
        [Column("Ints25")]
        public double? Ints25 { get; set; }
        [Column("Ints26")]
        public double? Ints26 { get; set; }
        [Column("Ints31")]
        public double? Ints31 { get; set; }
        [Column("Ints32")]
        public double? Ints32 { get; set; }
        [Column("Ints33")]
        public double? Ints33 { get; set; }
        [Column("Ints34")]
        public double? Ints34 { get; set; }
        [Column("Ints35")]
        public double? Ints35 { get; set; }
        [Column("Ints36")]
        public double? Ints36 { get; set; }
        [Column("Ints41")]
        public double? Ints41 { get; set; }
        [Column("Ints42")]
        public double? Ints42 { get; set; }
        [Column("Ints43")]
        public double? Ints43 { get; set; }
        [Column("Ints44")]
        public double? Ints44 { get; set; }
        [Column("Ints45")]
        public double? Ints45 { get; set; }
        [Column("Ints46")]
        public double? Ints46 { get; set; }
        [Column("Ints51")]
        public double? Ints51 { get; set; }
        [Column("Ints52")]
        public double? Ints52 { get; set; }
        [Column("Ints53")]
        public double? Ints53 { get; set; }
        [Column("Ints54")]
        public double? Ints54 { get; set; }
        [Column("Ints55")]
        public double? Ints55 { get; set; }
        [Column("Ints56")]
        public double? Ints56 { get; set; }
        [Column("Ints61")]
        public double? Ints61 { get; set; }
        [Column("Ints62")]
        public double? Ints62 { get; set; }
        [Column("Ints63")]
        public double? Ints63 { get; set; }
        [Column("Ints64")]
        public double? Ints64 { get; set; }
        [Column("Ints65")]
        public double? Ints65 { get; set; }
        [Column("Ints66")]
        public double? Ints66 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Ints
        {
            get
            {
                return allInts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allInts()
        {
            yield return new KeyValuePair<string, double?>("Ints11", Ints11);
            yield return new KeyValuePair<string, double?>("Ints12", Ints12);
            yield return new KeyValuePair<string, double?>("Ints13", Ints13);
            yield return new KeyValuePair<string, double?>("Ints14", Ints14);
            yield return new KeyValuePair<string, double?>("Ints15", Ints15);
            yield return new KeyValuePair<string, double?>("Ints16", Ints16);
            yield return new KeyValuePair<string, double?>("Ints21", Ints21);
            yield return new KeyValuePair<string, double?>("Ints22", Ints22);
            yield return new KeyValuePair<string, double?>("Ints23", Ints23);
            yield return new KeyValuePair<string, double?>("Ints24", Ints24);
            yield return new KeyValuePair<string, double?>("Ints25", Ints25);
            yield return new KeyValuePair<string, double?>("Ints26", Ints26);
            yield return new KeyValuePair<string, double?>("Ints31", Ints31);
            yield return new KeyValuePair<string, double?>("Ints32", Ints32);
            yield return new KeyValuePair<string, double?>("Ints33", Ints33);
            yield return new KeyValuePair<string, double?>("Ints34", Ints34);
            yield return new KeyValuePair<string, double?>("Ints35", Ints35);
            yield return new KeyValuePair<string, double?>("Ints36", Ints36);
            yield return new KeyValuePair<string, double?>("Ints41", Ints41);
            yield return new KeyValuePair<string, double?>("Ints42", Ints42);
            yield return new KeyValuePair<string, double?>("Ints43", Ints43);
            yield return new KeyValuePair<string, double?>("Ints44", Ints44);
            yield return new KeyValuePair<string, double?>("Ints45", Ints45);
            yield return new KeyValuePair<string, double?>("Ints46", Ints46);
            yield return new KeyValuePair<string, double?>("Ints51", Ints51);
            yield return new KeyValuePair<string, double?>("Ints52", Ints52);
            yield return new KeyValuePair<string, double?>("Ints53", Ints53);
            yield return new KeyValuePair<string, double?>("Ints54", Ints54);
            yield return new KeyValuePair<string, double?>("Ints55", Ints55);
            yield return new KeyValuePair<string, double?>("Ints56", Ints56);
            yield return new KeyValuePair<string, double?>("Ints61", Ints61);
            yield return new KeyValuePair<string, double?>("Ints62", Ints62);
            yield return new KeyValuePair<string, double?>("Ints63", Ints63);
            yield return new KeyValuePair<string, double?>("Ints64", Ints64);
            yield return new KeyValuePair<string, double?>("Ints65", Ints65);
            yield return new KeyValuePair<string, double?>("Ints66", Ints66);
        }
    }

    public interface IGTAPIOAggReg
    {
        int Number { get; set; }
        string Region { get; set; }
        double? Ints11 { get; set; }
        double? Ints12 { get; set; }
        double? Ints13 { get; set; }
        double? Ints14 { get; set; }
        double? Ints15 { get; set; }
        double? Ints16 { get; set; }
        double? Ints21 { get; set; }
        double? Ints22 { get; set; }
        double? Ints23 { get; set; }
        double? Ints24 { get; set; }
        double? Ints25 { get; set; }
        double? Ints26 { get; set; }
        double? Ints31 { get; set; }
        double? Ints32 { get; set; }
        double? Ints33 { get; set; }
        double? Ints34 { get; set; }
        double? Ints35 { get; set; }
        double? Ints36 { get; set; }
        double? Ints41 { get; set; }
        double? Ints42 { get; set; }
        double? Ints43 { get; set; }
        double? Ints44 { get; set; }
        double? Ints45 { get; set; }
        double? Ints46 { get; set; }
        double? Ints51 { get; set; }
        double? Ints52 { get; set; }
        double? Ints53 { get; set; }
        double? Ints54 { get; set; }
        double? Ints55 { get; set; }
        double? Ints56 { get; set; }
        double? Ints61 { get; set; }
        double? Ints62 { get; set; }
        double? Ints63 { get; set; }
        double? Ints64 { get; set; }
        double? Ints65 { get; set; }
        double? Ints66 { get; set; }

    }

    [Table("IFsIOCoefCalcBaseFull")]
    public partial class IFsIOCoefCalcBaseFull : GTAPIOAggReg
    {
    }

    [Table("IFsIOCoefCalcBaseFullHist")]
    public partial class IFsIOCoefCalcBaseFullHist : GTAPIOAggReg
    {
    }

    public abstract partial class GTAPValueAdd : IGTAPValueAdd
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("Land1")]
        public double? Land1 { get; set; }
        [Column("Land2")]
        public double? Land2 { get; set; }
        [Column("Land3")]
        public double? Land3 { get; set; }
        [Column("Land4")]
        public double? Land4 { get; set; }
        [Column("Land5")]
        public double? Land5 { get; set; }
        [Column("Land6")]
        public double? Land6 { get; set; }
        [Column("UnskilledLabor1")]
        public double? UnskilledLabor1 { get; set; }
        [Column("UnskilledLabor2")]
        public double? UnskilledLabor2 { get; set; }
        [Column("UnskilledLabor3")]
        public double? UnskilledLabor3 { get; set; }
        [Column("UnskilledLabor4")]
        public double? UnskilledLabor4 { get; set; }
        [Column("UnskilledLabor5")]
        public double? UnskilledLabor5 { get; set; }
        [Column("UnskilledLabor6")]
        public double? UnskilledLabor6 { get; set; }
        [Column("SkilledLabor1")]
        public double? SkilledLabor1 { get; set; }
        [Column("SkilledLabor2")]
        public double? SkilledLabor2 { get; set; }
        [Column("SkilledLabor3")]
        public double? SkilledLabor3 { get; set; }
        [Column("SkilledLabor4")]
        public double? SkilledLabor4 { get; set; }
        [Column("SkilledLabor5")]
        public double? SkilledLabor5 { get; set; }
        [Column("SkilledLabor6")]
        public double? SkilledLabor6 { get; set; }
        [Column("Capital1")]
        public double? Capital1 { get; set; }
        [Column("Capital2")]
        public double? Capital2 { get; set; }
        [Column("Capital3")]
        public double? Capital3 { get; set; }
        [Column("Capital4")]
        public double? Capital4 { get; set; }
        [Column("Capital5")]
        public double? Capital5 { get; set; }
        [Column("Capital6")]
        public double? Capital6 { get; set; }
    }

    public interface IGTAPValueAdd
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        double? Land1 { get; set; }
        double? Land2 { get; set; }
        double? Land3 { get; set; }
        double? Land4 { get; set; }
        double? Land5 { get; set; }
        double? Land6 { get; set; }
        double? UnskilledLabor1 { get; set; }
        double? UnskilledLabor2 { get; set; }
        double? UnskilledLabor3 { get; set; }
        double? UnskilledLabor4 { get; set; }
        double? UnskilledLabor5 { get; set; }
        double? UnskilledLabor6 { get; set; }
        double? SkilledLabor1 { get; set; }
        double? SkilledLabor2 { get; set; }
        double? SkilledLabor3 { get; set; }
        double? SkilledLabor4 { get; set; }
        double? SkilledLabor5 { get; set; }
        double? SkilledLabor6 { get; set; }
        double? Capital1 { get; set; }
        double? Capital2 { get; set; }
        double? Capital3 { get; set; }
        double? Capital4 { get; set; }
        double? Capital5 { get; set; }
        double? Capital6 { get; set; }
    }

    [Table("GTAPValueAddedCountry")]
    public partial class GTAPValueAddedCountry : GTAPValueAdd
    {
    }

    [Table("GTAPValueAddedCountryHist")]
    public partial class GTAPValueAddedCountryHist : GTAPValueAdd
    {
    }

    [Table("IFsIOCoefCalcCountry")]
    public partial class IFsIOCoefCalcCountry : GTAPIOAgg
    {
    }

    [Table("IFsIOCoefCalcCountryHist")]
    public partial class IFsIOCoefCalcCountryHist : GTAPIOAgg
    {
    }

    [Table("IFsIOCoefCalcCountryEnAdj")]
    public partial class IFsIOCoefCalcCountryEnAdj : GTAPIOAgg
    {
    }

    [Table("IFsIOCoefCalcCountryEnAdjHist")]
    public partial class IFsIOCoefCalcCountryEnAdjHist : GTAPIOAgg
    {
    }

    public abstract partial class IOMatCalc : IIOMatCalc
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Matrix")]
        public int Matrix { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RowSector")]
        public int RowSector { get; set; }
        [Column("AG Sector")]
        public double? AGSector { get; set; }
        [Column("RM Sector")]
        public double? RMSector { get; set; }
        [Column("PE Sector")]
        public double? PESector { get; set; }
        [Column("MN Sector")]
        public double? MNSector { get; set; }
        [Column("SR Sector")]
        public double? SRSector { get; set; }
        [Column("IC Sector")]
        public double? ICSector { get; set; }
        [Column("Threshold")]
        public double? Threshold { get; set; }
    }

    public interface IIOMatCalc
    {
        int Matrix { get; set; }
        int RowSector { get; set; }
        double? AGSector { get; set; }
        double? RMSector { get; set; }
        double? PESector { get; set; }
        double? MNSector { get; set; }
        double? SRSector { get; set; }
        double? ICSector { get; set; }
        double? Threshold { get; set; }
    }

    [Table("IOMatricesCalc")]
    public partial class IOMatricesCalc : IOMatCalc
    {
    }

    [Table("IOMatricesCalcHist")]
    public partial class IOMatricesCalcHist : IOMatCalc
    {
    }

    public abstract partial class IOLabC : IIOLabC
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Coefficient")]
        public string Coefficient { get; set; }
        [Column("AG Sector")]
        public double? AGSector { get; set; }
        [Column("RM Sector")]
        public double? RMSector { get; set; }
        [Column("PE Sector")]
        public double? PESector { get; set; }
        [Column("MN Sector")]
        public double? MNSector { get; set; }
        [Column("SR Sector")]
        public double? SRSector { get; set; }
        [Column("IC Sector")]
        public double? ICSector { get; set; }
        [Column("Threshold")]
        public double? Threshold { get; set; }
    }

    public interface IIOLabC
    {
        string Coefficient { get; set; }
        double? AGSector { get; set; }
        double? RMSector { get; set; }
        double? PESector { get; set; }
        double? MNSector { get; set; }
        double? SRSector { get; set; }
        double? ICSector { get; set; }
        double? Threshold { get; set; }
    }

    [Table("IOLaborCoefs")]
    public partial class IOLaborCoefs : IOLabC
    {
    }

    [Table("IOLaborCoefsHist")]
    public partial class IOLaborCoefsHist : IOLabC
    {
    }

    [Table("Country Translation")]
    public partial class CountryTranslation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("GTAP Country Codes")]
        public string GTAPCountryCodes { get; set; }
        [Column("COW Codes")]
        public string COWCodes { get; set; }
        [Column("COW Numbers")]
        public string COWNumbers { get; set; }
        [Column("ESRI Countries")]
        public string ESRICountries { get; set; }
        [Column("IFs Countries Forecast")]
        public string IFsCountriesForecast { get; set; }        
    }

    [Table("Economic")]
    public partial class Economic
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("GDPGRBal")]
        public float? GDPGRBal { get; set; }
        [Column("IGDPR")]
        public float? IGDPR { get; set; }
    }

    [Table("SwingStates")]
    public partial class SwingStates
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("Brazil")]
        public float? Brazil { get; set; }
        [Column("Indonesia")]
        public float? Indonesia { get; set; }
        [Column("Mexico")]
        public float? Mexico { get; set; }
        [Column("Nigeria")]
        public float? Nigeria { get; set; }
        [Column("Pakistan")]
        public float? Pakistan { get; set; }
        [Column("Russia")]
        public float? Russia { get; set; }
        [Column("SouthAfrica")]
        public float? SouthAfrica { get; set; }
        [Column("Turkey")]
        public float? Turkey { get; set; }
        [Column("Ukraine")]
        public float? Ukraine { get; set; }
    }

    [Table("AIInit")]
    public partial class AIInit
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("AIType")]
        public string AIType { get; set; }
        [Column("AITypeIdx")]
        public int AITypeIdx { get; set; }
        [Column("Year")]
        public int Year { get; set; }
        [Column("Value")]
        public float? Value { get; set; }
        [Column("GrowthRate")]
        public float? GrowthRate { get; set; }
    }

    [Table("Contiguity")]
    public partial class Contiguity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region")]
        public string Region { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region_Adjacent")]
        public string Region_Adjacent { get; set; }
        [Column("Distance")]
        public int Distance { get; set; }
    }

    [Table("TerritorialDisputes")]
    public partial class TerritorialDisputes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region")]
        public string Region { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region_Dispute")]
        public string Region_Dispute { get; set; }
        [Column("Magnitude")]
        public int Magnitude { get; set; }
    }

    [Table("CountryWasColonyOf")]
    public partial class CountryWasColonyOf
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("ukcol")]
        public double? ukcol { get; set; }
        [Column("fracol")]
        public double? fracol { get; set; }
        [Column("gercol")]
        public double? gercol { get; set; }
        [Column("porcol")]
        public double? porcol { get; set; }
        [Column("spacol")]
        public double? spacol { get; set; }
        [Column("netcol")]
        public double? netcol { get; set; }
        [Column("ruscol")]
        public double? ruscol { get; set; }
    }

    [Table("RegionWasColonyOf")]
    public partial class RegionWasColonyOf
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Column("ukcol")]
        public double? ukcol { get; set; }
        [Column("fracol")]
        public double? fracol { get; set; }
        [Column("gercol")]
        public double? gercol { get; set; }
        [Column("porcol")]
        public double? porcol { get; set; }
        [Column("spacol")]
        public double? spacol { get; set; }
        [Column("netcol")]
        public double? netcol { get; set; }
        [Column("ruscol")]
        public double? ruscol { get; set; }
    }

    [Table("HealthDetailedDeathsCtry")]
    public partial class HealthDetailedDeathsCtry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("sex")]
        public double sex { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CauseID")]
        public double CauseID { get; set; }
        [Column("Cause")]
        public string Cause { get; set; }
        [Column("p1")]
        public double? p1 { get; set; }
        [Column("p2")]
        public double? p2 { get; set; }
        [Column("p3")]
        public double? p3 { get; set; }
        [Column("p4")]
        public double? p4 { get; set; }
        [Column("p5")]
        public double? p5 { get; set; }
        [Column("p6")]
        public double? p6 { get; set; }
        [Column("p7")]
        public double? p7 { get; set; }
        [Column("p8")]
        public double? p8 { get; set; }
        [Column("p9")]
        public double? p9 { get; set; }
        [Column("p10")]
        public double? p10 { get; set; }
        [Column("p11")]
        public double? p11 { get; set; }
        [Column("p12")]
        public double? p12 { get; set; }
        [Column("p13")]
        public double? p13 { get; set; }
        [Column("p14")]
        public double? p14 { get; set; }
        [Column("p15")]
        public double? p15 { get; set; }
        [Column("p16")]
        public double? p16 { get; set; }
        [Column("p17")]
        public double? p17 { get; set; }
        [Column("p18")]
        public double? p18 { get; set; }
        [Column("p19")]
        public double? p19 { get; set; }
        [Column("p20")]
        public double? p20 { get; set; }
        [Column("p21")]
        public double? p21 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("p1", p1);
            yield return new KeyValuePair<string, double?>("p2", p2);
            yield return new KeyValuePair<string, double?>("p3", p3);
            yield return new KeyValuePair<string, double?>("p4", p4);
            yield return new KeyValuePair<string, double?>("p5", p5);
            yield return new KeyValuePair<string, double?>("p6", p6);
            yield return new KeyValuePair<string, double?>("p7", p7);
            yield return new KeyValuePair<string, double?>("p8", p8);
            yield return new KeyValuePair<string, double?>("p9", p9);
            yield return new KeyValuePair<string, double?>("p10", p10);
            yield return new KeyValuePair<string, double?>("p11", p11);
            yield return new KeyValuePair<string, double?>("p12", p12);
            yield return new KeyValuePair<string, double?>("p13", p13);
            yield return new KeyValuePair<string, double?>("p14", p14);
            yield return new KeyValuePair<string, double?>("p15", p15);
            yield return new KeyValuePair<string, double?>("p16", p16);
            yield return new KeyValuePair<string, double?>("p17", p17);
            yield return new KeyValuePair<string, double?>("p18", p18);
            yield return new KeyValuePair<string, double?>("p19", p19);
            yield return new KeyValuePair<string, double?>("p20", p20);
            yield return new KeyValuePair<string, double?>("p21", p21);
        }
    }

    [Table("HealthDetailedDeathsRegs")]
    public partial class HealthDetailedDeathsRegs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegNum")]
        public double RegNum { get; set; }
        [Column("RegName")]
        public string RegName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("sex")]
        public double sex { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CauseID")]
        public double CauseID { get; set; }
        [Column("Cause")]
        public string Cause { get; set; }
        [Column("R0")]
        public double? R0 { get; set; }
        [Column("R1")]
        public double? R1 { get; set; }
        [Column("R3")]
        public double? R3 { get; set; }
        [Column("R4")]
        public double? R4 { get; set; }
        [Column("R5")]
        public double? R5 { get; set; }
        [Column("R6")]
        public double? R6 { get; set; }
        [Column("R7")]
        public double? R7 { get; set; }
        [Column("R8")]
        public double? R8 { get; set; }
        [Column("R9")]
        public double? R9 { get; set; }
        [Column("R10")]
        public double? R10 { get; set; }
        [Column("R11")]
        public double? R11 { get; set; }
        [Column("R12")]
        public double? R12 { get; set; }
        [Column("R13")]
        public double? R13 { get; set; }
        [Column("R14")]
        public double? R14 { get; set; }
        [Column("R15")]
        public double? R15 { get; set; }
        [Column("R16")]
        public double? R16 { get; set; }
        [Column("R17")]
        public double? R17 { get; set; }
        [Column("R18")]
        public double? R18 { get; set; }
        [Column("R19")]
        public double? R19 { get; set; }
        [Column("R20")]
        public double? R20 { get; set; }
        [Column("R21")]
        public double? R21 { get; set; }
    }

    [Table("HealthWHOLowGDPModel")]
    public partial class HealthWHOLowGDPModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("Code")]
        public int? Code { get; set; }
        [Column("Code4")]
        public int? Code4 { get; set; }
    }

    [Table("PopCohortAIDSRegion")]
    public partial class PopCohortAIDSRegion
    {
        [Column("Region")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Number")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Sex")]
        public int Sex { get; set; }
        [Column("Cohort0")]
        public double? Cohort0 { get; set; }
        [Column("Cohort1")]
        public double? Cohort1 { get; set; }
        [Column("Cohort2")]
        public double? Cohort2 { get; set; }
        [Column("Cohort3")]
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Cohorts
        {
            get
            {
                return allCohorts().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, double?>> allCohorts()
        {
            yield return new KeyValuePair<string, double?>("Cohort0", Cohort0);
            yield return new KeyValuePair<string, double?>("Cohort1", Cohort1);
            yield return new KeyValuePair<string, double?>("Cohort2", Cohort2);
            yield return new KeyValuePair<string, double?>("Cohort3", Cohort3);
            yield return new KeyValuePair<string, double?>("Cohort4", Cohort4);
            yield return new KeyValuePair<string, double?>("Cohort5", Cohort5);
            yield return new KeyValuePair<string, double?>("Cohort6", Cohort6);
            yield return new KeyValuePair<string, double?>("Cohort7", Cohort7);
            yield return new KeyValuePair<string, double?>("Cohort8", Cohort8);
            yield return new KeyValuePair<string, double?>("Cohort9", Cohort9);
            yield return new KeyValuePair<string, double?>("Cohort10", Cohort10);
            yield return new KeyValuePair<string, double?>("Cohort11", Cohort11);
            yield return new KeyValuePair<string, double?>("Cohort12", Cohort12);
            yield return new KeyValuePair<string, double?>("Cohort13", Cohort13);
            yield return new KeyValuePair<string, double?>("Cohort14", Cohort14);
            yield return new KeyValuePair<string, double?>("Cohort15", Cohort15);
            yield return new KeyValuePair<string, double?>("Cohort16", Cohort16);
            yield return new KeyValuePair<string, double?>("Cohort17", Cohort17);
            yield return new KeyValuePair<string, double?>("Cohort18", Cohort18);
            yield return new KeyValuePair<string, double?>("Cohort19", Cohort19);
            yield return new KeyValuePair<string, double?>("Cohort20", Cohort20);
            yield return new KeyValuePair<string, double?>("Cohort21", Cohort21);
            yield return new KeyValuePair<string, double?>("Cohort22", Cohort22);
            yield return new KeyValuePair<string, double?>("Cohort23", Cohort23);
            yield return new KeyValuePair<string, double?>("Cohort24", Cohort24);
            yield return new KeyValuePair<string, double?>("Cohort25", Cohort25);
            yield return new KeyValuePair<string, double?>("Cohort26", Cohort26);
            yield return new KeyValuePair<string, double?>("Cohort27", Cohort27);
            yield return new KeyValuePair<string, double?>("Cohort28", Cohort28);
            yield return new KeyValuePair<string, double?>("Cohort29", Cohort29);
            yield return new KeyValuePair<string, double?>("Cohort30", Cohort30);
            yield return new KeyValuePair<string, double?>("Cohort31", Cohort31);
            yield return new KeyValuePair<string, double?>("Cohort32", Cohort32);
            yield return new KeyValuePair<string, double?>("Cohort33", Cohort33);
            yield return new KeyValuePair<string, double?>("Cohort34", Cohort34);
            yield return new KeyValuePair<string, double?>("Cohort35", Cohort35);
            yield return new KeyValuePair<string, double?>("Cohort36", Cohort36);
            yield return new KeyValuePair<string, double?>("Cohort37", Cohort37);
            yield return new KeyValuePair<string, double?>("Cohort38", Cohort38);
            yield return new KeyValuePair<string, double?>("Cohort39", Cohort39);
            yield return new KeyValuePair<string, double?>("Cohort40", Cohort40);
            yield return new KeyValuePair<string, double?>("Cohort41", Cohort41);
        }
    }

    [Table("SocialHist")]
    public partial class SocialHist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("CultReg")]
        public int? CultReg { get; set; }
    }

    [Table("WVSCohortWave2")]
    public partial class WVSCohortWave2
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("MPRCo1")]
        public float? MPRCo1 { get; set; }
        [Column("MPRCo2")]
        public float? MPRCo2 { get; set; }
        [Column("MPRCo3")]
        public float? MPRCo3 { get; set; }
        [Column("MPRCo4")]
        public float? MPRCo4 { get; set; }
        [Column("MPRCo5")]
        public float? MPRCo5 { get; set; }
        [Column("MPRCo6")]
        public float? MPRCo6 { get; set; }
        [Column("MPRTotal")]
        public float? MPRTotal { get; set; }
    }

    [Table("CulturalParameters")]
    public partial class CulturalParameters
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Column("Region1")]
        public float? Region1 { get; set; }
        [Column("Region2")]
        public float? Region2 { get; set; }
        [Column("Region3")]
        public float? Region3 { get; set; }
        [Column("Region4")]
        public float? Region4 { get; set; }
        [Column("Region5")]
        public float? Region5 { get; set; }
        [Column("Region6")]
        public float? Region6 { get; set; }
        [Column("Region7")]
        public float? Region7 { get; set; }
        [Column("Region8")]
        public float? Region8 { get; set; }
        [Column("Region9")]
        public float? Region9 { get; set; }
        [Column("Region10")]
        public float? Region10 { get; set; }
        [Column("Region11")]
        public float? Region11 { get; set; }
    }

    [Table("IOEngelAlphaCoefs")]
    public partial class IOEngelAlphaCoefs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Coefficient")]
        public string Coefficient { get; set; }
        [Column("AG Sector")]
        public float? AGSector { get; set; }
        [Column("RM Sector")]
        public float? RMSector { get; set; }
        [Column("PE Sector")]
        public float? PESector { get; set; }
        [Column("MN Sector")]
        public float? MNSector { get; set; }
        [Column("SR Sector")]
        public float? SRSector { get; set; }
        [Column("IC Sector")]
        public float? ICSector { get; set; }
    }

    //ttt 2019/03/29 Add Id As Primary Key //jrs 2019/04/05 removed Id
    [Table("HealthDiarrheaReg")]
    public partial class HealthDiarrheaReg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cohort")]
        public int Cohort { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("Mortality")]
        public double? Mortality { get; set; }
    }

    [Table("HealthMalariaReg")]
    public partial class HealthMalariaReg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cohort")]
        public int Cohort { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("Mortality")]
        public double? Mortality { get; set; }
    }

    [Table("HealthMentalHealthReg")]
    public partial class HealthMentalHealthReg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cohort")]
        public int Cohort { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("YLDtoPOP")]
        public double? YLDtoPOP { get; set; }
    }
    [Table("HealthRespInfReg")]
    public partial class HealthRespInfReg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Cohort")]
        public int Cohort { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("Mortality")]
        public double? Mortality { get; set; }
    }
    [Table("HealthGBDDeaths")]
    public partial class HealthGBDDeaths
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Code")]
        public string Code { get; set; }
        [Column("0-4")]
        public double? F0_4 { get; internal set; }
        [Column("5-14")]
        public double? F5_14 { get; internal set; }
        [Column("15-29")]
        public double? F15_29 { get; internal set; }
        [Column("30-44")]
        public double? F30_44 { get; internal set; }
        [Column("45-59")]
        public double? F45_59 { get; internal set; }
        [Column("60-69")]
        public double? F60_69 { get; internal set; }
        [Column("70-79")]
        public double? F70_79 { get; internal set; }
        [Column("80+")]
        public double? F80P { get; internal set; }
        [Column("Total")]
        public double? Total { get; set; }
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("GroupName")]
        public string GroupName { get; set; }
    }
    [Table("HealthGBDYLD")]
    public partial class HealthGBDYLD
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Code")]
        public string Code { get; set; }
        [Column("0-4")]
        public double? F0_4 { get; internal set; }
        [Column("5-14")]
        public double? F5_14 { get; internal set; }
        [Column("15-29")]
        public double? F15_29 { get; internal set; }
        [Column("30-44")]
        public double? F30_44 { get; internal set; }
        [Column("45-59")]
        public double? F45_59 { get; internal set; }
        [Column("60-69")]
        public double? F60_69 { get; internal set; }
        [Column("70-79")]
        public double? F70_79 { get; internal set; }
        [Column("80+")]
        public double? F80P { get; internal set; }
        [Column("Total")]
        public double? Total { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GroupName")]
        public string GroupName { get; set; }
    }

    [Table("PopCohortGlobal")]
    public partial class PopCohortGlobal
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Variable")]
        public string Variable { get; set; }
        [Column("Cohort0")]
        public double? Cohort0 { get; set; }
        [Column("Cohort1")]
        public double? Cohort1 { get; set; }
        [Column("Cohort2")]
        public double? Cohort2 { get; set; }
        [Column("Cohort3")]
        public double? Cohort3 { get; set; }
        [Column("Cohort4")]
        public double? Cohort4 { get; set; }
        [Column("Cohort5")]
        public double? Cohort5 { get; set; }
        [Column("Cohort6")]
        public double? Cohort6 { get; set; }
        [Column("Cohort7")]
        public double? Cohort7 { get; set; }
        [Column("Cohort8")]
        public double? Cohort8 { get; set; }
        [Column("Cohort9")]
        public double? Cohort9 { get; set; }
        [Column("Cohort10")]
        public double? Cohort10 { get; set; }
        [Column("Cohort11")]
        public double? Cohort11 { get; set; }
        [Column("Cohort12")]
        public double? Cohort12 { get; set; }
        [Column("Cohort13")]
        public double? Cohort13 { get; set; }
        [Column("Cohort14")]
        public double? Cohort14 { get; set; }
        [Column("Cohort15")]
        public double? Cohort15 { get; set; }
        [Column("Cohort16")]
        public double? Cohort16 { get; set; }
        [Column("Cohort17")]
        public double? Cohort17 { get; set; }
        [Column("Cohort18")]
        public double? Cohort18 { get; set; }
        [Column("Cohort19")]
        public double? Cohort19 { get; set; }
        [Column("Cohort20")]
        public double? Cohort20 { get; set; }
        [Column("Cohort21")]
        public double? Cohort21 { get; set; }
        [Column("Cohort22")]
        public double? Cohort22 { get; set; }
        [Column("Cohort23")]
        public double? Cohort23 { get; set; }
        [Column("Cohort24")]
        public double? Cohort24 { get; set; }
        [Column("Cohort25")]
        public double? Cohort25 { get; set; }
        [Column("Cohort26")]
        public double? Cohort26 { get; set; }
        [Column("Cohort27")]
        public double? Cohort27 { get; set; }
        [Column("Cohort28")]
        public double? Cohort28 { get; set; }
        [Column("Cohort29")]
        public double? Cohort29 { get; set; }
        [Column("Cohort30")]
        public double? Cohort30 { get; set; }
        [Column("Cohort31")]
        public double? Cohort31 { get; set; }
        [Column("Cohort32")]
        public double? Cohort32 { get; set; }
        [Column("Cohort33")]
        public double? Cohort33 { get; set; }
        [Column("Cohort34")]
        public double? Cohort34 { get; set; }
        [Column("Cohort35")]
        public double? Cohort35 { get; set; }
        [Column("Cohort36")]
        public double? Cohort36 { get; set; }
        [Column("Cohort37")]
        public double? Cohort37 { get; set; }
        [Column("Cohort38")]
        public double? Cohort38 { get; set; }
        [Column("Cohort39")]
        public double? Cohort39 { get; set; }
        [Column("Cohort40")]
        public double? Cohort40 { get; set; }
        [Column("Cohort41")]
        public double? Cohort41 { get; set; }
    }

    [Table("HealthRatioYLDYLLRegion")]
    public partial class HealthRatioYLDYLLRegion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Code")]
        public int Code { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public int Gender { get; set; }
        [Column("RatioYLDYLL")]
        public double? RatioYLDYLL { get; set; }
    }

    [Table("IncConSav")]
    public partial class IncConSav
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Age Category")]
        public double AgeC { get; set; }
        [Column("Ages")]
        public string Ages { get; set; }
        [Column("Income MDCs")]
        public double IncomeMDC { get; set; }
        [Column("Consumption  MDCs")]
        public double ConsumptionMDC { get; set; }
        [Column("Savings  MDCs")]
        public double SavingsMDC { get; set; }
        [Column("Income LDCs")]
        public double IncomeLDC { get; set; }
        [Column("Consumption  LDCs")]
        public double ConsumptionLDC { get; set; }
        [Column("Savings  LDCs")]
        public double SavingsLDC { get; set; }
    }

    [Table("HealthProxDrivRRsList")]
    public partial class HealthProxDrivRRsList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int ID { get; set; }
        [Column("TableName")]
        public string TableName { get; set; }
    }

    public abstract partial class ProxDrivRRs : IProxDrivRRs
    {
        public string ProxDriver { get; set; }
        public string DiseaseType { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public short DistributionIdx { get; set; }
        public double? RRValue { get; set; }
        public double? RRMin { get; set; }
        public double? RRMax { get; set; }
        public string Comments { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
    }

    public interface IProxDrivRRs
    {
        string ProxDriver { get; set; }
        string DiseaseType { get; set; }
        string Age { get; set; }
        string Gender { get; set; }
        short DistributionIdx { get; set; }
        double? RRValue { get; set; }
        double? RRMin { get; set; }
        double? RRMax { get; set; }
        string Comments { get; set; }
    }

    [Table("HealthProxDrivRRs")]
    public partial class HealthProxDrivRRs : ProxDrivRRs
    {
    }

    [Table("HealthProxDrivRRs-Black")]
    public partial class HealthProxDrivRRsBlack : ProxDrivRRs
    {
    }

    [Table("HealthProxDrivRRs-HH")]
    public partial class HealthProxDrivRRsHH : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-HL")]
    public partial class HealthProxDrivRRsHL : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-HM")]
    public partial class HealthProxDrivRRsHM : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-LH")]
    public partial class HealthProxDrivRRsLH : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-LL")]
    public partial class HealthProxDrivRRsLL : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-ML")]
    public partial class HealthProxDrivRRsML : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-Olofin")]
    public partial class HealthProxDrivRRsOlofin : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-Olofin-High")]
    public partial class HealthProxDrivRRsOlofinHigh : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-Olofin-Low")]
    public partial class HealthProxDrivRRsOlofinLow : ProxDrivRRs
    {
    }
    [Table("HealthProxDrivRRs-Olofin-W4A")]
    public partial class HealthProxDrivRRsOlofinW4A : ProxDrivRRs
    {
    }

    public abstract partial class MortFactors : IMortFactors
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Cohort { get; set; }
        public double Constant { get; set; }
        public double lnY { get; set; }
        public double lnHC { get; set; }
        public double lnY2 { get; set; }
        public double Year { get; set; }
        public double lnSI { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IncomeType { get; set; }
    }

    public interface IMortFactors
    {
        int Cohort { get; set; }
        double Constant { get; set; }
        double lnY { get; set; }
        double lnHC { get; set; }
        double lnY2 { get; set; }
        double Year { get; set; }
        double lnSI { get; set; }
        int IncomeType { get; set; }
    }

    [Table("HealthMortFactorCommDisMale")]
    public partial class HealthMortFactorCommDisMale : MortFactors
    {
    }

    [Table("HealthMortFactorCommDisFemale")]
    public partial class HealthMortFactorCommDisFemale : MortFactors
    {
    }

    [Table("HealthMortFactorNonCommDisMale")]
    public partial class HealthMortFactorNonCommDisMale : MortFactors
    {
        public int Group { get; set; }
    }

    [Table("HealthMortFactorNonCommDisFemale")]
    public partial class HealthMortFactorNonCommDisFemale : MortFactors
    {
        public int Group { get; set; }
    }

    [Table("HealthMortFactorInjuryMale")]
    public partial class HealthMortFactorInjuryMale : MortFactors
    {
        public int Group { get; set; }
    }

    [Table("HealthMortFactorInjuryFemale")]
    public partial class HealthMortFactorInjuryFemale : MortFactors
    {
        public int Group { get; set; }
    }

    [Table("HealthDetailed5YearsDeathsRegs")]
    public partial class HealthDetailed5YearsDeathsRegs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Column("RegionName")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TypeId")]
        public int TypeId { get; set; }
        [Column("Type")]
        public string Type { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Sex")]
        public int Sex { get; set; }
        [Column("Frmat")]
        public int Frmat { get; set; }
        [Column("PopFrmat")]
        public int PopFrmat { get; set; }
        [Column("Deaths1")]
        public int? Deaths1 { get; set; }
        [Column("Deaths2")]
        public int? Deaths2 { get; set; }
        [Column("Deaths3")]
        public int? Deaths3 { get; set; }
        [Column("Deaths4")]
        public int? Deaths4 { get; set; }
        [Column("Deaths5")]
        public int? Deaths5 { get; set; }
        [Column("Deaths6")]
        public int? Deaths6 { get; set; }
        [Column("Deaths7")]
        public int? Deaths7 { get; set; }
        [Column("Deaths8")]
        public int? Deaths8 { get; set; }
        [Column("Deaths9")]
        public int? Deaths9 { get; set; }
        [Column("Deaths10")]
        public int? Deaths10 { get; set; }
        [Column("Deaths11")]
        public int? Deaths11 { get; set; }
        [Column("Deaths12")]
        public int? Deaths12 { get; set; }
        [Column("Deaths13")]
        public int? Deaths13 { get; set; }
        [Column("Deaths14")]
        public int? Deaths14 { get; set; }
        [Column("Deaths15")]
        public int? Deaths15 { get; set; }
        [Column("Deaths16")]
        public int? Deaths16 { get; set; }
        [Column("Deaths17")]
        public int? Deaths17 { get; set; }
        [Column("Deaths18")]
        public int? Deaths18 { get; set; }
        [Column("Deaths19")]
        public int? Deaths19 { get; set; }
        [Column("Deaths20")]
        public int? Deaths20 { get; set; }
        [Column("Deaths21")]
        public int? Deaths21 { get; set; }
        [Column("Deaths22")]
        public int? Deaths22 { get; set; }
        [Column("Deaths23")]
        public int? Deaths23 { get; set; }
        [Column("Deaths24")]
        public int? Deaths24 { get; set; }
        [Column("Deaths25")]
        public int? Deaths25 { get; set; }
        [Column("Deaths26")]
        public int? Deaths26 { get; set; }
        [Column("Deaths27")]
        public int? Deaths27 { get; set; }
        [Column("Deaths28")]
        public int? Deaths28 { get; set; }
        [Column("Deaths29")]
        public int? Deaths29 { get; set; }
        [Column("Deaths30")]
        public int? Deaths30 { get; set; }
        [Column("Deaths31")]
        public int? Deaths31 { get; set; }
        [Column("Deaths32")]
        public int? Deaths32 { get; set; }
        [Column("Deaths33")]
        public int? Deaths33 { get; set; }
        [Column("Deaths34")]
        public int? Deaths34 { get; set; }
        [Column("Deaths35")]
        public int? Deaths35 { get; set; }
        [Column("Deaths36")]
        public int? Deaths36 { get; set; }
        [Column("Deaths37")]
        public int? Deaths37 { get; set; }
        [Column("Deaths38")]
        public int? Deaths38 { get; set; }
        [Column("Deaths39")]
        public int? Deaths39 { get; set; }
        [Column("Deaths40")]
        public int? Deaths40 { get; set; }
        [Column("Deaths41")]
        public int? Deaths41 { get; set; }
        [Column("Deaths42")]
        public int? Deaths42 { get; set; }
        [Column("Deaths43")]
        public int? Deaths43 { get; set; }
        [Column("Deaths44")]
        public int? Deaths44 { get; set; }
        [Column("Deaths45")]
        public int? Deaths45 { get; set; }
        [Column("Deaths46")]
        public int? Deaths46 { get; set; }
        [Column("Pop1")]
        public int? Pop1 { get; set; }
        [Column("Pop2")]
        public int? Pop2 { get; set; }
        [Column("Pop3")]
        public int? Pop3 { get; set; }
        [Column("Pop4")]
        public int? Pop4 { get; set; }
        [Column("Pop5")]
        public int? Pop5 { get; set; }
        [Column("Pop6")]
        public int? Pop6 { get; set; }
        [Column("Pop7")]
        public int? Pop7 { get; set; }
        [Column("Pop8")]
        public int? Pop8 { get; set; }
        [Column("Pop9")]
        public int? Pop9 { get; set; }
        [Column("Pop10")]
        public int? Pop10 { get; set; }
        [Column("Pop11")]
        public int? Pop11 { get; set; }
        [Column("Pop12")]
        public int? Pop12 { get; set; }
        [Column("Pop13")]
        public int? Pop13 { get; set; }
        [Column("Pop14")]
        public int? Pop14 { get; set; }
        [Column("Pop15")]
        public int? Pop15 { get; set; }
        [Column("Pop16")]
        public int? Pop16 { get; set; }
        [Column("Pop17")]
        public int? Pop17 { get; set; }
        [Column("Pop18")]
        public int? Pop18 { get; set; }
        [Column("Pop19")]
        public int? Pop19 { get; set; }
        [Column("Pop20")]
        public int? Pop20 { get; set; }
        [Column("Pop21")]
        public int? Pop21 { get; set; }
        [Column("Pop22")]
        public int? Pop22 { get; set; }
        [Column("Pop23")]
        public int? Pop23 { get; set; }
        [Column("Pop24")]
        public int? Pop24 { get; set; }
        [Column("Pop25")]
        public int? Pop25 { get; set; }
        [Column("Pop26")]
        public int? Pop26 { get; set; }
        [Column("Pop27")]
        public int? Pop27 { get; set; }
        [Column("Pop28")]
        public int? Pop28 { get; set; }
        [Column("Pop29")]
        public int? Pop29 { get; set; }
        [Column("Pop30")]
        public int? Pop30 { get; set; }
        [Column("Pop31")]
        public int? Pop31 { get; set; }
        [Column("Pop32")]
        public int? Pop32 { get; set; }
        [Column("Pop33")]
        public int? Pop33 { get; set; }
        [Column("Pop34")]
        public int? Pop34 { get; set; }
        [Column("Pop35")]
        public int? Pop35 { get; set; }
        [Column("Pop36")]
        public int? Pop36 { get; set; }
        [Column("Pop37")]
        public int? Pop37 { get; set; }
        [Column("Pop38")]
        public int? Pop38 { get; set; }
        [Column("Pop39")]
        public int? Pop39 { get; set; }
        [Column("Pop40")]
        public int? Pop40 { get; set; }
        [Column("Pop41")]
        public int? Pop41 { get; set; }
        [Column("Pop42")]
        public int? Pop42 { get; set; }
        [Column("Pop43")]
        public int? Pop43 { get; set; }
        [Column("Pop44")]
        public int? Pop44 { get; set; }
        [Column("Pop45")]
        public int? Pop45 { get; set; }
        [Column("Pop46")]
        public int? Pop46 { get; set; }
        public KeyValuePair<int?, int?> DeathPop(int idx)
        {
            {
                switch (idx)
                {
                    case 1:
                        return new KeyValuePair<int?, int?>(Deaths1, Pop1);
                    case 2:
                        return new KeyValuePair<int?, int?>(Deaths2, Pop2);
                    case 3:
                        return new KeyValuePair<int?, int?>(Deaths3, Pop3);
                    case 4:
                        return new KeyValuePair<int?, int?>(Deaths4, Pop4);
                    case 5:
                        return new KeyValuePair<int?, int?>(Deaths5, Pop5);
                    case 6:
                        return new KeyValuePair<int?, int?>(Deaths6, Pop6);
                    case 7:
                        return new KeyValuePair<int?, int?>(Deaths7, Pop7);
                    case 8:
                        return new KeyValuePair<int?, int?>(Deaths8, Pop8);
                    case 9:
                        return new KeyValuePair<int?, int?>(Deaths9, Pop9);
                    case 10:
                        return new KeyValuePair<int?, int?>(Deaths10, Pop10);
                    case 11:
                        return new KeyValuePair<int?, int?>(Deaths11, Pop11);
                    case 12:
                        return new KeyValuePair<int?, int?>(Deaths12, Pop12);
                    case 13:
                        return new KeyValuePair<int?, int?>(Deaths13, Pop13);
                    case 14:
                        return new KeyValuePair<int?, int?>(Deaths14, Pop14);
                    case 15:
                        return new KeyValuePair<int?, int?>(Deaths15, Pop15);
                    case 16:
                        return new KeyValuePair<int?, int?>(Deaths16, Pop16);
                    case 17:
                        return new KeyValuePair<int?, int?>(Deaths17, Pop17);
                    case 18:
                        return new KeyValuePair<int?, int?>(Deaths18, Pop18);
                    case 19:
                        return new KeyValuePair<int?, int?>(Deaths19, Pop19);
                    case 20:
                        return new KeyValuePair<int?, int?>(Deaths20, Pop20);
                    case 21:
                        return new KeyValuePair<int?, int?>(Deaths21, Pop21);
                    case 22:
                        return new KeyValuePair<int?, int?>(Deaths22, Pop22);
                    case 23:
                        return new KeyValuePair<int?, int?>(Deaths23, Pop23);
                    case 24:
                        return new KeyValuePair<int?, int?>(Deaths24, Pop24);
                    case 25:
                        return new KeyValuePair<int?, int?>(Deaths25, Pop25);
                    case 26:
                        return new KeyValuePair<int?, int?>(Deaths26, Pop26);
                    case 27:
                        return new KeyValuePair<int?, int?>(Deaths27, Pop27);
                    case 28:
                        return new KeyValuePair<int?, int?>(Deaths28, Pop28);
                    case 29:
                        return new KeyValuePair<int?, int?>(Deaths29, Pop29);
                    case 30:
                        return new KeyValuePair<int?, int?>(Deaths30, Pop30);
                    case 31:
                        return new KeyValuePair<int?, int?>(Deaths31, Pop31);
                    case 32:
                        return new KeyValuePair<int?, int?>(Deaths32, Pop32);
                    case 33:
                        return new KeyValuePair<int?, int?>(Deaths33, Pop33);
                    case 34:
                        return new KeyValuePair<int?, int?>(Deaths34, Pop34);
                    case 35:
                        return new KeyValuePair<int?, int?>(Deaths35, Pop35);
                    case 36:
                        return new KeyValuePair<int?, int?>(Deaths36, Pop36);
                    case 37:
                        return new KeyValuePair<int?, int?>(Deaths37, Pop37);
                    case 38:
                        return new KeyValuePair<int?, int?>(Deaths38, Pop38);
                    case 39:
                        return new KeyValuePair<int?, int?>(Deaths39, Pop39);
                    case 40:
                        return new KeyValuePair<int?, int?>(Deaths40, Pop40);
                    case 41:
                        return new KeyValuePair<int?, int?>(Deaths41, Pop41);
                    case 42:
                        return new KeyValuePair<int?, int?>(Deaths42, Pop42);
                    case 43:
                        return new KeyValuePair<int?, int?>(Deaths43, Pop43);
                    case 44:
                        return new KeyValuePair<int?, int?>(Deaths44, Pop44);
                    case 45:
                        return new KeyValuePair<int?, int?>(Deaths45, Pop45);
                    case 46:
                        return new KeyValuePair<int?, int?>(Deaths46, Pop46);
                    default:
                        return new KeyValuePair<int?, int?>(null, null);
                }
            }
        }
    }

    [Table("HealthMortRatios")]
    public partial class HealthMortRatios
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Disease")]
        public string Disease { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Gender")]
        public string Gender { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Age")]
        public string Age { get; set; }
        [Column("Ratio")]
        public double Ratio { get; set; }
    }

    [Table("HealthCostByType")]
    public partial class HealthCostByType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TypeId")]
        public short TypeId { get; set; }
        [Column("TypeName")]
        public string TypeName { get; set; }
        [Column("MorbidityCost")]
        public float MorbidityCost { get; set; }
        [Column("MortalityCost")]
        public float MortalityCost { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CountryIdx")]
        public short CountryIdx { get; set; }
    }

    [Table("SharedRiversCountry")]
    public partial class SharedRiversCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CountryACowNumber")]
        public string CountryACowNumber { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CountryBCowNumber")]
        public string CountryBCowNumber { get; set; }
    }

    [Table("GovConflict")]
    public partial class GovConflict
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country1")]
        public string Country1 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country2")]
        public string Country2 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Year")]
        public float Year { get; set; }
        [Column("StandCOI")]
        public float? StandCOI { get; set; }
        [Column("COW1")]
        public float COW1 { get; set; }
        [Column("COW2")]
        public float COW2 { get; set; }
    }

    [Table("Currency")]
    public partial class Currency
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("Currency Name")]
        public string CurrencyName { get; set; }
        [Column("Symbol")]
        public string Symbol { get; set; }
        [Column("Currency Value")]
        public float? CurrencyValue { get; set; }
        [Column("Currency Flag")]
        public bool CurrencyFlag { get; set; }
    }

    [Table("TariffRatePerSectorCountry")]
    public partial class TariffRatePerSectorCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("Agriculture")]
        public float? Agriculture { get; set; }
        [Column("Energy")]
        public float? Energy { get; set; }
        [Column("Materials")]
        public float? Materials { get; set; }
        [Column("Manufactures")]
        public float? Manufactures { get; set; }
        [Column("Services")]
        public float? Services { get; set; }
        [Column("ICT")]
        public float? ICT { get; set; }
    }

    [Table("TariffRevPerSectorCountry")]
    public partial class TariffRevPerSectorCountry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("Agriculture")]
        public float? Agriculture { get; set; }
        [Column("Energy")]
        public float? Energy { get; set; }
        [Column("Materials")]
        public float? Materials { get; set; }
        [Column("Manufactures")]
        public float? Manufactures { get; set; }
        [Column("Services")]
        public float? Services { get; set; }
        [Column("ICT")]
        public float? ICT { get; set; }
    }

    public interface IPovAgeSexCtry60
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        float? a0_14 { get; set; }
        float? a15_24 { get; set; }
        float? a25_34 { get; set; }
        float? a35_39 { get; set; }
        float? a40_49 { get; set; }
        float? a50_54 { get; set; }
        float? a55_59 { get; set; }
        float? a60andOver { get; set; }
        float? AllAges { get; set; }
    }

    public interface IPovAgeSexCtry602
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        float? a0_4 { get; set; }
        float? a5_9 { get; set; }
        float? a10_14 { get; set; }
        float? a15_24 { get; set; }
        float? a25_34 { get; set; }
        float? a35_39 { get; set; }
        float? a40_49 { get; set; }
        float? a50_54 { get; set; }
        float? a55_59 { get; set; }
        float? a60andOver { get; set; }
        float? AllAges { get; set; }
    }

    public interface IPovAgeSexCtry60EU
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        float? a0_4 { get; set; }
        float? a5_9 { get; set; }
        float? a10_14 { get; set; }
        float? a15_24 { get; set; }
        float? a25_34 { get; set; }
        float? a35_39 { get; set; }
        float? a40_44 { get; set; }
        float? a45_49 { get; set; }
        float? a50_54 { get; set; }
        float? a55_59 { get; set; }
        float? a60andOver { get; set; }
    }

    public interface IPovAgeSexCtry
    {
        string Country { get; set; }
        string FIPS_CODE { get; set; }
        float? a0_4 { get; set; }
        float? a5_9 { get; set; }
        float? a10_14 { get; set; }
        float? a15_19 { get; set; }
        float? a20_24 { get; set; }
        float? a25_29 { get; set; }
        float? a30_34 { get; set; }
        float? a35_39 { get; set; }
        float? a40_44 { get; set; }
        float? a45_49 { get; set; }
        float? a50_54 { get; set; }
        float? a55_59 { get; set; }
        float? a60_64 { get; set; }
        float? a65_69 { get; set; }
        float? a70_74 { get; set; }
        float? a75_79 { get; set; }
        float? a80andOver { get; set; }
        float? AllAges { get; set; }
    }

    public abstract partial class PovAgeSexCtry60 : IPovAgeSexCtry60
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("0-14")]
        public float? a0_14 { get; set; }
        [Column("15-24")]
        public float? a15_24 { get; set; }
        [Column("25-34")]
        public float? a25_34 { get; set; }
        [Column("35-39")]
        public float? a35_39 { get; set; }
        [Column("40-49")]
        public float? a40_49 { get; set; }
        [Column("50-54")]
        public float? a50_54 { get; set; }
        [Column("55-59")]
        public float? a55_59 { get; set; }
        [Column("60 and over")]
        public float? a60andOver { get; set; }
        [Column("All ages")]
        public float? AllAges { get; set; }
    }

    public abstract partial class PovAgeSexCtry602 : IPovAgeSexCtry602
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("0-4")]
        public float? a0_4 { get; set; }
        [Column("5-9")]
        public float? a5_9 { get; set; }
        [Column("10-14")]
        public float? a10_14 { get; set; }
        [Column("15-24")]
        public float? a15_24 { get; set; }
        [Column("25-34")]
        public float? a25_34 { get; set; }
        [Column("35-39")]
        public float? a35_39 { get; set; }
        [Column("40-49")]
        public float? a40_49 { get; set; }
        [Column("50-54")]
        public float? a50_54 { get; set; }
        [Column("55-59")]
        public float? a55_59 { get; set; }
        [Column("60 and over")]
        public float? a60andOver { get; set; }
        [Column("All ages")]
        public float? AllAges { get; set; }
    }


    public abstract partial class PovAgeSexCtry60EU : IPovAgeSexCtry60EU
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("0-4")]
        public float? a0_4 { get; set; }
        [Column("5-9")]
        public float? a5_9 { get; set; }
        [Column("10-14")]
        public float? a10_14 { get; set; }
        [Column("15-24")]
        public float? a15_24 { get; set; }
        [Column("25-34")]
        public float? a25_34 { get; set; }
        [Column("35-39")]
        public float? a35_39 { get; set; }
        [Column("40-44")]
        public float? a40_44 { get; set; }
        [Column("45-49")]
        public float? a45_49 { get; set; }
        [Column("50-54")]
        public float? a50_54 { get; set; }
        [Column("55-59")]
        public float? a55_59 { get; set; }
        [Column("60 and over")]
        public float? a60andOver { get; set; }
    }

    public abstract partial class PovAgeSexCtry : IPovAgeSexCtry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("0-4")]
        public float? a0_4 { get; set; }
        [Column("5-9")]
        public float? a5_9 { get; set; }
        [Column("10-14")]
        public float? a10_14 { get; set; }
        [Column("15-19")]
        public float? a15_19 { get; set; }
        [Column("20-24")]
        public float? a20_24 { get; set; }
        [Column("25-29")]
        public float? a25_29 { get; set; }
        [Column("30-34")]
        public float? a30_34 { get; set; }
        [Column("35-39")]
        public float? a35_39 { get; set; }
        [Column("40-44")]
        public float? a40_44 { get; set; }
        [Column("45-49")]
        public float? a45_49 { get; set; }
        [Column("50-54")]
        public float? a50_54 { get; set; }
        [Column("55-59")]
        public float? a55_59 { get; set; }
        [Column("60-64")]
        public float? a60_64 { get; set; }
        [Column("65-69")]
        public float? a65_69 { get; set; }
        [Column("70-74")]
        public float? a70_74 { get; set; }
        [Column("75-79")]
        public float? a75_79 { get; set; }
        [Column("80 and over")]
        public float? a80andOver { get; set; }
        [Column("All ages")]
        public float? AllAges { get; set; }
    }

    [Table("PovAgeSexGeneric")]
    public partial class PovAgeSexGeneric 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Name")]
        public string Name { get; set; }
        [Column("Region")]
        public string Region { get; set; }
        [Column("0-4")]
        public float? a0_4 { get; set; }
        [Column("5-9")]
        public float? a5_9 { get; set; }
        [Column("10-14")]
        public float? a10_14 { get; set; }
        [Column("15-24")]
        public float? a15_24 { get; set; }
        [Column("25-34")]
        public float? a25_34 { get; set; }
        [Column("35-39")]
        public float? a35_39 { get; set; }
        [Column("40-49")]
        public float? a40_49 { get; set; }
        [Column("50-54")]
        public float? a50_54 { get; set; }
        [Column("55-59")]
        public float? a55_59 { get; set; }
        [Column("60 and over")]
        public float? a60andOver { get; set; }
        [Column("All ages")]
        public float? AllAges { get; set; }

    }

    [Table("PovRateAgeBothSexes")]
    public partial class PovRateAgeBothSexes : PovAgeSexCtry602
    {
    }

    [Table("PovRateAgeBothSexesHist")]
    public partial class PovRateAgeBothSexesHist: PovAgeSexCtry602
    {
    }

    [Table("PovRateAgeBothSexesUN")]
    public partial class PovRateAgeBothSexesUN: PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeMale")]
    public partial class PovRateAgeMale: PovAgeSexCtry602
    {
    }

    [Table("PovRateAgeMaleHist")]
    public partial class PovRateAgeMaleHist: PovAgeSexCtry602
    {
    }

    [Table("PovRateAgeMaleUN")]
    public partial class PovRateAgeMaleUN : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeFemale")]
    public partial class PovRateAgeFemale : PovAgeSexCtry602
    {
   }

    [Table("PovRateAgeFemaleHist")]
    public partial class PovRateAgeFemaleHist : PovAgeSexCtry602
    {
     }

    [Table("PovRateAgeFemaleUN")]
    public partial class PovRateAgeFemaleUN : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeBothSexes32")]
    public partial class PovRateAgeBothSexes32 : PovAgeSexCtry60
    {
     }

    [Table("PovRateAgeBothSexes32IncLIS")]
    public partial class PovRateAgeBothSexes32IncLIS : PovAgeSexCtry
    {
    }
    
    [Table("PovRateAgeBothSexes32LIS")]
    public partial class PovRateAgeBothSexes32LIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeBothSexesHist32")]
    public partial class PovRateAgeBothSexesHist32 : PovAgeSexCtry60
    {
   }

    [Table("PovRateAgeMale32")]
    public partial class PovRateAgeMale32 : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeMale32IncLIS")]
    public partial class PovRateAgeMale32IncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMale32LIS")]
    public partial class PovRateAgeMale32LIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMaleHist32")]
    public partial class PovRateAgeMaleHist32 : PovAgeSexCtry60
    {
     }

    [Table("PovRateAgeFemale32")]
    public partial class PovRateAgeFemale32 : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeFemale32IncLIS")]
    public partial class PovRateAgeFemale32IncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeFemale32LIS")]
    public partial class PovRateAgeFemale32LIS : PovAgeSexCtry
    {
    }
    
    [Table("PovRateAgeFemaleHist32")]
    public partial class PovRateAgeFemaleHist32 : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeBothSexes55")]
    public partial class PovRateAgeBothSexes55 : PovAgeSexCtry60
    {
       }

    [Table("PovRateAgeBothSexes55IncLIS")]
    public partial class PovRateAgeBothSexes55IncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeBothSexes55LIS")]
    public partial class PovRateAgeBothSexes55LIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeBothSexesHist55")]
    public partial class PovRateAgeBothSexesHist55 : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeMale55")]
    public partial class PovRateAgeMale55 : PovAgeSexCtry60
    {
       }

    [Table("PovRateAgeMale55IncLIS")]
    public partial class PovRateAgeMale55IncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMale55LIS")]
    public partial class PovRateAgeMale55LIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMaleHist55")]
    public partial class PovRateAgeMaleHist55 : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeFemale55")]
    public partial class PovRateAgeFemale55 : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeFemale55IncLIS")]
    public partial class PovRateAgeFemale55IncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeFemale55LIS")]
    public partial class PovRateAgeFemale55LIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeFemaleHist55")]
    public partial class PovRateAgeFemaleHist55 : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeBothSexesNPL")]
    public partial class PovRateAgeBothSexesNPL : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeBothSexesNPLIncLIS")]
    public partial class PovRateAgeBothSexesNPLIncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeBothSexesNPLLIS")]
    public partial class PovRateAgeBothSexesNPLLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeBothSexes50MedEU")]
    public partial class PovRateAgeBothSexes50MedEU : PovAgeSexCtry60EU
    {
    }
    
    [Table("PovRateAgeBothSexesHistNPL")]
    public partial class PovRateAgeBothSexesHistNPL : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeMaleNPL")]
    public partial class PovRateAgeMaleNPL : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeMaleNPLIncLIS")]
    public partial class PovRateAgeMaleNPLIncLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMaleNPLLIS")]
    public partial class PovRateAgeMaleNPLLIS : PovAgeSexCtry
    {
    }

    [Table("PovRateAgeMale50MedEU")]
    public partial class PovRateAgeMale50MedEU : PovAgeSexCtry60EU
    {
    }
    
    [Table("PovRateAgeMaleHistNPL")]
    public partial class PovRateAgeMaleHistNPL : PovAgeSexCtry60
    {
    }

    [Table("PovRateAgeFemaleNPL")]
    public partial class PovRateAgeFemaleNPL : PovAgeSexCtry60
    {
        }

    [Table("PovRateAgeFemaleNPLIncLIS")]
    public partial class PovRateAgeFemaleNPLIncLIS : PovAgeSexCtry
    {
    }
    [Table("PovRateAgeFemaleNPLLIS")]
    public partial class PovRateAgeFemaleNPLLIS : PovAgeSexCtry
    {
    }
    [Table("PovRateAgeFemale50MedEU")]
    public partial class PovRateAgeFemale50MedEU : PovAgeSexCtry60EU
    {
    }

    [Table("PovRateAgeFemaleHistNPL")]
    public partial class PovRateAgeFemaleHistNPL : PovAgeSexCtry60
    {
     }

    [Table("IFsExogenousVars")]
    public partial class IFsExogenousVars
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ExogenousModelId")]
        public long ExogenousModelId { get; set; }
        [Column("VarName")]
        public string VarName { get; set; }
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TableName")]
        public string TableName { get; set; }
        [Column("Adjustment")]
        public string Adjustment { get; set; }
        [Column("AdjustmentTable")]
        public string AdjustmentTable { get; set; }
    }

    //jrs 2024/02/20
    [Table("IFsExogenousVarsGral")]
    public partial class IFsExogenousVarsGral
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("VarName")]
        public string VarName { get; set; }
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("TableName")]
        public string TableName { get; set; }
    }

    internal partial class IFsContext
    {
        //jrs 2024/02/20
        internal virtual DbSet<IFsExogenousVarsGral> IFsExogenousVarsGral { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes32IncLIS> PovRateAgeBothSexes32IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes32LIS> PovRateAgeBothSexes32LIS { get; set; }
        internal virtual DbSet<PovRateAgeMale32IncLIS> PovRateAgeMale32IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeMale32LIS> PovRateAgeMale32LIS { get; set; }
        internal virtual DbSet<PovRateAgeFemale32IncLIS> PovRateAgeFemale32IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeFemale32LIS> PovRateAgeFemale32LIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes55IncLIS> PovRateAgeBothSexes55IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes55LIS> PovRateAgeBothSexes55LIS { get; set; }
        internal virtual DbSet<PovRateAgeMale55IncLIS> PovRateAgeMale55IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeMale55LIS> PovRateAgeMale55LIS { get; set; }
        internal virtual DbSet<PovRateAgeFemale55IncLIS> PovRateAgeFemale55IncLIS { get; set; }
        internal virtual DbSet<PovRateAgeFemale55LIS> PovRateAgeFemale55LIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesNPLIncLIS> PovRateAgeBothSexesNPLIncLIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesNPLLIS> PovRateAgeBothSexesNPLLIS { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes50MedEU> PovRateAgeBothSexes50MedEU { get; set; }
        internal virtual DbSet<PovRateAgeMaleNPLIncLIS> PovRateAgeMaleNPLIncLIS { get; set; }
        internal virtual DbSet<PovRateAgeMaleNPLLIS> PovRateAgeMaleNPLLIS { get; set; }
        internal virtual DbSet<PovRateAgeMale50MedEU> PovRateAgeMale50MedEU { get; set; }
        internal virtual DbSet<PovRateAgeFemaleNPLIncLIS> PovRateAgeFemaleNPLIncLIS { get; set; }
        internal virtual DbSet<PovRateAgeFemaleNPLLIS> PovRateAgeFemaleNPLLIS { get; set; }
        internal virtual DbSet<PovRateAgeFemale50MedEU> PovRateAgeFemale50MedEU { get; set; }
        
        internal virtual DbSet<PovRateAgeFemaleHistNPL> PovRateAgeFemaleHistNPL { get; set; }
        internal virtual DbSet<PovRateAgeFemaleNPL> PovRateAgeFemaleNPL { get; set; }
        internal virtual DbSet<PovRateAgeMaleHistNPL> PovRateAgeMaleHistNPL { get; set; }
        internal virtual DbSet<PovRateAgeMaleNPL> PovRateAgeMaleNPL { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesHistNPL> PovRateAgeBothSexesHistNPL { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesNPL> PovRateAgeBothSexesNPL { get; set; }

        internal virtual DbSet<PovRateAgeFemaleHist55> PovRateAgeFemaleHist55 { get; set; }
        internal virtual DbSet<PovRateAgeFemale55> PovRateAgeFemale55 { get; set; }
        internal virtual DbSet<PovRateAgeMaleHist55> PovRateAgeMaleHist55 { get; set; }
        internal virtual DbSet<PovRateAgeMale55> PovRateAgeMale55 { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesHist55> PovRateAgeBothSexesHist55 { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes55> PovRateAgeBothSexes55 { get; set; }

        internal virtual DbSet<PovRateAgeFemaleHist32> PovRateAgeFemaleHist32 { get; set; }
        internal virtual DbSet<PovRateAgeFemale32> PovRateAgeFemale32 { get; set; }
        internal virtual DbSet<PovRateAgeMaleHist32> PovRateAgeMaleHist32 { get; set; }
        internal virtual DbSet<PovRateAgeMale32> PovRateAgeMale32 { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesHist32> PovRateAgeBothSexesHist32 { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes32> PovRateAgeBothSexes32 { get; set; }

        internal virtual DbSet<IFsExogenousVars> IFsExogenousVars { get; set; }
        
        internal virtual DbSet<RegionIdNewFull> RegionIdNewFull { get; set; }

        internal virtual DbSet<RegionalizationFull> RegionalizationFull { get; set; }

        internal virtual DbSet<PopAgeCohortCountryMale> PopAgeCohortCountryMale { get; set; }

        internal virtual DbSet<PopAgeCohortCountryMaleHist> PopAgeCohortCountryMaleHist { get; set; }

        internal virtual DbSet<PopAgeCohortCountryFemale> PopAgeCohortCountryFemale { get; set; }

        internal virtual DbSet<PopAgeCohortCountryFemaleHist> PopAgeCohortCountryFemaleHist { get; set; }

        internal virtual DbSet<PopAgeSingleCountryMale> PopAgeSingleCountryMale { get; set; }

        internal virtual DbSet<PopAgeSingleCountryMaleHist> PopAgeSingleCountryMaleHist { get; set; }

        internal virtual DbSet<PopAgeSingleCountryFemale> PopAgeSingleCountryFemale { get; set; }

        internal virtual DbSet<PopAgeSingleCountryFemaleHist> PopAgeSingleCountryFemaleHist { get; set; }

        internal virtual DbSet<PopFertilityCohortCountryFemale> PopFertilityCohortCountryFemale { get; set; }

        internal virtual DbSet<PopFertilityCohortCountryFemaleHist> PopFertilityCohortCountryFemaleHist { get; set; }

        internal virtual DbSet<PopMortalityCohortCountryMale> PopMortalityCohortCountryMale { get; set; }

        internal virtual DbSet<PopMortalityCohortCountryMaleHist> PopMortalityCohortCountryMaleHist { get; set; }

        internal virtual DbSet<PopMortalityCohortCountryFemale> PopMortalityCohortCountryFemale { get; set; }

        internal virtual DbSet<PopMortalityCohortCountryFemaleHist> PopMortalityCohortCountryFemaleHist { get; set; }

        internal virtual DbSet<HealthYLDComMatPerCohortCountryMale> HealthYLDComMatPerCohortCountryMale { get; set; }

        internal virtual DbSet<HealthYLDComMatPerCohortCountryFemale> HealthYLDComMatPerCohortCountryFemale { get; set; }

        internal virtual DbSet<HealthYLDNonComDisCohortCountryMale> HealthYLDNonComDisCohortCountryMale { get; set; }

        internal virtual DbSet<HealthYLDNonComDisCohortCountryFemale> HealthYLDNonComDisCohortCountryFemale { get; set; }

        internal virtual DbSet<HealthYLDInjuriesCohortCountryMale> HealthYLDInjuriesCohortCountryMale { get; set; }

        internal virtual DbSet<HealthYLDInjuriesCohortCountryFemale> HealthYLDInjuriesCohortCountryFemale { get; set; }

        //mti 2024/05/23 start
        internal virtual DbSet<EducAttainPrimaryMaleWICCountry> EducAttainPrimaryMaleWICCountry { get; set; }
        internal virtual DbSet<EducAttainPrimaryFemaleWICCountry> EducAttainPrimaryFemaleWICCountry { get; set; }
        internal virtual DbSet<EducAttainSecUpperMaleWICCountry> EducAttainSecUpperMaleWICCountry { get; set; }
        internal virtual DbSet<EducAttainSecUpperFemaleWICCountry> EducAttainSecUpperFemaleWICCountry { get; set; }
        internal virtual DbSet<EducAttainTertiaryMaleWICCountry> EducAttainTertiaryMaleWICCountry { get; set; }
        internal virtual DbSet<EducAttainTertiaryFemaleWICCountry> EducAttainTertiaryFemaleWICCountry { get; set; }
        //mti 2024/05/23 end

        internal virtual DbSet<EducAttainNoEducMaleCountry> EducAttainNoEducMaleCountry { get; set; }
        internal virtual DbSet<EducAttainNoEducFemaleCountry> EducAttainNoEducFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainNoEducTotalCountry> EducAttainNoEducTotalCountry { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteMaleCountry> EducAttainPriIncompleteMaleCountry { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteFemaleCountry> EducAttainPriIncompleteFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteTotalCountry> EducAttainPriIncompleteTotalCountry { get; set; }
        internal virtual DbSet<EducAttainPriCompleteMaleCountry> EducAttainPriCompleteMaleCountry { get; set; }
        internal virtual DbSet<EducAttainPriCompleteFemaleCountry> EducAttainPriCompleteFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainPriCompleteTotalCountry> EducAttainPriCompleteTotalCountry { get; set; }
        internal virtual DbSet<EducAttainSecLowerMaleCountry> EducAttainSecLowerMaleCountry { get; set; }
        internal virtual DbSet<EducAttainSecLowerFemaleCountry> EducAttainSecLowerFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainSecLowerTotalCountry> EducAttainSecLowerTotalCountry { get; set; }
        internal virtual DbSet<EducAttainSecUpperMaleCountry> EducAttainSecUpperMaleCountry { get; set; }
        internal virtual DbSet<EducAttainSecUpperFemaleCountry> EducAttainSecUpperFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainSecUpperTotalCountry> EducAttainSecUpperTotalCountry { get; set; }
        internal virtual DbSet<EducAttainTertiaryMaleCountry> EducAttainTertiaryMaleCountry { get; set; }
        internal virtual DbSet<EducAttainTertiaryFemaleCountry> EducAttainTertiaryFemaleCountry { get; set; }
        internal virtual DbSet<EducAttainTertiaryTotalCountry> EducAttainTertiaryTotalCountry { get; set; }

        internal virtual DbSet<GroupIDNewFull> GroupIDNewFull { get; set; }

        internal virtual DbSet<GroupingFull> GroupingFull { get; set; }

        internal virtual DbSet<GList> GList { get; set; }

        internal virtual DbSet<GListID> GListID { get; set; }

        internal virtual DbSet<GlobalParameters> GlobalParameters { get; set; }

        internal virtual DbSet<GlobalParametersHist> GlobalParametersHist { get; set; }

        internal virtual DbSet<GlobalParametersMult> GlobalParametersMult { get; set; }

        internal virtual DbSet<GlobalParametersMultHist> GlobalParametersMultHist { get; set; }

        internal virtual DbSet<GlobalParametersMultiple> GlobalParametersMultiple { get; set; }

        internal virtual DbSet<GlobalParametersMultipleHist> GlobalParametersMultipleHist { get; set; }

        internal virtual DbSet<CountryParameters> CountryParameters { get; set; }

        internal virtual DbSet<GlobalParametersMultipleCols> GlobalParametersMultipleCols { get; set; }

        internal virtual DbSet<GlobalParametersPolityRegions> GlobalParametersPolityRegions { get; set; }

        internal virtual DbSet<GlobalParametersTimeSeriesLong> GlobalParametersTimeSeriesLong { get; set; }
        
        internal virtual DbSet<AgGlobalFeedtoDomSuppRatios> AgGlobalFeedtoDomSuppRatios { get; set; }

        internal virtual DbSet<GTAPSectorConversion> GTAPSectorConversion { get; set; }

        internal virtual DbSet<IOThresholds> IOThresholds { get; set; }

        internal virtual DbSet<IOThresholdsHist> IOThresholdsHist { get; set; }

        internal virtual DbSet<CountryTranslation> CountryTranslation { get; set; }

        internal virtual DbSet<Economic> Economic { get; set; }

        internal virtual DbSet<SwingStates> SwingStates { get; set; }

        internal virtual DbSet<AIInit> AIInit { get; set; }

        internal virtual DbSet<Contiguity> Contiguity { get; set; }

        internal virtual DbSet<TerritorialDisputes> TerritorialDisputes { get; set; }

        internal virtual DbSet<CountryWasColonyOf> CountryWasColonyOf { get; set; }

        internal virtual DbSet<HealthDetailedDeathsCtry> HealthDetailedDeathsCtry { get; set; }

        internal virtual DbSet<HealthWHOLowGDPModel> HealthWHOLowGDPModel { get; set; }

        internal virtual DbSet<SocialHist> SocialHist { get; set; }

        internal virtual DbSet<WVSCohortWave2> WVSCohortWave2 { get; set; }

        internal virtual DbSet<CulturalParameters> CulturalParameters2 { get; set; }

        internal virtual DbSet<IOEngelAlphaCoefs> IOEngelAlphaCoefs { get; set; }

        //jrs 2022/09/19 not being used anymore, loaded directly from Detailed Deaths
        //ttt 2019/03/29
        internal virtual DbSet<HealthGBDDeaths> HealthGBDDeaths { get; set; }

        internal virtual DbSet<HealthGBDYLD> HealthGBDYLD { get; set; }

        //jrs 2019/07/18
        internal virtual DbSet<IncConSav> IncConSav { get; set; }

        internal virtual DbSet<HealthProxDrivRRsList> HealthProxDrivRRsList { get; set; }

        internal virtual DbSet<HealthProxDrivRRs> HealthProxDrivRRs { get; set; }

        internal virtual DbSet<HealthProxDrivRRsBlack> HealthProxDrivRRsBlack { get; set; }
        internal virtual DbSet<HealthProxDrivRRsHH> HealthProxDrivRRsHH { get; set; }
        internal virtual DbSet<HealthProxDrivRRsHL> HealthProxDrivRRsHL { get; set; }
        internal virtual DbSet<HealthProxDrivRRsHM> HealthProxDrivRRsHM { get; set; }
        internal virtual DbSet<HealthProxDrivRRsLH> HealthProxDrivRRsLH { get; set; }
        internal virtual DbSet<HealthProxDrivRRsLL> HealthProxDrivRRsLL { get; set; }
        internal virtual DbSet<HealthProxDrivRRsML> HealthProxDrivRRsML { get; set; }
        internal virtual DbSet<HealthProxDrivRRsOlofin> HealthProxDrivRRsOlofin { get; set; }
        internal virtual DbSet<HealthProxDrivRRsOlofinHigh> HealthProxDrivRRsOlofinHigh { get; set; }
        internal virtual DbSet<HealthProxDrivRRsOlofinLow> HealthProxDrivRRsOlofinLow { get; set; }
        internal virtual DbSet<HealthProxDrivRRsOlofinW4A> HealthProxDrivRRsOlofinW4A { get; set; }

        internal virtual DbSet<HealthMortFactorCommDisMale> HealthMortFactorCommDisMale { get; set; }
        internal virtual DbSet<HealthMortFactorCommDisFemale> HealthMortFactorCommDisFemale { get; set; }
        internal virtual DbSet<HealthMortFactorNonCommDisMale> HealthMortFactorNonCommDisMale { get; set; }
        internal virtual DbSet<HealthMortFactorNonCommDisFemale> HealthMortFactorNonCommDisFemale { get; set; }
        internal virtual DbSet<HealthMortFactorInjuryMale> HealthMortFactorInjuryMale { get; set; }
        internal virtual DbSet<HealthMortFactorInjuryFemale> HealthMortFactorInjuryFemale { get; set; }

        internal virtual DbSet<HealthDetailed5YearsDeathsRegs> HealthDetailed5YearsDeathsRegs { get; set; }

        internal virtual DbSet<HealthMortRatios> HealthMortRatios { get; set; }
        internal virtual DbSet<HealthCostByType> HealthCostByType { get; set; }

        internal virtual DbSet<SharedRiversCountry> SharedRiversCountry { get; set; }
        internal virtual DbSet<GovConflict> GovConflict { get; set; }

        internal virtual DbSet<Currency> Currency { get; set; }

        internal virtual DbSet<TariffRatePerSectorCountry> TariffRatePerSectorCountry { get; set; }
        internal virtual DbSet<TariffRevPerSectorCountry> TariffRevPerSectorCountry { get; set; }
        
        internal virtual DbSet<PovAgeSexGeneric> PovAgeSexGeneric { get; set; }
        internal virtual DbSet<PovRateAgeBothSexes> PovRateAgeBothSexes { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesHist> PovRateAgeBothSexesHist { get; set; }
        internal virtual DbSet<PovRateAgeBothSexesUN> PovRateAgeBothSexesUN { get; set; }
        internal virtual DbSet<PovRateAgeMale> PovRateAgeMale { get; set; }
        internal virtual DbSet<PovRateAgeMaleHist> PovRateAgeMaleHist { get; set; }
        internal virtual DbSet<PovRateAgeMaleUN> PovRateAgeMaleUN { get; set; }
        internal virtual DbSet<PovRateAgeFemale> PovRateAgeFemale { get; set; }
        internal virtual DbSet<PovRateAgeFemaleHist> PovRateAgeFemaleHist { get; set; }
        internal virtual DbSet<PovRateAgeFemaleUN> PovRateAgeFemaleUN { get; set; }
        //jrs 2022/05/02 EnergyNoProductionCountries
        internal virtual DbSet<EnergyNoProductionCountries> EnergyNoProductionCountries { get; set; }
    }

    internal partial class IFs2Context
    {

        internal virtual DbSet<AnnualGrowthRates> AnnualGrowthRates { get; set; }

        internal virtual DbSet<AnnualGrowthRatesExt> AnnualGrowthRatesExt { get; set; }
        
        internal virtual DbSet<AnnualGrowthRatesHist> AnnualGrowthRatesHist { get; set; }

        internal virtual DbSet<PopAgeCohortRegionMale> PopAgeCohortRegionMale { get; set; }

        internal virtual DbSet<PopAgeSingleRegionMale> PopAgeSingleRegionMale { get; set; }

        internal virtual DbSet<PopAgeCohortRegionMaleHist> PopAgeCohortRegionMaleHist { get; set; }

        internal virtual DbSet<PopAgeCohortRegionFemale> PopAgeCohortRegionFemale { get; set; }

        internal virtual DbSet<PopAgeSingleRegionFemale> PopAgeSingleRegionFemale { get; set; }

        internal virtual DbSet<PopAgeCohortRegionFemaleHist> PopAgeCohortRegionFemaleHist { get; set; }

        internal virtual DbSet<PopFertilityCohortRegionFemale> PopFertilityCohortRegionFemale { get; set; }

        internal virtual DbSet<PopFertilityCohortRegionFemaleHist> PopFertilityCohortRegionFemaleHist { get; set; }

        internal virtual DbSet<PopMortalityCohortRegionMale> PopMortalityCohortRegionMale { get; set; }

        internal virtual DbSet<PopMortalityCohortRegionMaleHist> PopMortalityCohortRegionMaleHist { get; set; }

        internal virtual DbSet<PopMortalityCohortRegionFemale> PopMortalityCohortRegionFemale { get; set; }

        internal virtual DbSet<PopMortalityCohortRegionFemaleHist> PopMortalityCohortRegionFemaleHist { get; set; }

        internal virtual DbSet<HealthYLDComMatPerCohortRegionMale> HealthYLDComMatPerCohortRegionMale { get; set; }

        internal virtual DbSet<HealthYLDComMatPerCohortRegionFemale> HealthYLDComMatPerCohortRegionFemale { get; set; }

        internal virtual DbSet<HealthYLDNonComDisCohortRegionMale> HealthYLDNonComDisCohortRegionMale { get; set; }

        internal virtual DbSet<HealthYLDNonComDisCohortRegionFemale> HealthYLDNonComDisCohortRegionFemale { get; set; }

        internal virtual DbSet<HealthYLDInjuriesCohortRegionMale> HealthYLDInjuriesCohortRegionMale { get; set; }

        internal virtual DbSet<HealthYLDInjuriesCohortRegionFemale> HealthYLDInjuriesCohortRegionFemale { get; set; }

        //mti 2024/05/23 start
        internal virtual DbSet<EducAttainPrimaryMaleWICRegion> EducAttainPrimaryMaleWICRegion { get; set; }
        internal virtual DbSet<EducAttainPrimaryFemaleWICRegion> EducAttainPrimaryFemaleWICRegion { get; set; }
        internal virtual DbSet<EducAttainSecUpperMaleWICRegion> EducAttainSecUpperMaleWICRegion { get; set; }
        internal virtual DbSet<EducAttainSecUpperFemaleWICRegion> EducAttainSecUpperFemaleWICRegion { get; set; }
        internal virtual DbSet<EducAttainTertiaryMaleWICRegion> EducAttainTertiaryMaleWICRegion { get; set; }
        internal virtual DbSet<EducAttainTertiaryFemaleWICRegion> EducAttainTertiaryFemaleWICRegion { get; set; }

        //mti 2024/05/23 end

        internal virtual DbSet<EducAttainNoEducMaleRegion> EducAttainNoEducMaleRegion { get; set; }
        internal virtual DbSet<EducAttainNoEducFemaleRegion> EducAttainNoEducFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainNoEducTotalRegion> EducAttainNoEducTotalRegion { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteMaleRegion> EducAttainPriIncompleteMaleRegion { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteFemaleRegion> EducAttainPriIncompleteFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainPriIncompleteTotalRegion> EducAttainPriIncompleteTotalRegion { get; set; }
        internal virtual DbSet<EducAttainPriCompleteMaleRegion> EducAttainPriCompleteMaleRegion { get; set; }
        internal virtual DbSet<EducAttainPriCompleteFemaleRegion> EducAttainPriCompleteFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainPriCompleteTotalRegion> EducAttainPriCompleteTotalRegion { get; set; }
        internal virtual DbSet<EducAttainSecLowerMaleRegion> EducAttainSecLowerMaleRegion { get; set; }
        internal virtual DbSet<EducAttainSecLowerFemaleRegion> EducAttainSecLowerFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainSecLowerTotalRegion> EducAttainSecLowerTotalRegion { get; set; }
        internal virtual DbSet<EducAttainSecUpperMaleRegion> EducAttainSecUpperMaleRegion { get; set; }
        internal virtual DbSet<EducAttainSecUpperFemaleRegion> EducAttainSecUpperFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainSecUpperTotalRegion> EducAttainSecUpperTotalRegion { get; set; }
        internal virtual DbSet<EducAttainTertiaryMaleRegion> EducAttainTertiaryMaleRegion { get; set; }
        internal virtual DbSet<EducAttainTertiaryFemaleRegion> EducAttainTertiaryFemaleRegion { get; set; }
        internal virtual DbSet<EducAttainTertiaryTotalRegion> EducAttainTertiaryTotalRegion { get; set; }

        internal virtual DbSet<SeriesRegGDP2011> SeriesRegGDP2011 { get; set; }

        internal virtual DbSet<GTAPIOAggregationCountry> GTAPIOAggregationCountry { get; set; }

        internal virtual DbSet<GTAPIOAggregationCountryHist> GTAPIOAggregationCountryHist { get; set; }

        internal virtual DbSet<GTAPValueAddedCountry> GTAPValueAddedCountry { get; set; }

        internal virtual DbSet<GTAPValueAddedCountryHist> GTAPValueAddedCountryHist { get; set; }

        internal virtual DbSet<IFsIOCoefCalcCountry> IFsIOCoefCalcCountry { get; set; }

        internal virtual DbSet<IFsIOCoefCalcCountryHist> IFsIOCoefCalcCountryHist { get; set; }

        internal virtual DbSet<IOMatricesCalc> IOMatricesCalc { get; set; }

        internal virtual DbSet<IOMatricesCalcHist> IOMatricesCalcHist { get; set; }

        internal virtual DbSet<IOLaborCoefs> IOLaborCoefs { get; set; }

        internal virtual DbSet<IOLaborCoefsHist> IOLaborCoefsHist { get; set; }

        internal virtual DbSet<IFsIOCoefCalcCountryEnAdj> IFsIOCoefCalcCountryEnAdj { get; set; }

        internal virtual DbSet<IFsIOCoefCalcCountryEnAdjHist> IFsIOCoefCalcCountryEnAdjHist { get; set; }

        internal virtual DbSet<IFsIOCoefCalcBaseFull> IFsIOCoefCalcBaseFull { get; set; }

        internal virtual DbSet<IFsIOCoefCalcBaseFullHist> IFsIOCoefCalcBaseFullHist { get; set; }

        internal virtual DbSet<RegionWasColonyOf> RegionWasColonyOf { get; set; }

        internal virtual DbSet<HealthDetailedDeathsRegs> HealthDetailedDeathsRegs { get; set; }

        internal virtual DbSet<PopCohortAIDSRegion> PopCohortAIDSRegion { get; set; }

        internal virtual DbSet<CulturalParameters> CulturalParameters { get; set; }

        //ttt 2019/03/29
        internal virtual DbSet<PopCohortGlobal> PopCohortGlobal { get; set; }

        internal virtual DbSet<HealthDiarrheaReg> HealthDiarrheaReg { get; set; }

        internal virtual DbSet<HealthMalariaReg> HealthMalariaReg { get; set; }

        internal virtual DbSet<HealthMentalHealthReg> HealthMentalHealthReg { get; set; }

        internal virtual DbSet<HealthRespInfReg> HealthRespInfReg { get; set; }

        internal virtual DbSet<HealthRatioYLDYLLRegion> HealthRatioYLDYLLRegion { get; set; }
    }
}

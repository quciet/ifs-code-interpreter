using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{
    public abstract partial class ForecastDyad : IForecastDyad
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Actor")]
        public int Actor { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Partner")]
        public int Partner { get; set; }
        [Column("Year")]
        public int Year { get; set; }
        [Column("Val")]
        public float Val { get; set; }
    }

    public abstract partial class ForecastDyadW : IForecastDyadW
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Actor")]
        public int Actor { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Partner")]
        public int Partner { get; set; }
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
        [Column("2026")]
        public float? Val2026 { get; set; }
        [Column("2027")]
        public float? Val2027 { get; set; }
        [Column("2028")]
        public float? Val2028 { get; set; }
        [Column("2029")]
        public float? Val2029 { get; set; }
        [Column("2030")]
        public float? Val2030 { get; set; }
        [Column("2031")]
        public float? Val2031 { get; set; }
        [Column("2032")]
        public float? Val2032 { get; set; }
        [Column("2033")]
        public float? Val2033 { get; set; }
        [Column("2034")]
        public float? Val2034 { get; set; }
        [Column("2035")]
        public float? Val2035 { get; set; }
        [Column("2036")]
        public float? Val2036 { get; set; }
        [Column("2037")]
        public float? Val2037 { get; set; }
        [Column("2038")]
        public float? Val2038 { get; set; }
        [Column("2039")]
        public float? Val2039 { get; set; }
        [Column("2040")]
        public float? Val2040 { get; set; }
        [Column("2041")]
        public float? Val2041 { get; set; }
        [Column("2042")]
        public float? Val2042 { get; set; }
        [Column("2043")]
        public float? Val2043 { get; set; }
        [Column("2044")]
        public float? Val2044 { get; set; }
        [Column("2045")]
        public float? Val2045 { get; set; }
        [Column("2046")]
        public float? Val2046 { get; set; }
        [Column("2047")]
        public float? Val2047 { get; set; }
        [Column("2048")]
        public float? Val2048 { get; set; }
        [Column("2049")]
        public float? Val2049 { get; set; }
        [Column("2050")]
        public float? Val2050 { get; set; }
        [Column("2051")]
        public float? Val2051 { get; set; }
        [Column("2052")]
        public float? Val2052 { get; set; }
        [Column("2053")]
        public float? Val2053 { get; set; }
        [Column("2054")]
        public float? Val2054 { get; set; }
        [Column("2055")]
        public float? Val2055 { get; set; }
        [Column("2056")]
        public float? Val2056 { get; set; }
        [Column("2057")]
        public float? Val2057 { get; set; }
        [Column("2058")]
        public float? Val2058 { get; set; }
        [Column("2059")]
        public float? Val2059 { get; set; }
        [Column("2060")]
        public float? Val2060 { get; set; }
        [Column("2061")]
        public float? Val2061 { get; set; }
        [Column("2062")]
        public float? Val2062 { get; set; }
        [Column("2063")]
        public float? Val2063 { get; set; }
        [Column("2064")]
        public float? Val2064 { get; set; }
        [Column("2065")]
        public float? Val2065 { get; set; }
        [Column("2066")]
        public float? Val2066 { get; set; }
        [Column("2067")]
        public float? Val2067 { get; set; }
        [Column("2068")]
        public float? Val2068 { get; set; }
        [Column("2069")]
        public float? Val2069 { get; set; }
        [Column("2070")]
        public float? Val2070 { get; set; }
        [Column("2071")]
        public float? Val2071 { get; set; }
        [Column("2072")]
        public float? Val2072 { get; set; }
        [Column("2073")]
        public float? Val2073 { get; set; }
        [Column("2074")]
        public float? Val2074 { get; set; }
        [Column("2075")]
        public float? Val2075 { get; set; }
        [Column("2076")]
        public float? Val2076 { get; set; }
        [Column("2077")]
        public float? Val2077 { get; set; }
        [Column("2078")]
        public float? Val2078 { get; set; }
        [Column("2079")]
        public float? Val2079 { get; set; }
        [Column("2080")]
        public float? Val2080 { get; set; }
        [Column("2081")]
        public float? Val2081 { get; set; }
        [Column("2082")]
        public float? Val2082 { get; set; }
        [Column("2083")]
        public float? Val2083 { get; set; }
        [Column("2084")]
        public float? Val2084 { get; set; }
        [Column("2085")]
        public float? Val2085 { get; set; }
        [Column("2086")]
        public float? Val2086 { get; set; }
        [Column("2087")]
        public float? Val2087 { get; set; }
        [Column("2088")]
        public float? Val2088 { get; set; }
        [Column("2089")]
        public float? Val2089 { get; set; }
        [Column("2090")]
        public float? Val2090 { get; set; }
        [Column("2091")]
        public float? Val2091 { get; set; }
        [Column("2092")]
        public float? Val2092 { get; set; }
        [Column("2093")]
        public float? Val2093 { get; set; }
        [Column("2094")]
        public float? Val2094 { get; set; }
        [Column("2095")]
        public float? Val2095 { get; set; }
        [Column("2096")]
        public float? Val2096 { get; set; }
        [Column("2097")]
        public float? Val2097 { get; set; }
        [Column("2098")]
        public float? Val2098 { get; set; }
        [Column("2099")]
        public float? Val2099 { get; set; }
        [Column("2100")]
        public float? Val2100 { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, float> ForecastYears
        {
            get
            {
                return allForecastYears().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        private IEnumerable<KeyValuePair<string, float?>> allForecastYears()
        {
            yield return new KeyValuePair<string, float?>("1960", Val1960);
            yield return new KeyValuePair<string, float?>("1961", Val1961);
            yield return new KeyValuePair<string, float?>("1962", Val1962);
            yield return new KeyValuePair<string, float?>("1963", Val1963);
            yield return new KeyValuePair<string, float?>("1964", Val1964);
            yield return new KeyValuePair<string, float?>("1965", Val1965);
            yield return new KeyValuePair<string, float?>("1966", Val1966);
            yield return new KeyValuePair<string, float?>("1967", Val1967);
            yield return new KeyValuePair<string, float?>("1968", Val1968);
            yield return new KeyValuePair<string, float?>("1969", Val1969);
            yield return new KeyValuePair<string, float?>("1970", Val1970);
            yield return new KeyValuePair<string, float?>("1971", Val1971);
            yield return new KeyValuePair<string, float?>("1972", Val1972);
            yield return new KeyValuePair<string, float?>("1973", Val1973);
            yield return new KeyValuePair<string, float?>("1974", Val1974);
            yield return new KeyValuePair<string, float?>("1975", Val1975);
            yield return new KeyValuePair<string, float?>("1976", Val1976);
            yield return new KeyValuePair<string, float?>("1977", Val1977);
            yield return new KeyValuePair<string, float?>("1978", Val1978);
            yield return new KeyValuePair<string, float?>("1979", Val1979);
            yield return new KeyValuePair<string, float?>("1980", Val1980);
            yield return new KeyValuePair<string, float?>("1981", Val1981);
            yield return new KeyValuePair<string, float?>("1982", Val1982);
            yield return new KeyValuePair<string, float?>("1983", Val1983);
            yield return new KeyValuePair<string, float?>("1984", Val1984);
            yield return new KeyValuePair<string, float?>("1985", Val1985);
            yield return new KeyValuePair<string, float?>("1986", Val1986);
            yield return new KeyValuePair<string, float?>("1987", Val1987);
            yield return new KeyValuePair<string, float?>("1988", Val1988);
            yield return new KeyValuePair<string, float?>("1989", Val1989);
            yield return new KeyValuePair<string, float?>("1990", Val1990);
            yield return new KeyValuePair<string, float?>("1991", Val1991);
            yield return new KeyValuePair<string, float?>("1992", Val1992);
            yield return new KeyValuePair<string, float?>("1993", Val1993);
            yield return new KeyValuePair<string, float?>("1994", Val1994);
            yield return new KeyValuePair<string, float?>("1995", Val1995);
            yield return new KeyValuePair<string, float?>("1996", Val1996);
            yield return new KeyValuePair<string, float?>("1997", Val1997);
            yield return new KeyValuePair<string, float?>("1998", Val1998);
            yield return new KeyValuePair<string, float?>("1999", Val1999);
            yield return new KeyValuePair<string, float?>("2000", Val2000);
            yield return new KeyValuePair<string, float?>("2001", Val2001);
            yield return new KeyValuePair<string, float?>("2002", Val2002);
            yield return new KeyValuePair<string, float?>("2003", Val2003);
            yield return new KeyValuePair<string, float?>("2004", Val2004);
            yield return new KeyValuePair<string, float?>("2005", Val2005);
            yield return new KeyValuePair<string, float?>("2006", Val2006);
            yield return new KeyValuePair<string, float?>("2007", Val2007);
            yield return new KeyValuePair<string, float?>("2008", Val2008);
            yield return new KeyValuePair<string, float?>("2009", Val2009);
            yield return new KeyValuePair<string, float?>("2010", Val2010);
            yield return new KeyValuePair<string, float?>("2011", Val2011);
            yield return new KeyValuePair<string, float?>("2012", Val2012);
            yield return new KeyValuePair<string, float?>("2013", Val2013);
            yield return new KeyValuePair<string, float?>("2014", Val2014);
            yield return new KeyValuePair<string, float?>("2015", Val2015);
            yield return new KeyValuePair<string, float?>("2016", Val2016);
            yield return new KeyValuePair<string, float?>("2017", Val2017);
            yield return new KeyValuePair<string, float?>("2018", Val2018);
            yield return new KeyValuePair<string, float?>("2019", Val2019);
            yield return new KeyValuePair<string, float?>("2020", Val2020);
            yield return new KeyValuePair<string, float?>("2021", Val2021);
            yield return new KeyValuePair<string, float?>("2022", Val2022);
            yield return new KeyValuePair<string, float?>("2023", Val2023);
            yield return new KeyValuePair<string, float?>("2024", Val2024);
            yield return new KeyValuePair<string, float?>("2025", Val2025);
            yield return new KeyValuePair<string, float?>("2026", Val2026);
            yield return new KeyValuePair<string, float?>("2027", Val2027);
            yield return new KeyValuePair<string, float?>("2028", Val2028);
            yield return new KeyValuePair<string, float?>("2029", Val2029);
            yield return new KeyValuePair<string, float?>("2030", Val2030);
            yield return new KeyValuePair<string, float?>("2031", Val2031);
            yield return new KeyValuePair<string, float?>("2032", Val2032);
            yield return new KeyValuePair<string, float?>("2033", Val2033);
            yield return new KeyValuePair<string, float?>("2034", Val2034);
            yield return new KeyValuePair<string, float?>("2035", Val2035);
            yield return new KeyValuePair<string, float?>("2036", Val2036);
            yield return new KeyValuePair<string, float?>("2037", Val2037);
            yield return new KeyValuePair<string, float?>("2038", Val2038);
            yield return new KeyValuePair<string, float?>("2039", Val2039);
            yield return new KeyValuePair<string, float?>("2040", Val2040);
            yield return new KeyValuePair<string, float?>("2041", Val2041);
            yield return new KeyValuePair<string, float?>("2042", Val2042);
            yield return new KeyValuePair<string, float?>("2043", Val2043);
            yield return new KeyValuePair<string, float?>("2044", Val2044);
            yield return new KeyValuePair<string, float?>("2045", Val2045);
            yield return new KeyValuePair<string, float?>("2046", Val2046);
            yield return new KeyValuePair<string, float?>("2047", Val2047);
            yield return new KeyValuePair<string, float?>("2048", Val2048);
            yield return new KeyValuePair<string, float?>("2049", Val2049);
            yield return new KeyValuePair<string, float?>("2050", Val2050);
            yield return new KeyValuePair<string, float?>("2051", Val2051);
            yield return new KeyValuePair<string, float?>("2052", Val2052);
            yield return new KeyValuePair<string, float?>("2053", Val2053);
            yield return new KeyValuePair<string, float?>("2054", Val2054);
            yield return new KeyValuePair<string, float?>("2055", Val2055);
            yield return new KeyValuePair<string, float?>("2056", Val2056);
            yield return new KeyValuePair<string, float?>("2057", Val2057);
            yield return new KeyValuePair<string, float?>("2058", Val2058);
            yield return new KeyValuePair<string, float?>("2059", Val2059);
            yield return new KeyValuePair<string, float?>("2060", Val2060);
            yield return new KeyValuePair<string, float?>("2061", Val2061);
            yield return new KeyValuePair<string, float?>("2062", Val2062);
            yield return new KeyValuePair<string, float?>("2063", Val2063);
            yield return new KeyValuePair<string, float?>("2064", Val2064);
            yield return new KeyValuePair<string, float?>("2065", Val2065);
            yield return new KeyValuePair<string, float?>("2066", Val2066);
            yield return new KeyValuePair<string, float?>("2067", Val2067);
            yield return new KeyValuePair<string, float?>("2068", Val2068);
            yield return new KeyValuePair<string, float?>("2069", Val2069);
            yield return new KeyValuePair<string, float?>("2070", Val2070);
            yield return new KeyValuePair<string, float?>("2071", Val2071);
            yield return new KeyValuePair<string, float?>("2072", Val2072);
            yield return new KeyValuePair<string, float?>("2073", Val2073);
            yield return new KeyValuePair<string, float?>("2074", Val2074);
            yield return new KeyValuePair<string, float?>("2075", Val2075);
            yield return new KeyValuePair<string, float?>("2076", Val2076);
            yield return new KeyValuePair<string, float?>("2077", Val2077);
            yield return new KeyValuePair<string, float?>("2078", Val2078);
            yield return new KeyValuePair<string, float?>("2079", Val2079);
            yield return new KeyValuePair<string, float?>("2080", Val2080);
            yield return new KeyValuePair<string, float?>("2081", Val2081);
            yield return new KeyValuePair<string, float?>("2082", Val2082);
            yield return new KeyValuePair<string, float?>("2083", Val2083);
            yield return new KeyValuePair<string, float?>("2084", Val2084);
            yield return new KeyValuePair<string, float?>("2085", Val2085);
            yield return new KeyValuePair<string, float?>("2086", Val2086);
            yield return new KeyValuePair<string, float?>("2087", Val2087);
            yield return new KeyValuePair<string, float?>("2088", Val2088);
            yield return new KeyValuePair<string, float?>("2089", Val2089);
            yield return new KeyValuePair<string, float?>("2090", Val2090);
            yield return new KeyValuePair<string, float?>("2091", Val2091);
            yield return new KeyValuePair<string, float?>("2092", Val2092);
            yield return new KeyValuePair<string, float?>("2093", Val2093);
            yield return new KeyValuePair<string, float?>("2094", Val2094);
            yield return new KeyValuePair<string, float?>("2095", Val2095);
            yield return new KeyValuePair<string, float?>("2096", Val2096);
            yield return new KeyValuePair<string, float?>("2097", Val2097);
            yield return new KeyValuePair<string, float?>("2098", Val2098);
            yield return new KeyValuePair<string, float?>("2099", Val2099);
            yield return new KeyValuePair<string, float?>("2100", Val2100);
        }
    }

    public interface IForecastDyad
    {
        int Actor { get; set; }
        int Partner { get; set; }
        int Year { get; set; }
        float Val { get; set; }
    }

    public interface IForecastDyadW
    {
        int Actor { get; set; }
        int Partner { get; set; }
        float? Val1960 { get; set; }
        float? Val1961 { get; set; }
        float? Val1962 { get; set; }
        float? Val1963 { get; set; }
        float? Val1964 { get; set; }
        float? Val1965 { get; set; }
        float? Val1966 { get; set; }
        float? Val1967 { get; set; }
        float? Val1968 { get; set; }
        float? Val1969 { get; set; }
        float? Val1970 { get; set; }
        float? Val1971 { get; set; }
        float? Val1972 { get; set; }
        float? Val1973 { get; set; }
        float? Val1974 { get; set; }
        float? Val1975 { get; set; }
        float? Val1976 { get; set; }
        float? Val1977 { get; set; }
        float? Val1978 { get; set; }
        float? Val1979 { get; set; }
        float? Val1980 { get; set; }
        float? Val1981 { get; set; }
        float? Val1982 { get; set; }
        float? Val1983 { get; set; }
        float? Val1984 { get; set; }
        float? Val1985 { get; set; }
        float? Val1986 { get; set; }
        float? Val1987 { get; set; }
        float? Val1988 { get; set; }
        float? Val1989 { get; set; }
        float? Val1990 { get; set; }
        float? Val1991 { get; set; }
        float? Val1992 { get; set; }
        float? Val1993 { get; set; }
        float? Val1994 { get; set; }
        float? Val1995 { get; set; }
        float? Val1996 { get; set; }
        float? Val1997 { get; set; }
        float? Val1998 { get; set; }
        float? Val1999 { get; set; }
        float? Val2000 { get; set; }
        float? Val2001 { get; set; }
        float? Val2002 { get; set; }
        float? Val2003 { get; set; }
        float? Val2004 { get; set; }
        float? Val2005 { get; set; }
        float? Val2006 { get; set; }
        float? Val2007 { get; set; }
        float? Val2008 { get; set; }
        float? Val2009 { get; set; }
        float? Val2010 { get; set; }
        float? Val2011 { get; set; }
        float? Val2012 { get; set; }
        float? Val2013 { get; set; }
        float? Val2014 { get; set; }
        float? Val2015 { get; set; }
        float? Val2016 { get; set; }
        float? Val2017 { get; set; }
        float? Val2018 { get; set; }
        float? Val2019 { get; set; }
        float? Val2020 { get; set; }
        float? Val2021 { get; set; }
        float? Val2022 { get; set; }
        float? Val2023 { get; set; }
        float? Val2024 { get; set; }
        float? Val2025 { get; set; }
        float? Val2026 { get; set; }
        float? Val2027 { get; set; }
        float? Val2028 { get; set; }
        float? Val2029 { get; set; }
        float? Val2030 { get; set; }
        float? Val2031 { get; set; }
        float? Val2032 { get; set; }
        float? Val2033 { get; set; }
        float? Val2034 { get; set; }
        float? Val2035 { get; set; }
        float? Val2036 { get; set; }
        float? Val2037 { get; set; }
        float? Val2038 { get; set; }
        float? Val2039 { get; set; }
        float? Val2040 { get; set; }
        float? Val2041 { get; set; }
        float? Val2042 { get; set; }
        float? Val2043 { get; set; }
        float? Val2044 { get; set; }
        float? Val2045 { get; set; }
        float? Val2046 { get; set; }
        float? Val2047 { get; set; }
        float? Val2048 { get; set; }
        float? Val2049 { get; set; }
        float? Val2050 { get; set; }
        float? Val2051 { get; set; }
        float? Val2052 { get; set; }
        float? Val2053 { get; set; }
        float? Val2054 { get; set; }
        float? Val2055 { get; set; }
        float? Val2056 { get; set; }
        float? Val2057 { get; set; }
        float? Val2058 { get; set; }
        float? Val2059 { get; set; }
        float? Val2060 { get; set; }
        float? Val2061 { get; set; }
        float? Val2062 { get; set; }
        float? Val2063 { get; set; }
        float? Val2064 { get; set; }
        float? Val2065 { get; set; }
        float? Val2066 { get; set; }
        float? Val2067 { get; set; }
        float? Val2068 { get; set; }
        float? Val2069 { get; set; }
        float? Val2070 { get; set; }
        float? Val2071 { get; set; }
        float? Val2072 { get; set; }
        float? Val2073 { get; set; }
        float? Val2074 { get; set; }
        float? Val2075 { get; set; }
        float? Val2076 { get; set; }
        float? Val2077 { get; set; }
        float? Val2078 { get; set; }
        float? Val2079 { get; set; }
        float? Val2080 { get; set; }
        float? Val2081 { get; set; }
        float? Val2082 { get; set; }
        float? Val2083 { get; set; }
        float? Val2084 { get; set; }
        float? Val2085 { get; set; }
        float? Val2086 { get; set; }
        float? Val2087 { get; set; }
        float? Val2088 { get; set; }
        float? Val2089 { get; set; }
        float? Val2090 { get; set; }
        float? Val2091 { get; set; }
        float? Val2092 { get; set; }
        float? Val2093 { get; set; }
        float? Val2094 { get; set; }
        float? Val2095 { get; set; }
        float? Val2096 { get; set; }
        float? Val2097 { get; set; }
        float? Val2098 { get; set; }
        float? Val2099 { get; set; }
        float? Val2100 { get; set; }

    }

    [Table("XDYAD")]
    public partial class XDYAD : ForecastDyad
    {
    }

    [Table("XDYADAGR")]
    public partial class XDYADAGR : ForecastDyad
    {
    }

    [Table("XDYADENE")]
    public partial class XDYADENE : ForecastDyad
    {
    }

    [Table("XDYADICT")]
    public partial class XDYADICT : ForecastDyad
    {
    }

    [Table("XDYADMAN")]
    public partial class XDYADMAN : ForecastDyad
    {
    }

    [Table("XDYADMAT")]
    public partial class XDYADMAT : ForecastDyad
    {
    }

    [Table("XDYADSER")]
    public partial class XDYADSER : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEAGR")]
    public partial class DYADTARIFFRATEAGR : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEENE")]
    public partial class DYADTARIFFRATEENE : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEICT")]
    public partial class DYADTARIFFRATEICT : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEMAN")]
    public partial class DYADTARIFFRATEMAN : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEMAT")]
    public partial class DYADTARIFFRATEMAT : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATESER")]
    public partial class DYADTARIFFRATESER : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVAGR")]
    public partial class DYADTARIFFREVAGR : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVENE")]
    public partial class DYADTARIFFREVENE : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVICT")]
    public partial class DYADTARIFFREVICT : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVMAN")]
    public partial class DYADTARIFFREVMAN : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVMAT")]
    public partial class DYADTARIFFREVMAT : ForecastDyad
    {
    }

    [Table("DYADTARIFFREVSER")]
    public partial class DYADTARIFFREVSER : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTAGR")]
    public partial class DYADTRADECOMPLEMENTAGR : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTENE")]
    public partial class DYADTRADECOMPLEMENTENE : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTICT")]
    public partial class DYADTRADECOMPLEMENTICT : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTMAN")]
    public partial class DYADTRADECOMPLEMENTMAN : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTMAT")]
    public partial class DYADTRADECOMPLEMENTMAT : ForecastDyad
    {
    }

    [Table("DYADTRADECOMPLEMENTSER")]
    public partial class DYADTRADECOMPLEMENTSER : ForecastDyad
    {
    }

    [Table("XDYADMIL")]
    public partial class XDYADMIL : ForecastDyad
    {
    }

    [Table("XDYADFDI")]
    public partial class XDYADFDI : ForecastDyad
    {
    }

    [Table("XDYADAID")]
    public partial class XDYADAID : ForecastDyad
    {
    }

    [Table("XDYADMIGRANT")]
    public partial class XDYADMIGRANT : ForecastDyad
    {
    }

    [Table("XDYADLOR")]
    public partial class XDYADLOR : ForecastDyad
    {
    }

    [Table("DYADMIGRANTFORCED")]
    public partial class DYADMIGRANTFORCED : ForecastDyad
    {
    }

    [Table("DYADMIGRANTFORCEDRETURN")]
    public partial class DYADMIGRANTFORCEDRETURN : ForecastDyad
    {
    }

    [Table("DYADPOPFOREIGNFORCED")]
    public partial class DYADPOPFOREIGNFORCED : ForecastDyad
    {
    }

    [Table("DYADMIGFORCEDTARGETPCTPOP")]
    public partial class DYADMIGFORCEDTARGETPCTPOP : ForecastDyad
    {
    }

    [Table("DYADMIGVOLTARGETPCTPOP")]
    public partial class DYADMIGVOLTARGETPCTPOP : ForecastDyad
    {
    }

    [Table("MDYAD")]
    public partial class MDYAD : ForecastDyad
    {
    }

    [Table("MDYADAGR")]
    public partial class MDYADAGR : ForecastDyad
    {
    }

    [Table("MDYADENE")]
    public partial class MDYADENE : ForecastDyad
    {
    }

    [Table("MDYADICT")]
    public partial class MDYADICT : ForecastDyad
    {
    }

    [Table("MDYADMAN")]
    public partial class MDYADMAN : ForecastDyad
    {
    }

    [Table("MDYADMAT")]
    public partial class MDYADMAT : ForecastDyad
    {
    }

    [Table("MDYADSER")]
    public partial class MDYADSER : ForecastDyad
    {
    }

    [Table("MDYADMIL")]
    public partial class MDYADMIL : ForecastDyad
    {
    }

    [Table("MDYADFDI")]
    public partial class MDYADFDI : ForecastDyad
    {
    }

    [Table("MDYADAID")]
    public partial class MDYADAID : ForecastDyad
    {
    }

    [Table("MDYADMIGRANT")]
    public partial class MDYADMIGRANT : ForecastDyad
    {
    }

    [Table("MDYADLOR")]
    public partial class MDYADLOR : ForecastDyad
    {
    }

    [Table("Distance")]
    public partial class Distance : ForecastDyad
    {
    }

    [Table("DYADALLIANCE")]
    public partial class DYADALLIANCE : ForecastDyad
    {
    }

    [Table("DYADIGO")]
    public partial class DYADIGO : ForecastDyad
    {
    }

    [Table("DYADTREATY")]
    public partial class DYADTREATY : ForecastDyad
    {
    }

    [Table("DYADBANDWIDTH")]
    public partial class DYADBANDWIDTH : ForecastDyad
    {
    }

    [Table("DYADDEPENDENCE")]
    public partial class DYADDEPENDENCE : ForecastDyad
    {
    }

    [Table("DYADINFLUENCE")]
    public partial class DYADINFLUENCE : ForecastDyad
    {
    }

    //jrs 2020/07/09 *** begin ***
    [Table("DYADINF_AID_PCTGDP")]
    public partial class DYADINF_AID_PCTGDP : ForecastDyad
    {
    }

    [Table("DYADINF_AID_PCTTOTALAID")]
    public partial class DYADINF_AID_PCTTOTALAID : ForecastDyad
    {
    }

    [Table("DYADINF_ARMS_PCTGDSMIL")]
    public partial class DYADINF_ARMS_PCTGDSMIL : ForecastDyad
    {
    }

    [Table("DYADINF_ARMS_PCTMONADICARMS")]
    public partial class DYADINF_ARMS_PCTMONADICARMS : ForecastDyad
    {
    }

    [Table("DYADINF_ARMSBANDWIDTH")]
    public partial class DYADINF_ARMSBANDWIDTH : ForecastDyad
    {
    }

    [Table("DYADINF_LOR_AVG")]
    public partial class DYADINF_LOR_AVG : ForecastDyad
    {
    }

    [Table("DYADINF_TOTALTRADE_PCTGDP")]
    public partial class DYADINF_TOTALTRADE_PCTGDP : ForecastDyad
    {
    }

    [Table("DYADINF_TOTALTRADE_PCTMONADICTRADE")]
    public partial class DYADINF_TOTALTRADE_PCTMONADICTRADE : ForecastDyad
    {
    }

    [Table("DYADINF_TRADEBANDWIDTH")]
    public partial class DYADINF_TRADEBANDWIDTH : ForecastDyad
    {
    }
    //jrs 2020/07/09 *** end ***

    [Table("DYADPOPFOREIGN")]
    public partial class DYADPOPFOREIGN : ForecastDyad
    {
    }

    [Table("DYADMILMSTOCK")]
    public partial class DYADMILMSTOCK : ForecastDyad
    {
    }
    //ttt 2019/09/05
    [Table("DYADMIGRANTVOL")]
    public partial class DYADMIGRANTVOL : ForecastDyad
    {
    }

    [Table("DYADPOPFOREIGNVOL")]
    public partial class DYADPOPFOREIGNVOL : ForecastDyad
    {
    }

    //jrs 2022/01/03
    [Table("DYADTRADEAGREEMENTINDEX")]
    public partial class DYADTRADEAGREEMENTINDEX : ForecastDyad
    {
    }

    //clp 2024/07/10
    [Table("CONFLICTYEARS")]
    public partial class CONFLICTYEARS : ForecastDyad
    {
    }

    //clp 2024/07/10
    [Table("CONFLICTYEARS_MIES")]
    public partial class CONFLICTYEARS_MIES : ForecastDyad
    {
    }

    //clp 2024/07/10
    [Table("CONFLICTYEARS_UCDP")]
    public partial class CONFLICTYEARS_UCDP : ForecastDyad
    {
    }

    //clp 2024/12/20
    [Table("RIVALRY")]
    public partial class RIVALRY : ForecastDyad
    {
    }

    //clp 2025/10/30
    [Table("DYDLDRTRIP")]
    public partial class DYDLDRTRIP : ForecastDyad
    {
    }

    //jrs 2025/07/29
    [Table("DYADTARIFFRATEAGRWGEL")]
    public partial class DYADTARIFFRATEAGRWGEL : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEENEWGEL")]
    public partial class DYADTARIFFRATEENEWGEL : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEICTWGEL")]
    public partial class DYADTARIFFRATEICTWGEL : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEMANWGEL")]
    public partial class DYADTARIFFRATEMANWGEL : ForecastDyad
    {
    }

    [Table("DYADTARIFFRATEMATWGEL")]
    public partial class DYADTARIFFRATEMATWGEL : ForecastDyad
    {
    }


    //ttt 2019/09/03
    [Table("ParametersDefault")]
    internal partial class ParametersDefault
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Column("DefaultValue")]
        public float DefaultValue { get; set; }
        [Column("Dim3")]
        public float? Dim3 { get; set; }
    }

    [Table("ParametersDetail")]
    internal partial class ParametersDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ParameterName")]
        public string ParameterName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Actor")]
        public int Actor { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Partner")]
        public int Partner { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Sector")]
        public int Sector { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Year")]
        public int Year { get; set; }
        [Column("ParameterValue")]
        public float ParameterValue { get; set; }
    }

    internal partial class IFsForDyadicContext
    {
        //jrs 2025/07/29
        internal virtual DbSet<DYADTARIFFRATEAGRWGEL> DYADTARIFFRATEAGRWGEL { get; set; }
        internal virtual DbSet<DYADTARIFFRATEENEWGEL> DYADTARIFFRATEENEWGEL { get; set; }
        internal virtual DbSet<DYADTARIFFRATEICTWGEL> DYADTARIFFRATEICTWGEL { get; set; }
        internal virtual DbSet<DYADTARIFFRATEMANWGEL> DYADTARIFFRATEMANWGEL { get; set; }
        internal virtual DbSet<DYADTARIFFRATEMATWGEL> DYADTARIFFRATEMATWGEL { get; set; }

        internal virtual DbSet<XDYAD> XDYAD { get; set; }
        internal virtual DbSet<XDYADAGR> XDYADAGR { get; set; }
        internal virtual DbSet<XDYADENE> XDYADENE { get; set; }
        internal virtual DbSet<XDYADICT> XDYADICT { get; set; }
        internal virtual DbSet<XDYADMAN> XDYADMAN { get; set; }
        internal virtual DbSet<XDYADMAT> XDYADMAT { get; set; }
        internal virtual DbSet<XDYADSER> XDYADSER { get; set; }

        internal virtual DbSet<XDYADMIL> XDYADMIL { get; set; }
        internal virtual DbSet<XDYADFDI> XDYADFDI { get; set; }
        internal virtual DbSet<XDYADAID> XDYADAID { get; set; }
        internal virtual DbSet<XDYADMIGRANT> XDYADMIGRANT { get; set; }
        internal virtual DbSet<XDYADLOR> XDYADLOR { get; set; }

        internal virtual DbSet<DYADMIGRANTFORCED> DYADMIGRANTFORCED { get; set; }
        internal virtual DbSet<DYADMIGRANTFORCEDRETURN> DYADMIGRANTFORCEDRETURN { get; set; }
        internal virtual DbSet<DYADPOPFOREIGNFORCED> DYADPOPFOREIGNFORCED { get; set; }
        internal virtual DbSet<DYADMIGFORCEDTARGETPCTPOP> DYADMIGFORCEDTARGETPCTPOP { get; set; }
        internal virtual DbSet<DYADMIGVOLTARGETPCTPOP> DYADMIGVOLTARGETPCTPOP { get; set; }

        internal virtual DbSet<MDYAD> MDYAD { get; set; }
        internal virtual DbSet<MDYADAGR> MDYADAGR { get; set; }
        internal virtual DbSet<MDYADENE> MDYADENE { get; set; }
        internal virtual DbSet<MDYADICT> MDYADICT { get; set; }
        internal virtual DbSet<MDYADMAN> MDYADMAN { get; set; }
        internal virtual DbSet<MDYADMAT> MDYADMAT { get; set; }
        internal virtual DbSet<MDYADSER> MDYADSER { get; set; }

        internal virtual DbSet<MDYADMIL> MDYADMIL { get; set; }
        internal virtual DbSet<MDYADFDI> MDYADFDI { get; set; }
        internal virtual DbSet<MDYADAID> MDYADAID { get; set; }
        internal virtual DbSet<MDYADMIGRANT> MDYADMIGRANT { get; set; }
        internal virtual DbSet<MDYADLOR> MDYADLOR { get; set; }

        internal virtual DbSet<Distance> Distance { get; set; }
        internal virtual DbSet<DYADALLIANCE> DYADALLIANCE { get; set; }
        internal virtual DbSet<DYADIGO> DYADIGO { get; set; }
        internal virtual DbSet<DYADTREATY> DYADTREATY { get; set; }
        internal virtual DbSet<DYADBANDWIDTH> DYADBANDWIDTH { get; set; }
        internal virtual DbSet<DYADDEPENDENCE> DYADDEPENDENCE { get; set; }
        internal virtual DbSet<DYADINFLUENCE> DYADINFLUENCE { get; set; }
        internal virtual DbSet<DYADPOPFOREIGN> DYADPOPFOREIGN { get; set; }
        internal virtual DbSet<DYADMILMSTOCK> DYADMILMSTOCK { get; set; }

        internal virtual DbSet<DYADTARIFFRATEAGR> DYADTARIFFRATEAGR { get; set; }
        internal virtual DbSet<DYADTARIFFRATEENE> DYADTARIFFRATEENE { get; set; }
        internal virtual DbSet<DYADTARIFFRATEICT> DYADTARIFFRATEICT { get; set; }
        internal virtual DbSet<DYADTARIFFRATEMAN> DYADTARIFFRATEMAN { get; set; }
        internal virtual DbSet<DYADTARIFFRATEMAT> DYADTARIFFRATEMAT { get; set; }
        internal virtual DbSet<DYADTARIFFRATESER> DYADTARIFFRATESER { get; set; }

        internal virtual DbSet<DYADTARIFFREVAGR> DYADTARIFFREVAGR { get; set; }
        internal virtual DbSet<DYADTARIFFREVENE> DYADTARIFFREVENE { get; set; }
        internal virtual DbSet<DYADTARIFFREVICT> DYADTARIFFREVICT { get; set; }
        internal virtual DbSet<DYADTARIFFREVMAN> DYADTARIFFREVMAN { get; set; }
        internal virtual DbSet<DYADTARIFFREVMAT> DYADTARIFFREVMAT { get; set; }
        internal virtual DbSet<DYADTARIFFREVSER> DYADTARIFFREVSER { get; set; }

        internal virtual DbSet<DYADTRADECOMPLEMENTAGR> DYADTRADECOMPLEMENTAGR { get; set; }
        internal virtual DbSet<DYADTRADECOMPLEMENTENE> DYADTRADECOMPLEMENTENE { get; set; }
        internal virtual DbSet<DYADTRADECOMPLEMENTICT> DYADTRADECOMPLEMENTICT { get; set; }
        internal virtual DbSet<DYADTRADECOMPLEMENTMAN> DYADTRADECOMPLEMENTMAN { get; set; }
        internal virtual DbSet<DYADTRADECOMPLEMENTMAT> DYADTRADECOMPLEMENTMAT { get; set; }
        internal virtual DbSet<DYADTRADECOMPLEMENTSER> DYADTRADECOMPLEMENTSER { get; set; }

        //jrs 2022/01/03
        internal virtual DbSet<DYADTRADEAGREEMENTINDEX> DYADTRADEAGREEMENTINDEX { get; set; }

        //ttt
        internal virtual DbSet<DYADMIGRANTVOL> DYADMIGRANTVOL { get; set; }
        internal virtual DbSet<DYADPOPFOREIGNVOL> DYADPOPFOREIGNVOL { get; set; }
        internal virtual DbSet<ParametersDefault> ParametersDefault { get; set; }
        internal virtual DbSet<ParametersDetail> ParametersDetail { get; set; }

        //jrs 2020/07/09
        internal virtual DbSet<DYADINF_AID_PCTGDP> DYADINF_AID_PCTGDP { get; set; }
        internal virtual DbSet<DYADINF_AID_PCTTOTALAID> DYADINF_AID_PCTTOTALAID { get; set; }
        internal virtual DbSet<DYADINF_ARMS_PCTGDSMIL> DYADINF_ARMS_PCTGDSMIL { get; set; }
        internal virtual DbSet<DYADINF_ARMS_PCTMONADICARMS> DYADINF_ARMS_PCTMONADICARMS { get; set; }
        internal virtual DbSet<DYADINF_ARMSBANDWIDTH> DYADINF_ARMSBANDWIDTH { get; set; }
        internal virtual DbSet<DYADINF_LOR_AVG> DYADINF_LOR_AVG { get; set; }
        internal virtual DbSet<DYADINF_TOTALTRADE_PCTGDP> DYADINF_TOTALTRADE_PCTGDP { get; set; }
        internal virtual DbSet<DYADINF_TOTALTRADE_PCTMONADICTRADE> DYADINF_TOTALTRADE_PCTMONADICTRADE { get; set; }
        internal virtual DbSet<DYADINF_TRADEBANDWIDTH> DYADINF_TRADEBANDWIDTH { get; set; }
        internal virtual DbSet<CONFLICTYEARS> CONFLICTYEARS { get; set; } // clp 2024/07/10
        internal virtual DbSet<CONFLICTYEARS_MIES> CONFLICTYEARS_MIES { get; set; } // clp 2025/06/04
        internal virtual DbSet<CONFLICTYEARS_UCDP> CONFLICTYEARS_UCDP { get; set; } // clp 2024/07/10

        internal virtual DbSet<RIVALRY> RIVALRY { get; set; } // clp 2024/12/20

        internal virtual DbSet<DYDLDRTRIP> DYDLDRTRIP { get; set; } // clp 2025/10/30


    }
}

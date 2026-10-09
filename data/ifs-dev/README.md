# IFs/Vanilla Project

Last revised: 2018-12-23. Author: LIU Yu

## Introduction

This is the development repository for the *IFs/Vanilla* project developed by *Beijing YottaGene Technology Co., Ltd.* for the [IFs](http://www.ifs.du.edu) project of the [Pardee Center for International Futures](https://pardee.du.edu). *IFs/Vanilla* serves as a proof-of-concept framework for the migration of the full [IFs](http://www.ifs.du.edu) system from *Visual Basic 6* to *.NET Framework* (*.NET Core 2* / *VB.net*). Features and key properties of the project all conform to the specifications authored by [José Solórzano](http://pardee.du.edu/JoséSolórzano) -- Lead System Developer of the [IFs](http://www.ifs.du.edu) project.


## Getting Started

*IFs/Vanilla* was developed with [Visual Studio Code](https://code.visualstudio.com) (VS Code) on an early 2015 MacBook Pro (running *macOS High Seierra* 10.13.5). It has been tested with [Visual Studio 2017 Community Edition](https://www.visualstudio.com/vs/community) (VS 2017) on a virtual machine (running 64bit *Windows 7* with Service Pack 1). The following instructions assume macOS or Windows systems using either [VS Code](https://code.visualstudio.com) or [VS 2017](https://www.visualstudio.com/vs/community) as the IDE.

### Building and running

*IFs/Vanilla* can be compiled and launched from the command line as follows,

```bash
$ dotnet build ifs.sln -f netcoreapp2.2
$ dotnet bin/Debug/netcoreapp2.2/ifs.dll 5 2100 -1 false false 0
$ dotnet bin/Debug/netcoreapp2.2/ifs.dll 5 2100 -1 false true 1
```

### Querying

For querying the variables, we have developed a simple, HTTP-based Web API.

First start the API server,

```bash
$ dotnet dev-certs https --trust
$ dotnet build ifs.web.sln -f netcoreapp2.2
$ dotnet bin/Debug/netcoreapp2.2/ifs.web.dll
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
    User profile is available. Using '~/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
Hosting environment: Production
Content root path: ~/Desktop/IFs/Vanilla/ifs-vanilla
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

Then open up a Web browser (e.g. Google Chrome), or a CLI tool (e.g. cURL), and visit,

- http://localhost:5000/api/query/EDBASENRG/2015,2050?sDim=USA,Male
- http://localhost:5000/api/query/POP/2015,2050?sDim=China

For users to decipher the numbers found in the categorical dimensions of variable data, we have provided the following helper API for querying the metadata of varaibles and dimensions,

- http://localhost:5000/api/query/EDBASENRG/_meta
- http://localhost:5000/api/query/POP/_meta


### Unit testing

```bash
$ dotnet test -f netcoreapp2.2
```

### Debugging

- VS Code

To debug the code base in step-by-step execution mode, set a break point from the Explorer View (⇧⌘E) of [VS Code](https://code.visualstudio.com), then switch to the Debug View (⇧⌘D) and click on the `.NET Core Launch (console)` configuration.

- VS Community 2017

TODO

## Source code structure

The code base of *IFs/Vanilla* consists of a [.NET Core](https://github.com/dotnet/core) solution `ifs.sln` and multiple VB.net or C# projects.

- `ifs-cli` (VB.net): The entry point and user interface sub-project at `src/IFs.Cli`. It implements the command-line interface `ifs.exe` (invoked as `dotnet ifs.dll` when targeting [.NET Core](https://github.com/dotnet/core));
- `ifs-core` (VB.net): The business logic sub-project at `src/IFs.Core`. It implements core business logics of *IFs/Vanilla*, including high-level I/O operations on *dataframes*, algorithms for rebuilding base, and sample procedure for running forecast models.
- `ifs-data` (C#): The data persistence sub-project at `src/IFs.Data`. It implements a vendor-agnostic wrapper atop the configuration parameters, relational models to be stored in `Working.run`, and low-level I/O operations on binary data stream;

Each of the above project has a corresponding test project for unit testing with [xUnit.net](https://xunit.github.io).

- `ifs-cli-test` (VB.net): The unit testing sub-project at `src/IFs.Cli.Test`.
- `ifs-core-test` (VB.net): The unit testing sub-project at `src/IFs.Core.Test`.
- `ifs-data-test` (C#): The test framework sub-project at `src/IFs.Data.Test`.

Besides *IFs/Vanilla* and its test suite, we have also implemented an `ASP.NET Core Web API` project for the query test.

- `ifs-web` (VB.net): The query test sub-project at `src/IFs.Web`. It implements a simple, HTTP-based [REST API](https://en.wikipedia.org/wiki/Representational_state_transfer) for showcasing the query procedure as specified.

## Technology stack and dependencies

*IFs/Vanilla* is founded on cutting-edge .NET technologies. On macOS, it requires the open-source [.NET Core](https://github.com/dotnet/core) (version `2.2.0` or above) both as the development SDK and runtime dependency. On Windows, besides [.NET Core](https://github.com/dotnet/core), *IFs/Vanilla* may also target the native [.NET Framework](https://www.microsoft.com/net/) (version 4.7.1 or above) as runtime dependency. *IFs/Vanilla* utilizes Microsoft's open source [EF Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/), and optionally rely on the third-party [EF Jet](https://www.nuget.org/packages/EntityFrameworkCore.Jet/) database provider when the configuration parameters are stored in Jet (MDB / ACCDB) files. [EF Jet](https://www.nuget.org/packages/EntityFrameworkCore.Jet/) relies on [OLEDB](https://msdn.microsoft.com/en-us/library/ms722784%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396) and [ACE](https://www.microsoft.com/en-us/download/details.aspx?id=54920), which are not available on non-Windows platforms.

*IFs/Vanilla* provides a high-level dependency framework for tracking the interaction among variables using [attributes](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/attributes/). Internal to *IFs/Vanilla*'s `IFs.Data` package, the dependency of variables are discovered from attributes through *reflection*, and collected to form a `BidirectionalGraph` (i.e., a kind of [directed acyclic graph](https://en.wikipedia.org/wiki/Directed_acyclic_graph)) using the third-party graph theory package [QuickGraph](https://archive.codeplex.com/?p=quickgraph). The forecasting order of variables, can then be computed through applying [topological sort](https://en.wikipedia.org/wiki/Topological_sorting) to the vertices of the directed graph.

*IFs/Vanilla* defines a two-layered storage structure for `Working.run`. Metadata about varaiables, dimensions, and configuration parameters are stored in [SQLite](https://www.nuget.org/packages/Microsoft.Data.SQLite/) database tables; then the *dataframes* of variables are stored in the database as opaque [BLOB](https://www.sqlite.org/datatype3.html). Internal to the *dataframes*, we use the Apache [Parquet](https://www.nuget.org/packages/Parquet.Net/) columnar data storage format to manage the dimensions and metrics of variables. Query processing over *dataframes* can take advantage of multiple technologies defined by the [Parquet](https://github.com/apache/parquet-format) format, including row groups filtering, dictionary encoding, and [snappy](https://google.github.io/snappy/) compression.

The query test project `IFs.Web` for *IFs/Vanilla* is based on [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/?view=aspnetcore-2.2&tabs=aspnetcore2x) (version `2.2.0` or above). It implements a JSON-based Web API for querying and filtering data of a target variable. As the default setting of ASP.NET Core, `IFs.Web` hosts an embedded [Kestrel](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-2.2&tabs=aspnetcore2x) web server for serving the Web API via HTTP or HTTPS. In the production environment, this embedded (application) server can be deployed behind a reverse proxy server (e.g. IIS, Nginx, or Apache) which forwards and throttles the traffic from/to the internet.

## On dataframe design and usage

Dataframe is a flavor of data-accessing API originated from programming languages and data management systems for big data analytics, for instance, Python (Pandas / Dask), Spark, R, etc. Conceptually, a dataframe is a read-optimized (on-disk or in-memory) data structure that resembles a relational database table. It typically defines a [method-chaining API](https://en.wikipedia.org/wiki/Method_chaining) for performing various data operations such as SPJ (select-project-join) query processing, OLAP data cube analytics (slicing, dicing, pivoting, etc.), and conversion from / to external data formats (i.e., JSON, Avro, Parquet, ProtoBuf, Thrift, etc.).

### Schema and metadata

 To address the diversity of data processing needs, we have tailor-made a dataframe for *IFs/Vanilla*; and this structure is hereafter referred to as *IFs dataframe*. Using [data warehousing](https://en.wikipedia.org/wiki/Data_warehouse) terms, an *IFs dataframe* is the [fact table](https://en.wikipedia.org/wiki/Fact_table) of a particular *variable*, and the schema of the table is (i) one or more key column(s) each corresponding to a *dimension*, plus (ii) exactly one metrics column containing values of the *variable*. Taking `EDBASENRG` (*Gross Basic Education Enrollment Rate*) for example, the structure of the [fact table](https://en.wikipedia.org/wiki/Fact_table) is as follows.

| 0 (Time ID) | 1 (Region ID) | 2 (Gender ID) | 3 (Value) |
|-------------|---------------|---------------|-----------|
| 2005        | 1             | 1             | 99.41129  |
| 2005        | 1             | 2             | 56.46212  |
| 2005        | 1             | 3             | 78.69447  |
| 2005        | 2             | 1             | NULL      |
| ...

An *IFs dataframe* implements a likewise in-memory data structure as above, which, in part, has the following programming interface for accessing metadata about the *variable* and its *dimensions*.
```vb
Public Partial Interface IDataframe
    ' Source variable of which the data belong
    ReadOnly Property Variable As IVariable
    ' Dimensions of the variable
    ReadOnly Property Dimensions As IReadOnlyList(Of IDimension)
End Interface
```

Programmatically, a *variable* can be any object that implements the following interface,
```vb
Public Interface IVariable
    ' Unique identifier of the variable
    ReadOnly Property Name As String
    ' Human-readable name of the variable (optional)
    ReadOnly Property DisplayName As String
    ' IFs-defined type of the variable
    ReadOnly Property Type As Integer
    ' Unique identifiers of the dimensions of the variable
    ReadOnly Property Dimensions As IReadOnlyList(Of Integer)
End Interface
```
where (i) `.Name` is the unique identifier of the variable, for example, `POP` for *Pupolation*, `EDBASENRG` for *Gross Basic Education Enrollment Rate*, etc.; (ii) `.DisplayName` is an optional, human readable name of the variable; (iii) `.Type` is the type of the variable as defined by *IFs*, and (iv) `.Dimensions` is a read-only list of unique identifiers, each corresponding to a *dimension* of the *variable* that has the following interface,
```vb
Public Interface IDimension
    ' Unique identifier of the dimension
    ReadOnly Property Id As Integer
    ' Human-readable name of the dimension (optional)
    ReadOnly Property Name As String
    ' Maps categorical values of the dimension to their literal names
    ReadOnly Property Buckets As IReadOnlyDictionary(Of Short, String)
End Interface
```
where (i) `.Id` is the unique identifier of the dimension, for example, `0` for *Time*, `1` for *Region*, etc.; (ii) `.Name` is an optional name of the variable; (iii) `.Buckets` is a *read-only* dictionary which maps categorical values of the dimension to respective literal names. Taking dimension #`23` (*Gender*) for example, there are three categorical values `1`, `2`, and `3`, which maps to `Male`, `Female`, and `Total`, respectively.

### Composition and query processing

In *IFs/Vanilla*, variables are defined on a per-*context* basis. A *context* is a comprehensive collection of metadata about variables, dimensions, parameters, etc., that altogether defines a *forecasting process*. Given a *context* `c` and the name of a variable, say `EDBASENRG`, defined by `c`, one can compose a *dataframe* `df` for variable `EDBASENRG` using the factory method `Dataframe.Create()`, i.e.,
```vb
Dim df As IDataframe = Dataframe _
    .Create("EDBASENRG", c) _
    .Append(2005S, 1S, 1S, 99.41129f) _
    .Append(2005S, 1S, 2S, 56.46212f) _
    .Append(2015S, 1S, 3S, 78.69447f) _
    .Append(2015S, 2S, 1S, Nothing)
```

While to compose dataframes from scratch is technically viable, it is *not* a very common use pattern for *IFs/Vanilla*. More often, dataframes are composed in batch from historical data by the *context service* (i.e., `IFs.Data.IContextService`), or retrieved through querying a *run* database (i.e., `Working.run`) via the *storage service* (i.e., `IFs.Data.IStorageService`). In both cases, dataframes are obtained by submitting *queries* to these services, and the composition of dataframe is a part of the query processing logic internal to respective services. Generally speaking, queries are expressed through a [method-chaining API](https://en.wikipedia.org/wiki/Method_chaining), i.e.,
```vb
Dim year As IDimension = service.GetDimension(0)
Dim region As IDimension = service.GetDimension(1)
Dim df As IDataframe = service _
    .Query("EDBASENRG") _
    .Filter(year.BTWN(2000, 2005)) _ ' find for years 2000, 2001, ..., 2005
    .Filter(year.NEQ(2004)) - ' AND exclude year 2004
    .Sort(year.ASC, region.ASC) _ ' sort by year and region (NOT implemented)
    .Execute()
```
where (i) `.Query()` initiate sa *query builder* (i.e., `IFs.Core.IQueryBuilder`) against the specified *variable*, (ii) each `.Filter()` applies an additional *filters* (i.e. `IFs.Core.IQueryFilter`) to the *query*, (iii) `.Sort()` applies sorting to the outcome dataframe, and (iv) `.Execute()` constructs a query plan,submits the plan to the *service* for execution, and receives query result in the form of a dataframe.

The received dataframe can be filtered, sorted, and aggregated through likewise [method-chaining API](https://en.wikipedia.org/wiki/Method_chaining), i.e.,
```vb
Dim df As IDataframe = service.Query("EDBASENRG")
Dim gender As IDimension = df.Dimensions(2) ' the 3rd dimension of the variable
Dim femaleRate As Single? = df _
    .Filter(gender.EQ(2)) _ ' for gender Female
    .Average() ' apply AVG aggregation (NOT implemented)
```

The two business logic services, i.e., *context* and *storage*, have different capabilities when it comes to query processing. The *context service* is for accessing parameters and historical data from a collection of IFs-defined legacy databases, and is currently only used in *rebuild base*. It, therefore, limits the query type to filtering by the *base year* (i.e., `FirstYear`). Submitting other predicates to the *context service* is considered an absue of the *context service*, and would trigger a `NotSupportedException`. The *storage service*, on the other hand, is capable of processing more diversified filtering predicates, which we summarize as follows,

| Predicate               | Description
|-------------------------|-----------------
| d.EQ(c)                 | find records where *dimension* `d` has value `c`.
| d.NEQ(c)                | find records where *dimension* `d` does *NOT* have value `c`.
| d.ANY(c1, c2, ...)      | find records where *dimension* `d` has any value of `c1`, `c2`, ...
| d.BTWN(lb, ub)          | find records where *dimension* `d` has a value between `lb` and `ub` (inclusive).

Filtering predicates can be chained together through either `.Conjunct()` for *Logical And* or `.Disjunct()` for *Logical Or*. For example, to find records for years `2015`, and `2017`, one can use the predicate as follows,
```vb
year.BTWN(2015S, 2017S).Conjunct(year.NEQ(2016S))
```
which is functionally equivalent to,
```vb
year.ANY(2015S, 2017S)
```

### Data extraction and modification

TODO

## On dependency management

For instance, if forecasting one variable `B` requires another variable `A` as input, then the producer method of `B` should have a `<ConsumesVariable("A")>` annotation; likewise on the producer of `A`, there should be a `<ProducesVariable("B")>` annotation.

TODO

## On database design

While working with the legacy data files of *IFs*, we noticed a few options for optimizing the database design, that could lead to more performant and (or) more fluent program logic. For example,

### Use long table structure

We understand that *IFs* has been making wide use of pivoted / wide table structures for historical / practical reasons. And pivot / wide tables does have their merit, such as being more legible for human to read, and — as you have pointed out — having fewer number of records than equivalent long tables. However, it is generally believed to be an anti-pattern among database engineers, because, programmatically, they are not as convenient / performant to work with as long tables.

For example, the series tables in `IFsHistSeries` database are in the form of pivot tables with *years* as column names. To determine the availability of data from a particular year, one would have to query the schema of the series table and see if there exists a corresponding column. The downside is two-fold, (i) unlike querying records, querying database schema is programmatically more difficult, and lacks standardized mechanisms, (ii) appending new data to the database always incurs schema change, which is expensive to maintain and to execute. 

With hand-written SQL, it’s probably okay to work with pivot tables, but when we need to access the database through an Object-Relational Mapper (ORM), it could be somewhat head-scratching. With ORM, we define entities as programming language objects (classes), and bind the properties of the entities to database columns. If the series table all use the same long table structure, we can define one entity structure and use that structure to access all series uniformly. However, with a pivoted / wide table structure, one would have to define one entity class for each series table because the set of available years are different, and therefore the tables are all heterogeneous in their schema. In IFs.Data, we have chosen to query the series tables through program-generated SQL, and skip the ORM layer entirely. If we were to use the ORM by-the-book, It would incur huge maintenance cost to make the database structure and entity definitions consistent — as new year columns keeps coming each year.

Even with hand-written SQL, there are performance penalties and inconvenience to query pivoted tables. For example, with a long table structure of, say, `SeriesPopulation`, (i.e. `year` is in a single column), querying data from a subset of years 2015, 2016, ..., would be,

```sql
SELECT Country, Year, Value FROM SeriesPopulation WHERE Year In (2015, 2016, ...);
```

But with a pivoted / wide table having one column for each year, we would have to either (i) issue separate queries for each year and assemble the results with hand-written program logic, or (ii) use `UNION ALL` to combine the results of multiple subqueries,

```sql
SELECT Country, 2015 As Year, [2015] As Value FROM SeriesPopulation
UNION ALL
SELECT Country, 2016 As Year, [2016] As Value FROM SeriesPopulation
UNION ALL
...
```

In both cases, there would be further performance penalties when we need to sort or filter the results — with separate queries, we’d need to sort in-memory with hand-written program logic, with `UNION ALL`, the sort is also bound to be inefficient due to the evaluation order of subqueries.

In general, I would recommend that we use long-table database design in the future whenever possible.

### Use primary keys / indices

A number of database tables lack primary keys / indices (e.g., `Country` and `FIPS_CODE` columns in `IFsHistSeries`, `NewRegion` in `IFs`, etc.). Defining primary keys on database relations is generally considered a best practice;

TODO

### Unify database usage

The content of `IFs.grp` file could have been stored as a long database table (i.e., <`dimension`, `value`>) as opposed to a plain text file;

TODO

## Contact

- José Solórzano (Pardee) <jsolorza@du.edu>
- LIU Yu (YottaGene) <liuyu@yottagene.com>

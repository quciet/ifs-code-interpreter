This readme provides additional build and run information after the upgrade towards .Net8
# Build
This new build only supports .Net8 LTS because the web application no longer uses this module.
All built artifacts will be put into the `./bin`` folder at solution level.

When a new build is created and a new version is needed, this should be set into the `Directory.Build.props` file in the root of the solution.

```
<Project>
	<PropertyGroup>
		<Deterministic>false</Deterministic>
		<AssemblyVersion>1.1.0</AssemblyVersion>
		<InformationalVersion>1.1.0</InformationalVersion>
	</PropertyGroup>
</Project>
```

# Publish
For publishing there are multiple options possible which can be chosen from the publish dialog:

![](./Readme.images/publish01.png)

- Deployment mode: Choose *framework-dependent* when you expect to have the dotnet 8 runtime installed at the destination and choose *self-contained* when you want to be as indpedent as possible from the target environment and include the required runtime libraries.
- Produce single file: Check this box if you want the runtime libraries to be packed into one assembly instead of a collection of independent assemblies.

![](./Readme.images/publish02.png)
# ParquetTools
The ParquetTools project contains a utility which can be used to export Apache Parquet dataframes to a text file.

# Utility notes
This utility notes section explain the different steps that are taken to compare the out of two versions/builds of the model.


Comparing two SQLite datafiles can be done using the sqldiff utility:
```
> sqldiff.exe --summary --primarykey netcore2.2B\working.run.db net8B\working.run.db

__EFMigrationsHistory: 0 changes, 0 inserts, 0 deletes, 1 unchanged
ifs_dim: 0 changes, 0 inserts, 0 deletes, 58 unchanged
ifs_dim_bucket: 0 changes, 0 inserts, 0 deletes, 623 unchanged
ifs_reg: 0 changes, 0 inserts, 0 deletes, 1966 unchanged
ifs_reg_coeff: 0 changes, 0 inserts, 0 deletes, 14209 unchanged
ifs_reg_point: 0 changes, 0 inserts, 0 deletes, 100 unchanged
ifs_var: 0 changes, 0 inserts, 0 deletes, 1835 unchanged
ifs_var_blob: 673 changes, 0 inserts, 0 deletes, 1162 unchanged
ifs_var_dim: 0 changes, 0 inserts, 0 deletes, 3677 unchanged
```

Exporting blob elements from the database using the SQLite command line client:
```
> sqlite3 working.run.db "SELECT writefile('exp\' || variableName, data) FROM ifs_var_blob"
```

Use the ParquetTools utility to export the dataframe to text:
```
..\ParquetTools.exe exp\POPURBAN out\POPURBAN.out
```

Merge all text files into one:
```
copy /b *.out merged.txt
```

Sort the textfile:
```
sort merged.txt > sorted.txt
```
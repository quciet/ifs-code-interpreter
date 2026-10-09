# Logging configuration
The NLog logging framework has been added.
The key item here is the `NLog.config` file in the root of the web project.
Currently one a file based target is configured:

`
<target xsi:type="File" name="f" fileName="{basedir}/../../Log/${shortdate}.log" layout="${longdate} ${uppercase:${level}} ${callsite} ${message:withexception=true}" />
`

The fileName attribute needs to be modified to suite the needs of your environment.

For convenience during operations two additional files are included:
- `NLog.config.prod` : contains normal production configuration without debug level.
- `Nlog.config.debug` : contains debug configuration to capture as much details as possible.

Depending on the needs of the moment one can replace the `Nlog.config` file with one of there pre-configured ones in the published/installed setup. If an end-user reports a problem one could ask to replace the `Nlog.config` with the `NLog.config.debug` one to capture more details for problem resolution.

The current cultureInfo of the application is also written to the log file upon startup of the application.

# Build
All referenced libraries are located in the `../libs/` folder or referenced using Nuget.
When a new version is checked out from the repository onto a clean source tree care should be taken to update the packages.
In general the `packages` folder in the root of the solution is not included in the repository. For a new clean checkout you should remove this folder upon checkout. Note that you might have to close Visual Studio prior to doing this because it might hold locks on libraries in that folder.
Visual Studio might already prompt to restore the packages and present a button to do so.
In Visual Studio in the menu select *Tools > Nuget Package Manager > Package Manager Console* which will open the Nuget command prompt. On the prompt enter the `Update-Package -reinstall` command which will refresh all packages.
The `packages` folder should now be filled with libraries.

# Versioning
## .Net Framework web application
When a new release is created this release should also get a unique version number which is verifiable on the binary.
The file `My Project\AssemblyInfo.vb` contains this version information and should be updated manually before the creation of the release if one prefers to set a specific release number. If specific numbering is not important, the version can also be automatically generated. (see https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assemblyversionattribute for details) 
The application version is written to the log file upon application startup.

The same version should be used as a tag to the corresponding changeset in GitHub. For more information see https://docs.github.com/en/desktop/managing-commits/managing-tags-in-github-desktop

## .Net core model
When a new release is created this release should also get a unique version number which is verifiable on the binary.
The file `Directory.Build.props` in the root source directory contains this version and should be updated manually before the creation of the release.

# Publish
A folder publish profile has been created and is located in `My Project\PublishProfiles\FolderProfile.pubxml`.
It will create the deployment in the folder `..\web_publish_` but this can be changed.
**Be careful!** When the file `My Project\PublishProfiles\FolderProfile.pubxml.user` is also included this contains a full path reference to the deployment folder so you might have to update this.

The content of this publish profile needs to be added to the installation (instead of the web site source tree previously).
Note that in the published folder all *.*aspx files have been replaced with marker files i.e. placeholders without content. They should **NOT** be removed!. All compiled aspx files are all merged together into one specific assemblu IFsWeb.Pages.
# Deployment
The complete content of the publish folder should be copied (and replace all) on the deployment destination folder.
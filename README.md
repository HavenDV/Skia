# Skia
<b>Skia</b> is separate TPM for developing Skia applications within a single project, if there are other TargetFrameworks using the main `net7.0` TPM.

## Usage
Just install and add `net7.0-skia` to `TargetFrameworks`.

## Install
You can install Skia workload for .NET 6.0/7.0/8.0 by using the installer script.
- On Linux / macOS:
```
curl -sSL https://raw.githubusercontent.com/HavenDV/Skia/main/scripts/workload-install.sh | sudo bash
```
if you want to install a specific version of Skia workload or install to a specific directory, use the following command:
```
curl -sSL https://raw.githubusercontent.com/HavenDV/Skia/main/scripts/workload-install.sh | bash /dev/stdin -v <version> -d <directory>
```
- On Windows:
```
Invoke-WebRequest 'https://raw.githubusercontent.com/HavenDV/Skia/main/scripts/workload-install.ps1' -OutFile 'workload-install.ps1';
./workload-install.ps1 [-v <version>] [-d <directory>]
```
You can see the Skia workload as follows if it is properly installed.
```
PS D:\workspace> dotnet workload list

This command lists only workloads that were installed via `dotnet workload install` in this version of the SDK and not those that were installed via Visual Studio.

Installed Workload Ids
----------------------
maui
skia

Use `dotnet workload search` to find additional workloads to install.

Updates are avaliable for the following workload(s): maui skia. Run `dotnet workload update` to get the latest  
```

## Development
You can test this project using these commands(tested on macOS) in `src/tasks/tasks` folder:
```
dotnet build /t:TestWorkload
dotnet build /t:WorkloadUninstall

other possible targets(it already included in targets above):
DownloadDotnetInstall
DotnetInstall
WorkloadInstall
BuildPackages
CleanArtifactsAndTemporaryFiles

You can override these properties:
/p:UseCurrentDotnet=true - Will install workload to current dotnet instead downloaded.
```
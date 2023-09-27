# Skia .NET
![image](https://user-images.githubusercontent.com/14328614/115324121-49473f00-a1c4-11eb-844c-86970c5e0764.png)

<b>Skia .NET</b> is an advanced way to develop applications with .NET technology for Skia OS.

Skia .NET is :
- Support .NET [MAUI](https://github.com/dotnet/maui)
- Support .NET 6, 7 and 8

## Prerequisites

**- Skia SDK**
  * [Skia Extensions for Visual Studio Family](https://developer.skia.org/development/skia-extensions-visual-studio-family) or 
  * [Skia Studio](https://developer.skia.org/development/skia-studio)

**- .NET 6 SDK**
  * Linux / Windows / macOS : https://dotnet.microsoft.com/download/dotnet/6.0
  
**- Visual Studio 2022**
  * To create Skia .NET with .NET 6, you need the latest version of [Visual Studio 2022](https://visualstudio.microsoft.com/)

**- Skia .NET Workload**
  * [Installing Skia .NET Workload using Visual Studio / Visual Studio Code / CLI](https://github.com/HavenDV/Skia/wiki/Installing-Skia-.NET-Workload).

## Getting Started with Visual Studio 2022
See [here](https://github.com/HavenDV/Skia/wiki/Build-your-first-(.NET6)-Skia-App-using-Visual-Studio-2022) for more details.

![](https://github.com/HavenDV/Skia/blob/a710d7095ca9ba0a759705dc59461140dec13ae4/assets/hello-skia-net6-vs2022.gif)

## Getting Started with CLI

#### 1. Check the Skia templates before creating a new Skia Project
You can see the Skia template as follows if it is properly installed.
```sh
dotnet new --list
Template Name                                 Short Name      Language    Tags                  
--------------------------------------------  --------------  ----------  ----------------------
Console Application                           console         [C#],F#,VB  Common/Console        
Class Library                                 classlib        [C#],F#,VB  Common/Library        
Worker Service                                worker          [C#],F#     Common/Worker/Web     
MSTest Test Project                           mstest          [C#],F#,VB  Test/MSTest           
NUnit 3 Test Item                             nunit-test      [C#],F#,VB  Test/NUnit            
NUnit 3 Test Project                          nunit           [C#],F#,VB  Test/NUnit            
xUnit Test Project                            xunit           [C#],F#,VB  Test/xUnit            
*Skia .NET Application**                    *skia*          *[C#]*      *Skia*
Razor Component                               razorcomponent  [C#]        Web/ASP.NET           
Razor Page                                    page            [C#]        Web/ASP.NET           
...

```  

#### 2. Creates a New Project
```sh
dotnet new skia -n HelloSkiaNet6
```
When the project is successfully created, the following files are created.
```sh
└── HelloSkiaNet6
    ├── HelloSkiaNet6.csproj
    ├── Main.cs
    ├── shared
    └── skia-manifest.xml
```

> This is a Skia .NET app, not a .NET MAUI app.


#### 3. Build the application
```sh
dotnet build 
```
When the project builds successfully, skia app package (.tpk) is created as follows.
```sh
Microsoft (R) Build Engine version 16.10.0-preview-21181-07+073022eb4 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  Restored /home/rookiejava/workspace/HelloSkiaNet6/HelloSkiaNet6.csproj (in 165 ms).
  You are using a preview version of .NET. See: https://aka.ms/dotnet-core-preview
  HelloSkiaNet6 -> /home/rookiejava/workspace/HelloSkiaNet6/bin/Debug/net6.0-skia/HelloSkiaNet6.dll
  SkiaTpkFiles : shared/res/HelloSkiaNet6.png
  SkiaTpkFiles : skia-manifest.xml
  HelloSkiaNet6 is signed with Default Certificates!
  HelloSkiaNet6 -> /home/rookiejava/workspace/HelloSkiaNet6/bin/Debug/net6.0-skia/com.companyname.HelloSkiaNet6-1.0.0.tpk

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:04.83
```

#### 4. Run the application 
```sh
dotnet build HelloSkiaNet6/HelloSkiaNet6.csproj -f net6.0-skia -t:Run
```

> ℹ️ You need to use Skia emulator 7.0 or higher version to run .NET 6 based app.

## Developers
You can test project using these commands(tested on macOS):
```
sudo dotnet build /t:TestWorkload
sudo dotnet build /t:WorkloadUninstall

other possible targets(it already included in targets above):
DownloadDotnetInstall
DotnetInstall
WorkloadInstall
BuildPackages
CleanArtifactsAndTemporaryFiles
```
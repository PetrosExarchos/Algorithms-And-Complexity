# Algorithms-And-Complexity

Knapsack 0-1 problem solver.

Step-by-Step Installation Guide:
(Installation Require Internet Connection)

Phase 1# (Gathering the tools)

1. Download and Install Visual Studio Code, System Installer-64x bit (https://code.visualstudio.com/download).

2. Download and Install .Net Core Sdk 3.0 or 3.1 (https://dotnet.microsoft.com/download/dotnet-core/sdk-for-vs-code?utm_source=vs-code&amp;utm_medium=referral&amp;utm_campaign=sdk-install).

3. Open Visual Studio Code and navigate to the extension tab (Ctrl+Shift+X). Typing in the search bar will reveal availiable modules for download.

4. Download and install the official c# extension from Microsoft.

5. Download and install c# Extensions extension by jchannon.

6. Download and install NuGet Package Manager by jmrog.

Phase 2# (CSharping the tools)

1. Create a new folder where a new project will be created.

2. Open the folder in Visual Studio Code.

3. Open the Terminal or create a new Terminal (Ctrl+Shift+`).

4. Type: "dotnet new console" (no quotes) and press Enter to create a new console C# project.

5. Type: "dotnet build" (no quotes) to build the created project.

(more information regarding creation of a new c# console project: https://docs.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code)

Phase 3# (Code transplant surgery)

1. Copy all the csharp files (.cs) from the Scripts folder and paste to your project's root directory. Replace where asked.

2. Copy all the files in the Dependencies folder to @projectname@/bin/Debug/netcoreapp#.# . If you did not build your project on phase 2# step 5 this folder will be absent.

Phase 4# (Cleaning the red stains)

1. To download Google-OrTools Open the terminal and type "dotnet add package Google.OrTools --version 7.4.7247" and press Enter.

2. Open your project file and add this line between <PropertyGroup> tag: <PlatformTarget>x64</PlatformTarget> to avoid errors regarding OrTools.

3. Open the Command Palette (Crtl+Shift+P) and type "NuGet Package Manager: Add Package". if you did not install nuget package manager on phase 1# step 6 this command will be absent.

4. having completed step 3 successfully type on the searchbar that just opened "Microsoft.Net.Test.Sdk" and download and install the latest version.

5. Repeat step 3 and type "xunit" and download and install the latest version.

6. Repeat step 3 and type "xunit.runner.visualstudio" and download and install the latest version.

7. Open the terminal and type "dotnet restore". if every step was completed correctly all the errors will disapear.

8. Build the project by typing "dotnet build" in the terminal.

(more information regarding xunit installation: https://www.youtube.com/watch?v=HQmbAdjuB88)

Phase 5# (Time to fill those knapsacks)

1. Open the terminal and type "dotnet test". if all five tests pass, everything is good to go !

2. Run the application by either typing "dotnet run" on the console or double clicking the .exe file at @projectname@/bin/Debug/netcoreapp#.# .

EXTRA:



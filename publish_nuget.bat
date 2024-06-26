﻿REM clean the build dirs first
dotnet clean --configuration Release .\SDK.Core\
dotnet clean --configuration Release .\SDK\

REM build the SDK projects
dotnet build --framework net8.0 --runtime win-x64 --no-self-contained --configuration Release .\SDK.Core\
dotnet build --framework net8.0 --runtime win-x64 --no-self-contained --configuration Release .\SDK\

REM publish to local self hosted BaGet
dotnet nuget push -s https://baget.modbros.net/v3/index.json -k %1 .\SDK.Core\bin\Release\*.nupkg --skip-duplicate
dotnet nuget push -s https://baget.modbros.net/v3/index.json -k %1 .\SDK\bin\Release\*.nupkg --skip-duplicate

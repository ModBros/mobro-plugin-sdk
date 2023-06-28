REM clean the build dirs first
dotnet clean --configuration Release .\SDK.Domain\
dotnet clean --configuration Release .\SDK\

REM build the SDK projects
dotnet build --framework net7.0 --runtime win-x64 --no-self-contained --configuration Release .\SDK.Domain\
dotnet build --framework net7.0 --runtime win-x64 --no-self-contained --configuration Release .\SDK\

REM publish to local self hosted BaGet
dotnet nuget push -s https://baget.modbros.net/v3/index.json -k %1 .\SDK.Domain\bin\Release\*.nupkg --skip-duplicate
dotnet nuget push -s https://baget.modbros.net/v3/index.json -k %1 .\SDK\bin\Release\*.nupkg --skip-duplicate

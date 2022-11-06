REM clean the build dir first
dotnet clean --configuration Release .

REM build the SDK project
dotnet build --framework net6.0 --runtime win-x64 --no-self-contained --configuration Release .

REM publish to local self hosted BaGet
dotnet nuget push -s https://baget.modbros.net/v3/index.json -k %1 .\bin\Release\*.nupkg --skip-duplicate

dotnet restore
dotnet build
dotnet publish -c Release -o .build/out
docker build -t dockerexampleweb .

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

EXPOSE 80
ENV ASPNETCORE_URLS http://+:80

COPY .build/out .
ENTRYPOINT ["dotnet", "dockerExampleWeb2.dll"]
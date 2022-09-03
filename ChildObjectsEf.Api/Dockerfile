FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ChildObjectsEf.Api/bin/publish/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "ChildObjectsEf.Api.dll"]
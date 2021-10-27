FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /admin
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=development
ENV Spring:Cloud:Config:Uri=http://configserver:8888
ENV Spring:Cloud:Config:FailFast=true

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY ./BluePaw.Administration ./BluePaw.Administration
COPY ./BluePaw.Shared ./BluePaw.Shared
RUN dotnet publish ./BluePaw.Administration/BluePaw.Administration.csproj -c release -o /admin

FROM base as final
WORKDIR /admin
COPY --from=build /admin ./
ENTRYPOINT ["dotnet", "BluePaw.Administration.dll"]

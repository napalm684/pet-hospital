FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /router
EXPOSE 5100

ENV ASPNETCORE_URLS=http://+:5100
ENV ASPNETCORE_ENVIRONMENT=development
ENV Spring:Cloud:Config:Uri=http://configserver:8888
ENV Spring:Cloud:Config:FailFast=true

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY ./BluePaw.Router ./BluePaw.Router
COPY ./BluePaw.Shared ./BluePaw.Shared
RUN dotnet publish ./BluePaw.Router/BluePaw.Router.csproj -c release -o /router

FROM base as final
WORKDIR /router
COPY --from=build /router ./
ENTRYPOINT ["dotnet", "BluePaw.Router.dll"]

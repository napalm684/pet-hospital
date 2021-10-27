FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /ui
EXPOSE 5200

ENV ASPNETCORE_URLS=http://+:5200
ENV ASPNETCORE_ENVIRONMENT=development
ENV Spring:Cloud:Config:Uri=http://configserver:8888
ENV Spring:Cloud:Config:FailFast=true

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY ./BluePaw.Ui ./BluePaw.Ui
COPY ./BluePaw.Shared ./BluePaw.Shared
RUN dotnet publish ./BluePaw.Ui/BluePaw.Ui.csproj -c release -o /ui

FROM base as final
WORKDIR /ui
COPY --from=build /ui ./
ENTRYPOINT ["dotnet", "BluePaw.Ui.dll"]

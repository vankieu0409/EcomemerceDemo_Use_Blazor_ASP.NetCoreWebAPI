FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_URLS=https://+:7032;http://+:5008
WORKDIR /app
EXPOSE 7032
EXPOSE 5008

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /
COPY ["ASMC6P/Clients/Client/ASMC6P.Client.csproj", "ASMC6P/Clients/Client"]
COPY ["ASMC6P/Servers/Server/ASMC6P.Server.csproj", "ASMC6P/Servers/Server"]
COPY ["ASMC6P/Shared/ASMC6P.Shared.csproj", "ASMC6P/Shared"]
RUN dotnet restore "ASMC6P/Shared/ASMC6P.Shared.csproj" --disable-parallel
COPY . .
WORKDIR "ASMC6P/Servers/Server"
RUN dotnet build "ASMC6P.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ASMC6P.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
WORKDIR /app/Files
WORKDIR /app
ENTRYPOINT ["dotnet", "ASMC6P.Server.dll"]
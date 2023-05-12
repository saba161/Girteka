FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Girteka.AggregationApp/Girteka.AggregationApp.csproj", "Girteka.AggregationApp/"]
RUN dotnet restore "Girteka.AggregationApp/Girteka.AggregationApp.csproj"
COPY . .
WORKDIR "/src/Girteka.AggregationApp"
RUN dotnet build "Girteka.AggregationApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Girteka.AggregationApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Girteka.AggregationApp.dll"]

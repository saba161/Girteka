FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Girteka.ElectricAggregate.Job/Girteka.ElectricAggregate.Job.csproj", "Girteka.ElectricAggregate.Job/"]
COPY ["Girteka.ElectricAggregate.Domain/Girteka.ElectricAggregate.Domain.csproj", "Girteka.ElectricAggregate.Domain/"]
COPY ["Girteka.ElectricAggregate.Persistence/Girteka.ElectricAggregate.Persistence.csproj", "Girteka.ElectricAggregate.Persistence/"]
RUN dotnet restore "Girteka.ElectricAggregate.Job/Girteka.ElectricAggregate.Job.csproj"
COPY . .
WORKDIR "/src/Girteka.ElectricAggregate.Job"
RUN dotnet build "Girteka.ElectricAggregate.Job.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Girteka.ElectricAggregate.Job.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Girteka.ElectricAggregate.Job.dll"]

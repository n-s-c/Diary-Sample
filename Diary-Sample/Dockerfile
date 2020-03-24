FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Diary-Sample/Diary-Sample.csproj", "Diary-Sample/"]
RUN dotnet restore "Diary-Sample/Diary-Sample.csproj"
COPY . .
WORKDIR "/src/Diary-Sample"
RUN dotnet build "Diary-Sample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Diary-Sample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Diary-Sample.dll"]
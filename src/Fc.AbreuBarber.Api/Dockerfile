#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fc.AbreuBarber.Api/Fc.AbreuBarber.Api.csproj", "Fc.AbreuBarber.Api/"]
RUN dotnet restore "Fc.AbreuBarber.Api/Fc.AbreuBarber.Api.csproj"
COPY . .
WORKDIR "/src/Fc.AbreuBarber.Api"
RUN dotnet build "Fc.AbreuBarber.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fc.AbreuBarber.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fc.AbreuBarber.Api.dll"]
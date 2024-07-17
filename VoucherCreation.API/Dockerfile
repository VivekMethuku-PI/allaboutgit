#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["piVMS_Creation_API/VoucherCreation.API/VoucherCreation.API.csproj", "piVMS_Creation_API/VoucherCreation.API/"]
COPY ["piVMS_Common_BAL/Service.BAL.csproj", "piVMS_Common_BAL/"]
COPY ["piVMS_Creation_API/VoucherCreation.BAL/VoucherCreation.BAL.csproj", "piVMS_Creation_API/VoucherCreation.BAL/"]
COPY ["piVMS_Creation_API/VoucherCreation.DAL/VoucherCreation.DAL.csproj", "piVMS_Creation_API/VoucherCreation.DAL/"]
RUN dotnet restore "piVMS_Creation_API/VoucherCreation.API/VoucherCreation.API.csproj"
COPY . .
WORKDIR "/src/piVMS_Creation_API/VoucherCreation.API"
RUN dotnet build "VoucherCreation.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VoucherCreation.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoucherCreation.API.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PASMicroservice/PASMicroservice.csproj", "PASMicroservice/"]
RUN dotnet restore "PASMicroservice/PASMicroservice.csproj"
COPY . .
WORKDIR "/src/PASMicroservice"
RUN dotnet build "PASMicroservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PASMicroservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PASMicroservice.dll"]
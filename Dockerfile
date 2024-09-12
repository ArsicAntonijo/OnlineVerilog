FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN apt update && \
    apt install -y iverilog

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["OnlineVerilog/OnlineVerilog.csproj", "OnlineVerilog/"]
RUN dotnet restore "OnlineVerilog/OnlineVerilog.csproj"
COPY . .
WORKDIR /src/OnlineVerilog
RUN dotnet build "OnlineVerilog.csproj" -c ${BUILD_CONFIGURATION} -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "OnlineVerilog.csproj" -c ${BUILD_CONFIGURATION} -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "OnlineVerilog.dll" ]
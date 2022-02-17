FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
# Install libs for back-end image handle
RUN apt-get update && apt-get install -y libgdiplus
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["api/Lapis.API.csproj", "./"]
RUN dotnet restore "Lapis.API.csproj"
COPY api/ .
WORKDIR "/src/."
RUN dotnet build "Lapis.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lapis.API.csproj" -c Release -o /app/publish --runtime linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Lapis.API.dll --timezone "SE Asia Standard Time"

# Użyj obrazu z gotowym środowiskiem .NET SDK do zbudowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Kopiujemy plik projektu i pobieramy biblioteki (Restore)
COPY ["CarRentalSystem/CarRentalSystem.csproj", "CarRentalSystem/"]
RUN dotnet restore "CarRentalSystem/CarRentalSystem.csproj"

# Kopiujemy resztę plików i budujemy
COPY . .
WORKDIR "/src/CarRentalSystem"
RUN dotnet build "CarRentalSystem.csproj" -c Release -o /app/build

# Publikujemy gotową aplikację do folderu /app/publish
FROM build AS publish
RUN dotnet publish "CarRentalSystem.csproj" -c Release -o /app/publish

# Tworzymy finalny, lekki obraz do uruchomienia
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentalSystem.dll"]
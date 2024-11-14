# 1. Adım: Uygulamayı derlemek için SDK imajını kullan
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# Proje dosyalarını kopyala ve bağımlılıkları yükle
COPY *.sln .
COPY DressedUp.API/DressedUp.API.csproj DressedUp.API/
COPY DressedUp.Application/DressedUp.Application.csproj DressedUp.Application/
COPY DressedUp.Domain/DressedUp.Domain.csproj DressedUp.Domain/
COPY DressedUp.Infrastructure/DressedUp.Infrastructure.csproj DressedUp.Infrastructure/

# Bağımlılıkları geri yükleme
RUN dotnet restore

# Uygulamanın geri kalanını kopyala ve yayına hazır hale getir
COPY . .
WORKDIR /app/DressedUp.API
RUN dotnet publish -c Release -o /out

# 2. Adım: Runtime imajını kullanarak uygulamayı çalıştır
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:8080

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "DressedUp.API.dll"]

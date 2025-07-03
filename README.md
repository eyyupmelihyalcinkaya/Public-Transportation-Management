# Public Transportation Management API / Toplu Taşıma Yönetim API

A .NET 8 Web API for managing public transportation routes, stops, and trips with JWT authentication.  
JWT kimlik doğrulaması ile toplu taşıma rotaları, durakları ve seferleri yöneten .NET 8 Web API.

## Tech Stack / Teknoloji Yığını

- **.NET 8** Web API
- **PostgreSQL** Database / Veritabanı
- **Entity Framework Core** ORM
- **JWT** Authentication / Kimlik Doğrulama
- **Swagger** Documentation / Dokümantasyon

## Quick Start / Hızlı Başlangıç

```bash
# Clone repository / Projeyi klonla
git clone <repository-url>
cd internshipProject1

# Configure database in appsettings.json / Veritabanını yapılandır
# Update connection string / Bağlantı stringini güncelle

# Run migrations / Migration'ları çalıştır
dotnet ef database update

# Start application / Uygulamayı başlat
dotnet run
```

**Swagger UI**: `https://localhost:7009/swagger`

## API Endpoints

### Public (No Auth Required / Kimlik Doğrulama Gerektirmez)

| Method | Endpoint | Description / Açıklama |
|--------|----------|------------------------|
| `GET` | `/api/routes` | List all routes / Tüm rotalar |
| `GET` | `/api/routes/{id}` | Get route by ID / ID'ye göre rota |
| `GET` | `/api/routes/{id}/stops` | Get route stops / Rota durakları |
| `GET` | `/api/stops` | List all stops / Tüm duraklar |
| `GET` | `/api/stops/{id}` | Get stop by ID / ID'ye göre durak |
| `GET` | `/api/stops/nearby?lat=40.99&lon=29.02` | **NEW!** Nearby stops / Yakın duraklar |
| `GET` | `/api/trips?routeId=1&day=weekday` | Filter trips / Sefer filtresi |
| `POST` | `/api/user/register` | User registration / Kullanıcı kaydı |
| `POST` | `/api/user/login` | User login / Kullanıcı girişi |

### Admin (JWT Token Required / JWT Token Gerekli)

| Method | Endpoint | Description / Açıklama |
|--------|----------|------------------------|
| `POST` | `/api/routes` | Create route / Rota oluştur |
| `POST` | `/api/stops/Create` | Create stop / Durak oluştur |
| `POST` | `/api/trips` | Create trip / Sefer oluştur |
| `POST` | `/api/routestop` | Add stop to route / Rotaya durak ekle |

## Database Schema / Veritabanı Şeması

```
Route (Id, Name, Description)
Stop (Id, Name, Latitude, Longitude)  
Trip (Id, RouteId, StartTime, EndTime, DayType)
RouteStop (Id, RouteId, StopId, Order)
User (Id, userName, passwordHash, passwordSalt)
```

## Usage Example / Kullanım Örneği

### Authentication / Kimlik Doğrulama
```json
POST /api/user/login
{
  "userName": "admin",
  "password": "123456"
}
```

### Add Stop / Durak Ekle
```json
POST /api/stops/Create
Authorization: Bearer {token}
{
  "name": "Central Station",
  "latitude": 40.7128,
  "longitude": -74.006
}
```

### Find Nearby Stops / Yakın Durakları Bul
```
GET /api/stops/nearby?lat=40.9921&lon=29.0277
```
Response:
```json
[
  {
    "id": 1,
    "name": "Kadıköy İskelesi",
    "latitude": 40.9921,
    "longitude": 29.0277,
    "distanceInKm": "0.00"
  }
]
```

### Query Trips / Sefer Sorgula
```
GET /api/trips?routeId=1&day=weekday
```

## Configuration / Yapılandırma

Update `appsettings.json`:  
`appsettings.json` dosyasını güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=internshipProject1;Username=postgres;Password=your_password;"
  },
  "Token": {
    "SecurityKey": "your-secret-key",
    "Expiration": 60
  }
}
```

## Development Status / Geliştirme Durumu

+ **Core API** / Temel API (Completed)
+ **JWT Auth** / JWT Kimlik Doğrulama (Completed)
+ **CRUD Operations** / CRUD İşlemleri (Completed)
+ **Geolocation API** (nearby stops / yakın duraklar) (Completed)
- **Docker Support** / Docker Desteği (In Progress)
- **Redis Caching** / Redis Önbellekleme (In Progress)
- **Frontend** / Frontend desteği (In Progress)

## Features / Özellikler

### **Geolocation Service / Konum Servisi**
- **Haversine Formula** for distance calculation / Mesafe hesaplama için Haversine Formülü
- Find stops within radius / Yarıçap içindeki durakları bulma
- Sorted by distance / Mesafeye göre sıralama

### **Recent Updates / Son Güncellemeler**
- **Nearby Stops API** implemented / Yakın duraklar API'si eklendi
- **Distance calculation** with Haversine formula / Haversine formülü ile mesafe hesaplama
- **Stop details by ID** endpoint / ID'ye göre durak detayı endpoint'i

## Notes / Notlar

- **Day Types**: `weekday`, `saturday`, `sunday` / Gün tipleri
- **Coordinates**: Use decimal degrees (e.g., 40.7128) / Koordinatlar: Ondalık derece kullanın
- **Authentication**: Include `Bearer {token}` in headers / Kimlik Doğrulama: Header'da `Bearer {token}` ekleyin
- **Distance**: Calculated in kilometers using Haversine formula / Mesafe Haversine formülü ile kilometre cinsinden hesaplanır

---

**Version / Versiyon**: 1.0.0  
**Status / Durum**: Development / Geliştirme Aşamasında 
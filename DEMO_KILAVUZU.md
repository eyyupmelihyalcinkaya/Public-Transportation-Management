# 🎬 Demo Kılavuzu
## Şehir İçi Ulaşım Yönetim Sistemi

---

## 📋 Demo Öncesi Hazırlık

### Sistem Gereksinimleri
- **.NET 8 SDK** yüklü olmalı
- **PostgreSQL** veritabanı çalışır durumda
- **Redis** cache server aktif
- **RabbitMQ** message broker çalışıyor

### Başlatılacak Servisler
```bash
# 1. Ana API Servisi
cd internshipProject1
dotnet run

# 2. API Gateway
cd ApiGateway
dotnet run

# 3. Payment Service
cd PaymentService
dotnet run

# 4. GPS Service
cd GPSService
dotnet run

# 5. Web UI
cd internshipProject1.WebUI
dotnet run
```

---

## 🎯 Demo Senaryoları

### Senaryo 1: Kullanıcı Kaydı ve Girişi

#### 1.1 Yeni Kullanıcı Kaydı
```
📍 URL: https://localhost:7001/Account/Register
📝 Adımlar:
1. "Kayıt Ol" butonuna tıkla
2. Form bilgilerini doldur:
   - Kullanıcı Adı: demo_user
   - E-posta: demo@example.com
   - Şifre: Demo123!
   - Şifre Tekrar: Demo123!
3. "Kayıt Ol" butonuna tıkla
✅ Beklenen Sonuç: Başarılı kayıt mesajı
```

#### 1.2 Kullanıcı Girişi
```
📍 URL: https://localhost:7001/Account/Login
📝 Adımlar:
1. Giriş bilgilerini doldur:
   - Kullanıcı Adı: demo_user
   - Şifre: Demo123!
2. "Giriş Yap" butonuna tıkla
✅ Beklenen Sonuç: Dashboard'a yönlendirme
```

### Senaryo 2: Kart Yönetimi

#### 2.1 Yeni Kart Oluşturma
```
📍 Dashboard > "Kart Oluştur" butonu
📝 Adımlar:
1. "Kart Oluştur" butonuna tıkla
2. Kart adını gir: "Ana Kartım"
3. "Kartı Oluştur" butonuna tıkla
✅ Beklenen Sonuç: 
   - 3D kart animasyonu
   - Başarı mesajı
   - Kart numarası gösterimi
```

#### 2.2 Bakiye Yükleme
```
📍 Dashboard > "Kart Dolum İşlemi" butonu
📝 Adımlar:
1. "Kart Yükle" butonuna tıkla
2. Form bilgilerini doldur:
   - Kart Numarası: [Otomatik doldurulur]
   - Yüklenecek Tutar: ₺50
   - Ödeme Yöntemi: Kredi Kartı
3. "Kart Yükle" butonuna tıkla
✅ Beklenen Sonuç:
   - Başarı mesajı
   - Bakiye güncelleme
   - İşlem geçmişi
```

#### 2.3 Kart Okutma (Ödeme)
```
📍 Dashboard > "Kart Okutma" butonu
📝 Adımlar:
1. "Kart Okut" butonuna tıkla
2. Kartınızı seçin
3. "Kart Okut ve Öde" butonuna tıkla
✅ Beklenen Sonuç:
   - ₺40.00 ücret kesimi
   - Başarı mesajı
   - Bakiye güncelleme
```

### Senaryo 3: Araç Takip Sistemi

#### 3.1 Araç Takibi
```
📍 Dashboard > "Araç Takip" butonu
📝 Adımlar:
1. "Takip Et" butonuna tıkla
2. Harita üzerinde araçları görüntüle
3. Rota bilgilerini incele
✅ Beklenen Sonuç:
   - Gerçek zamanlı araç konumları
   - Rota görselleştirme
   - Durak bilgileri
```

### Senaryo 4: Admin Panel

#### 4.1 Admin Girişi
```
📍 URL: https://localhost:7001/Account/Login
📝 Admin Bilgileri:
   - Kullanıcı Adı: admin
   - Şifre: admin123
✅ Beklenen Sonuç: Admin paneline yönlendirme
```

#### 4.2 Rota Yönetimi
```
📍 Admin Panel > "Routes" tab
📝 Adımlar:
1. "Add New Route" butonuna tıkla
2. Rota bilgilerini doldur:
   - Rota Adı: Demo Rota
   - Açıklama: Demo amaçlı rota
3. "Save" butonuna tıkla
✅ Beklenen Sonuç: Yeni rota oluşturma
```

#### 4.3 Durak Yönetimi
```
📍 Admin Panel > "Stops" tab
📝 Adımlar:
1. "Add New Stop" butonuna tıkla
2. Durak bilgilerini doldur:
   - Durak Adı: Demo Durak
   - Enlem: 41.0082
   - Boylam: 28.9784
3. "Save" butonuna tıkla
✅ Beklenen Sonuç: Yeni durak oluşturma
```

---

## 🎨 Kullanıcı Arayüzü Özellikleri

### Modern Tasarım Elementleri

#### 1. Glass Morphism Efekti
```
🎨 Özellikler:
- Şeffaf cam efekti
- Blur arka plan
- Hafif gölge efektleri
- Modern görünüm
```

#### 2. 3D Kart Tasarımı
```
💳 Özellikler:
- Çevrilebilir kart animasyonu
- Gerçekçi kart tasarımı
- Chip ve logo detayları
- Hover efektleri
```

#### 3. Responsive Tasarım
```
📱 Özellikler:
- Mobil uyumlu
- Tablet optimizasyonu
- Desktop deneyimi
- Esnek layout
```

#### 4. Dark/Light Tema
```
🌙/☀️ Özellikler:
- Tema değiştirme
- Otomatik tema algılama
- Renk paleti uyumu
- Smooth geçişler
```

---

## 🔧 Teknik Demo Özellikleri

### 1. API Endpoints Test
```
📍 Swagger UI: https://localhost:7009/swagger
📝 Test Edilecek Endpoints:
- POST /api/user/login
- GET /api/card/GetCardByCustomerEmail
- POST /api/payment/boarding
- GET /api/routes
```

### 2. Real-time İşlemler
```
⚡ Özellikler:
- Anlık bakiye güncelleme
- Gerçek zamanlı araç takibi
- Canlı işlem bildirimleri
- WebSocket bağlantıları
```

### 3. Güvenlik Testleri
```
🔒 Test Senaryoları:
- JWT token doğrulama
- Role-based access control
- API key validation
- Input validation
```

---

## 📊 Performans Metrikleri

### Sistem Performansı
```
⚡ Metrikler:
- API Response Time: < 200ms
- Database Query Time: < 50ms
- Cache Hit Ratio: > 90%
- Concurrent Users: 1000+
```

### Kullanıcı Deneyimi
```
👥 UX Metrikleri:
- Sayfa Yükleme: < 2 saniye
- İşlem Tamamlama: < 5 saniye
- Hata Oranı: < 1%
- Kullanıcı Memnuniyeti: > 95%
```

---

## 🚀 Demo İpuçları

### Sunum Sırasında
1. **Sistemi Önceden Test Edin**: Tüm servislerin çalıştığından emin olun
2. **Demo Verilerini Hazırlayın**: Gerçekçi test verileri kullanın
3. **Yedek Plan Hazırlayın**: Teknik sorunlar için alternatif senaryolar
4. **Soru-Cevap Hazırlığı**: Olası sorular için cevaplarınızı hazırlayın

### Teknik Hazırlık
1. **Browser Cache'i Temizleyin**: Temiz bir demo için
2. **Network Bağlantısını Kontrol Edin**: Tüm servislerin erişilebilir olduğundan emin olun
3. **Log Dosyalarını İzleyin**: Hata durumunda hızlı müdahale için
4. **Backup Alın**: Demo öncesi sistem durumunu kaydedin

---

## 📝 Demo Script

### Giriş (2-3 dakika)
```
"Merhaba, bugün size geliştirdiğim Şehir İçi Ulaşım Yönetim Sistemi'ni tanıtacağım. 
Bu sistem, modern teknolojiler kullanarak toplu taşıma işlemlerini dijitalleştiren 
kapsamlı bir çözümdür."
```

### Teknoloji Stack (1-2 dakika)
```
"Sistem .NET 8, PostgreSQL, Redis ve RabbitMQ gibi modern teknolojiler kullanıyor. 
Mikroservis mimarisi ile ölçeklenebilir bir yapı kuruldu."
```

### Ana Özellikler (5-7 dakika)
```
"Şimdi sistemin ana özelliklerini göstereceğim:
1. Kullanıcı kaydı ve girişi
2. Kart yönetimi ve ödeme işlemleri
3. Araç takip sistemi
4. Admin paneli"
```

### Teknik Detaylar (3-4 dakika)
```
"Sistemin teknik detaylarını açıklayayım:
- Clean Architecture prensipleri
- JWT tabanlı güvenlik
- Real-time işlemler
- Responsive tasarım"
```

### Sonuç (1-2 dakika)
```
"Bu proje, modern yazılım geliştirme prensiplerini uygulayarak 
gerçek dünya problemlerini çözen kapsamlı bir sistemdir. 
Sorularınızı almaya hazırım."
```

---

## ❓ Olası Sorular ve Cevaplar

### Teknik Sorular
**S: Neden mikroservis mimarisi seçtiniz?**
C: Ölçeklenebilirlik, bağımsız geliştirme ve deployment kolaylığı için.

**S: Güvenlik nasıl sağlanıyor?**
C: JWT token, role-based access control ve input validation ile.

**S: Performans optimizasyonu nasıl yapıldı?**
C: Redis cache, database indexing ve connection pooling ile.

### İş Değeri Soruları
**S: Bu sistem hangi problemleri çözüyor?**
C: Toplu taşıma yönetimini dijitalleştiriyor, operasyonel verimliliği artırıyor.

**S: Maliyet avantajları nelerdir?**
C: Manuel süreçleri otomatikleştirerek maliyet tasarrufu sağlıyor.

**S: Gelecek planları nelerdir?**
C: Mobile app, AI analytics ve multi-language support planlanıyor.

---

*Bu demo kılavuzu, projenizi etkili bir şekilde sunmanıza yardımcı olacaktır.*

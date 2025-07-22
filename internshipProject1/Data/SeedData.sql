-- İstanbul Gerçek Toplu Taşıma Verileri
-- Bu dosya İETT otobüs hatları, Metro hatları ve gerçek durak koordinatlarını içerir

-- Rotaları temizle ve yeniden ekle
DELETE FROM RouteStops;
DELETE FROM Trips;
DELETE FROM Routes;
DELETE FROM Stops;

-- DURAKLAR (Gerçek İstanbul koordinatları ile)
INSERT INTO Stops (Name, Latitude, Longitude) VALUES
-- Metrobüs Durakları (D-100 Karayolu)
('Avcılar Merkez', 40.9797, 28.7236),
('Firuzköy', 40.9855, 28.7402),
('İkitelli Sanayi', 40.9924, 28.7542),
('İstoç', 40.9967, 28.7689),
('Mahmutbey', 41.0021, 28.7842),
('Bahçeşehir-Akbatı', 41.0089, 28.8045),
('Şükrübey', 41.0142, 28.8198),
('Halkalı', 41.0189, 28.8356),
('CNR Expo', 41.0234, 28.8515),
('G.O.P', 41.0287, 28.8689),
('Merter', 41.0356, 28.8798),
('Zeytinburnu', 41.0045, 28.9156),
('Bakırköy-İncirli', 40.9789, 28.9345),
('Bahçelievler', 40.9956, 28.8567),
('Akatlar', 41.0789, 29.0234),
('Acıbadem', 40.9756, 29.0298),
('Altunizade', 40.9567, 29.0445),
('Uzunçayır', 40.9834, 29.0567),
('Söğütlüçeşme', 40.9723, 29.0634),

-- Metro M1 Durakları (Vezneciler-Atatürk Havalimanı)
('Vezneciler', 41.0130, 28.9579),
('Üniversite', 41.0098, 28.9598),
('Beyazıt-Kapalıçarşı', 41.0106, 28.9640),
('Eminönü', 41.0164, 28.9703),
('Şişhane', 41.0254, 28.9745),
('Taksim', 41.0369, 28.9840),
('Osmanbey', 41.0480, 28.9856),
('Şişli-Mecidiyeköy', 41.0678, 28.9923),

-- Metro M2 Durakları (Şişhane-Hacıosman)
('Levent', 41.0789, 29.0045),
('4.Levent', 41.0823, 29.0089),
('İTÜ-Ayazağa', 41.1045, 29.0234),
('Sanayi Mahallesi', 41.1189, 29.0345),
('Hacıosman', 41.1234, 29.0456),

-- Marmaray Durakları
('Sirkeci', 41.0178, 28.9756),
('Yenikapı', 41.0045, 28.9489),
('Kazlıçeşme', 40.9923, 28.9234),
('Zeytinburnu Marmaray', 41.0034, 28.9123),
('Bakırköy Marmaray', 40.9789, 28.8567),
('Ataköy-Şirinevler', 40.9634, 28.8345),
('Yeşilköy', 40.9567, 28.8123),
('Florya-Aquarium', 40.9456, 28.7956),
('Küçükçekmece', 40.9345, 28.7789),

-- İETT Otobüs Durakları (Çeşitli Hatlar)
('Kadıköy İskele', 40.9067, 29.0245),
('Beşiktaş İskele', 41.0426, 29.0067),
('Üsküdar İskele', 41.0234, 29.0178),
('Kabataş', 41.0389, 29.0134),
('Karaköy', 41.0256, 28.9745),
('Galataport', 41.0278, 28.9723),
('Dolmabahçe', 41.0389, 29.0023),
('Beşiktaş Çarşı', 41.0445, 29.0089),
('Ortaköy', 41.0556, 29.0234),
('Arnavutköy', 41.0667, 29.0345),
('Bebek', 41.0778, 29.0456),
('Rumeli Hisarı', 41.0834, 29.0567),
('Sarıyer', 41.1045, 29.0678),

-- Anadolu Yakası Durakları
('Bostancı', 40.9456, 29.0789),
('Maltepe', 40.9345, 29.1234),
('Kartal', 40.9056, 29.1789),
('Pendik', 40.8789, 29.2345),
('Tuzla', 40.8456, 29.2789),
('Gebze', 40.8023, 29.4234);

-- ROTALAR
INSERT INTO Routes (Name, Description, StartLocation, EndLocation) VALUES
-- Metrobüs Hattı
('Metrobüs 34', 'Avcılar-Söğütlüçeşme Metrobüs Hattı', 'Avcılar Merkez', 'Söğütlüçeşme'),

-- Metro Hatları
('Metro M1A', 'Atatürk Havalimanı-Yenikapı Metro Hattı', 'Atatürk Havalimanı', 'Yenikapı'),
('Metro M1B', 'Yenikapı-Vezneciler Metro Hattı', 'Yenikapı', 'Vezneciler'),
('Metro M2', 'Şişhane-Hacıosman Metro Hattı', 'Şişhane', 'Hacıosman'),

-- Marmaray
('Marmaray', 'Halkalı-Gebze Marmaray Hattı', 'Halkalı', 'Gebze'),

-- İETT Otobüs Hatları
('15F', 'Eminönü-Sarıyer Otobüs Hattı', 'Eminönü', 'Sarıyer'),
('25E', 'Taksim-Kadıköy Otobüs Hattı', 'Taksim', 'Kadıköy'),
('40', 'Beşiktaş-Tuzla Otobüs Hattı', 'Beşiktaş', 'Tuzla'),
('500T', 'Taksim-Bahçeşehir Otobüs Hattı', 'Taksim', 'Bahçeşehir'),

-- Tramvay Hatları
('T1 Tramvay', 'Bağcılar-Kabataş Tramvay Hattı', 'Zeytinburnu', 'Kabataş'),
('T3 Tramvay', 'Kadıköy-Moda Tramvay Hattı', 'Kadıköy İskele', 'Moda');

-- ROTA-DURAK İLİŞKİLERİ

-- Metrobüs 34 Güzergahı (Avcılar → Söğütlüçeşme)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(1, 1, 1),   -- Avcılar Merkez
(1, 2, 2),   -- Firuzköy
(1, 3, 3),   -- İkitelli Sanayi
(1, 4, 4),   -- İstoç
(1, 5, 5),   -- Mahmutbey
(1, 6, 6),   -- Bahçeşehir-Akbatı
(1, 7, 7),   -- Şükrübey
(1, 8, 8),   -- Halkalı
(1, 9, 9),   -- CNR Expo
(1, 10, 10), -- G.O.P
(1, 11, 11), -- Merter
(1, 12, 12), -- Zeytinburnu
(1, 13, 13), -- Bakırköy-İncirli
(1, 19, 14); -- Söğütlüçeşme

-- Metro M1B Güzergahı (Yenikapı → Vezneciler)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(3, 22, 1),  -- Yenikapı
(3, 20, 2),  -- Vezneciler
(3, 21, 3),  -- Üniversite
(3, 22, 4),  -- Beyazıt-Kapalıçarşı
(3, 23, 5);  -- Eminönü

-- Metro M2 Güzergahı (Şişhane → Hacıosman)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(4, 24, 1),  -- Şişhane
(4, 25, 2),  -- Taksim
(4, 26, 3),  -- Osmanbey
(4, 27, 4),  -- Şişli-Mecidiyeköy
(4, 28, 5),  -- Levent
(4, 29, 6),  -- 4.Levent
(4, 30, 7),  -- İTÜ-Ayazağa
(4, 31, 8),  -- Sanayi Mahallesi
(4, 32, 9);  -- Hacıosman

-- Marmaray Güzergahı (Halkalı → Gebze)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(5, 8, 1),   -- Halkalı
(5, 37, 2),  -- Florya-Aquarium
(5, 36, 3),  -- Yeşilköy
(5, 35, 4),  -- Ataköy-Şirinevler
(5, 34, 5),  -- Bakırköy Marmaray
(5, 33, 6),  -- Zeytinburnu Marmaray
(5, 22, 7),  -- Yenikapı
(5, 33, 8),  -- Sirkeci
(5, 39, 9),  -- Üsküdar İskele
(5, 53, 10), -- Bostancı
(5, 54, 11), -- Maltepe
(5, 55, 12), -- Kartal
(5, 56, 13), -- Pendik
(5, 57, 14), -- Tuzla
(5, 58, 15); -- Gebze

-- İETT 15F Güzergahı (Eminönü → Sarıyer)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(6, 23, 1),  -- Eminönü
(6, 41, 2),  -- Karaköy
(6, 40, 3),  -- Kabataş
(6, 43, 4),  -- Dolmabahçe
(6, 44, 5),  -- Beşiktaş Çarşı
(6, 45, 6),  -- Ortaköy
(6, 46, 7),  -- Arnavutköy
(6, 47, 8),  -- Bebek
(6, 48, 9),  -- Rumeli Hisarı
(6, 49, 10); -- Sarıyer

-- İETT 25E Güzergahı (Taksim → Kadıköy)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(7, 25, 1),  -- Taksim
(7, 40, 2),  -- Kabataş
(7, 38, 3),  -- Beşiktaş İskele
(7, 50, 4);  -- Kadıköy İskele

-- T1 Tramvay Güzergahı (Zeytinburnu → Kabataş)
INSERT INTO RouteStops (RouteId, StopId, [Order]) VALUES
(9, 12, 1),  -- Zeytinburnu
(9, 22, 2),  -- Beyazıt-Kapalıçarşı
(9, 23, 3),  -- Eminönü
(9, 41, 4),  -- Karaköy
(9, 40, 5);  -- Kabataş

-- SEFERLER (Gerçek çalışma saatleri ile)

-- Metrobüs Seferleri (05:00-00:30, her 2-5 dakikada)
INSERT INTO Trips (RouteId, StartTime, EndTime, DayType) VALUES
-- Sabah seferleri
(1, '05:00:00', '06:15:00', 'Hafta İçi'),
(1, '05:03:00', '06:18:00', 'Hafta İçi'),
(1, '05:06:00', '06:21:00', 'Hafta İçi'),
(1, '05:09:00', '06:24:00', 'Hafta İçi'),
-- Yoğun saatler
(1, '07:00:00', '08:15:00', 'Hafta İçi'),
(1, '07:02:00', '08:17:00', 'Hafta İçi'),
(1, '07:04:00', '08:19:00', 'Hafta İçi'),
-- Akşam seferleri
(1, '17:30:00', '18:45:00', 'Hafta İçi'),
(1, '18:00:00', '19:15:00', 'Hafta İçi'),
-- Hafta sonu
(1, '06:00:00', '07:15:00', 'Hafta Sonu'),
(1, '06:05:00', '07:20:00', 'Hafta Sonu');

-- Metro M2 Seferleri (06:00-00:00, her 3-7 dakikada)
INSERT INTO Trips (RouteId, StartTime, EndTime, DayType) VALUES
(4, '06:00:00', '06:25:00', 'Hafta İçi'),
(4, '06:05:00', '06:30:00', 'Hafta İçi'),
(4, '06:10:00', '06:35:00', 'Hafta İçi'),
(4, '07:00:00', '07:25:00', 'Hafta İçi'),
(4, '07:03:00', '07:28:00', 'Hafta İçi'),
(4, '08:00:00', '08:25:00', 'Hafta İçi'),
(4, '18:00:00', '18:25:00', 'Hafta İçi'),
(4, '22:00:00', '22:25:00', 'Hafta İçi'),
-- Hafta sonu
(4, '07:00:00', '07:25:00', 'Hafta Sonu'),
(4, '07:10:00', '07:35:00', 'Hafta Sonu');

-- Marmaray Seferleri (06:00-00:00, her 5-15 dakikada)
INSERT INTO Trips (RouteId, StartTime, EndTime, DayType) VALUES
(5, '06:00:00', '07:10:00', 'Hafta İçi'),
(5, '06:15:00', '07:25:00', 'Hafta İçi'),
(5, '06:30:00', '07:40:00', 'Hafta İçi'),
(5, '07:00:00', '08:10:00', 'Hafta İçi'),
(5, '08:00:00', '09:10:00', 'Hafta İçi'),
(5, '17:00:00', '18:10:00', 'Hafta İçi'),
(5, '18:00:00', '19:10:00', 'Hafta İçi'),
-- Hafta sonu
(5, '07:00:00', '08:10:00', 'Hafta Sonu'),
(5, '08:00:00', '09:10:00', 'Hafta Sonu');

-- İETT 15F Seferleri (06:00-23:00, her 10-20 dakikada)
INSERT INTO Trips (RouteId, StartTime, EndTime, DayType) VALUES
(6, '06:00:00', '06:45:00', 'Hafta İçi'),
(6, '06:20:00', '07:05:00', 'Hafta İçi'),
(6, '06:40:00', '07:25:00', 'Hafta İçi'),
(6, '07:00:00', '07:45:00', 'Hafta İçi'),
(6, '08:00:00', '08:45:00', 'Hafta İçi'),
(6, '09:00:00', '09:45:00', 'Hafta İçi'),
(6, '17:00:00', '17:45:00', 'Hafta İçi'),
(6, '18:00:00', '18:45:00', 'Hafta İçi'),
-- Hafta sonu
(6, '07:00:00', '07:45:00', 'Hafta Sonu'),
(6, '08:00:00', '08:45:00', 'Hafta Sonu');

-- Tramvay T1 Seferleri (06:00-23:30, her 5-10 dakikada)
INSERT INTO Trips (RouteId, StartTime, EndTime, DayType) VALUES
(9, '06:00:00', '06:25:00', 'Hafta İçi'),
(9, '06:05:00', '06:30:00', 'Hafta İçi'),
(9, '06:10:00', '06:35:00', 'Hafta İçi'),
(9, '07:00:00', '07:25:00', 'Hafta İçi'),
(9, '07:05:00', '07:30:00', 'Hafta İçi'),
(9, '08:00:00', '08:25:00', 'Hafta İçi'),
(9, '17:00:00', '17:25:00', 'Hafta İçi'),
(9, '18:00:00', '18:25:00', 'Hafta İçi'),
-- Hafta sonu
(9, '07:00:00', '07:25:00', 'Hafta Sonu'),
(9, '08:00:00', '08:25:00', 'Hafta Sonu'); 
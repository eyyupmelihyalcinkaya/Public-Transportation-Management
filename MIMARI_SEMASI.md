# 🏗️ Proje Mimari Şeması

## Sistem Genel Bakış

```mermaid
graph TB
    subgraph "🌐 Client Layer"
        Web[Web Browser]
        Mobile[Mobile App - Future]
    end
    
    subgraph "🚪 Gateway Layer"
        Gateway[API Gateway]
        Auth[Authentication Service]
        RateLimit[Rate Limiter]
        Health[Health Monitor]
    end
    
    subgraph "🔧 Microservices Layer"
        MainAPI[Main API Service]
        Payment[Payment Service]
        GPS[GPS Service]
        Card[Card Management]
    end
    
    subgraph "🗄️ Data Layer"
        PostgreSQL[(PostgreSQL)]
        Redis[(Redis Cache)]
        RabbitMQ[(RabbitMQ)]
    end
    
    Web --> Gateway
    Mobile --> Gateway
    
    Gateway --> Auth
    Gateway --> RateLimit
    Gateway --> Health
    
    Gateway --> MainAPI
    Gateway --> Payment
    Gateway --> GPS
    Gateway --> Card
    
    MainAPI --> PostgreSQL
    MainAPI --> Redis
    Payment --> PostgreSQL
    Payment --> RabbitMQ
    GPS --> RabbitMQ
    Card --> PostgreSQL
```

## Clean Architecture Katmanları

```mermaid
graph LR
    subgraph "🎨 Presentation Layer"
        WebUI[Web UI - MVC]
        API[Web API]
    end
    
    subgraph "📋 Application Layer"
        Commands[Commands]
        Queries[Queries]
        DTOs[DTOs]
        Validators[Validators]
    end
    
    subgraph "🏗️ Domain Layer"
        Entities[Entities]
        Enums[Enums]
        Services[Domain Services]
    end
    
    subgraph "🔧 Infrastructure Layer"
        Repositories[Repositories]
        ExternalServices[External Services]
        Database[Database]
    end
    
    WebUI --> API
    API --> Commands
    API --> Queries
    Commands --> Entities
    Queries --> Entities
    Entities --> Repositories
    Repositories --> Database
```

## Veri Akış Diyagramı

```mermaid
sequenceDiagram
    participant U as User
    participant UI as Web UI
    participant GW as API Gateway
    participant API as Main API
    participant PAY as Payment Service
    participant GPS as GPS Service
    participant DB as Database
    participant RMQ as RabbitMQ
    participant REDIS as Redis Cache

    U->>UI: Login Request
    UI->>GW: Authenticate
    GW->>API: Validate Token
    API->>DB: Check User
    DB-->>API: User Data
    API-->>GW: JWT Token
    GW-->>UI: Success Response
    UI-->>U: Dashboard

    U->>UI: Card Payment
    UI->>GW: Payment Request
    GW->>PAY: Process Payment
    PAY->>DB: Update Balance
    PAY->>RMQ: Send Notification
    GPS->>RMQ: Receive Location
    GPS->>REDIS: Cache Location
    PAY-->>GW: Payment Success
    GW-->>UI: Confirmation
    UI-->>U: Success Message
```

## Güvenlik Katmanları

```mermaid
graph TD
    subgraph "🔒 Security Layers"
        JWT[JWT Authentication]
        RBAC[Role-Based Access Control]
        APIKey[API Key Validation]
        RateLimit[Rate Limiting]
        Validation[Input Validation]
    end
    
    subgraph "🛡️ Data Protection"
        HTTPS[HTTPS Encryption]
        Hash[Password Hashing]
        SQLInjection[SQL Injection Protection]
        XSS[XSS Protection]
    end
    
    JWT --> RBAC
    RBAC --> APIKey
    APIKey --> RateLimit
    RateLimit --> Validation
    
    HTTPS --> Hash
    Hash --> SQLInjection
    SQLInjection --> XSS
```

## Mikroservis İletişimi

```mermaid
graph LR
    subgraph "📡 Service Communication"
        Gateway[API Gateway]
        MainAPI[Main API]
        Payment[Payment Service]
        GPS[GPS Service]
        Card[Card Service]
    end
    
    subgraph "📨 Message Queue"
        RabbitMQ[(RabbitMQ)]
        Events[Event Bus]
        Commands[Command Bus]
    end
    
    subgraph "💾 Data Storage"
        PostgreSQL[(PostgreSQL)]
        Redis[(Redis)]
    end
    
    Gateway --> MainAPI
    Gateway --> Payment
    Gateway --> GPS
    Gateway --> Card
    
    Payment --> RabbitMQ
    GPS --> RabbitMQ
    MainAPI --> PostgreSQL
    Payment --> PostgreSQL
    Card --> PostgreSQL
    MainAPI --> Redis
```

## Kullanıcı Rolleri ve İzinler

```mermaid
graph TD
    subgraph "👥 User Roles"
        SuperAdmin[SuperAdmin]
        Admin[Admin]
        Passenger[Passenger]
    end
    
    subgraph "🔐 Permissions"
        UserMgmt[User Management]
        RouteMgmt[Route Management]
        CardMgmt[Card Management]
        PaymentMgmt[Payment Management]
        ReportMgmt[Report Management]
    end
    
    subgraph "📋 Access Rights"
        FullAccess[Full Access]
        ReadWrite[Read/Write]
        ReadOnly[Read Only]
        LimitedAccess[Limited Access]
    end
    
    SuperAdmin --> FullAccess
    SuperAdmin --> UserMgmt
    SuperAdmin --> RouteMgmt
    SuperAdmin --> CardMgmt
    SuperAdmin --> PaymentMgmt
    SuperAdmin --> ReportMgmt
    
    Admin --> ReadWrite
    Admin --> RouteMgmt
    Admin --> CardMgmt
    Admin --> PaymentMgmt
    
    Passenger --> ReadOnly
    Passenger --> CardMgmt
```

## Performans Optimizasyonu

```mermaid
graph TB
    subgraph "⚡ Performance Layers"
        CDN[CDN]
        LoadBalancer[Load Balancer]
        Cache[Redis Cache]
        Database[Database Indexing]
    end
    
    subgraph "📊 Monitoring"
        HealthCheck[Health Checks]
        Metrics[Metrics Collection]
        Logging[Logging]
        Alerting[Alerting]
    end
    
    subgraph "🔄 Optimization"
        ConnectionPool[Connection Pooling]
        QueryOptimization[Query Optimization]
        Caching[Application Caching]
        Compression[Response Compression]
    end
    
    CDN --> LoadBalancer
    LoadBalancer --> Cache
    Cache --> Database
    
    HealthCheck --> Metrics
    Metrics --> Logging
    Logging --> Alerting
    
    ConnectionPool --> QueryOptimization
    QueryOptimization --> Caching
    Caching --> Compression
```

## Deployment Mimarisi

```mermaid
graph TB
    subgraph "🌍 Production Environment"
        subgraph "🖥️ Web Servers"
            Web1[Web Server 1]
            Web2[Web Server 2]
            Web3[Web Server 3]
        end
        
        subgraph "🔧 Application Servers"
            App1[App Server 1]
            App2[App Server 2]
            App3[App Server 3]
        end
        
        subgraph "🗄️ Database Cluster"
            DB1[(Primary DB)]
            DB2[(Replica DB)]
            DB3[(Backup DB)]
        end
        
        subgraph "💾 Cache Cluster"
            Redis1[(Redis 1)]
            Redis2[(Redis 2)]
        end
        
        subgraph "📨 Message Queue"
            RMQ1[(RabbitMQ 1)]
            RMQ2[(RabbitMQ 2)]
        end
    end
    
    Web1 --> App1
    Web2 --> App2
    Web3 --> App3
    
    App1 --> DB1
    App2 --> DB1
    App3 --> DB1
    
    App1 --> Redis1
    App2 --> Redis2
    App3 --> Redis1
    
    App1 --> RMQ1
    App2 --> RMQ2
    App3 --> RMQ1
```

## Teknoloji Stack Detayı

```mermaid
graph LR
    subgraph "🎨 Frontend"
        HTML[HTML5]
        CSS[CSS3]
        JS[JavaScript ES6+]
        Bootstrap[Bootstrap 5]
        FontAwesome[Font Awesome]
    end
    
    subgraph "🔧 Backend"
        DotNet[.NET 8]
        ASPCore[ASP.NET Core]
        EF[Entity Framework]
        JWT[JWT Authentication]
    end
    
    subgraph "🗄️ Database"
        PostgreSQL[(PostgreSQL)]
        Redis[(Redis)]
        RabbitMQ[(RabbitMQ)]
    end
    
    subgraph "🛠️ Tools"
        Swagger[Swagger]
        MediatR[MediatR]
        FluentValidation[FluentValidation]
        AutoMapper[AutoMapper]
    end
    
    HTML --> CSS
    CSS --> JS
    JS --> Bootstrap
    Bootstrap --> FontAwesome
    
    DotNet --> ASPCore
    ASPCore --> EF
    EF --> JWT
    
    EF --> PostgreSQL
    ASPCore --> Redis
    ASPCore --> RabbitMQ
    
    Swagger --> MediatR
    MediatR --> FluentValidation
    FluentValidation --> AutoMapper
```

---

## 📊 Sistem Metrikleri

| Bileşen | Teknoloji | Versiyon | Amaç |
|---------|-----------|----------|------|
| **Frontend** | ASP.NET Core MVC | 8.0 | Kullanıcı arayüzü |
| **API Gateway** | ASP.NET Core | 8.0 | Merkezi yönetim |
| **Main API** | ASP.NET Core Web API | 8.0 | Ana iş mantığı |
| **Payment Service** | ASP.NET Core | 8.0 | Ödeme işlemleri |
| **GPS Service** | .NET Worker Service | 8.0 | Konum takibi |
| **Database** | PostgreSQL | 15+ | Ana veritabanı |
| **Cache** | Redis | 7.0+ | Önbellekleme |
| **Message Queue** | RabbitMQ | 3.12+ | Mesajlaşma |

---

*Bu mimari şeması, projenin teknik yapısını ve bileşenler arası ilişkileri görsel olarak sunmaktadır.*

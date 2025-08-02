const tabs = document.querySelectorAll('.tab');
const content = document.getElementById('tab-content');

tabs.forEach(tab => {
    tab.addEventListener('click', () => {
        tabs.forEach(t => t.classList.remove('active'));
        tab.classList.add('active');
        const tabName = tab.getAttribute('data-tab');

        switch (tabName) {
            case 'routes':
                content.innerHTML = `<h3>Routes</h3><p>Routes listesi ve işlemler burada olacak.</p>`;
                break;
            case 'stops':
                content.innerHTML = `<h3>Stops</h3><p>Stops listesi ve işlemler burada olacak.</p>`;
                break;
            case 'trips':
                content.innerHTML = `<h3>Trips</h3><p>Trips listesi ve işlemler burada olacak.</p>`;
                break;
            case 'routestops':
                content.innerHTML = `<h3>RouteStops</h3><p>RouteStops listesi ve işlemler burada olacak.</p>`;
                break;
        }
    });
});

// ============ SADE ROUTE STOPS MODAL FONKSİYONLARI ============

// Basit modal yönetimi
class SimpleRouteStopsModal {
    constructor() {
        this.modal = document.getElementById('routeStopsModal');
        this.modalBody = document.getElementById('routeStopsModalBody');
        this.init();
    }

    init() {
        if (!this.modal) {
            console.error('Modal elementi bulunamadı!');
            return;
        }
        
        console.log('Modal hazır');
    }

    showLoading() {
        if (!this.modalBody) return;
        
        this.modalBody.innerHTML = `
            <tr>
                <td colspan="3" class="text-center py-4">
                    <div class="loading-spinner"></div>
                    <p class="mt-3 mb-0 text-muted">Route stops yükleniyor...</p>
                </td>
            </tr>
        `;
    }

    showEmptyState() {
        if (!this.modalBody) return;
        
        this.modalBody.innerHTML = `
            <tr>
                <td colspan="3" class="text-center py-4">
                    <div class="empty-state">
                        <i class="fas fa-map-marker-alt"></i>
                        <h5 class="mb-2">Henüz durak bulunmuyor</h5>
                        <p class="text-muted mb-0">Bu route için henüz durak eklenmemiş.</p>
                    </div>
                </td>
            </tr>
        `;
    }

    loadRouteStops(routeId) {
        if (!this.modalBody) return;

        this.showLoading();

        // API'den route stops verilerini al
        fetch(`https://localhost:7007/api/routes/${routeId}/stops`, {
            headers: {
                'X-Api-Key': 'melihin-muhtesem-otesi-apisi',
                'Authorization': 'Bearer melihin-muhtesem-otesi-apisi'
            }
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Route stops yüklenemedi');
            }
            return response.json();
        })
        .then(data => {
            this.renderRouteStops(data);
        })
        .catch(error => {
            console.error('Route stops yükleme hatası:', error);
            this.showError('Route stops yüklenirken bir hata oluştu.');
        });
    }

    renderRouteStops(stops) {
        if (!this.modalBody) return;

        if (!stops || stops.length === 0) {
            this.showEmptyState();
            return;
        }

        const stopsHtml = stops.map((stop, index) => `
            <tr class="route-stop-row" data-stop-id="${stop.id}">
                <td>
                    <span class="badge bg-primary rounded-pill">${index + 1}</span>
                </td>
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fas fa-map-marker-alt text-primary me-2"></i>
                        <span class="fw-semibold">${stop.name || 'İsimsiz Durak'}</span>
                    </div>
                </td>
                <td>
                    <div class="d-flex align-items-center">
                        <i class="fas fa-location-dot text-success me-2"></i>
                        <span>${stop.latitude}, ${stop.longitude}</span>
                    </div>
                </td>
            </tr>
        `).join('');

        this.modalBody.innerHTML = stopsHtml;
    }

    showError(message) {
        if (!this.modalBody) return;
        
        this.modalBody.innerHTML = `
            <tr>
                <td colspan="3" class="text-center py-4">
                    <div class="empty-state">
                        <i class="fas fa-exclamation-triangle text-warning"></i>
                        <h5 class="mb-2 text-warning">Hata</h5>
                        <p class="text-muted mb-0">${message}</p>
                    </div>
                </td>
            </tr>
        `;
    }

    openModal(routeId) {
        if (!this.modal) return;
        
        // Modal'ı aç
        const modal = new bootstrap.Modal(this.modal);
        modal.show();
        
        // Route stops verilerini yükle
        if (routeId) {
            setTimeout(() => {
                this.loadRouteStops(routeId);
            }, 300);
        }
    }
}

// Modal instance'ını oluştur
const routeStopsModal = new SimpleRouteStopsModal();

// Global fonksiyon olarak expose et
window.openRouteStopsModal = function(routeId) {
    if (routeStopsModal) {
        routeStopsModal.openModal(routeId);
    }
};

// Sayfa yüklendiğinde modal'ı initialize et
document.addEventListener('DOMContentLoaded', function() {
    console.log('Route Stops Modal hazır!');
});

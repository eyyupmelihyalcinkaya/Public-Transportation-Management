// API Configuration - Bu dosya server-side'dan konfigürasyon alır
// Gizli bilgiler burada hardcode edilmez

// API helper fonksiyonları
window.apiConfig = {
    // API istekleri için helper fonksiyon
    async makeApiRequest(endpoint, options = {}) {
        const defaultOptions = {
            headers: {
                'Content-Type': 'application/json',
                'X-Api-Key': window.apiKey // Server-side'dan gelen API key
            }
        };

        // Options'ları merge et
        const finalOptions = {
            ...defaultOptions,
            ...options,
            headers: {
                ...defaultOptions.headers,
                ...options.headers
            }
        };

        const url = `${window.gatewayUrl}${endpoint}`;
        
        try {
            const response = await fetch(url, finalOptions);
            
            if (!response.ok) {
                throw new Error(`API request failed: ${response.status} ${response.statusText}`);
            }
            
            return await response.json();
        } catch (error) {
            console.error('API request error:', error);
            throw error;
        }
    },

    // Login helper
    async login(username, password) {
        return this.makeApiRequest('/api/user/login', {
            method: 'POST',
            body: JSON.stringify({ username, password })
        });
    },

    // Register helper - Customer bilgileri ile
    async register(userData) {
        return this.makeApiRequest('/api/user/register', {
            method: 'POST',
            body: JSON.stringify(userData)
        });
    },

    // Routes helper
    async getRoutes(page = 1, pageSize = 10) {
        return this.makeApiRequest(`/api/routes?page=${page}&pageSize=${pageSize}`);
    },

    async createRoute(routeData) {
        return this.makeApiRequest('/api/routes', {
            method: 'POST',
            body: JSON.stringify(routeData)
        });
    },

    async deleteRoute(id) {
        return this.makeApiRequest(`/api/routes/${id}`, {
            method: 'DELETE'
        });
    },

    async getRoutesCount() {
        return this.makeApiRequest('/api/routes/TotalCount');
    },

    // Stops helper
    async getStops(page = 1, pageSize = 10) {
        return this.makeApiRequest(`/api/stops?page=${page}&pageSize=${pageSize}`);
    },

    async createStop(stopData) {
        return this.makeApiRequest('/api/stops', {
            method: 'POST',
            body: JSON.stringify(stopData)
        });
    },

    async getStopsCount() {
        return this.makeApiRequest('/api/stops/TotalCount');
    },

    // Trips helper
    async getTrips(page = 1, pageSize = 10) {
        return this.makeApiRequest(`/api/trips?page=${page}&pageSize=${pageSize}`);
    },

    async getTripsCount() {
        return this.makeApiRequest('/api/trips/TotalCount');
    },

    // RouteStops helper
    async getRouteStops(page = 1, pageSize = 10) {
        return this.makeApiRequest(`/api/routestop/GetAll?page=${page}&pageSize=${pageSize}`);
    },

    async getRouteStopsCount() {
        return this.makeApiRequest('/api/routestop/TotalCount');
    }
}; 
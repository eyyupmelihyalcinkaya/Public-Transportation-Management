document.addEventListener("DOMContentLoaded", function () {
    initLoginFormValidation();
    initThemeToggle();
    
    // Global tema yönetimi - Modern animasyonlu geçiş
    window.setTheme = function(theme, animated = false) {
        console.log('setTheme called with:', theme);
        
        if (animated) {
            // Animasyonlu tema geçişi
            const overlay = document.getElementById('theme-transition-overlay');
            const themeIcon = document.getElementById('theme-icon');
            
            // 1. Overlay animasyonunu başlat
            if (overlay) {
                overlay.classList.add('active');
            }
            
            // 2. Icon'u döndür
            if (themeIcon) {
                themeIcon.style.transform = 'rotate(360deg) scale(1.2)';
                themeIcon.style.transition = 'transform 0.5s cubic-bezier(0.68, -0.55, 0.265, 1.55)';
            }
            
            // 3. Kısa gecikme sonrası tema değiştir
            setTimeout(() => {
                document.body.classList.remove('light', 'dark');
                document.body.classList.add(theme);
                
                // Icon'u güncelle
                if (themeIcon) {
                    if (theme === 'dark') {
                        themeIcon.classList.remove('fa-moon');
                        themeIcon.classList.add('fa-sun');
                    } else {
                        themeIcon.classList.remove('fa-sun');
                        themeIcon.classList.add('fa-moon');
                    }
                    
                    // Icon animasyonunu sıfırla
                    setTimeout(() => {
                        themeIcon.style.transform = 'rotate(0deg) scale(1)';
                    }, 100);
                }
                
                localStorage.setItem('theme', theme);
                console.log('Theme set to:', theme);
                
                // 4. Overlay'i kaldır
                if (overlay) {
                    setTimeout(() => {
                        overlay.classList.remove('active');
                    }, 100);
                }
            }, 150);
            
        } else {
            // Hızlı tema geçişi (sayfa yüklenirken)
            document.body.classList.remove('light', 'dark');
            document.body.classList.add(theme);
            localStorage.setItem('theme', theme);
            
            const themeIcon = document.getElementById('theme-icon');
            if (themeIcon) {
                if (theme === 'dark') {
                    themeIcon.classList.remove('fa-moon');
                    themeIcon.classList.add('fa-sun');
                } else {
                    themeIcon.classList.remove('fa-sun');
                    themeIcon.classList.add('fa-moon');
                }
            }
            console.log('Theme set to:', theme);
        }
    };
    
    // Tema'yı başlangıçta yükle
    const savedTheme = localStorage.getItem('theme') || 'light';
    window.setTheme(savedTheme);
    
    // Login durumunu kontrol et
    checkAuthStatus();
    
    // Global tema toggle fonksiyonu - Animasyonlu
    window.toggleTheme = function() {
        console.log('toggleTheme called');
        const currentTheme = document.body.classList.contains('dark') ? 'dark' : 'light';
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        window.setTheme(newTheme, true); // Animasyonlu geçiş
    };
    
    // Auth durumu kontrol fonksiyonları
    window.checkAuthStatus = function() {
        const token = localStorage.getItem('token');
        const authButtons = document.getElementById('auth-buttons');
        const profileMenu = document.getElementById('profile-menu');
        const usernameDisplay = document.getElementById('username-display');
        
        if (token && token !== 'null' && token !== '""') {
            // Kullanıcı giriş yapmış
            if (authButtons) authButtons.classList.add('d-none');
            if (profileMenu) profileMenu.classList.remove('d-none');
            
            // Token'dan kullanıcı bilgilerini al (basit parse)
            try {
                const parsedToken = JSON.parse(token);
                if (usernameDisplay && parsedToken.username) {
                    usernameDisplay.textContent = parsedToken.username;
                }
            } catch (e) {
                if (usernameDisplay) {
                    usernameDisplay.textContent = 'Kullanıcı';
                }
            }
        } else {
            // Kullanıcı giriş yapmamış
            if (authButtons) authButtons.classList.remove('d-none');
            if (profileMenu) profileMenu.classList.add('d-none');
        }
    };
    
    // Logout fonksiyonu
    window.logout = function() {
        if (confirm('Çıkış yapmak istediğinize emin misiniz?')) {
            localStorage.removeItem('token');
            window.location.href = '/Home/Index';
        }
    };
    
    // Profile sayfası göster
    window.showProfile = function() {
        alert('Profil sayfası yakında eklenecek!');
    };
    
    // Settings sayfası göster
    window.showSettings = function() {
        // AdminPanel'deki settings modal'ını aç
        const settingsModal = document.getElementById('settingsModal');
        if (settingsModal) {
            const modal = new bootstrap.Modal(settingsModal);
            modal.show();
        } else {
            alert('Ayarlar sayfası yakında eklenecek!');
        }
    };
});

function initLoginFormValidation() {
    var form = document.querySelector("form");
    if (form) {
        form.addEventListener("submit", function (event) {
            var userName = form.querySelector('input[name="Username"]').value;
            var password = form.querySelector('input[name="Password"]').value;
            if (userName.trim() === "" || password.trim() === "") {
                alert("Username and password cannot be empty.");
                event.preventDefault();
            }
        });
    }
}

function initThemeToggle() {
    console.log('initThemeToggle called'); // Debug
    
    const toggleButton = document.getElementById('theme-toggle');
    console.log('Toggle button found:', toggleButton); // Debug
    
    if (!toggleButton) {
        console.log('Theme toggle button not found!'); // Debug
        return;
    }

    const themeIcon = document.getElementById('theme-icon');
    const savedTheme = localStorage.getItem('theme') || 'light';
    
    console.log('Saved theme:', savedTheme); // Debug

    // Önce mevcut tema sınıflarını temizle
    document.body.classList.remove('light', 'dark');
    document.body.classList.add(savedTheme);
    
    if (themeIcon) {
        if (savedTheme === 'dark') {
            themeIcon.classList.remove('fa-moon');
            themeIcon.classList.add('fa-sun');
        } else {
            themeIcon.classList.remove('fa-sun');
            themeIcon.classList.add('fa-moon');
        }
    }

    toggleButton.addEventListener('click', function () {
        console.log('Theme toggle clicked'); // Debug
        
        let newTheme = document.body.classList.contains('dark') ? 'light' : 'dark';
        document.body.classList.remove('light', 'dark');
        document.body.classList.add(newTheme);

        console.log('New theme:', newTheme); // Debug

        if (themeIcon) {
            if (newTheme === 'dark') {
                themeIcon.classList.remove('fa-moon');
                themeIcon.classList.add('fa-sun');
            } else {
                themeIcon.classList.remove('fa-sun');
                themeIcon.classList.add('fa-moon');
            }
        }

        localStorage.setItem('theme', newTheme);
        console.log('Theme saved to localStorage'); // Debug
    });
}

// Global namespace for our functions
const App = {};

/**
 * Sets the color theme for the entire application.
 * @param {string} theme - The theme to set ('light' or 'dark').
 * @param {boolean} animated - Whether to use a transition animation.
 */
App.setTheme = (theme, animated = false) => {
    const themeIcon = document.getElementById('theme-icon');

    const applyTheme = () => {
        document.body.classList.remove('light', 'dark');
        document.body.classList.add(theme);
        localStorage.setItem('theme', theme);
        if (themeIcon) {
            themeIcon.className = theme === 'dark' ? 'fas fa-sun' : 'fas fa-moon';
        }
    };

    if (animated) {
        const overlay = document.getElementById('theme-transition-overlay');
        if (overlay) {
            overlay.classList.add('active');
            setTimeout(() => {
                applyTheme();
                setTimeout(() => overlay.classList.remove('active'), 150);
            }, 150);
        }
    } else {
        applyTheme();
    }
};

/**
 * Toggles the color theme between light and dark.
 */
App.toggleTheme = () => {
    const currentTheme = document.body.classList.contains('dark') ? 'dark' : 'light';
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    App.setTheme(newTheme, true);
};

/**
 * Updates the navigation bar based on the user's authentication status.
 */
App.updateNavbar = () => {
    const token = localStorage.getItem('token');
    const authButtons = document.getElementById('auth-buttons');
    const profileMenu = document.getElementById('profile-menu');
    const usernameDisplay = document.getElementById('username-display');

    if (token) {
        // User is logged in
        authButtons?.classList.add('d-none');
        profileMenu?.classList.remove('d-none');
        try {
            const parsedToken = JSON.parse(atob(token.split('.')[1])); // Decode JWT
            if (usernameDisplay) {
                usernameDisplay.textContent = parsedToken.sub || 'User'; // 'sub' is standard for subject/username
            }
        } catch (e) {
            console.error('Error parsing token:', e);
            if (usernameDisplay) usernameDisplay.textContent = 'User';
        }
    } else {
        // User is not logged in
        authButtons?.classList.remove('d-none');
        profileMenu?.classList.add('d-none');
    }
};

/**
 * Initiates the logout process by showing a confirmation modal.
 */
App.logout = () => {
    const logoutModal = new bootstrap.Modal(document.getElementById('logoutModal'));
    logoutModal.show();
};

/**
 * Handles the main initialization when the DOM is ready.
 */
document.addEventListener('DOMContentLoaded', () => {
    // Set initial theme
    const savedTheme = localStorage.getItem('theme') || 'light';
    App.setTheme(savedTheme);

    // Update navbar based on auth status
    App.updateNavbar();

    // Add event listeners
    const themeToggleButton = document.getElementById('theme-toggle');
    themeToggleButton?.addEventListener('click', App.toggleTheme);

    const confirmLogoutBtn = document.getElementById('confirmLogoutBtn');
    confirmLogoutBtn?.addEventListener('click', () => {
        localStorage.removeItem('token');
        window.location.href = '/Account/Login'; // Redirect to login after logout
    });
});
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

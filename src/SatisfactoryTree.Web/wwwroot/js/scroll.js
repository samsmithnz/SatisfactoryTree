window.scrollToFactoryAnchor = (anchor) => {
    if (!anchor) return;
    const el = document.getElementById(anchor);
    if (el) {
        // Use smooth scrolling into view
        el.scrollIntoView({ behavior: 'smooth', block: 'start' });
        // Optionally flash highlight
        el.classList.add('recently-added-factory');
        setTimeout(()=> el.classList.remove('recently-added-factory'), 2000);
    }
};

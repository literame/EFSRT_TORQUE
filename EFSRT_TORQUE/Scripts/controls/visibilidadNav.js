function toggleDivVisibility(divId) {
    var div = document.getElementById(divId);
    var nav = document.getElementsByClassName('leftSide');

    if (div) {
        if (nav.style.display === 'flex') {
            nav.style.display = 'block';
        } else {
            nav.style.display = '';
        }
    } else {
        console.log(`El elemento con id '${divId}' no existe.`);
    }
};

toggleDivVisibility("Loggeo");

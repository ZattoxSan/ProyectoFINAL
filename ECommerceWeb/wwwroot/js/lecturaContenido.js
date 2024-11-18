let lastSpeechTime = 0; // Variable para almacenar el tiempo de la última lectura
let isVoiceEnabled = JSON.parse(localStorage.getItem('isVoiceEnabled')) || false;
let isColorblindModeEnabled = JSON.parse(localStorage.getItem('isColorblindModeEnabled')) || false;
let isDarkModeEnabled = JSON.parse(localStorage.getItem('isDarkModeEnabled')) || false;
let isHighContrastModeEnabled = JSON.parse(localStorage.getItem('isHighContrastModeEnabled')) || false;
let isTextSpacingEnabled = JSON.parse(localStorage.getItem('isTextSpacingEnabled')) || false;
let isTextMagnificationEnabled = JSON.parse(localStorage.getItem('isTextMagnificationEnabled')) || false;

let currentSpeech;

function actualizarBotonVoz() {
    const toggleVoiceButton = document.querySelector('#toggleVoice');
    if (isVoiceEnabled) {
        toggleVoiceButton.style.backgroundColor = 'green';
        toggleVoiceButton.innerText = 'Desactivar Voz';
    } else {
        toggleVoiceButton.style.backgroundColor = 'red';
        toggleVoiceButton.innerText = 'Activar Voz';
    }
}

function leerTexto(texto) {
    const currentTime = new Date().getTime();
    if (currentTime - lastSpeechTime < 200 || !isVoiceEnabled) return;
    lastSpeechTime = currentTime;
    window.speechSynthesis.cancel();

    currentSpeech = new SpeechSynthesisUtterance(texto);
    currentSpeech.lang = 'es-MX';
    currentSpeech.rate = 1;
    currentSpeech.pitch = 1;

    const voices = window.speechSynthesis.getVoices();
    const spanishVoice = voices.find(voice => voice.lang === 'es-MX' || voice.lang === 'es-ES');
    if (spanishVoice) {
        currentSpeech.voice = spanishVoice;
    }
    window.speechSynthesis.speak(currentSpeech);
}

function actualizarModoDaltonismo() {
    const body = document.body;
    const toggleColorblindButton = document.querySelector('#toggleColorblind');
    if (isColorblindModeEnabled) {
        body.style.filter = 'grayscale(100%) contrast(1.2)';
        body.style.backgroundColor = '#f0f0f0';
        const elements = body.querySelectorAll('a, button, h1, h2, h3, h4, h5, h6, p, span');
        elements.forEach(element => element.style.color = '#000');
        const buttons = body.querySelectorAll('.btn');
        buttons.forEach(button => {
            button.style.backgroundColor = '#333';
            button.style.color = '#fff';
        });
        toggleColorblindButton.innerText = 'Desactivar Modo Daltonismo';
    } else {
        body.style.filter = '';
        body.style.backgroundColor = '';
        const elements = body.querySelectorAll('a, button, h1, h2, h3, h4, h5, h6, p, span');
        elements.forEach(element => element.style.color = '');
        const buttons = body.querySelectorAll('.btn');
        buttons.forEach(button => {
            button.style.backgroundColor = '';
            button.style.color = '';
        });
        toggleColorblindButton.innerText = 'Activar Modo Daltonismo';
    }
}

function actualizarModoOscuro() {
    const body = document.body;
    const toggleDarkModeButton = document.querySelector('#toggleDarkMode');
    if (isDarkModeEnabled) {
        body.style.backgroundColor = '#121212';
        body.style.color = '#ffffff';
        toggleDarkModeButton.innerText = 'Desactivar Modo Oscuro';
    } else {
        body.style.backgroundColor = '';
        body.style.color = '';
        toggleDarkModeButton.innerText = 'Activar Modo Oscuro';
    }
}

function actualizarModoAltoContraste() {
    const body = document.body;
    const toggleHighContrastButton = document.querySelector('#toggleHighContrast');
    if (isHighContrastModeEnabled) {
        body.style.backgroundColor = '#000000';
        body.style.color = '#FFFF00';
        toggleHighContrastButton.innerText = 'Desactivar Alto Contraste';
    } else {
        body.style.backgroundColor = '';
        body.style.color = '';
        toggleHighContrastButton.innerText = 'Activar Alto Contraste';
    }
}

function actualizarEspaciadoTexto() {
    const body = document.body;
    const toggleTextSpacingButton = document.querySelector('#toggleTextSpacing');
    if (isTextSpacingEnabled) {
        body.style.letterSpacing = '0.2em';
        body.style.wordSpacing = '0.3em';
        body.style.lineHeight = '1.8';
        toggleTextSpacingButton.innerText = 'Desactivar Espaciado de Texto';
    } else {
        body.style.letterSpacing = '';
        body.style.wordSpacing = '';
        body.style.lineHeight = '';
        toggleTextSpacingButton.innerText = 'Activar Espaciado de Texto';
    }
}

function actualizarMagnificacionTexto() {
    const body = document.body;
    const toggleTextMagnificationButton = document.querySelector('#toggleTextMagnification');
    if (isTextMagnificationEnabled) {
        body.style.fontSize = '1.5em';
        toggleTextMagnificationButton.innerText = 'Desactivar Magnificación de Texto';
    } else {
        body.style.fontSize = '';
        toggleTextMagnificationButton.innerText = 'Activar Magnificación de Texto';
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const toggleVoiceButton = document.querySelector('#toggleVoice');
    const toggleColorblindButton = document.querySelector('#toggleColorblind');
    const toggleDarkModeButton = document.querySelector('#toggleDarkMode');
    const toggleHighContrastButton = document.querySelector('#toggleHighContrast');
    const toggleTextSpacingButton = document.querySelector('#toggleTextSpacing');
    const toggleTextMagnificationButton = document.querySelector('#toggleTextMagnification');
    const accessibilityMenu = document.querySelector('#accessibilityMenu');

    actualizarBotonVoz();
    actualizarModoDaltonismo();
    actualizarModoOscuro();
    actualizarModoAltoContraste();
    actualizarEspaciadoTexto();
    actualizarMagnificacionTexto();

    toggleVoiceButton.addEventListener('click', () => {
        isVoiceEnabled = !isVoiceEnabled;
        localStorage.setItem('isVoiceEnabled', JSON.stringify(isVoiceEnabled));
        actualizarBotonVoz();
        if (!isVoiceEnabled) {
            window.speechSynthesis.cancel();
        }
    });

    toggleColorblindButton.addEventListener('click', () => {
        isColorblindModeEnabled = !isColorblindModeEnabled;
        localStorage.setItem('isColorblindModeEnabled', JSON.stringify(isColorblindModeEnabled));
        actualizarModoDaltonismo();
    });

    toggleDarkModeButton.addEventListener('click', () => {
        isDarkModeEnabled = !isDarkModeEnabled;
        localStorage.setItem('isDarkModeEnabled', JSON.stringify(isDarkModeEnabled));
        actualizarModoOscuro();
    });

    toggleHighContrastButton.addEventListener('click', () => {
        isHighContrastModeEnabled = !isHighContrastModeEnabled;
        localStorage.setItem('isHighContrastModeEnabled', JSON.stringify(isHighContrastModeEnabled));
        actualizarModoAltoContraste();
    });

    toggleTextSpacingButton.addEventListener('click', () => {
        isTextSpacingEnabled = !isTextSpacingEnabled;
        localStorage.setItem('isTextSpacingEnabled', JSON.stringify(isTextSpacingEnabled));
        actualizarEspaciadoTexto();
    });

    toggleTextMagnificationButton.addEventListener('click', () => {
        isTextMagnificationEnabled = !isTextMagnificationEnabled;
        localStorage.setItem('isTextMagnificationEnabled', JSON.stringify(isTextMagnificationEnabled));
        actualizarMagnificacionTexto();
    });

    // Implementar la funcionalidad de mover el botón de accesibilidad
    let isDragging = false;
    let offsetX = 0;
    let offsetY = 0;

    accessibilityMenu.addEventListener('mousedown', (e) => {
        isDragging = true;
        offsetX = e.clientX - accessibilityMenu.offsetLeft;
        offsetY = e.clientY - accessibilityMenu.offsetTop;
        document.addEventListener('mousemove', moveElement);
        document.addEventListener('mouseup', () => {
            isDragging = false;
            document.removeEventListener('mousemove', moveElement);
        });
    });

    function moveElement(e) {
        if (isDragging) {
            accessibilityMenu.style.left = e.clientX - offsetX + 'px';
            accessibilityMenu.style.top = e.clientY - offsetY + 'px';
        }
    }

    document.querySelectorAll('.lectura').forEach(el => {
        el.addEventListener('mouseenter', () => leerTexto(el.innerText));
        el.addEventListener('mouseleave', () => window.speechSynthesis.cancel());
    });
});

export function SetTheme() {

    let prismStyleLink;
    const isDarkMode = localStorage.getItem('sharpsplash-darkmode');

    const matches = document.querySelectorAll("link[href^='css/prism/']")
    if (matches.length === 0) {
        prismStyleLink = document.createElement('link');
    } else {
        prismStyleLink = matches[0];
    }

    prismStyleLink.setAttribute('rel', 'stylesheet');

    if (isDarkMode === "true") {
        prismStyleLink.setAttribute('href', 'css/prism/prism-tomorrow.min.css');
    } else {
        prismStyleLink.setAttribute('href', 'css/prism/prism-default.min.css');
    }

    document.head.appendChild(prismStyleLink);
}

export function HighLight(code, language) {
    let prismLanguage = Prism.languages[language];
    if (!prismLanguage) {
        if (language) {
            console.log(`unsupported language: "${language}", "${code}"`);
            return code;
        } else {
            prismLanguage = Prism.languages.javascript;
        }
    }
    return Prism.highlight(code, prismLanguage, language);
}
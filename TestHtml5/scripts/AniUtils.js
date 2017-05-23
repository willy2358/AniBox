function shuffleArray(array) {
    for (var i = array.length - 1; i >= 0; i--) {
        var randomIndex = Math.floor(Math.random() * (i + 1));
        var itemAtIndex = array[randomIndex];
        array[randomIndex] = array[i];
        array[i] = itemAtIndex;
    }
    return array;
};

function randomNumber(min, max) {
    return Math.random() * (max - min + 1) + min;
};
// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById('pokemonFilter').addEventListener('input', function () {
    var filter = this.value.toLowerCase();
    var cards = document.querySelectorAll('.pokemon-card');

    cards.forEach(function (card) {
        if (card.getAttribute('data-name').includes(filter)) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });
});
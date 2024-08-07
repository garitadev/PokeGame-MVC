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


$("body").on("click", "[data-agregar-pkm]", function (e) {
    var card = $(this).closest('.card-body');
    var pokemon = {
        Name: card.find('.card-title').data('nombre'),
        Id: card.find('.card-text[data-id]').data('id'),
        Weight: card.find('.card-text[data-weight]').data('weight'),
        Types: card.find('.card-text[data-types]').data('types')
    };


    console.log(pokemon)

    $.ajax({
        url: '/TuControlador/TuAccion',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(pokemon),
        success: function (response) {
            alert('Pokemon agregado a la pokedex!');
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error: ' + error);
        }
    });

    console.log(pokemon)
})
$('[data-agregar-pkm]').on('click', function () {
    
});

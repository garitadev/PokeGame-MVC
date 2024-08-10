﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*document.getElementById('pokemonFilter').addEventListener('input', function () {
    var filter = this.value.toLowerCase();
    var cards = document.querySelectorAll('.pokemon-card');

    cards.forEach(function (card) {
        var cardName = card.getAttribute('data-name').toLowerCase();
        card.style.display = cardName.includes(filter) ? 'block' : 'none';
    });
});*/

$("body").on("click", "[data-pokedit]", function () {
    var pokeditData = $('#pokedit').data('pokedit');
    Swal.fire({
        title: 'Mensaje: ' + pokeditData,
        text: 'El Pokémon ha sido editado correctamente.',
        icon: 'success',
        confirmButtonText: 'Aceptar'
    });
});

$("body").on("click", "[data-agregar-pkm]", function () {
    var card = $(this).closest('.card-body');
    var pokemonData = {
        name: card.find('.card-title').data('nombre') || '',
        id: card.find('.card-text[data-id]').data('id') || 0,
        weight: card.find('.card-text[data-weight]').data('weight') || 0,
        types: card.find('.card-text[data-types]').data('types')
    };

    $.ajax({
        url: '/Pokedex/AgregarPokemon',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify({ pokemon: pokemonData }),
        success: function () {
            alert('¡Pokémon agregado a la Pokédex!');
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error: ' + error);
        }
    });
});

$(document).ready(function () {
    var container = document.querySelector('.container[data-editsuccess]');
    var isSuccess = container.getAttribute('data-editsuccess') === 'True';

    if (isSuccess) {
        Swal.fire({
            icon: 'success',
            title: 'Registro editado',
            text: 'El Pokémon ha sido editado correctamente.',
            confirmButtonText: 'Aceptar'
        });
    }
});

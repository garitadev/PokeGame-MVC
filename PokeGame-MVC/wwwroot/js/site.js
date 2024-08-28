// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*$("body").on("input", "[data-buscar-pokedex]", function () {
    console.log("sdfsfdsf")
   
});*/
$("body").on("input", "[data-buscar-pokedex]", function () {
    console.log("sdfsfdsf")

    var filter = $(this).val().toLowerCase(); // Obtiene el valor del input y lo convierte a minúsculas
    var cards = $("#pokemonList .card"); // Selecciona todas las tarjetas de Pokémon

    cards.each(function () {
        var cardName = $(this).find('[data-nombre]').data('nombre').toLowerCase(); // Obtiene el nombre del Pokémon desde el atributo data-nombre
        $(this).toggle(cardName.includes(filter)); // Muestra u oculta la tarjeta según si coincide con el filtro
    });
});

$("body").on("click", "[data-usuariodelete]", function ()
{
    var usuariodata = $(this).data('usuariodelete');

    Swal.fire({
        title: "Estás seguro de eliminar este usuario?",
        text: "El usuario será eliminado!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Eliminar!"
    }).then((result) => {


        if (result.isConfirmed) {
            $.ajax({
                url: '/Sistema/Eliminar',
                type: 'POST',
                data: { id: usuariodata },
                success: function () {
                    Swal.fire({
                        title: "Elimnado!",
                        text: "El usuario ha sido eliminado.",
                        icon: "success"
                    });
                },
                error: function (xhr, status, error) {
                    alert('Ocurrió un error: ' + error);
                }
            });
        }
    });

});
$("body").on("click", "[data-pokedelete]", function ()
{
    //var pokeditData = $('#pokedelete').data('pokedelete');
    var pokeditData = $(this).data('pokedelete');
    console.log(pokeditData)
    Swal.fire({
        title: "Estás seguro de eliminar este pokemon?",
        text: "El pokemon se eliminará de la pokedex!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Eliminar!"
    }).then((result) => {

        
        if (result.isConfirmed) {
            $.ajax({
                url: '/Pokedex/Eliminar',
                type: 'POST',
                data: { id: pokeditData },
                success: function () {
                    Swal.fire({
                        title: "Elimnado!",
                        text: "El pokemon ha sido eliminado.",
                        icon: "success"
                    });
                },
                error: function (xhr, status, error) {
                    alert('Ocurrió un error: ' + error);
                }
            });
        }
    });

});


$("body").on("click", "[data-pokedit]", function () {
    var pokeditData = $('#pokedit').data('pokedit');
    Swal.fire({
        title: 'Mensaje: ' + pokeditData,
        text: 'El Pokémon ha sido editado correctamente.',
        icon: 'success',
        confirmButtonText: 'Aceptar'
    });
});


$("body").on("click", "[data-agregar-equipo]", function () {

    var pokedexId = $(this).closest('.card').find('.pokedex-id').val();
    console.log(pokedexId)
    
    //return
    $.ajax({
        url: '/Entrenador/AgregarPokemon',
        type: 'POST',
        data: { id: pokedexId },
        success: function () {
            Swal.fire({
                icon: 'success',
                title: 'Pokemon Agregado',
                text: 'El Pokémon ha sido agregado correctamente.',
                confirmButtonText: 'Aceptar'
            });
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error: ' + error);
        }
    });
})

$("body").on("click", "[data-agregar-pkm]", function () {
    var card = $(this).closest('.card-body');
    var pokemonData = {
        name: card.find('.card-title').data('nombre') || '',
        id: card.find('.card-text[data-id]').data('id') || 0,
        weight: card.find('.card-text[data-weight]').data('weight') || 0,
        types: card.find('.card-text[data-types]').data('types')
    };
    console.log(pokemonData)
    //return
    $.ajax({
        url: '/Pokedex/AgregarPokemon',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify( pokemonData ),
        success: function () {
            Swal.fire({
                icon: 'success',
                title: 'Pokemon Agregado',
                text: 'El Pokémon ha sido agregado correctamente.',
                confirmButtonText: 'Aceptar'
            });
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error: ' + error);
        }
    });
});

$(document).ready(function () {
    var container = document.querySelector('.container[data-editsuccess]');

    //EDITAR
    if (container != null) {
        var isSuccess = container.getAttribute('data-editsuccess') === 'True';

        if (isSuccess) {
            Swal.fire({
                icon: 'success',
                title: 'Registro editado',
                text: 'El registro ha sido editado correctamente.',
                confirmButtonText: 'Aceptar'
            });
        }
    }
    
});

$("body").on("click", "[data-enfermeria-equipo]", function () {
    // Obtén el valor del atributo data-enfermeria-equipo
    var pokemonId = $(this).data("enfermeria-equipo");
    var usuarioId = $(this).data("usuario-id");
    console.log(usuarioId)
    
    $.ajax({
        url: '/Enfermeria/EnviarAPokemonaEnfermeria',
        type: 'POST',
        data: {
            pokemonId: pokemonId,
            usuarioId: usuarioId
        }, 
        success: function () {
            Swal.fire({
                icon: 'success',
                title: 'Pokemon enviado',
                text: 'El Pokémon ha sido enviado a la enfermeria.',
                confirmButtonText: 'Aceptar'
            });
        },
        error: function (xhr, status, error) {
            alert('Ocurrió un error: ' + error);
        }
    });
    // Imprime el valor en la consola (puedes hacer lo que necesites con él)
    console.log(valor);
});


//
$("body").on("input", "[data-buscar-mensaje]", function () {
    console.log("dddd")
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById('searchInput');
    filter = input.value.toUpperCase();
    table = document.getElementById('mensajesTable');
    tr = table.getElementsByTagName('tr');

    for (i = 1; i < tr.length; i++) {  // Empieza en 1 para saltar el encabezado
        td = tr[i].getElementsByTagName('td')[0]; // Filtra por la primera columna (destinatario)
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
});

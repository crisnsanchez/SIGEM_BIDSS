//JS General para Pantallas Create y Edit

//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function () {
        _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });


})

//Funcion de Solo letras en el textbox
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}


function soloLetrastIPOSANGRE(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '+-áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

function noespaciosincio(e) {
    var valor = e.value.replace(/^ */, '');
    e.value = valor;
}

$('#are_Descripcion').change(function () {
    $('#areId').hide();
});
$('#est_Descripcion').change(function () {
    $('#estId').hide();
});
$('#tmo_Nombre').change(function () {
    $('#NombreMonedaId').hide();
});

$('#tmo_Abreviatura').change(function () {
    $('#abreviatura').hide();
});
$('#tpsal_Descripcion').change(function () {
    $('#salario').hide();
});
$('#tptran_Descripcion').change(function () {
    $('#Transporte').hide();
});
$('#uni_Abreviatura').change(function () {
    $('#Unidad').hide();
});
$('#tptran_Descripcion').change(function () {
    $('#Medida').hide();
});

$('#tpv_Descripcion').change(function () {
    $('#Viatico').hide();
});

//Mostrar Mensaje:Campo requerido en tiempo real
$("#tps_Descripcion").change(function () {
    var tps_Id= $("#tps_Descripcion").val();
    if (tps_Id != '') {
        $('#tps_IdRequered').text('');
    }
    else {
        $('#tps_IdRequered').after('<p id="tps_IdRequered" style="color:red">Campo Descripción requerido</p>');
    }
});

//borrar mensajes en tiempo real
$("#tps_Descripcion").keyup(function () {
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered').text('');
    $('#tps_IdRequered  ').text('');
});
﻿function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#CargarFoto").change(function () {
    readURL(this);
});


//Solo numeros
$('#par_TelefonoEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9\-]+$/.test(tecla);

});


//Validar que se borre mensaje mientras se escribe
$('#par_NombreEmpresa').change(function () {
    $('#errornombreempresa').hide();
});

$('#par_TelefonoEmpresa').change(function () {
    $('#errortelefonoempresa').hide();
    $('#errorformatotelefono').hide();

});

$('#par_CorreoEmpresa').change(function () {
    $('#errorcorreoempresa').hide();
});

$('#par_Emisor').change(function () {
    $('#erroremisor').hide();
});

$('#par_Password').change(function () {
    $('#errorpassword').hide();
});

$('#par_Mensaje').change(function () {
    $('#errorMensaje').hide();
});

$('#par_Asunto').change(function () {
    $('#errorAsunto').hide();
});

$('#par_Destinatario').change(function () {
    $('#errorDestinatario').hide();
});

$('#par_Servidor').change(function () {
    $('#errorServidor').hide();
});

$('#par_Puerto').change(function () {
    $('#errorPuerto').hide();
});



$('#CargarFoto').change(function () {
    $('#errorlogo').hide();
});

//Numero de telefono
function validartel(e) {
    campo = event.target;
    $(campo).on("input", function (event) {
        var Telefono = this.value.match(/[0-9\s]+/);

        if (Telefono != null) {
            this.value = '+' + ((Telefono).toString().replace(/[^ 0-9a-záéíóúñ@._-\s]\d +/ig, ""));
        }
        else {
            this.value = null;
        }
    });
}

//campos especiales
$('#par_NombreEmpresa').keypress(function (e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }
    // Patron de entrada, en este caso solo acepta numeros y letras
    patron = /[A-Za-z0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
})


//Correo electronico
function Caracteres_Email(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ1234567890@.-_]+$/.test(tecla);

}

function CorreoElectronico(string) {//Algunos caracteres especiales para el correo
    var out = '';
    //Se añaden las letras validas
    var filtro = 'abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890@ .-_';//Caracteres validos

    for (var i = 0; i < string.length; i++)
        if (filtro.indexOf(string.charAt(i)) != -1)
            out += string.charAt(i);

    return out;
}

//Validar Correo Electronico
$('#par_CorreoEmpresa').change(function (e) {
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var EmailId = this.value;
    correo = $("#par_CorreoEmpresa").val();

    if (emailRegex.test(EmailId)) {
        $('#errorcorreo').text('');
        this.style.backgroundColor = "";
        console.log("hola1");
    }

    else {
        if (correo != "") {
            console.log("hola2");
            valido = document.getElementById('errorcorreo');
            valido.innerText = "Dirección de Correo Electrónico Incorrecto";
            return false

        }
    }


});
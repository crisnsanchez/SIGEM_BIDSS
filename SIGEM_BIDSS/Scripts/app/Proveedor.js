document.getElementById('prov_Email').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailEoK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#prov_Email').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#prov_Email').focus();
        $('#prov_Email').addClass('is-invalid');
    }
});
////TELEFONO
$('#prov_Telefono').inputmask('(999) 9999-9999')

$(document).ready(function () {
    bsCustomFileInput.init();
});



//////SOLO NUMEROS
$("#prov_RTN").on("keypress keyup blur", function (event) {

    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});


///////VALIDACION DE RTN
$('#prov_RTN').change(function (e) {
    var RTN = $.trim(this.value);
    if (RTN != '') {
        $('#RTN').text('');
        $('#errorRTN').text('');
    }
    $('#errorRTN').text('');
    var RTN = $("#prov_RTN").val();
    $("#errorRTN").remove();
    var length = $("#prov_RTN").val().length;
    if (length < 14) {
        $('#errorRTN').text('');
        $('#validationRTN').after('<ul id="errorRTN" class="validation-summary-errors text-danger">RTN debe tener 14 dígitos</ul>');
        $("#prov_RTN").focus();
    }
    else
        $('#errorRTN').text('');
});

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
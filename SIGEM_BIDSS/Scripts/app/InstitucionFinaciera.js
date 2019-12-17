
document.getElementById('insf_Correo').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailEoK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#insf_Correo').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#insf_Correo').focus();
        $('#insf_Correo').addClass('is-invalid');
    }
});



$('#insf_Nombre').change(function () {
    $('#Nombres').hide();
});

$('#insf_Contacto').change(function () {
    $('#Contacto').hide();
});
$('#insf_Telefono').change(function () {
    $('#Telefono').hide();
});
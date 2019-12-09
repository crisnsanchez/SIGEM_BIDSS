//JS General para Pantallas Create y Edit


document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function () {
        _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });


})

function NoSpace(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '/";
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




//Funcion de Solo letras en el textbox
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '/áéíóúabcdefghijklmnñopqrstuvwxyz";
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

//Funcion no aceptar espacios en el textbox
function noespaciosincio(e) {
    var valor = e.value.replace(/^ */, '');
    e.value = valor;
}


//Eliminar espacios al principio y al final de la cadena de texto en el textbox
document.getElementById("Save").onclick = function () {
    var txtObj = document.getElementById("");
    txtObj.value = txtObj.value.replace(/^\s+/, ""); //Left trim
    txtObj.value = txtObj.value.replace(/\s+$/, ""); //Right trim
};



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
﻿//JS General para Pantallas Create y Edit


//Funcion de Solo letras en el textbox
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "$# '/áéíóúabcdefghijklmnñopqrstuvwxyz";
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
    var txtObj = document.getElementById("des");
    txtObj.value = txtObj.value.replace(/^\s+/, ""); //Left trim
    txtObj.value = txtObj.value.replace(/\s+$/, ""); //Right trim
};
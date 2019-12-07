﻿//JS General para Pantallas Create y Edit

$(function () {
    var vinput = document.getElementsByTagName('input');
    vinput.ondrop = function (e) {
        e.preventDefault();
        //alert("esta acción está prohibida");
    }
    getElementsByTagName('input').each(function (item) {
        item.ondrop = function (e) {
            e.preventDefault();
            //alert("esta acción está prohibida");
        }
    });
})


//-----------Bloquear Ctrl + C y Ctrl + V
$('input').bind('keydown', function (event) {
    if (event.ctrlKey || event.metaKey) {
        switch (String.fromCharCode(event.which).toLowerCase()) {
            case 'c':
                event.preventDefault();
                console.log("Ctrl + C")
                break;
            case 'v':
                console.log("Ctrl + V")

                event.preventDefault();
                break;
        }
    }
})






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


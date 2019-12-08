//JS General para Pantallas Create y Edit



//-----------Bloquear Ctrl + C y Ctrl + V
//$('input').bind('keydown', function (event) {
//        if ($('#txtrut').val().length == 0) {
//            alert('Ingrese rut');
//            return false;
//        }
//    }
//if (event.ctrlKey || event.metaKey) {
//    switch (String.fromCharCode(event.which).toLowerCase()) {
//        case 'c':
//            event.preventDefault();
//            console.log("Ctrl + C")
//            break;
//        case 'v':
//            console.log("Ctrl + V")

//            event.preventDefault();
//            break;
//    }
//}
//)
document.addEventListener("input", function () {
    //console.log($("input").attr("id"));
    $("input[type=text]", 'form').each(function () {
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


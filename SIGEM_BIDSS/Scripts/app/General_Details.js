//JS General para pantallas Details
$(function () {
    //Inicializacion de la funcion
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });


    //Mensaje que mostrar segun funcion
    _Action = $('#vSwal').val();
    console.log(_Action)
    if (_Action == "Revisada") {
        Toast.fire({
            type: 'success',
            title: 'Solicitud Revisada.'
        })
    }
    else if (_Action == "Aceptada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha aprobado la solicitud.'
        })
    } else if (_Action == "Rechazada") {
        Toast.fire({
            type: 'error',
            title: 'Se ha rechazado la solicitud.'
        })
    }
})


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("textarea", 'body').each(function (e) {
        var _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        _value = _value.trimStart();

    });
    $(".normalize", 'body').each(function (e) {
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    });


});
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
    if (_Action == "Revisada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha actualizado el estado con Exito.'
        })
    }
    else if (_Action == "Edited") {
        Toast.fire({
            type: 'success',
            title: 'Se ha actualizado con Exito.'
        })
    } else {
    }
})

////Approve
$(document).on("click", "#_ModalApprove", function () {
    SendDataApprove();
});


function SendDataApprove() {
    var Ansal_Id = $('#Ansal_Id').val()
    $.ajax({
        url: "/AnticipoSalario/Approve",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: Ansal_Id}),
    })
        .done(function (data) {
            location.reload()
        });

}


////Reject
$(document).on("click", "#_ModalReject", function () {
    SendData();
});


function SendData() {
    var Ansal_Id = $('#Ansal_Id').val(),
        RazonInactivacion = $('#RazonInactivacion').val();
    $.ajax({
        url: "/AnticipoSalario/Reject",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: Ansal_Id, Ansal_RazonRechazo: RazonInactivacion }),
    })
        .done(function (data) {
            location.reload()
        });

}

//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("textarea", 'body').each(function (e) {
        _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();

    });
    $(".normalize", 'body').each(function (e) {
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    });
});
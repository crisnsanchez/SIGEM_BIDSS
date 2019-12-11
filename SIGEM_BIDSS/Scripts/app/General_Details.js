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
            title: 'Se ha guardado con Exito.'
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
﻿$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})


$(document).on("click", "#_ModalApprove", function () {
    SendData(_idPrimary = "Ansal_Id", _controller = "AnticipoSalario", _action = "Approve", _spinnerbody = "spinner-body", _spinner = "spinnerd");
});










//---------------------Rechazar-----------------------------------------
$(document).on("click", "#_ModalReject", function () {
    var _spanRR = document.getElementById("RazonRechazo")
    console.log(_spanRR.value)

    //    
    if (_spanRR.value == "" || _spanRR.value == null) {
        document.getElementById("spanRazonRechazo").innerText = "Razon de Rechazo Requerida";
    }
    else {
        document.getElementById("spanRazonRechazo").innerText = "";
        SendData(_idPrimary = "Ansal_Id", _controller = "AnticipoSalario", _action = "Reject", _spinnerbody = "spinner-body-reject", _spinner = "spinnerd-reject");
    }
});








function SendData(_idPrimary, _controller, _action, _spinnerbody, _spinner) {
    var _Id = $("#" + _idPrimary + "").val(),
        RazonInactivacion = $('#RazonRechazo').val();
    var _url = "/" + _controller + "/" + _action;

    document.getElementById("" + _spinnerbody + "").classList.add("overlay");
    document.getElementById("" + _spinner + "").removeAttribute("hidden");

    $.ajax({
        url: _url,
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: _Id, RazonRechazo: RazonInactivacion }),
    }).done(function (data) {
        var str = data.toString();
        if (!str.startsWith("-1")) {
            console.log(str + " || " + data);
            location.reload();
        }
        else {
            console.log("false || " + str);
            Toast.fire({
                type: 'error',
                title: 'Error al actualizar el estado.'
            });
        }
    }).fail(function (xhr, a, error) {
        console.log("error", error);
        Toast.fire({
            type: 'error',
            title: 'Error al actualizar el estado.'
        });
    }


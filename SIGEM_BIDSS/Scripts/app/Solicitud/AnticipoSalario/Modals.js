//JS de Anticipo de Salario(Index)
//Details Anticipo Salario
$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})


$(document).on("click", "#Approve", function () {
    idItem = $('#Ansal_Id').val();
});
$(document).on("click", "#Reject", function () {
    idItem = $('#Ansal_Id').val();
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

$(document).on("click", "#_ModalApprove", function () {
    SendData(_idPrimary = "Ansal_Id", _controller = "AnticipoSalario", _action = "Approve", _spinnerbody = "spinner-body", _spinner = "spinnerd");
});


function SendData(_idPrimary, _controller, _action, _spinnerbody, _spinner) {
    var _Id = $("#" + _idPrimary + "").val(),
        RazonInactivacion = $('#RazonRechazo').val();
    var _url = "/" + _controller + "/" + _action;
    console.log(" || _idPrimary: " + _idPrimary + " || _controller: " + _controller + " || _Id: " + _Id + " || _action: " + _action + " || _url: " + _url + " || _spinnerbody: " + _spinnerbody + " || _spinner: " + _spinner);

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
        }
    }).fail(function (xhr, a, error) {
        console.log("error", error);
        Toast.fire({
            type: 'error',
            title: 'Error al actualizar el estado.'
        });
    }




//<---------------------/Rechazar/----------------------------------------->//






//---------------------Approve-----------------------------------------


function Approve() {
            var Ansal_Id = $('#Ansal_Id').val();
            document.getElementById('spinner-body').classList.add("overlay");
            document.getElementById('spinnerd').removeAttribute("hidden");
            $.ajax({
                url: "/AnticipoSalario/Approve",
                method: "POST",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: Ansal_Id }),
            }).done(function (data) {
                var str = data.toString();
                if (!str.startsWith("-1")) {
                    console.log(str + " || " + data);
                    location.reload();
                }
                else {
                    console.log("false || " + str);
                }
            });
        }

const _id = document.getElementById('RazonRechazo');
    _id.addEventListener("input", function () {
        _id.value.trimStart();
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_id.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    })
//<---------------------/Approve/----------------------------------------->//









//function UpdateStateDetails(Ansal_Id, Accion) {
//    $(Accion).modal('show');
//    //Inicializacion de la funcion
//    if (Accion == "#ModalReject") {
//        document.getElementById('spinner-body-reject').classList.remove("overlay");
//        document.getElementById('spinnerd-reject').remove();
//        document.getElementById("divArea").style.display = "block";
//    }
//    else if (Accion == "#ModalApprove") {
//        document.getElementById('spinner-body').classList.remove("overlay");
//        document.getElementById('spinnerd').remove();
//    }
//}




//$(document).on("click", "#dataTable tbody tr td input#Reject", function () {
//    idItem = $(this).closest('tr').data('id');
//    $('#Ansal_Id').val(idItem);
//    UpdateState(idItem, '#ModalReject');
//    document.getElementById("divArea").style.display = "none";

//})


//$(document).on("click", "#dataTable tbody tr td input#Approve", function () {
//    idItem = $(this).closest('tr').data('id');
//    $('#Ansal_Id').val(idItem);
//    UpdateState(idItem, '#ModalApprove');
//})


//function UpdateState(Ansal_Id, Accion) {
//    $(Accion).modal('show');
//    $.ajax({
//        url: "/AnticipoSalario/Revisar",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ id: Ansal_Id }),
//    }).done(function (data) {
//        //Inicializacion de la funcion
//        const Toast = Swal.mixin({
//            toast: true,
//            position: 'top-end',
//            showConfirmButton: false,
//            timer: 3000
//        });
//        if (data == "true") {
//            if (Accion == "#ModalReject") {
//                document.getElementById('spinner-body-reject').classList.remove("overlay");
//                document.getElementById('spinnerd-reject').remove();
//                document.getElementById("divArea").style.display = "block";
//            }
//            else if (Accion == "#ModalApprove") {
//                document.getElementById('spinner-body').classList.remove("overlay");
//                document.getElementById('spinnerd').remove();
//            }
//        }
//        else if (data == "false") {
//            if (Accion == "#ModalReject") {
//                $("#ModalReject").modal('hide')
//                Toast.fire({
//                    type: 'error',
//                    title: 'Error al actualizar el estado.'
//                })
//            }
//            else if (Accion == "#ModalApprove") {
//                $("#ModalApprove").modal('hide')
//                Toast.fire({
//                    type: 'error',
//                    title: 'Error al actualizar el estado.'
//                })
//            }
//        }
//    });
//}

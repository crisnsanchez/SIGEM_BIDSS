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
        SendData();
    }
});


function SendData() {
    var Ansal_Id = $('#Ansal_Id').val(),
        RazonInactivacion = $('#RazonRechazo').val();
    document.getElementById('spinner-body-reject').classList.add("overlay");
    document.getElementById('spinnerd-reject').removeAttribute("hidden");
    //document.getElementById("divArea").style.display = "block";
    $.ajax({
        url: "/AnticipoSalario/Reject",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: Ansal_Id, Ansal_RazonRechazo: RazonInactivacion }),
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
//<---------------------/Rechazar/----------------------------------------->//






//---------------------Approve-----------------------------------------

$(document).on("click", "#_ModalApprove", function () {
    Approve();
});


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

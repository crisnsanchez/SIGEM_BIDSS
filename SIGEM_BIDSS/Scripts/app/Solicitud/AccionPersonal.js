$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})

//--Date Picker--//
//$('#Acp_FechaSolicitud,').datepicker({
//    format: "dd/mm/yyyy",
//    startDate: "01/01/1990",
//    language: "es",
//    daysOfWeekDisabled: "0"
//});
//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
 
    $("input[type='text']", 'form').each(function () {
        _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });

    $("textarea").each(function () {
        _id = $(this).attr("id");
        console.log("Hi:", _id);
    });

});



//Get Current Date
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;
    return currentDate;
}
//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Acp_JefeInmediato").change(function () {
    var JefeInmediato = $("#Acp_JefeInmediato").val();
    if (JefeInmediato !== "") {
        $("#JefeInmediato").text("");
    }
});


$("#tipmo_id").change(function () {
    var CodigoMun = $("#tipmo_id").val();
    if (CodigoMun !== "") {
        $("#CodigoMun").text(""
        );
    }
});

$("#Anvi_tptran_Id").change(function () {
    var tptran_Id = $("#Anvi_tptran_Id").val();
    if (tptran_Id !== "") {
        $("#tptran_Id").text("");
    }
});

//Textbox
$("#Acp_Comentario").change(function () {
    var Cliente = $("#Acp_Comentario").val();
    if (Cliente !== "") {
        $("#Cliente").text("");
    }
});






$(document).on("click", "#Approve", function () {
    idItem = $('#Acp_Id').val();
});
$(document).on("click", "#Reject", function () {
    idItem = $('#Acp_Id').val();
});






//---------------------Rechazar-----------------------------------------
$(document).on("click", "#_ModalReject", function () {
    $('#Acp_FechaSolicitud').val(GetTodayDate());
    var _spanRR = document.getElementById("RazonRechazo")
    console.log(_spanRR.value)

    //    
    if (_spanRR.value == "" || _spanRR.value == null) {
        document.getElementById("spanRazonRechazo").innerText = "Razon de Rechazo Requerida";
    }
    else {
        document.getElementById("spanRazonRechazo").innerText = "";
        SendData(_idPrimary = "Acp_Id", _controller = "AccionPersonal", _action = "Reject", _spinnerbody = "spinner-body-reject", _spinner = "spinnerd-reject");
    }
});

$(document).on("click", "#_ModalApprove", function () {
    SendData(_idPrimary = "Acp_Id", _controller = "AccionPersonal", _action = "Approve", _spinnerbody = "spinner-body", _spinner = "spinnerd");
});

const _id = document.getElementById('RazonRechazo');
_id.addEventListener("input", function () {
    this.value = this.value.trimStart()
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_id.value)) {
        this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
    }
})

$("#RazonRechazo").change(function () {
    var RazonRechazo = $("#RazonRechazo").val();
    if (RazonRechazo !== "") {
        $("#spanRazonRechazo").text("");
    }
});





function SendData(_idPrimary, _controller, _action, _spinnerbody, _spinner) {
    var _Id = $("#" + _idPrimary + "").val(),
        RazonRechazo = $('#RazonRechazo').val();
    var _url = "/" + _controller + "/" + _action;
    console.log(" || _idPrimary: " + _idPrimary + " || _controller: " + _controller + " || _Id: " + _Id + " || _action: " + _action + " || _url: " + _url + " || _spinnerbody: " + _spinnerbody + " || _spinner: " + _spinner);

    document.getElementById("" + _spinnerbody + "").classList.add("overlay");
    document.getElementById("" + _spinner + "").removeAttribute("hidden");

    $.ajax({
        url: _url,
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: _Id, RazonRechazo: RazonRechazo }),
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
        })
    })
}









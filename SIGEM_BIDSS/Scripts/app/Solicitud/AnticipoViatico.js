var Formulario=[];
var token;
var Anvi_JefeInmediato;
var Anvi_GralFechaSolicitud;
var Anvi_Cliente;
var Anvi_FechaViaje;
var dep_codigo;
var mun_Codigo;
var Anvi_PropositoVisita;
var Anvi_DiasVisita;
var Anvi_tptran_Id;

$(document).ready(function () {
    $("#Anvi_Cliente")[0].maxLength = 100;
    $('#Anvi_GralFechaSolicitud').val(GetTodayDate());
    var CodDepartamento = $('#dep_codigo').val();
    var muncod = $('#munCodigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/AnticipoViatico/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                    $.each(data, function (key, val) {
                        if (muncod !== "") {
                            if (val.mun_codigo === muncod) {
                                $('#mun_Codigo').append("<option selected='selected' value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                            }
                            else
                                $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                        }
                        else
                            $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });
                    $('#mun_Codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                }
            });
    }
});

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


//

$("#btnAutorizacion").click(function () {
    Formulario={
        Anvi_JefeInmediato: Anvi_JefeInmediato,
        Anvi_GralFechaSolicitud: Anvi_GralFechaSolicitud,
        Anvi_Cliente: Anvi_Cliente,
        Anvi_FechaViaje: Anvi_FechaViaje,
        mun_Codigo: mun_Codigo,
        Anvi_PropositoVisita: Anvi_PropositoVisita,
        Anvi_DiasVisita: Anvi_DiasVisita,
        Anvi_tptran_Id: Anvi_tptran_Id
    };

    $.ajax({
        url: "/AnticipoViatico/Create",
        method: "POST",
        dataType: 'json',
        data: { __RequestVerificationToken: token, tbAnticipoViatico: Formulario, dep_codigo: "ajax" }
    })
        .done(function (data) {
            if (data === "Index") {
                $("#AutorizacionModal").modal("hide");
                window.location.href = "/AnticipoViatico/Index";
            }
            else {
                Anvi_JefeInmediato = $("#Anvi_JefeInmediato").val();
                $("#AutorizacionModal").modal("hide");
                //Anvi_GralFechaSolicitud = $("#Anvi_GralFechaSolicitud").val();
                //Anvi_Cliente = $("#Anvi_Cliente").val();
                //Anvi_FechaViaje = $("#Anvi_FechaViaje").val();
                //dep_codigo = $("#dep_codigo").val();
                //mun_Codigo = $("#mun_Codigo").val();
                //Anvi_PropositoVisita = $("#Anvi_PropositoVisita").val();
                //Anvi_DiasVisita = $("#Anvi_DiasVisita").val();
                //Anvi_tptran_Id = $("#Anvi_tptran_Id").val();
            }
        }).fail(function (xhr, a, error) {
            console.log("error",error);
        });
});

//--Date Picker--//
$('#Anvi_GralFechaSolicitud,#Anvi_FechaViaje').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});

$(document).on("change", "#dep_codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_codigo').val();
    if (CodDepartamento !== "") {
        $.ajax({
            url: "/AnticipoViatico/GetMunicipios",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CodDepartamento: CodDepartamento })
        })
            .done(function (data) {
                if (data.length > 0) {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                    $.each(data, function (key, val) {
                        $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                    });

                    $('#mun_Codigo').trigger("chosen:updated");
                }
                else {
                    $('#mun_Codigo').empty();
                    $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                }
            });
    }
    else {
        $('#mun_Codigo').empty();
        $('#mun_Codigo').append("<option value=''>Seleccione</option>");
    }
}

//Get Current Date
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;
    return currentDate;
}

//Prevent Submit
$("#frmAnticipoViatico").submit(function (event) {    
    token = $('[name=__RequestVerificationToken]').val();
    Anvi_JefeInmediato = $("#Anvi_JefeInmediato").val();
    Anvi_GralFechaSolicitud = $("#Anvi_GralFechaSolicitud").val();
    Anvi_Cliente = $("#Anvi_Cliente").val();
    Anvi_FechaViaje = $("#Anvi_FechaViaje").val();
    dep_codigo = $("#dep_codigo").val();
    mun_Codigo = $("#mun_Codigo").val();
    Anvi_PropositoVisita = $("#Anvi_PropositoVisita").val();
    Anvi_DiasVisita = $("#Anvi_DiasVisita").val();
    Anvi_tptran_Id = $("#Anvi_tptran_Id").val();

    if (Anvi_JefeInmediato !== "" && Anvi_GralFechaSolicitud !== "" && Anvi_Cliente !== "" && Anvi_FechaViaje !== "" && dep_codigo !== "" && mun_Codigo !== "" && Anvi_PropositoVisita !== "" && Anvi_DiasVisita !== "" && Anvi_tptran_Id !== "") {
        event.preventDefault();
        dep_codigo = "ajax";
        $("#AutorizacionModal").modal();
     }
});

//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Anvi_JefeInmediato").change(function () {
    var JefeInmediato = $("#Anvi_JefeInmediato").val();
    if (JefeInmediato !== "") {
        $("#JefeInmediato").text("");
    }
});

$("#dep_codigo").change(function () {
    var UsuarioCrea = $("#dep_codigo").val();
    if (UsuarioCrea !== "") {
        $("#UsuarioCrea").text("");
    }
});

$("#mun_Codigo").change(function () {
    var CodigoMun = $("#mun_Codigo").val();
    if (CodigoMun !== "") {
        $("#CodigoMun").text("");
    }
});

$("#Anvi_tptran_Id").change(function () {
    var tptran_Id = $("#Anvi_tptran_Id").val();
    if (tptran_Id !== "") {
        $("#tptran_Id").text("");
    }
});

//Textbox
$("#Anvi_Cliente").change(function () {
    var Cliente = $("#Anvi_Cliente").val();
    if (Cliente !== "") {
        $("#Cliente").text("");
    }
});


//////////////////////////////////////
$(function () {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false
    });
})


$(document).on("click", "#Approve", function () {
    idItem = $('#Anvi_Id').val();
});
$(document).on("click", "#Reject", function () {
    idItem = $('#Anvi_Id').val();
});

$(document).ready(function () {
    $('#dataTable').DataTable({
        "searching": true,
        "lengthChange": true,
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
        }
    });
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
    var Anvi_Id = $('#Anvi_Id').val(),
        RazonInactivacion = $('#RazonRechazo').val();
    document.getElementById('spinner-body-reject').classList.add("overlay");
    document.getElementById('spinnerd-reject').removeAttribute("hidden");
    //document.getElementById("divArea").style.display = "block";
    $.ajax({
        url: "/AnticipoViatico/Rejects",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: Anvi_Id, Anvi_RazonRechazo: RazonInactivacion }),
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
        url: "/AnticipoViatico/Approve",
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

$('#Anvi_JefeInmediato').change(function () {
    $('#JefeInmediato').hide();
});

$('#Anvi_GralFechaSolicitud').keyup(function () {
    $('#GralFechaSolicitud').hide();
});

$('#Anvi_Cliente').keyup(function () {
    $('#Cliente').hide();
});


$('#Anvi_FechaViaje').keyup(function () {
    $('#FechaViaje').hide();
});


$('#Anvi_UsuarioCrea').change(function () {
    $('#UsuarioCrea').hide();
});

$('#mun_Codigo').change(function () {
    $('#CodigoMun').hide();
});

$('#Anvi_PropositoVisita').keyup(function () {
    $('#PropositoVisita').hide();
});

$('#Anvi_DiasVisita').keyup(function () {
    $('#DiasVisita').hide();
});

$('#Anvi_tptran_Id').change(function () {
    $('#tptran_Id').hide();
});

$('#Anvi_Hospedaje').keyup(function () {
    $('#Hospedaje').hide();
});

$('#Anvi_Comentario').keyup(function () {
    $('#Comentario').hide();
});


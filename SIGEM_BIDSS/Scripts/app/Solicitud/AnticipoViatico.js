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
    console.log("muncod", muncod);
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
//--------------------------------------------------------------------------------//
$("#frmsubmit").click(function () {
    document.getElementById('spinnerForm').classList.add("overlay");
    document.getElementById('spinnerDiv').removeAttribute("hidden");
    $("form").submit()
})

//--Date Picker--//
$('#VPE_GralFechaSolicitud,#VPE_FechaInicio').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());
//--Date Picker--//
$('#VPE_FechaFin').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker();

$(document).ready(function () {
    $("#VPE_Comentario")[0].maxLength = 250;
});


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $(".nospace", 'form').each(function (e) {
        var _id = $(this).attr("id");
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();

    });
    $(".normalize", 'form').each(function (e) {
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    });
});


//Funcion de Solo letras en el textbox
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " '/áéíóúabcdefghijklmnñopqrstuvwxyz";
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


function GetFechas() {
    var Fechas = {
        FechaInicio: document.getElementById("VPE_FechaInicio").value,
        FechaFin: document.getElementById("VPE_FechaFin").value
    };
    return Fechas;
}


document.getElementById("VPE_FechaInicio").addEventListener("blur", function () {
    CalcularFechas()
});

document.getElementById("VPE_FechaFin").addEventListener("blur", function () {
    CalcularFechas()
});

function CalcularFechas() {
    vFechas = GetFechas()
    console.log("JS: " + vFechas);

    $.ajax({
        url: "/VacacionesPermisosEspeciales/CalcularFecha",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalFechas: vFechas }),
    })
        .done(function (data) {
            console.log(data);
                document.getElementById("spanFechaInicio").innerText = data.MASspan
                document.getElementById("spanFechaFin").innerText = data.MASspanFecha
        });
}

////SOLO NUMEROS 
function soloNumeros(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}




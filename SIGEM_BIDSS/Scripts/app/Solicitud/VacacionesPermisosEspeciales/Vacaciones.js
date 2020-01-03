//--------------------------------------------------------------------------------//


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
}).datepicker("setDate", new Date() + 1d);


$(document).ready(function () {
    $("#Ansal_Justificacion")[0].maxLength = 250;
    $("#Ansal_Comentario")[0].maxLength = 250;
});


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function (e) {
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





document.getElementById("Cantidad").onblur = function () {
    if (!isNaN(this.value || this.value != "")) {
        //number-format the user input
        this.value = parseFloat(this.value.replace(/,/g, ""))
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");

        //set the numeric value to a number input
        document.getElementById("number").value = this.value.replace(/,/g, "")
    }
}

function GetDecimales() {
    var Decimales = {
        empSueldo: parseInt(document.getElementById("Sueldo").value).toFixed(2),
        empPorcetanje: parseInt(document.getElementById("Porcentaje").value).toFixed(2),
        empMonto: parseInt(document.getElementById("number").value).toFixed(2)
    };
    return Decimales;
}

var monto = document.getElementById("Cantidad");

monto.addEventListener("input", function () {
    document.getElementById("number").value = this.value.replace(/,/g, "")
    vDecimales = GetDecimales()
    console.log("JS: " + vDecimales);
    $.ajax({
        url: "/AnticipoSalario/Calcular",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalDecimal: vDecimales }),
    })
        .done(function (data) {
            console.log(data);
            document.getElementById("spanCantidad").innerText = data.spanCantidad
        });
});











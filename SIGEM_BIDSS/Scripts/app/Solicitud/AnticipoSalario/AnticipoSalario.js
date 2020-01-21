
const formatter = new Intl.NumberFormat('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
});

$(document).ready(function () {
    $("#Ansal_Justificacion")[0].maxLength = 250;
    $("#Ansal_Comentario")[0].maxLength = 250;
});

$("#frmsubmit").click(function () {
    document.getElementById('spinnerForm').classList.add("overlay");
    document.getElementById('spinnerDiv').removeAttribute("hidden"); 
    document.getElementById('frmAnticipoSalario').submit();
})



document.getElementById("Cantidad").onblur = function () {
    if (this.value != "") {
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
        data: JSON.stringify({ cCalDecimal: vDecimales}),
    })
        .done(function (data) {
            console.log(data);
            document.getElementById("spanCantidad").innerText = data.spanCantidad
        });
});


//--Date Picker--//
$('#Ansal_GralFechaSolicitud').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1900",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());;


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $(".nospace", 'form').each(function (e) {
        var _id = $(this).attr("id");
        var el = document.getElementById('' + _id + '')
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });
    $(".normalize", 'form').each(function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-záéíóúüñ]+/ig, "");
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





//Quitar mensajes de error cuando el valor cambia
//DropdownList
$("#Ansal_JefeInmediato").change(function () {
    var JefeInmediato = $("#Ansal_JefeInmediato").val();
    if (JefeInmediato !== "") {
        $("#JefeInmediato").text("");
    }
});

$("#tpsal_id").change(function () {
    var tpsal_id = $("#tpsal_id").val();
    if (tpsal_id !== "") {
        $("#spantpsal_id").text("");
    }
});

//TextBox
$("#Cantidad").change(function () {
    var Cantidad = $("#Cantidad").val();
    if (Cantidad !== "") {
        $("#spanCantidad").text("");
    }
});

$("#Ansal_Justificacion").change(function () {
    var Justificacion = $("#Ansal_Justificacion").val();
    if (Justificacion !== "") {
        $("#spanJustificacion").text("");
    }
});

$("#Ansal_Comentario").change(function () {
    var Comentario = $("#Ansal_Comentario").val();
    if (Comentario !== "") {
        $("#spanComentario").text("");
    }
});
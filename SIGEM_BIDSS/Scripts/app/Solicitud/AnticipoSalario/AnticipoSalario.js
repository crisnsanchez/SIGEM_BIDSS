


$("#frmsubmit").click(function () {
    $("form").submit()
})

//$("#Cantidad").on({
//    "focus": function (event) {
//        $(event.target).select();
//    },
//    "keyup": function (event) {
//        $(event.target).val(function (index, value) {
//            return value.replace(/\D/g, "")
//                .replace(/([0-9])([0-9]{2})$/, '$1.$2')
//                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
//        });
//    }
//});

var monto = document.getElementById("Cantidad");

monto.addEventListener("input", function () {
    empSueldo = parseFloat(document.getElementById("Sueldo").value);
    empMonto = parseFloat(document.getElementById("Cantidad").value);
    console.log(empSueldo+"  |  "+empMonto);
    if (empMonto > empSueldo) {
        spanCantidad = document.getElementById("spanCantidad").innerText = "Monto solicitado mayor que el Sueldo";
        console.log(spanCantidad);
    } else {
        spanCantidad = document.getElementById("spanCantidad").innerText = "";
    }

});
//document.getElementById("Ansal_MontoSolicitado").onblur = function () {
//    //number-format the user input
//    this.value = parseFloat(this.value.replace(/,/g, ""))
//        .toFixed(2)
//        .toString()
//        .replace(/\B(?=(\d{3})+(?!\d))/g, ",");

//    //set the numeric value to a number input
//    document.getElementById("number").value = this.value.replace(/,/g, "")

//}

//--Date Picker--//
$('#Ansal_GralFechaSolicitud').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0",
}).datepicker("setDate", new Date());;


$(document).ready(function () {
    $("#Ansal_Justificacion")[0].maxLength = 250;
    $("#Ansal_Comentario")[0].maxLength = 250;
});


//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $("input[type='text']", 'form').each(function (e) {
        _id = $(this).attr("id");
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


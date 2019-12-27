
const formatter = new Intl.NumberFormat('en-US', {
    minimumFractionDigits: 2,
    maximumFractionDigits: 2,
});

$("#frmsubmit").click(function () {
    $("form").submit()
})

//$("#Cantidad").on({
//    "focus": function (event) {
//        $(event.target).select();
//    },
//    "blur": function (event) {
//        $(event.target).val(function (index, value) {
//            this.value = parseFloat(this.value.replace(/,/g, ""))
//                                    .toFixed(2)
//                                    .toString()
//                                    .replace(/\B(?=(\d{3})+(?!\d))/g,",");
//        });
//    },
//});

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

//document.getElementById("Ansal_MontoSolicitado").onblur = function () {
//    //number-format the user input
//    this.value = parseFloat(this.value.replace(/,/g, ""))
//        .toFixed(2)
//        .toString()
//        .replace(/\B(?=(\d{3})+(?!\d))/g, ",");

//    //set the numeric value to a number input
//    document.getElementById("number").value = this.value.replace(/,/g, "")

//}

var monto = document.getElementById("Cantidad");

monto.addEventListener("input", function () {

    document.getElementById("number").value = this.value.replace(/,/g, "")

    empSueldo =  parseInt(document.getElementById("Sueldo").value);

    empPorcetanje = parseInt(document.getElementById("Porcentaje").value);

    empMonto = parseInt(document.getElementById("number").value);
 
    if (this.value != "") {

        console.log("Porcentaje: " + empPorcetanje + " - Monto " + empMonto + " - Sueldo " + empSueldo);

        if (empMonto > empSueldo) {
            spanCantidad = document.getElementById("spanCantidad").innerText = "Monto solicitado mayor que el Sueldo";
        } else {
            spanCantidad = document.getElementById("spanCantidad").innerText = "";
        }
        if (empMonto > empPorcetanje) {
            spanCantidad = document.getElementById("spanCantidad").innerText = "El monto no puede ser mayor que el pocentaje permitido";
            
        } else {
            spanCantidad = document.getElementById("spanCantidad").innerText = "";
        }
    }
    else {
        console.log("false");
    }
  
});


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


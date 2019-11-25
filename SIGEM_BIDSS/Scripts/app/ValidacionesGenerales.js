//DATE PICKET
$("#sol_PerFechaInicio,#sol_PerFechaRegreso,#sol_GralFechaSolicitud").datepicker({
    format: "dd/mm/yyyy",
    language: "es",
    daysOfWeekDisabled: "0"
}).datepicker('setDate', new Date());



//////SOLO LETRAS

function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}



////SOLO NUMEROS 
function soloNumeros(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}
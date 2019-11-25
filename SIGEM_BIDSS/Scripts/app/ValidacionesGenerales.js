//DATE PICKET
$("#sol_PerFechaInicio,#sol_PerFechaRegreso,#sol_GralFechaSolicitud").datepicker({
    dateFormat: 'mm/dd/yy',
    monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
    dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
    minDate: '-100Y',
    maxDate: '-18Y',
    prevText: 'Ant',
    nextText: 'Sig',
    changeMonth: true,
    changeYear: true,
    monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic']
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

///Validacion para no pegar caracteres
function Caracteres(string) {
    var out = '';
}


/////DATE PICKER
$("#emp_FechaNacimiento,#emp_FechaIngreso").datepicker({
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




////max lengh

$(document).ready(function () {
    $("#emp_Nombres")[0].maxLength = 50;
    $("#emp_Apellidos")[0].maxLength = 50;
    $("#emp_Identificacion")[0].maxLength = 13;
    $("#emp_CorreoElectronico")[0].maxLength = 100;
    //$("#emp_Telefono")[0].maxLength = 8;
    $("#emp_Correo").attr("autocomplete", "randomString");
    $("#ban_NombreContacto").attr("autocomplete", "randomString");
})
    

////validar correo
function ValidarCorreo(valor) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3,4})+$/.test(valor)) {
        alert("La dirección de email " + valor + " es correcta.");
    } else {
        alert("La dirección de email es incorrecta.");
        //$('#emp_CorreoElectronico').val("");
        //$('#emp_CorreoElectronico').focus();
    }
}

////imagen
function readURL(input) {



    if (input.files && input.files[0]) {
        var reader = new FileReader();



        reader.onload = function (e) {
            $('#imgpreview').attr('src', e.target.result);
        }



        reader.readAsDataURL(input.files[0]);
    }
}



$("#CargarFoto").change(function () {
    readURL(this);
});


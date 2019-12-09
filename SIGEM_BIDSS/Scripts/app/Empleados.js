﻿

/////DATE PICKER
$('#emp_FechaNacimiento,#emp_FechaIngreso').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});



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




////MAX LENGTH DE LOS CAMPOS

$(document).ready(function () {
    $("#emp_Nombres")[0].maxLength = 50;
    $("#emp_Apellidos")[0].maxLength = 50;
    $("#emp_Identificacion")[0].maxLength = 25;
    $("#emp_CorreoElectronico")[0].maxLength = 100;
    //$("#emp_Telefono")[0].maxLength = 8;
    $("#emp_Correo").attr("autocomplete", "randomString");
    $("#ban_NombreContacto").attr("autocomplete", "randomString");





    $('#municipio').hide();


    var muni = $('#municipios').val();
    if (muni == "true") { GetMunicipios() } else { console.log(muni); }





    $('#municipio').hide();


    var are = $('#municipios').val();
    if (are == "true") { GetMunicipios() } else { console.log(are); }

})
    



//document.getElementById('emp_CorreoElectronico').addEventListener('input', function () {
//    campo = event.target;
//    valido = document.getElementById('emailOK');
//    //selector = document.getElementById('emp_CorreoElectronico')
//    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
//    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
//    if (emailRegex.test(campo.value)) {
//        valido.innerText = "";
//        $('#emp_CorreoElectronico').removeClass('is-invalid');
//    } else {
//        valido.innerText = "Correo Inválido";
//        $('#emp_CorreoElectronico').focus();
//        $('#emp_CorreoElectronico').addClass('is-invalid');
//    }
//});






////IMAGEN REFRES E INSERT
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

////OBTENER MUNICIPIO
$(document).on("change", "#dep_Codigo", function () {
    GetMunicipios();
});




function GetMunicipios() {

  
    var CodDepartamento = $('#dep_Codigo').val(),
        _selectedMun = $('#selectedMun').val();
    console.log(CodDepartamento)
    $.ajax({
        url: "/Empleado/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDepartamento: CodDepartamento }),
    })
        .done(function (data) {
            $('#municipio').show();
            if (data.length > 0) {
                $('#mun_codigo').empty();
                $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
                $.each(data, function (key, val) {
                    $('#mun_codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                });
                console.log(data)
                $('#mun_codigo').trigger("chosen:updated");
                if (_selectedMun != null || _selectedMun != "") {
                    $("#mun_codigo option[value='" + _selectedMun + "']").attr("selected", true);
                } else { console.log("No es") }
            }
            else {
                $('#mun_codigo').empty();
                $('#mun_codigo').append("<option value=''>Seleccione Municipio</option>");
            }
        });



  
    

       


   
}


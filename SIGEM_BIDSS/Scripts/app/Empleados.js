/////DATE PICKER
$('#emp_FechaNacimiento,#emp_FechaIngreso').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});



function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}


////SOLO NUMEROS 
function soloNumeros(e) {

        tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla);
        return /^[0-9]+$/.test(tecla);
}




////MAX LENGTH DE LOS CAMPOS

$(document).ready(function () {
    $("#emp_Nombres")[0].maxLength = 50;
    $("#emp_Apellidos")[0].maxLength = 50;
    $("#emp_Identificacion")[0].maxLength = 25;
    $("#emp_CorreoElectronico")[0].maxLength = 100;
   
    $("#emp_Correo").attr("autocomplete", "randomString");



    var selectedPuesto = $('#selectedPuesto').val();
    if (selectedPuesto !== "" || selectedPuesto !== null)  { Getpuesto(); } else { console.log(selectedPuesto);  }

    console.log(selectedPuesto);
    var muni = $('#municipios').val();
    if (muni === "true") { GetMunicipios(); } else { console.log(muni); }
    var are = $('#municipios').val();
    if (are === "true") { GetMunicipios(); } else { console.log(are); }

})
    


////IMAGEN REFRES E INSERT
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            const maxLength = 2000000
            console.log(e.loaded + "" + e.total);
            console.log(e);
            if (e.loaded < maxLength) {
                document.getElementById('ImageLength').innerText = "";
                $('#imgpreview').attr('src', e.target.result);
            }
            else {
                $('#imgpreview').attr('src',"../../Content/img/descarga.jpg");
                document.getElementById('lblCargarFoto').innerText = "";
                document.getElementById('CargarFoto').value = "";
                document.getElementById('ImageLength').innerText = "Limite Excedido";
            }
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
                $('#mun_codigo').trigger("chosen:updated");
                if (_selectedMun !== null || _selectedMun !== "") {
                    $("#mun_codigo option[value='" + _selectedMun + "']").attr("selected", true);
                } else { console.log("No es"); }
            }
            else {
                $('#mun_codigo').empty();
                $('#mun_codigo').append("<option value=''>Seleccione</option>");
            }
        });

}

////OBTENER AREA
$(document).on("change", "#are_Id", function () {
    Getpuesto();
});

function Getpuesto() {
    var are_Id = $('#are_Id').val();
    var selectedPuesto = $('#selectedPuesto').val();
    console.log(selectedPuesto);
    $.ajax({
        url: "/Empleado/Getpuesto",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ are_Id: are_Id }),
    })
        .done(function (data) {
           
            if (data.length > 0) {
                $('#pto_Id').empty();
                $('#pto_Id').append("<option value=''>Seleccione Área</option>");
                $.each(data, function (key, val) {
                    $('#pto_Id').append("<option value=" + val.pto_Id + ">" + val.pto_Descripcion + "</option>");
                });
                $('#pto_Id').trigger("chosen:updated");
                if (selectedPuesto !== null || selectedPuesto !== "") {
                    $("#pto_Id option[value='" + selectedPuesto + "']").attr("selected", true);
                } else { console.log("No es"); }
            }

            else {
                $('#pto_Id').empty();
                $('#pto_Id').append("<option value=''>Seleccione</option>");
            }
        });

}




document.getElementById('emp_CorreoElectronico').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailOK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#emp_CorreoElectronico').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#emp_CorreoElectronico').focus();
        $('#emp_CorreoElectronico').addClass('is-invalid');
    }
});


$('#emp_Nombres').keyup(function () {
    $('#Nombres').hide();
});

$('#emp_Apellidos').keyup(function () {
    $('#Apellidos').hide();
});

$('#emp_Sexo').keyup(function () {
    $('#Sexo').hide();
});

$('#emp_FechaNacimiento').keyup(function () {
    $('#FechaNacimiento').hide();
});

$('#tps_Id').keyup(function () {
    $('#ti').hide();
});

$('#emp_Telefono').keyup(function () {
    $('#Telefono').hide();
});

$('#emp_CorreoElectronico').keyup(function () {
    $('#emailOK').hide();
});

$('#are_Id').keyup(function () {
    $('#Area').hide();
});


$('#pto_Id').keyup(function () {
    $('#Puesto').hide();
});

$('#emp_Direccion').keyup(function () {
    $('#Direccion').hide();
});

$('#emp_EsJefe').keyup(function () {
    $('#EsJefe').hide();
});

$('#dep_codigo').keyup(function () {
    $('#Departamento').hide();
});

$('#mun_codigo').keyup(function () {
    $('#Municipio').hide();
});

$('#emp_FechaIngreso').keyup(function () {
    $('#FechaIngreso').hide();
});



//Get Current Date
function GetTodayDate() {
    var tdate = new Date();
    var dd = tdate.getDate(); //yields day
    var MM = tdate.getMonth(); //yields month
    var yyyy = tdate.getFullYear(); //yields year
    var currentDate = dd + "/" + (MM + 1) + "/" + yyyy;
    return currentDate;
}

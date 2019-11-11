var contador = 0;
var contadorEdit = 0;
$('#AgregarMunicipios').click(function () {
    var MunCodigo = $('#mun_Codigo').val();
    var MunNombre = $('#mun_Nombre').val();


    if (MunCodigo == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');
    }
    else if (MunCodigo.length >= 5) {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationCodigoCreate').after('<ul id="CodigoError" class="validation-summary-errors text-danger">No se Aceptan  mas 4 números.</ul>');
    }
  

    else if (MunNombre == '') {
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');
    }

    else if (MunNombre.substring(0, 1) == " ") {
        $('#ValidationCreate').text('');
        $('#MessageError').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidationNombreCreate').after('<ul id="NombreError" class="validation-summary-errors text-danger">No se Aceptan Espacios en blanco.</ul>');
    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'MunCodigo'>" + $('#mun_Codigo').val() + "</td>";
        copiar += "<td id = 'MunNombre'>" + $('#mun_Nombre').val() + "</td>";
        copiar += "<td>" + '<button id="removeMunicipios" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tblMunicipios').append(copiar);


        var tbMunicipio = GetMunicipio();
        $.ajax({
            url: "/Departamento/AnadirMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: tbMunicipio }),
        })
            .done(function (data) {
                $('#mun_Codigo').val('');
                $('#mun_Nombre').val('');
                $('#MessageError').text('');
                $('#NombreError').text('');

            });



    }

});

function GetMunicipio() {
    var Municipio = {
        mun_Codigo: $('#mun_Codigo').val(),
        mun_Nombre: $('#mun_Nombre').val(),
        mun_UsuarioCrea: contador
    };
    return Municipio;
}

$(document).on("click", "#tblMunicipios tbody tr td button#removeMunicipios", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var Municipios = {
        mun_UsuarioCrea: idItem,
    };
    $.ajax({
        url: "/Departamento/RemoveMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Municipios: Municipios }),
    });
});

/////////////////////////MODAL
$('#AgregarMunicipiosEdit').click(function () {
    var MunCodigo = $('#mun_CodigoEdit').val();
    var MunNombre = $('#mun_NombreEdit').val();


    if (MunCodigo == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunCodigoEdit').after('<ul id="CodigoError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else if (MunCodigo.length < 4) {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunCodigoEdit').after('<ul id="CodigoError" class="validation-summary-errors text-danger">No se Aceptan menos de 4 números.</ul>');
    }
    else if (MunNombre == '') {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunNombreEdit').after('<ul id="NombreError" class="validation-summary-errors text-danger">Campo Requerido</ul>');
    }
    else if (MunNombre.substring(0, 1) == " ") {
        $('#ValidationCreate').text('');
        $('#CodigoError').text('');
        $('#NombreError').text('');
        $('#ValidacionMunNombreEdit').after('<ul id="NombreError" class="validation-summary-errors text-danger">No se Aceptan Espacios en blanco.</ul>');
    }
    else {
        contadorEdit = contadorEdit + 1;
        copiar = "<tr data-id=" + contadorEdit + ">";
        copiar += "<td id = 'MunCodigo'>" + $('#mun_CodigoEdit').val() + "</td>";
        copiar += "<td id = 'MunNombre'>" + $('#mun_NombreEdit').val() + "</td>";
        copiar += "<td>" + '<button id="removeMunicipiosEdit" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#tblMunicipiosEdit').append(copiar);

        var Municipio = {
            mun_Codigo: $('#mun_CodigoEdit').val(),
            mun_Nombre: $('#mun_NombreEdit').val(),
            mun_UsuarioCrea: contadorEdit,
        };
        $.ajax({
            url: "/Departamento/AnadirMunicipio",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ Municipio: Municipio }),
        })
            .done(function (data) {
                $('#ValidationCreate').text('');
                $('#mun_CodigoEdit').val('');
                $('#mun_NombreEdit').val('');
                $('#CodigoError').text('');
                $('#NombreError').text('');
            });
    }

});

////////////////////////////////////
function btnActualizar(mun_Codigo) {

    var Municipio = mun_Codigo;
    var Depatamento = $('#dep_Codigo').val();
    var NombreMunicipio = $("#MunNombre_" + mun_Codigo).val();


    console.log(Municipio);
    console.log(NombreMunicipio);
    console.log(Depatamento);

    $.ajax({
        url: "/Departamento/ActualizarMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ mun_Codigo: Municipio, dep_Codigo: Depatamento, mun_Nombre: NombreMunicipio }),
    }).done(function (data) {
        if (data == '') {
            location.reload();
        }
        else if (data == '-1') {
            $('#MensajeError' + mun_Codigo).text('');
            $('#ValidationMessageFor' + mun_Codigo).after('<ul id="MensajeError' + mun_Codigo + '" class="validation-summary-errors text-danger">No se ha podido Actualizar el registro.</ul>');
        }
        else {
            $('#MensajeError' + mun_Codigo).text('');
            $('#ValidationMessageFor' + mun_Codigo).after('<ul id="MensajeError' + mun_Codigo + '" class="validation-summary-errors text-danger">Campo Requerido</ul>');
        }
    });
}


//Guardar Municipio Modales
$('#btnNuevo').click(function () {

    var CodigoMun = $('#mun_Codigo').val();
    var Depatamento = $('#dep_Codigo').val();
    var NombreMun = $('#mun_Nombre').val();

    console.log(CodigoMun)
    console.log(NombreMun)
    //var munCodigo = $('#mun_Codigo').val();
    //var munNombre = $('#mun_Nombre').val();


    if (CodigoMun == '') {
        $('#mun_Codigo').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');
        console.log('HOLAAAA')
    }

    else if (NombreMun == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationCodigoUpdate').after('<ul id="errorCodigo" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');
        console.log('ADIOS')
    }

    else {
        $.ajax({
            url: "/Departamento/GuardarMunicipioModal",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ mun_Codigo: CodigoMun, dep_Codigo: Depatamento, mun_Nombre: NombreMun }),

        })
            .done(function (data) {
                if (data == '') {
                    $('#ValidationNombreUpdate').after('<ul id="ValidationNombreUpdate" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
                }
                else {
                    window.location.href = '/Departamento/Index';
                }
            })
    }
});

////////////////////////////
function EditMunicipioRecord(mun_Codigo) {
    $.ajax({
        url: "/Departamento/getMunicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ munCodigo: mun_Codigo }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#mun_CodigoEdit_").val(item.mun_Codigo);
                    $("#mun_NombreEdit_").val(item.mun_Nombre);
                    $("#EditMunicipioModal").modal();
                })
            }
        })
}

$("#BtnsubmitMunicipio").click(function () {
    var depCodigo = $('#dep_Codigo').val();
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/Departamento/ActualizarMunicipio",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                window.location.href = '/Departamento/Edit/' + depCodigo;
        }
    });
})

////////////////////////////////////
//GUARDAR MUNICIPIOS
$('#btnGuardar').click(function () {
    var munCodigo = $('#mun_Codigo').val();
    var munNombre = $('#mun_Nombre').val();

    if (munNombre == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');

    }


    else if (munCodigo == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationCodigoUpdate').after('<ul id="errorCodigo" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');

    }

    else {
        var tbMunicipio = GetMunicipio();
        $.ajax({
            url: "/Departamento/GuardarMun",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbMunicipio: tbMunicipio }),

        })
            .done(function (data) {
                if (data == '') {
                    $('#ValidationNombreUpdate').after('<ul id="ValidationNombreUpdate" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
                }
                else {
                    window.location.href = '/Departamento/Index';
                }
            })
    }
});
/////////////////////////////////////
//Guardar Municipio Modales
$('#btnNuevo').click(function () {

    var CodigoMun = $('#mun_Codigo').val();
    var Depatamento = $('#dep_Codigo').val();
    var NombreMun = $('#mun_Nombre').val();

    console.log(CodigoMun)
    console.log(NombreMun)
    //var munCodigo = $('#mun_Codigo').val();
    //var munNombre = $('#mun_Nombre').val();


    if (CodigoMun == '') {
        $('#mun_Codigo').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationNombre').after('<ul id="errorNombre" class="validation-summary-errors text-danger">Campo Municipio Requerido</ul>');
        console.log('HOLAAAA')
    }

    else if (NombreMun == '') {
        $('#mun_Nombre').text('');
        $('#errorCodigo').text('');
        $('#errorNombre').text('');
        $('#ValidationCodigoUpdate').after('<ul id="errorCodigo" class="validation-summary-errors text-danger">Campo Codigo Municipio Requerido</ul>');
        console.log('ADIOS')
    }

    else {
        $.ajax({
            url: "/Departamento/GuardarMunicipioModal",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ mun_Codigo: CodigoMun, dep_Codigo: Depatamento, mun_Nombre: NombreMun }),

        })
            .done(function (data) {
                if (data == '') {
                    $('#ValidationNombreUpdate').after('<ul id="ValidationNombreUpdate" class="validation-summary-errors text-danger">No se pudo actualizar el registro, contacte con el administrador</ul>');
                }
                else {
                    window.location.href = '/Departamento/Index';
                }
            })
    }
});

$("#mun_NombreEdit").change(function () {
    var str = $("#mun_NombreEdit").val();
    var res = str.toUpperCase();
    $("#mun_NombreEdit").val(res);
});

$("#mun_NombreEdit").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})

$('#btnNuevoEdit').click(function () {
    var depCodigo = $('#dep_Codigo').val();
    $.ajax({
        url: "/Departamento/GuardarMuninicipio",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ depCodigo: depCodigo }),
    })
        .done(function (data) {
            if (data == '-1') {
                $('#ValidationCreate').text('');
                $('#ValidationCreate_').after('<ul id="ValidationCreate" class="validation-summary-errors text-danger">No se pudo guardar el registro, contacte al administrador</ul>');
            }
            else {
                window.location.href = '/Departamento/Edit/' + depCodigo;
            }
        })
});

$("#mun_NombreEdit_").change(function () {
    var str = $("#mun_NombreEdit").val();
    var res = str.toUpperCase();
    $("#mun_NombreEdit").val(res);
});

$("#mun_NombreEdit_").on("keypress", function () {
    $input = $(this);
    setTimeout(function () {
        $input.val($input.val().toUpperCase());
    }, 50);
})
/////////////////////////////////////////

$(document).ready(function () {
    //QUE SOLO ACEPTE 4 NUMEROS
    $("#mun_CodigoEdit")[0].maxLength = 4;
    $("#mun_CodigoEdit_")[0].maxLength = 4;
    $("#mun_CodigoEdit")[0].minLength = 4;
    $("#mun_CodigoEdit_")[0].minLength = 4;


    //VALIDAR SOLO NUMEROS
    $('#mun_CodigoEdit').bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
    $('#mun_CodigoEdit_').bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    //Que solo acepte 30 letras
    $("#mun_NombreEdit")[0].maxLength = 30;
    $("#mun_NombreEdit_")[0].maxLength = 30;
    $("#mun_NombreEdit")[0].minLength = 30;
    $("#mun_NombreEdit_")[0].minLength = 30;


    //VALIDAR SOLO LETRAS

    $('#dep_Nombre').on('input', function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ]+/ig, "");
        }
    });

    $('#mun_NombreEdit').on('input', function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ]+/ig, "");
        }
    });

    $('#mun_NombreEdit_').on('input', function (e) {
        if (!/^[ a-záéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ a-záéíóúüñ]+/ig, "");
        }
    });



    //Bloqueo de Botón//
    $('#mun_NombreEdit_').change(function () {
        if ($(this).val() == 0) {
            $('#BtnsubmitMunicipio').attr('disabled', 'disabled');
        }
        else {
            $('#BtnsubmitMunicipio').removeAttr("disabled");
        }
    });




    // Copiar y Pegar///
    $(document).ready(function () {
        $('#mun_CodigoEdit_').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });

    $(document).ready(function () {
        $('#mun_CodigoEdit').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });
})
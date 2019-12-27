var contador = 0;



//Validacion de solo letras
function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = "áéíóúabcdefghijklmnñopqrstuvwxyz ";
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

//Validar Los campos numericos
function format(input) {
    var num = input.value.replace(/\,/g, '');
    if (!isNaN(num)) {
        input.value = num;
    }
    else {
        //alert('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}
//fin
///Editar por medio de una Modal, tambien obtiene los datos para mostrarlos
//function EditStudentRecord(pscat_Id) {


//    $("#MsjError").text("");

//    $.ajax({
//        url: "/ProductoCategoria/GetSubCate",
//        method: "POST",
//        dataType: 'json',
//        contentType: "application/json; charset=utf-8",
//        data: JSON.stringify({ pscat_Id }),
//    })
//        .done(function (data) {
//            $.each(data, function (i, item) {
//                $("#pscat_Id").val(item.pscat_Id);
//                $("#pscat_Descripcion_edit").val(item.pscat_Descripcion);




//                $("#MyModal").modal();

//            })
//        })
//        .fail(function (jqXHR, textStatus, errorThrown) {
//            console.log('jqXHR', jqXHR);
//            console.log('textStatus', textStatus);
//            console.log('errorThrown', errorThrown);
//        })
//}

//$("#Btnsubmit").click(function () {
//    var data = $("#SubmitForm").serializeArray();
//    var impu = $("#pscat_ISV_Edit").val();
//    if (impu > 100) {
//        $("#MsjISV").text("Campo ISV solo Permite un Rango de 0 a 100");
//    }
//    else if (impu == '') {
//        $("#MsjISV").text("Campo ISV Requerido");
//    }
//    else {
//        $.ajax({
//            type: "Post",
//            url: "/ProductoCategoria/UpdateSubCategoria",
//            data: data,
//            success: function (result) {
//                if (result == '-1')
//                    $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
//                else

//                    location.reload();
//            }
//        });
//    }



//    function GetSubCategoria() {
//        var SubCategoria = {
//            pscat_Descripcion: $('#pscat_Descripcion').val(),
//            pscat_ISV: $('#pscat_ISV').val(),
//            pscat_UsuarioCrea: contador,
//            pscat_Id: contador,

//        };
//        return SubCategoria;
//    }
//})

$(document).on("click", "#TablaSub tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})

$(document).on("click", "#Datatable tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
});

$('#AgregarSubCategorias').click(function () {

    var Descripcion = $('#pscat_Descripcion').val();

    if (Descripcion == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Descripcion'>" + $('#pscat_Descripcion').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#Datatable').append(copiar);


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#pscat_Descripcion').val('');
                $('#pscat_ISV').val('');

                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
    
            });
    }

});

function GetSubCategoria() {
    var SubCategoria = {
        pscat_Descripcion: $('#pscat_Descripcion').val(),
 
        pscat_UsuarioCrea: contador,
        pscat_Id: contador,

    };
    return SubCategoria;
}

$('#CrearSubCategoria').click(function () {

    var Descripcion = $('#pscat_Descripcion').val();


    if (Descripcion == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = 'Descripcion'>" + $('#pscat_Descripcion').val() + "</td>";
      
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#TablaSub').append(copiar);


        var SubCategoria = GetSubCategoria();

        $.ajax({
            url: "/ProductoCategoria/GuardarSubCategoria",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ tbsubcategoria: SubCategoria }),
        })
            .done(function (data) {
                $('#pscat_Descripcion').val('');
                $('#pscat_ISV').val('');

                $('#MessageError').text('');
                $('#ErrorDescripcion').text('');
                $('#ErrorISV').text('');
            });
    }

});

function GetSubCategoria() {
    var SubCategoria = {
        pscat_Descripcion: $('#pscat_Descripcion').val(),
        pscat_ISV: $('#pscat_ISV').val(),
        pscat_UsuarioCrea: contador,
        pscat_Id: contador,

    };
    return SubCategoria;
}

///REMOVER EL DETALLE
$(document).on("click", "#Datatable tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})


///REMOVER EL DETALLE
$(document).on("click", "#TablaSub tbody tr td button#removerSubCategoria", function () {
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/ProductoCategoria/removeSubCategoria",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})

//$('#pscat_Descripcion').blur(function () {
//    if ($.trim($('#pscat_Descripcion').val()) == 0) {
//        $('#errorDescripcion').text('');
//        $('#validationDescripcion').after('<ul id="errorDescripcion" class="validation-summary-errors text-danger">Campo Descripcion Requerido</ul>');
//    }
//});
////////////////////////////////////////////////////////////////////////
/////////Abrir modal y obtener los valores para los textbox
function EditStudentRecord(pscat_Id) {
    $.ajax({
        url: "/ProductoCategoria/GetSubCate",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ pscat_Id: pscat_Id }),

    })
        .done(function (data) {
            if (data.length > 0) {
                $.each(data, function (i, item) {
                    $("#pscat_Id").val(item.pscat_Id);
                    $("#pscat_Descripcion_edit").val(item.pscat_Descripcion);
                    $("#MyModal").modal();
                })
            }
        })
}


//------------------Actualizar Municipio(Modal)     Guardar los cambios del modal de Editar
$("#Btnsubmit").click(function () {
   
    var data = $("#SubmitForm").serializeArray();
    $.ajax({
        type: "Post",
        url: "/ProductoCategoria/UpdateSubCategoria",
        data: data,
        success: function (result) {
            if (result == '-1')
                $("#MsjError").text("No se pudo actualizar el registro, contacte al administrador");
            else
                location.reload();
        }
    });
})
////////////////////////////////////////////////////////////////////////
$(document).ready(function () {
    $('#TablaSub').DataTable(
        {
            "searching": true,
            "lengthChange": true,
            "oLanguage": {
                "oPaginate": {
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior",
                },
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sEmptyTable": "No hay registros",
                "sInfoEmpty": "Mostrando 0 de 0 Entradas",
                "sSearch": "Buscar",
                "sInfo": "Mostrando _START_ a _END_ Entradas",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            }
        });
});


$('#pscat_Descripcion_edit').blur(function () {
    if ($.trim($('#pscat_Descripcion_edit').val()) == 0) {
        $('#DescripcionErrorEdit').text('');
        $('#validationDescripcionEdit').after('<ul id="DescripcionErrorEdit" class="validation-summary-errors text-danger">Campo Descripcion Requerido</ul>');
    }
});

$("#pcat_Nombre").change(function () {
    var str = $("#pcat_Nombre").val();
    var res = str.toUpperCase();
    $("#pcat_Nombre").val(res);
});

$("#pscat_Descripcion").change(function () {
    var str = $("#pscat_Descripcion").val();
    var res = str.toUpperCase();
    $("#pscat_Descripcion").val(res);
});

$("#pscat_Descripcion_edit").change(function () {
    var str = $("#pscat_Descripcion_edit").val();
    var res = str.toUpperCase();
    $("#pscat_Descripcion_edit").val(res);
});

$(document).keydown(function (e) {

    if ((e.key == 'g' || e.key == 'G') && (e.ctrlKey || e.metaKey)) {

        e.preventDefault();
        $("form").submit();
        return false;
    }
    return true;

});

$("#bton").keypress(function (e) {
    if (e.which == 13) {

        $("form").submit();
    }
});


var contador = 0;




/////DATE PICKER//////////////////////
$('#Lianvi_FechaInicioViaje,#Lianvi_FechaFinViaje,#Lianvi_FechaLiquida,#Lianvide_FechaGasto').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"


});

////////////////////////////ARCHIVO///////////////////////////////////////////
$("#CargarArchivo").change(function () {
    readURL(this);
});

////////////////////////////AGREGAR DETALLE//////////////////////////////////
$('#AgregarDetalle').click(function () {

    var table = $('#dataTable').DataTable();
    var LianvideArchivo = $('#Lianvide_Archivo').val();
    var TipoGasto = $('#tpv_Id').val();
    var FechaGasto = $('#Lianvide_FechaGasto').val();
    var MontoGastos = $('#Lianvide_MontoGasto').val();
    var Concepto = $('#Lianvide_Concepto').val();

 /////////////VALIDACION VACIO////////////////////////////////////
    if (FechaGasto == '') {
        $('#MessageError').text('');
    }
    else if (TipoGasto == '') {
        $('#MessageError').text('');
    }
    else if (Concepto == '') {
        $('#MessageError').text('');
    }

    else if (LianvideArchivo == '') {
        $('#MessageError').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }
    else if (MontoGastos == '') {
        $('#MessageError').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }

    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Lianvide_FechaGasto'>" + $('#Lianvide_FechaGasto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'TipoGasto'>" + $('#tpv_Id').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Concepto'>" + $('#Lianvide_Concepto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'LianvideArchivo'>" + $('#Lianvide_Archivo').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'MontoGasto'>" + $('#Lianvide_MontoGasto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td>" + '<button id="removerSubCategoria" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#dataTable').append(copiar);


        var tbLiquidacionAnticipoViaticoDetalle = GetLiquidacionViatico();
        $.ajax({
            url: "/LiquidacionAnticipoViaticoDetalle/SaveLiquidacionAnticipoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ LIQUIDACIONDETALLE: tbLiquidacionAnticipoViaticoDetalle }),
        }).done(function (data) {
     
            if (data == tpv_Id) {
                $("#tbLiquidacionViatico td").each(function () {
                    var prueba = $(this).text()
                    if (prueba == tpv_Id) {
                        
                        table.row($(this).parents('tr')).remove().draw();
                
                        table.row.add([
                            $('#Lianvide_FechaGasto').val(),
                            $('#TipoGastoText').text(),
                            $('#Lianvide_Concepto').val(),
                            $('#Archivo_Name').text(),
                            $('#Lianvide_MontoGasto').val(),

                            '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',
                     
  
                        ]).draw(false);
                    }
                });

            }
            else {
                table.row.add([
                    $('#Lianvide_FechaGasto').val(),
                    $('#tpv_Id').val(),
                    $('#Lianvide_Concepto').val(),
                    $('#Archivo_Name').text(),
                    $('#Lianvide_MontoGasto').val(),
                    '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',

                ]).draw(false);
            }

        }).done(function (data) {
               $('#Lianvide_FechaGasto').val(''),
                   $('#tpv_Id').text(''),
                   $('#Lianvide_Concepto').val(''),
                   $('#Archivo_Name').text(''),
                $('#Lianvide_MontoGasto').val(''),

            $('#MessageError').text('');
            $('#Error_Producto').text('');
            $('#Error_Barras').text('');
            $('#Error_PuntoReorden').text('');
            $('#Error_CantidadMinima').text('');
            $('#Error_CantidadMaxima').text('');
            $('#Error_Costo').text('');
            $('#Error_CostoPromedioo').text('');
        });
    }
});
function GetLiquidacionViatico() {
    var LIQUIDACIONDETALLE = {
      


    
        tpv_Id: $('#tpv_Id').val(),
        Lianvide_FechaGasto: $('#Lianvide_FechaGasto').val(),
        Lianvide_MontoGasto: $('#bodd_CantidadMaxima').val(),
        LianvideArchivo: $('#Lianvide_Archivo').val(),
        //Fecha: $('#fechaCreate').val(),
        Lianvide_UsuarioCrea: contador,
        Lianvide_Id: contador,
    };
    return LIQUIDACIONDETALLE;
}
///REMOVER EL DETALLE
$(document).on("click", "#dataTable tbody tr td button#RemoveDetalle", function () {
   
    $(this).closest('tr').remove();
    idItem = $(this).closest('tr').data('id');
    var borrar = {
        pscat_Id: idItem,
    };
    $.ajax({
        url: "/LiquidacionAnticipoViaticoDetalle/RemoveDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ borrado: borrar }),
    });
})





function GetFechas() {
    var Fechas = {
        FechaInicio: document.getElementById("Lianvi_FechaInicioViaje").value,
        FechaFin: document.getElementById("Lianvi_FechaFinViaje").value
    };
    return Fechas;
}


document.getElementById("Lianvi_FechaInicioViaje").addEventListener("blur", function () {
    CalcularFechas()

});

document.getElementById("Lianvi_FechaFinViaje").addEventListener("blur", function () {
    CalcularFechas()
});
function CalcularFechas() {
    vFechas = GetFechas()
    console.log("JS: " + vFechas);

    $.ajax({
        url: "/LiquidacionAnticipoViatico/CalcularFecha",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ cCalFechas: vFechas }),
    })
        .done(function (data) {
            console.log(data);
         
                document.getElementById("spanFechaInicio").innerText = data.MASspan
            
       
            document.getElementById("spanFechaFin").innerText = data.MASspan1
            
        });
}





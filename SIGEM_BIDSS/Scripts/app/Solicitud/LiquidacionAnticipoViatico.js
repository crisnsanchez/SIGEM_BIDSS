/////DATE PICKER
$('#Lianvi_FechaInicioViaje,#Lianvi_FechaFinViaje,#Lianvi_FechaLiquida,#Lianvide_FechaGasto').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});
$("#CargarArchivo").change(function () {
    readURL(this);
});



$('#AgregarDetalle').click(function () {
    console.log('boton');
    var table = $('#dataTable').DataTable();
    var LianvideArchivo = $('#Lianvide_Archivo').val();
    var tpv_Id = $('#tpv_Id').val();
    var Lianvide_FechaGasto = $('#Lianvide_FechaGasto').val();
    var Lianvide_MontoGasto = $('#Lianvide_MontoGasto').val();
   
    var Lianvide_Concepto = $('#Lianvide_Concepto').val();


    if (Lianvide_FechaGasto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorProducto_Create').after('<ul id="Error_Producto" class="validation-summary-errors text-danger">*Codigo De Barra Requerido</ul>');

    }
    else if (tpv_Id == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#Error_Barras').text('');
        $('#ErrorBarras_Create').after('<ul id="Error_Barras" class="validation-summary-errors text-danger">*Codigo De Barras Requerido</ul>');
    }
    else if (Lianvide_Concepto == '') {

        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorCantidadMinima_Create').after('<ul id="Error_CantidadMinima" class="validation-summary-errors text-danger">*Cantidad Miníma Requerido</ul>');
    }

    else if (LianvideArchivo == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }
    else if (Lianvide_MontoGasto == '') {
        $('#MessageError').text('');
        $('#Error_Producto').text('');
        $('#Error_PuntoReorden').text('');
        $('#Error_CantidadMinima').text('');
        $('#Error_CantidadMaxima').text('');
        $('#Error_Costo').text('');
        $('#Error_CostoPromedioo').text('');
        $('#ErrorPuntoReorden_Create').after('<ul id="Error_PuntoReorden" class="validation-summary-errors text-danger">*Campo Punto Reorden Requerido</ul>');
    }

    else {

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
                            $('#tpv_Id').val(),
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
                $('#tpv_Id').val(''),
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
    };
    return LIQUIDACIONDETALLE;
}
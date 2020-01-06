
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

$('#AgregarDetalle').click(function() {
    var table = $('#dataTable').DataTable();
    var Archivo = $('#Lianvide_Archivo').val();
    var TipoGasto = $('#tpv_Id').val();
    var FechaGasto = $('#Lianvide_FechaGasto').val();
    var MontoGastos = $('#Lianvide_MontoGasto').val();
    var Concepto = $('#Lianvide_Concepto').val();

    if (Archivo == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    if (TipoGasto == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    if (FechaGasto == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    if (MontoGastos == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    if (Concepto == '') {
        $('#MessageError').text('');
        $('#ErrorDescripcion').text('');
        $('#ErrorISV').text('');
        $('#DescripcionError').after('<ul id="ErrorDescripcion" class="validation-summary-errors text-danger">Campo Descripción Requerido</ul>');

    }
    else {
        contador = contador + 1;
        copiar = "<tr data-id=" + contador + ">";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Archivo'>" + $('#Lianvide_Archivo').text() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'TipoGasto'>" + $('#tpv_Id').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'FechaGasto'>" + $('#Lianvide_FechaGasto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'MontoGastos'>" + $('#Lianvide_MontoGasto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td id = 'Concepto'>" + $('#Lianvide_Concepto').val() + "</td>";
        copiar += "<td id = ''></td>";
        copiar += "<td>" + '<button id="RemoveDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>' + "</td>";
        copiar += "</tr>";
        $('#dataTable').append(copiar);

        var LiquidacionAnticipoViaticoDetalle = GetLiquidacionViatico();

        $.ajax({
            url: "/LiquidacionAnticipoViaticoDetalle/SaveLiquidacionAnticipoDetalle",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ LIQUIDACIONDETALLE: LiquidacionAnticipoViaticoDetalle }),
        })



            .done(function(data) {

                if (data == tpv_Id) {
                    $("#tbLiquidacionViatico td").each(function() {
                        var prueba = $(this).text()
                        if (prueba == Lianvide_Id) {

                            table.row($(this).parents('tr')).remove().draw();

                            table.row.add([
                                $('#Lianvide_FechaGasto').val(),
                                $('#tpv_Id').val(),
                                $('#Lianvide_Concepto').val(),
                                $('#Archivo_Name').text(),
                                $('#Lianvide_MontoGasto').val(),

                                '<button id="RemoveDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>',

                            
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

                        '<button id="RemoveDetalle" class="btn btn-danger btn-xs eliminar" type="button">Quitar</button>',

                    ]).draw(false);
                }

            }).done(function(data) {
                $('#Lianvide_FechaGasto').val(''),
                    $('#tpv_Id').text(),
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

$('#tpv_Id').change(function() {
    var text = $(this).find('option:selected').text();
  
});

//ObtenerCampos

function GetLiquidacionViatico() {
    var LIQUIDACIONDETALLE = {
     
        Lianvide_Archivo: $('#Lianvide_Archivo').val(),
        tpv_Id: $('#tpv_Id').val(),
        Lianvide_FechaGasto: $('#Lianvide_FechaGasto').val(),
        Lianvide_MontoGasto: $('#Lianvide_MontoGasto').val(),
        Lianvide_Concepto: $('#Lianvide_Concepto').val(),
        Lianvide_UsuarioCrea: contador,
        Lianvide_Id: contador,
    };
    return LIQUIDACIONDETALLE;
}
////Remover detalle///
$(document).on("click", "#dataTable tbody tr td button#RemoveDetalle", function() {
    idItem = $(this).closest('tr').data('id');
    var vIdDetalle = $(this).closest("tr").find("td:eq(0)").text();
    var tbLquidacionDetalle = {
        Lianvi_Id: vIdDetalle,
        Lianvide_UsuarioCrea: vIdDetalle
    };
    var table = $('#dataTable').DataTable();
    table
        .row($(this).parents('tr'))
        .remove()
        .draw();

    $.ajax({
        url: "/LiquidacionAnticipoViaticoDetalle/RemoveDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ LiquidacionDetalle: tbLquidacionDetalle }),
        success: function(data) {
            contador = contador - 1
            console.log("Contador: " + contador)
            RejectUnload()
        }
    });
});

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


//CalculodeFechas
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






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

    var LiquidacionAnticipoViatico = GetLiquidacionViatico();
    console.log(LiquidacionAnticipoViatico);

   
    $.ajax({
        url: "/LiquidacionAnticipoViaticoDetalle/SaveLiquidacionAnticipoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbLiquidacionAnticipoViaticoDetalle: LiquidacionAnticipoViatico }),
    })

        .done(function (data) {
            console.log(contador + 'hid');

            if (LiquidacionAnticipoViatico.tpv_Id == data) {
                $('#dataTable td').each(function () {
                    var constId = $(this).text();

                    if (LiquidacionAnticipoViatico.tpv_IdText == constId) {
                        var q = table.row($(this).parents('tr')).remove().draw()
                        var t = $(this).closest('tr').find('td:eq(3)').text()
                        var suma = parseInt(LiquidacionAnticipoViatico.Lianvide_MontoGasto) + parseInt(t);
                        table.row.add([
                            LiquidacionAnticipoViatico.Lianvide_FechaGasto,
                            LiquidacionAnticipoViatico.tpv_IdText,
                            LiquidacionAnticipoViatico.Lianvide_Concepto,
                            suma,



                            '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'



                        ]).draw(false)
                    }
                });
            }

            else {


                contador = contador + 1;
                console.log(data);
                table.row.add([

                    LiquidacionAnticipoViatico.Lianvide_FechaGasto,
                    LiquidacionAnticipoViatico.tpv_IdText,
                    LiquidacionAnticipoViatico.Lianvide_Concepto,
                    LiquidacionAnticipoViatico.Lianvide_MontoGasto,

                    '<button id = "RemoveDetalle" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'


                ]).draw(false)

            }
        });

    });




//ObtenerCampos

function GetLiquidacionViatico() {

    var R = document.getElementById('tpv_Id')
    var LIQUIDACIONDETALLE = {


        Lianvide_Id: contador,
        tpv_Id: $('#tpv_Id').val(),
        tpv_IdText: R.options[R.selectedIndex].text,
        Lianvide_FechaGasto: $('#Lianvide_FechaGasto').val(),
        Lianvide_MontoGasto: $('#Lianvide_MontoGasto').val(),
        Lianvide_Concepto: $('#Lianvide_Concepto').val(),
     
    };
    return LIQUIDACIONDETALLE;
}









function GetFechas() {
    var Fechas = {
        FechaInicio: document.getElementById("Lianvi_FechaInicioViaje").value,
        FechaFin: document.getElementById("Lianvi_FechaFinViaje").value
    };
    return Fechas;
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


//document.getElementById("Lianvi_FechaInicioViaje").addEventListener("blur", function () {
//    CalcularFechas()

//});
//document.getElementById("Lianvi_FechaFinViaje").addEventListener("blur", function () {
//    CalcularFechas()
//});
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




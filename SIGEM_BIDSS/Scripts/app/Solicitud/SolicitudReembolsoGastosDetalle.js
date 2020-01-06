///Date picket
$('#ReemgaDet_FechaGasto').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1900",
    language: "es",
    daysOfWeekDisabled: "0"
});

//Agregar Detalle
$('#AgregarDetalle').click(function () {
    var table = $('#dataTable').DataTable();
    var SolicitudReembolsoGastosDetalle = GetDetalle();

    console.log(SolicitudReembolsoGastosDetalle);




    table.row.add([
        SolicitudReembolsoGastosDetalle.ReemgaDet_FechaGasto,
        SolicitudReembolsoGastosDetalle.tpv_IdText,
        SolicitudReembolsoGastosDetalle.ReemgaDet_MontoGasto,
        SolicitudReembolsoGastosDetalle.ReemgaDet_Concepto,
        SolicitudReembolsoGastosDetalle.ReemgaDet_TotalGastos,
        '<button id = "removeMunicipios" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'

    ]).draw(false)



    $.ajax({
        url: "/SolicitudReembolsoGastosDetalles/SaveReembolsoDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbSolicitudReembolsoGastosDetalle: SolicitudReembolsoGastosDetalle }),
    })

        .done(function (data) {
            console.log(data);

        });
});



function GetDetalle()
{
    var R = document.getElementById('tpv_Id')
    var ReembolsoDetalle = {
        ReemgaDet_FechaGasto: $('#ReemgaDet_FechaGasto').val(),
        tpv_Id: $('#tpv_Id').val(),
        tpv_IdText: R.options[R.selectedIndex].text,
        ReemgaDet_MontoGasto: $('#ReemgaDet_MontoGasto').val(),
        ReemgaDet_Concepto: $('#ReemgaDet_Concepto').val(),
        ReemgaDet_TotalGastos: $('#ReemgaDet_TotalGastos').val()



    }
    return ReembolsoDetalle;
};
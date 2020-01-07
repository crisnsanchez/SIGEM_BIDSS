var contador = 0;


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

    var reader = new FileReader();
    var Documento = document.getElementById('CargarFoto').reader;
    console.log(Documento)

    //$.ajax({
    //    url: "/SolicitudReembolsoGastosDetalles/SaveReembolsoDetalle",
    //    method: "POST",
    //    dataType: 'json',
    //    contentType: "application/json; charset=utf-8",
    //    data: JSON.stringify({ tbSolicitudReembolsoGastosDetalle: SolicitudReembolsoGastosDetalle }),
    //})
    //    .done(function (data) {
        
    //            contador = contador + 1

    //            table.row.add([
    //                SolicitudReembolsoGastosDetalle.ReemgaDet_FechaGasto,
    //                SolicitudReembolsoGastosDetalle.tpv_IdText,
    //                SolicitudReembolsoGastosDetalle.ReemgaDet_MontoGasto,
    //                SolicitudReembolsoGastosDetalle.ReemgaDet_Concepto,

    //                '<button id = "RemoveReembolso" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'

    //            ]).draw(false)
  
    //    });

});



function GetDetalle() {

    var R = document.getElementById('tpv_Id')
    var ReembolsoDetalle = {
        ReemgaDet_Id: contador,
        ReemgaDet_FechaGasto: $('#ReemgaDet_FechaGasto').val(),
        tpv_Id: $('#tpv_Id').val(),
        tpv_IdText: R.options[R.selectedIndex].text,
        ReemgaDet_MontoGasto: $('#ReemgaDet_MontoGasto').val(),
        ReemgaDet_Concepto: $('#ReemgaDet_Concepto').val(),
    }
    return ReembolsoDetalle;
};



////IMAGEN REFRES E INSERT
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            const maxLength = 2000000
            console.log(e.loaded + "" + e.total);
            console.log(e);
            if (e.loaded < maxLength) {
                //document.getElementById('ImageLength').innerText = "";
                $('#imgpreview').attr('src', e.target.result);
            }
            else {
                $('#imgpreview').attr('src', "../../Content/img/descarga.jpg");
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

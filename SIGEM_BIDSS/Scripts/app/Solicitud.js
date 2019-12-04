$(function () {
   
});

$(document).ready(function () {
    $('#tbEmp').DataTable(
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

$("#inline_content input[name='typeAcPer']").click(function () {
    var meterId = $('input:radio[name=typeAcPer]:checked').val();
     if (meterId && meterId != '') {
        $.ajax({
            url: '_AccionPersonal',
            type: 'GET',
            cache: false,
            data: { meter_id: meterId }
        }).done(function (result) {
            console.log(result)

            $('#container').html(result);
        });
    }
});



$(document).on("click", "#tbEmp tbody tr td button#SeleccionarEmp", function () {
    var emp_Id = this.value;
    GetEmpleado(emp_Id)
})


function GetEmpleado(_emp_Id) {
    $.ajax({
        url: "/Solicitud/GetEmp",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ emp_Id: _emp_Id }),
    })
        .done(function (data) {
            console.log(data);
            var _jefeEmpName = document.getElementById('sol_GralJefeInmediato');
            _jefeEmpName.value += data._jefeEmpName;

            var _jefeEmpMail = document.getElementById('sol_GralCorreoJefeInmediato');
            _jefeEmpMail.value += data._jefeEmpMail;

        })
}





//$('#typeAcPer:radio').change(function () {
//    var meterId = $("typeAcPer").val();
//    console.log(meterId);

//    //if (meterId && meterId != '') {
//    //    $.ajax({
//    //        url: '@Url.Action("MeterInfoPartial")',
//    //        type: 'GET',
//    //        cache: false,
//    //        data: { meter_id: meterId }
//    //    }).done(function (result) {
//    //        $('#container').html(result);
//    //    });
//    //}
//});
//--Date Picker--//
$('#Anvi_GralFechaSolicitud,#Anvi_FechaViaje').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});

$('#Anvi_GralFechaSolicitud').val(GetTodayDate());

$(document).on("change", "#dep_codigo", function () {
    GetMunicipios();
});

function GetMunicipios() {
    var CodDepartamento = $('#dep_codigo').val();
    console.log(CodDepartamento);
    $.ajax({
        url: "/AnticipoViatico/GetMunicipios",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ CodDepartamento: CodDepartamento })
    })
        .done(function (data) {
            if (data.length > 0) {
                $('#mun_Codigo').empty();
                $('#mun_Codigo').append("<option value=''>Seleccione</option>");
                $.each(data, function (key, val) {
                    $('#mun_Codigo').append("<option value=" + val.mun_codigo + ">" + val.mun_nombre + "</option>");
                });

                $('#mun_Codigo').trigger("chosen:updated");
            }
            else {
                $('#mun_Codigo').empty();
                $('#mun_Codigo').append("<option value=''>Seleccione</option>");
            }
        });
}

$(document).ready(function () {
    $("#Anvi_Cliente")[0].maxLength = 100;
    //$("#Anvi_PropositoVisita")[0].maxLength = 50;
    //$("#Anvi_Comentario")[0].maxLength = 250;
});

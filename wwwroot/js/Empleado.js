$(document).ready(function () {
    var MunId = $("#MunId").val();
    if (MunId == null) {
        console.log('IsNull');
        GetValue();
    }
})


$(document).on("change", "#DepId", function () {
    GetValue();
});

function GetValue() {
    var DepId = $("#DepId").val();
    $.ajax({
        url: "/Empleado/GetValue",
        method: "POST",
        dataType: 'json',
        data:{ DepId: DepId},
    })
        .done(function (data) {
            if (data.length > 0) {
                $('#MunId').empty();
                $.each(data, function (key, val) {
                    $('#MunId').append("<option value=" + val.munId + ">" + val.munNombre + "</option>");
                });
                $('#MunId').trigger("chosen:updated");
            }
            else {
                $('#MunId').empty();
                $('#MunId').append("<option value=''>error</option>");
            }
        })
}

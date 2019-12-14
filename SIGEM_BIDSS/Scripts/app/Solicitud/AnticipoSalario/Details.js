//Details Anticipo Salario


////Reject
$(document).on("click", "#_ModalReject", function () {
    SendData();
});


function SendData() {
    var Ansal_Id = $('#Ansal_Id').val(),
        RazonInactivacion = $('#RazonInactivacion').val();
    $.ajax({
        url: "/AnticipoSalario/Reject",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: Ansal_Id, Ansal_RazonRechazo: RazonInactivacion }),
    })
        .done(function (data) {
            location.reload()
        });

}
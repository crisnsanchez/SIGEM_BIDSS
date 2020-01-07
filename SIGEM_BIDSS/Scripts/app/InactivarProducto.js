$('#Inactivar').click(function () {
    var prod_Id = $('#prod_Id').val();
    var Activo = 0
    var Razon_Inactivacion = $('#razonInac').val();
    console.log(prod_Codigo)
    console.log(Activo)
    console.log(Razon_Inactivacion)
    if (Razon_Inactivacion == "") {
        valido = document.getElementById('Mensaje');
        valido.innerText = "La razón inactivación es requerida";
    }
    else {
        $.ajax({
            url: "/Producto/EstadoInactivar",
            method: "POST",
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ prod_Id: prod_Id, Activo: Activo, Razon_Inactivacion: Razon_Inactivacion }),

        })
            .done(function (data) {
                if (data.length > 0) {
                    var url = $("#RedirectTo").val();
                    location.reload();
                }
                else {
                    alert("Registro No Actualizado");
                }
            });
    }

})

const _id = document.getElementById('razonInac');
_id.addEventListener("input", function () {
    this.value = this.value.trimStart()
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(_id.value)) {
        this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
    }
})
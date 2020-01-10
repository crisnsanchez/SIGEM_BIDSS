var contador = 0;

$(document).ready(function () {
    $("#Reqde_Justificacion")[0].maxLength = 50;
    $('.select2').select2()
});

function GetDetalle() {
    var prod = document.getElementById('prod_Id')
    var CompraDetalle = {
        prod_Id: $('#prod_Id').val(),
        Reqde_Cantidad: $('#Cantidad').val(),
        Reqde_Justificacion: $('#Reqde_Justificacion').val(),
        prod_Text: prod.options[prod.selectedIndex].text,
    }
    return CompraDetalle;
};




document.getElementById("Cantidad").onblur = function () {
    if (this.value != "") {
        //number-format the user input
        this.value = parseFloat(this.value.replace(/,/g, ""))
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");

        //set the numeric value to a number input
        document.getElementById("number").value = this.value.replace(/,/g, "")
    }
}



//Funcion no aceptar espacios en el textbox
document.addEventListener("input", function () {
    $(".nospace", 'form').each(function (e) {
        var _id = $(this).attr("id");
        var el = document.getElementById('' + _id + '')
        _value = document.getElementById(_id).value;
        document.getElementById(_id).value = _value.trimStart();
    });
    $(".normalize", 'form').each(function (e) {
        if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
            this.value = this.value.replace(/[^ .,a-z0-9áéíóúüñ]+/ig, "");
        }
    });
});
const Add = document.getElementById('')



$('#addTable').click(function () {
    var table = $('#dataTable').DataTable();

    var CompraDetalle = GetDetalle();
    var prod_Text = $('#prod_Id').val();
    var Reqde_Cantidad = $('#Cantidad').val();
    var Reqde_Justificacion = $('#Reqde_Justificacion').val();
    _prod_Text = true;
    _Reqde_Cantidad = true;
    _Reqde_Justificacion = true;

    //------------------------------Validaciones Departamento------------------------------
    //---------------------------------------------------------------------------------------
    //------------------------------Validaciones Codigo Departamento------------------------------

    if (prod_Text == '') {
        $('#prod_IdCodigoError').text('');
        $('#Validationprod_IdCreate').after('<span id="prod_IdCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span> Campo Producto Requerido</span>');
        _prod_Text = false;
    }
    else {
        $('#Validationprod_IdCreate').text('');
    }
    if (Reqde_Cantidad == '') {
        $('#CantidadCodigoError').text('');
        $('#ValidationCantidadCreate').after('<span id="CantidadCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span> Campo Cantidad Requerido</span>');
        _Reqde_Cantidad = false;
    } else {
        $('#ValidationCantidadCreate').text('');
    }
    if (Reqde_Justificacion == '') {
        $('#JustificacionCodigoError').text('');
        $('#ValidationJustificacionCreate').after('<span id="JustificacionCodigoError" class="validation-summary-errors field-validation-error text-danger"><span class="fa fa-ban text-danger"></span>  Campo Justificacion Requerido.</span>');
        _Reqde_Justificacion = false;
    } else {
        $('#ValidationJustificacionCreate').text('');
    }
    //------------------------------Fin Validaciones Codigo Departamento------------------------------
    if (!prod_Text || !Reqde_Cantidad || !Reqde_Justificacion) {
        return false
    }
    else {
       
    $.ajax({
        url: "/RequisionCompraDetalle/SaveCompraDetalle",
        method: "POST",
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ tbRequisionCompraDetalle: CompraDetalle }),
    })
        .done(function (data) {

            if (CompraDetalle.tpv_Id == data) {

                $('#dataTable td').each(function () {
                    var constId = $(this).text();

                    if (CompraDetalle.prod_Id == constId) {
                        var q = table.row($(this).parents('tr')).remove().draw()
                        var t = $(this).closest('tr').find('td:eq(1)').text()
                        var suma = parseInt(CompraDetalle.Reqde_Cantidad) + parseInt(t);
                        table.row.add([
                            CompraDetalle.prod_Text,
                            suma,
                            CompraDetalle.Reqde_Justificacion,
                            '<button id = "remove" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'
                        ]).draw(false)
                    }
                });
            }
            else {
               
                contador = contador + 1
                table.row.add([
                    CompraDetalle.prod_Text,
                    CompraDetalle.Reqde_Cantidad,
                    CompraDetalle.Reqde_Justificacion,
                    '<button id = "remove" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>'
                ]).draw(false)

            }
            document.getElementById('prod_Id').tabIndex = '';
            document.getElementById('Cantidad').value = '';
            document.getElementById('Reqde_Justificacion').value = '';
        });
    }
});
var prodtable = $('#tblBusquedaGenerica').DataTable();

// #myInput is a <input type="text"> element
$('#prodSearch').on('keyup', function () {
    console.log(this.value)
    prodtable.search(this.value).draw();
});
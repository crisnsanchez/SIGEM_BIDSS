/////DATE PICKER
$('#Lianvi_FechaInicioViaje,#Lianvi_FechaFinViaje,#Lianvi_FechaLiquida').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});
$("#CargarArchivo").change(function () {
    readURL(this);
});

//--------Obtiene los valores de los textbox y los agrega a la tabla(con Datatables)
function AgregarMunicipiosDT() {
    var Lianvide_Archivo = $('#Lianvide_Archivo').val();
    var tpv_Id = $('#tpv_Id').val();
    var Lianvide_FechaGasto = $('#Lianvide_FechaGasto').val();
    var Lianvide_MontoGasto = $('#Lianvide_MontoGasto').val();
    var LianvideArchivo = $('#Lianvide_Archivo').val();
    var tpvId = $('#tpv_Id').val();
    var LianvideFechaGasto = $('#Lianvide_FechaGasto').val();
    var LianvideMontoGasto = $('#Lianvide_MontoGasto').val();
    var table = $('#tblMunicipio').DataTable();
    _depCodigo = true;
    _depLength = true;
    _MunCodigo = true;
    _munLength = true;
    _munNombreLength = true;
    _MunNombre = true;
    _MunNombreSP = true;
    _DepNombre = true;


    //------------------------------Validaciones Departamento------------------------------
    //---------------------------------------------------------------------------------------
    //------------------------------Validaciones Codigo Departamento------------------------------

    if (Lianvide_Archivo == '') {
        $('#dep_CodigoCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Departamento Requerido</ul>');
        _depCodigo = false;
    }
    if (tpv_Id == '') {
        $('#dep_CodigoCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Departamento Requerido</ul>');
        _depCodigo = false;
    }
    if (Lianvide_FechaGasto == '') {
        $('#dep_CodigoCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Departamento Requerido</ul>');
        _depCodigo = false;
    }
    if (Lianvide_MontoGasto == '') {
        $('#dep_CodigoCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Departamento Requerido</ul>');
        _depCodigo = false;
    }
    else {
        $('#dep_CodigoCodigoError').text('');
    }

    if (dep_Codigo.length > 2) {
        $('#dep_CodigoLegthCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoLegthCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan mas de 2 números.</ul>');
        _depLength = false;
    } else {
        $('#dep_CodigoLegthCodigoError').text('');
    }
    if (_depCodigo && dep_Codigo.length < 2) {
        $('#dep_CodigoLegthLessCodigoError').text('');
        $('#ValidationDepCodigoCreate').after('<ul id="dep_CodigoLegthLessCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan menos de 2 números.</ul>');
        _depLength = false;
    } else {
        $('#dep_CodigoLegthLessCodigoError').text('');
    }
    //------------------------------Fin Validaciones Codigo Departamento------------------------------

    if (dep_Nombre == '') {
        $('#DepNombreNombreError').text('');
        $('#ValidationDepNombreCreate').after('<ul id="DepNombreNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Departamento Requerido</ul>');
        _DepNombre = false;
    } else {
        $('#DepNombreNombreError').text('');

    }
    //------------------------------Fin Validaciones Departamento------------------------------

    //------------------------------Validaciones Municipio------------------------------
    //---------------------------------------------------------------------------------------
    //------------------------------Validaciones Codigo Municipio------------------------------

    if (MunCodigo == '') {
        $('#MunCodigoCodigoError').text('');

        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Codigo Municipio Requerido</ul>');
        _MunCodigo = false;
    } else {
        $('#MunCodigoCodigoError').text('');
    }

    if (MunCodigo.length > 2) {
        $('#MunCodigoLegthMaxCodigoError').text('');
        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoLegthMaxCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan mas de 2 números.</ul>');
        _munLength = false;
    } else {
        $('#MunCodigoLegthCodigoError').text('');

    }
    if (_MunCodigo && MunCodigo.length < 2) {
        $('#MunCodigoLegthLessCodigoError').text('');
        $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoLegthLessCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se aceptan menos de 2 números.</ul>');
        _munLength = false;
    } else {
        $('#MunCodigoLegthLessCodigoError').text('');

    }
    //------------------------------Fin Validaciones Codigo Municipio------------------------------


    if (MunNombre == '') {
        $('#MunNombreNombreError').text('');
        $('#ValidationMunNombreCreate').after('<ul id="MunNombreNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Campo Municipio Requerido</ul>');
        _MunNombre = false;
    } else {
        $('#MunNombreNombreError').text('');

    }

    if (MunNombre.substring(0, 1) == " ") {
        $('#MunNombreSNNombreError').text('');
        $('#ValidationMunNombreCreate').after('<ul id="MunNombreSNNombreError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> No se Aceptan Espacios en blanco.</ul>');
        _munNombreLength = false;
    } else {
        $('#MunNombreSNNombreError').text('');
    }
    //------------------------------Fin Validaciones Municipio------------------------------



    //-----------------------------Validaciones de Municipio Existente------------------------------
    if ($('#tblMunicipio >tbody >tr').length > 0) {
        $('#tblMunicipio >tbody >tr').each(function () {
            _stringCodigoTable = $(this).find("td:eq(0)").text()

            _MunCodigoTabla = _stringCodigoTable.substring(2, 4)

            if (_MunCodigo && MunCodigo == _MunCodigoTabla) {
                $('#MunCodigoExistCodigoError').text('');
                $('#ValidationMunCodigoCreate').after('<ul id="MunCodigoExistCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Ya existe el código de municipio</ul>');
                _MunCodigo = false;
            } else if (_MunCodigo) {
                $('#MunCodigoExistCodigoError').text('');
            }

            _MunNombreTabla = $(this).find("td:eq(1)").text()
            if (_MunNombre && MunNombre == _MunNombreTabla) {
                $('#MunNombreExistCodigoError').text('');
                $('#ValidationMunNombreCreate').after('<ul id="MunNombreExistCodigoError" class="validation-summary-errors text-danger"><span class="fa fa-ban text-danger"></span> Ya existe el nombre del municipio</ul>');
                _MunNombre = false;
            } else if (_MunNombre) {
                $('#MunNombreExistCodigoError').text('');
            }
        })
    }




    if (!_depCodigo || !_depLength || !_DepNombre || !_MunCodigo || !_munLength || !_munNombreLength || !_MunNombre || !_MunNombreSP) {
        return false
    }
    else {

        $('#dep_Codigo').prop('readonly', true);
        $('#dep_Nombre').prop('readonly', true);
        $('#Valida').prop('disabled', false);

        var tbMunicipio = GetMunicipio();

        contador = contador + 1;
        table.row.add([
            tbMunicipio.mun_Codigo,
            tbMunicipio.mun_Nombre.toUpperCase().trim(),
            '<button id = "removeMunicipios" class= "btn btn-danger btn-xs eliminar" type = "button">Eliminar</button>',
        ]).draw(false);

        _ValuesTrue = { _Bool: true, _tbMunicipio: tbMunicipio }

        return _ValuesTrue
    }
}
S
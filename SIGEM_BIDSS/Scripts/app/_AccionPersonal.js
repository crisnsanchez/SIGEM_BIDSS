
$(function () {
    $('#container').hide();
    //$("#sol_Acper_Anterior").val('----');
    //$("#sol_Acper_Nuevo").val('----');
});


$("input[name*='tipmo_id']").change(function () {
    console.log("Entra")
    _TipoMovimiento = this.value
    console.log(_TipoMovimiento)
    if (_TipoMovimiento == 1) {
        $('#container').hide();
        $("#sol_Acper_Anterior").val('');
        $("#sol_Acper_Nuevo").val('');
        document.getElementById('lblsol_Acper_Anterior').innerHTML = 'Sueldo Anterior o Actual';
        document.getElementById('lblsol_Acper_Nuevo').innerHTML = 'Sueldo Nuevo';
        $('#container').show();
    }
    else if (_TipoMovimiento == 2) {
        $('#container').hide();
        $("#sol_Acper_Anterior").val('');
        $("#sol_Acper_Nuevo").val('');
        document.getElementById('lblsol_Acper_Anterior').innerHTML = 'Puesto Anterior';
        document.getElementById('lblsol_Acper_Nuevo').innerHTML = 'Puesto Nuevo';
        $('#container').show();
    }
    else {
        $('#container').hide();
        $("#sol_Acper_Anterior").val('----');
        $("#sol_Acper_Nuevo").val('----');
    }
});
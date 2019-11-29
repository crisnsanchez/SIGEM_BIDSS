//----------Datepicker
$('#sol_PerFechaInicio,#sol_PerFechaRegreso,#sol_GralFechaSolicitud,#sol_PerFechaMedioDia').datepicker({
    format: "dd/mm/yyyy",
    startDate: "01/01/1990",
    language: "es",
    daysOfWeekDisabled: "0"
});




document.getElementById('insf_Correo').addEventListener('input', function () {
    campo = event.target;
    valido = document.getElementById('emailOK');
    //selector = document.getElementById('emp_CorreoElectronico')
    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test(campo.value)) {
        valido.innerText = "";
        $('#emp_CorreoElectronico').removeClass('is-invalid');
    } else {
        valido.innerText = "Correo Inválido";
        $('#emp_CorreoElectronico').focus();
        $('#emp_CorreoElectronico').addClass('is-invalid');
    }
});




//HI

//    function ShowHideDiv(chkCantDias) {
//                var sol_PerMedioDia = document.getElementById("sol_PerFechaMedioDia");
//    sol_PerMedioDia.style.display = chkCantDias.checked ? "block" : "none";
//}
      
//    <label for="chkCantDias">

//        ¿Permiso Medio Dia?
//            <input type="checkbox" id="chkCantDias" onclick="ShowHideDiv(this)" />
//    </label>
//    <hr />
//    <div id="sol_PerFechaMedioDia" style="display: none">

//        @Html.LabelFor(model => model.sol_PerFechaMedioDia, htmlAttributes: new { @class = "control-label col-md-2" })
//            <div class="col-md-7">
//            @Html.EditorFor(model => model.sol_PerFechaMedioDia, new {htmlAttributes = new { @class = "form-control" } })
//                @Html.ValidationMessageFor(model => model.sol_PerFechaMedioDia, "", new { @class = "text-danger" })
//            </div>
//    </div>




//////SOLO LETRAS
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}



////SOLO NUMEROS 
function soloNumeros(e) {

    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[0-9]+$/.test(tecla);
}

///Validacion para no pegar caracteres
function Caracteres(string) {
    var out = '';
}
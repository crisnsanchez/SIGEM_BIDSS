var prodtable = $('#tblBusquedaGenerica').DataTable();





$(document).on("click", "#tblBusquedaGenerica tbody tr td button#seleccionar", function () {
    var prod_Text = $(this).closest('tr').find('td:eq(0)').text()
    var prod_Id = this.value;
    document.getElementById("prod_Id").value = prod_Id
    document.getElementById("prod_Text").value = prod_Text.trimStart().trimEnd()
});
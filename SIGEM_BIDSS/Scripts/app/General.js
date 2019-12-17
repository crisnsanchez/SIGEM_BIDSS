﻿//JS General para Pantalla Index

$(function () {
    //Inicializacion de la funcion
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });


    //Mensaje que mostrar segun funcion
    _Action = $('#vSwal').val();
    console.log(_Action)
    if (_Action == "Created") {
        console.log("Verdadero")

        Toast.fire({
            type: 'success',
            title: 'Se ha guardado con Éxito.'
        })
    }
    else if (_Action == "Edited") {
        Toast.fire({
            type: 'success',
            title: 'Se ha actualizado con Éxito.'
        })
    }
    else if (_Action == "Delete") {
        Toast.fire({
            type: 'warning',
            title: 'Se ha borrado con Éxito.'
        })
    }
    else if (_Action == "Dependencias") {
        Toast.fire({
            type: 'warning',
            title: 'Este municipio está siendo usado por otra tabla, no se puede Eliminar.'
        })
    }
    else if (_Action == "Rechazada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha rechazado con Éxito.'
        })
    }

    


    //Inicializacion de el Datatable
    $('#dataTable').DataTable(
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

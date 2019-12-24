//JS General para Pantalla Index

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
    else if (_Action == "ExisteCo") {
        Toast.fire({
            type: 'warning',
            title: 'Este nombre ya éxiste, por favor revise la tabla de Municipios.'
        })
    }
    else if (_Action == "ExisteNom") {
        Toast.fire({
            type: 'warning',
            title: 'Este nombre ya éxiste, por favor revise la tabla de Municipios.'
        })
    }
    else if (_Action == "Rechazada") {
        Toast.fire({
            type: 'success',
            title: 'Se ha rechazado con Éxito.'
        })
    }
});

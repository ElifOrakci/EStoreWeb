$(() => {
    $('.remove-btn').on('click', (evt) => {

        Swal.fire({
            icon: 'warning',
            title: 'UYARI',
            html: 'Bu kayýt <b>TAMAMEN</b> silinecektir. Devam etmek istiyor mususnuz?',
            confirmButtonText: '<i class="bi bi-trash text-danger"></i>SÝL',
            showCancelButton: true,
            cancelButtonText: 'ÝPTAL',
        })
            .then((result) => {
                if (result.isConfirmed) {
                    window.location = $(evt.currentTarget).attr('href');
                }

            });;
        return false;

    });

})
$(function () {
   
});


$("#inline_content input[name='typeAcPer']").click(function () {
    var meterId = $('input:radio[name=typeAcPer]:checked').val();
     if (meterId && meterId != '') {
        $.ajax({
            url: '_AccionPersonal',
            type: 'GET',
            cache: false,
            data: { meter_id: meterId }
        }).done(function (result) {
            console.log(result)

            $('#container').html(result);
        });
    }
});



//$('#typeAcPer:radio').change(function () {
//    var meterId = $("typeAcPer").val();
//    console.log(meterId);

//    //if (meterId && meterId != '') {
//    //    $.ajax({
//    //        url: '@Url.Action("MeterInfoPartial")',
//    //        type: 'GET',
//    //        cache: false,
//    //        data: { meter_id: meterId }
//    //    }).done(function (result) {
//    //        $('#container').html(result);
//    //    });
//    //}
//});
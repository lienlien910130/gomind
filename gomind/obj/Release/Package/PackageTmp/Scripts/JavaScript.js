
    $('#manageindex').click(function () {
        $.ajax({
            url: "/UserDatas/Edit",
            type: 'GET',
            success: function (result) {
                $('#userdata').html(result);
            }
        });
    });

    $('#editcancel').click(function () {
        $.ajax({
            url: "/UserDatas/Cancel",
            type: 'GET',
            success: function (result) {
                $('#userdata').html(result);
            }
        });
    });

    $('#salesettingindex').click(function () {
        $.ajax({
            url: "/SaleSettings/Edit",
            type: 'GET',
            data:{ id: item.id } ,
            success: function (result) {
                $('#returns').html(result);
            }
        });
    });


$(function () {
    $('.image-preview-clear').click(function () {
        $('.image-preview-filename').val("");
        $('.image-preview-clear').hide();
        $('.image-preview-input input:file').val("");
        $(".image-preview-input-title").text("選擇照片");
        $("#img1").attr("src", "");
    });
    $(".image-preview-input input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            $(".image-preview-input-title").text("更換照片");
            $(".image-preview-clear").show();
            img.attr('src', e.target.result);
            $("#img1").attr("src", e.target.result);
        }
        reader.readAsDataURL(file);
    });
    $('.image-preview-clear1').click(function () {
        $('.image-preview-filename1').val("");
        $('.image-preview-clear1').hide();
        $('.image-preview-input1 input:file').val("");
        $(".image-preview-input-title1").text("選擇照片");
        $("#img2").attr("src", "");
    });
    $(".image-preview-input1 input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            $(".image-preview-input-title1").text("更換照片");
            $(".image-preview-clear1").show();
            img.attr('src', e.target.result);
            $("#img2").attr("src", e.target.result);
        }
        reader.readAsDataURL(file);
    });

    $('.image-preview-clear2').click(function () {
        $('.image-preview-filename2').val("");
        $('.image-preview-clear2').hide();
        $('.image-preview-input2 input:file').val("");
        $(".image-preview-input-title2").text("選擇照片");
        $("#img3").attr("src", "");
    });
    $(".image-preview-input2 input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            $(".image-preview-input-title2").text("更換照片");
            $(".image-preview-clear2").show();
            img.attr('src', e.target.result);
            $("#img3").attr("src", e.target.result);
        }
        reader.readAsDataURL(file);
    });

    $('.image-preview-clear3').click(function () {
        $('.image-preview-filename3').val("");
        $('.image-preview-clear3').hide();
        $('.image-preview-input3 input:file').val("");
        $(".image-preview-input-title3").text("選擇照片");
        $("#img4").attr("src", "");
    });
    $(".image-preview-input3 input:file").change(function () {
        var img = $('<img/>', {
            id: 'dynamic',
            width: 250,
            height: 200
        });
        var file = this.files[0];
        var reader = new FileReader();
        reader.onload = function (e) {
            $(".image-preview-input-title3").text("更換照片");
            $(".image-preview-clear3").show();
            img.attr('src', e.target.result);
            $("#img4").attr("src", e.target.result);
        }
        reader.readAsDataURL(file);
    });
});
var Avaterjcrop_api;




$(document).ready(
   function () {
       $('#fileUploadAvaterImage').on('change', function () {

           $("#mdAvaterImage").modal("show");
           $(".divAvaterImageLoader").show();
           $("#divAvaterImage").hide();

           var data = new FormData();
           var files = $("#fileUploadAvaterImage").get(0).files;
           if (files.length > 0) {
               data.append("UploadedImage", files[0]);
           }

           var ajaxRequest = $.ajax({
               type: "POST",
               url: "/api/FileUpload/UploadImageAvater",
               contentType: false,
               processData: false,
               data: data
           });


           ajaxRequest.done(function (data, xhr, textStatus) {
               if (data.substr(0, 3) === '#E#') {
                   ShowMessage('dvAvaterErrorMessage', data.replace('#E#', ''));
                   $(".divAvaterImageLoader").hide();
                   $("#btnUpdateAvaterImage").hide();
                   return;
               }
               $("#btnUpdateAvaterImage").show();

               InitCropAvater(data);
               $(".divAvaterImageLoader").hide();
               $("#divAvaterImage").show();
           });





       });

      

   }
 );


function InitCropAvater(imageurl) {

    if (typeof Avaterjcrop_api !== "undefined") {
        Avaterjcrop_api.destroy();
    }
    //$('#imgAvaterImage').removeAttr("style");
    $('#imgAvaterImage').attr("src", imageurl);


    $('#imgAvaterImage').Jcrop({
        setSelect: [0, 0, AvaterimageMinW, AvaterimageMinH],
        allowResize: false,
        keySupport: false,
        minSize: [AvaterimageMinW, AvaterimageMinH],
        maxSize: [AvaterimageMaxW, AvaterimageMaxH],
        onChange: AvaterstoreCoords
    }, function () {
        Avaterjcrop_api = this;
    });

}

function AvaterstoreCoords(c) {

    $('#AX').val(parseInt(c.x));
    $('#AY').val(parseInt(c.y));
    $('#AW').val(parseInt(c.w));
    $('#AH').val(parseInt(c.h));
};



function CoverstoreCoords(c) {

    $('#CX').val(parseInt(c.x));
    $('#CY').val(parseInt(c.y));
    $('#CW').val(parseInt(c.w));
    $('#CH').val(parseInt(c.h));
};
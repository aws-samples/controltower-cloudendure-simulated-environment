$(document).ready(function (){
    $("form#formData").on("submit", function(event){
        event.preventDefault();
        $.post($(this).attr("action"), $(this).serialize())
            .done(function (data){
                if($("form#formData").attr("action-callback") != null){
                    var func = window[$("form#formData").attr("action-callback")];
                    var callback = $.Callbacks();
                    callback.add(func);
                    callback.fire(data);
                }
            });
    });
})
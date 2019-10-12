$(document).ready(function (){
    $("#btnMorePopup").on("click", function(){
        var newPage = parseInt($("#pageIndexPopup").val(), 10) + 1;
        $("#pnlmorePopup").html("<i class=\"fa fa-refresh fa-spin\" style=\"font-size:24px\"></i>");
        $(this).find("i").addClass("fa-refresh fa-spin");
        $.ajax({
            type: "GET",
            url: $('#tableContentsPopup').attr("action-data"),
            data: { sortOrder: $("#sortOrderPopup").val(), searchString: $("#searchStringPopup").val(), page: newPage, isPopup: true },
            dataType: "html", 
            success: function(response){
                $('#lineMorePopup').remove();
                $('#tableContentsPopup').append(response);
            }
        });
    });
})
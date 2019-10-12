$(document).ready(function (){
    $("#btnMore").on("click", function(){
        var newPage = parseInt($("#pageIndex").val(), 10) + 1;
        $("#pnlmore").html("<i class=\"fa fa-refresh fa-spin\" style=\"font-size:24px\"></i>");
        $(this).find("i").addClass("fa-refresh fa-spin");
        $.ajax({
            type: "GET",
            url: $('#tableContents').attr("action-data"),
            data: { sortOrder: $("#sortOrder").val(), searchString: $("#searchString").val(), page: newPage },
            dataType: "html", 
            success: function(response){
                $('#lineMore').remove();
                $('#tableContents').append(response);
            }
        });
    });
})
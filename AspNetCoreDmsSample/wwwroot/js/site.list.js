//List of records - Index Pages
$(document).ready(function (){
    $.ajax({
        type: "GET",
        url: $('#tableContents').attr("action-data"),
        data: { sortOrder: "", searchString: $("#searchString").val(), page: 1 },
        dataType: "html", 
        success: function(response){
            $('#tableContents').html(response);
        }
    });
    $("#dataTable > thead > tr > th").find("a").on("click", function(){
        $("#sortOrder").val(($(this).attr("sort-direction") == "") ? $(this).attr("sort-order") : $(this).attr("sort-order") + "_" + $(this).attr("sort-direction"));
        $("#tableContents").html("<tr><td colspan=\"3\" class=\"text-center\"><i class=\"fa fa-refresh fa-spin\" style=\"font-size:24px\"></i></td></tr>");
        $.ajax({
            type: "GET", 
            url: $(this).attr("action"),
            data: { sortOrder: $("#sortOrder").val(), searchString: $("#searchString").val(), page: 1 },
            dataType: "html",
            success: function(response){
                $('#tableContents').html(response);
            }
        });
        $(this).attr("sort-direction",  $(this).attr("sort-direction") == "" ? "desc" : "");
    });
})
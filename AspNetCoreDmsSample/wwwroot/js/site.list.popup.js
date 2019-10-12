//List of records - Index Pages
$(document).ready(function (){
    $.ajax({
        type: "GET",
        url: $('#tableContentsPopup').attr("action-data"),
        data: { sortOrder: "", searchString: $("#searchString").val(), page: 1 , isPopup: true},
        dataType: "html", 
        success: function(response){
            $('#tableContentsPopup').html(response);
        }
    });
    $("#dataTablePopup > thead > tr > th").find("a").on("click", function(){
        $("#sortOrderPopup").val(($(this).attr("sort-direction") == "") ? $(this).attr("sort-order") : $(this).attr("sort-order") + "_" + $(this).attr("sort-direction"));
        $("#tableContentsPopup").html("<tr><td colspan=\"3\" class=\"text-center\"><i class=\"fa fa-refresh fa-spin\" style=\"font-size:24px\"></i></td></tr>");
        $.ajax({
            type: "GET", 
            url: $(this).attr("action"),
            data: { sortOrder: $("#sortOrderPopup").val(), searchString: $("#searchStringPopup").val(), page: 1, isPopup: true},
            dataType: "html",
            success: function(response){
                $('#tableContentsPopup').html(response);
            }
        });
        $(this).attr("sort-direction",  $(this).attr("sort-direction") == "" ? "desc" : "");
    });
})
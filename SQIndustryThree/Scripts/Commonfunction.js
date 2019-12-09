function LoadBusinessUnit(bunit) {
    $(bunit).css({ "border-color": "#d3d3d3" });
    var urlpath = '@Url.Action("LoadBusinessUnit", "CapexApproval")';
    $.ajax({
        url: urlpath,
        dataType: 'json',
        type: "Post",
        data: {},
        async: true,
        success: function (data) {
            $(bunit).empty();
            $(bunit).append("<option value='0'>--Select Business Unit--</option>");
            for (var i = 0; i < data.length; i++) {
                $(bunit).append($("<option></option>").val(data[i].BusinessUnitId)
                    .html(data[i].BusinessUnitName));
            }
        }
    });
}
function LoadCapexCatagory(ccat) {
    $(ccat).css({ "border-color": "#d3d3d3" });
    var urlpath = '@Url.Action("LoadCapexCatagory", "CapexApproval")';
    $.ajax({
        url: urlpath,
        dataType: 'json',
        type: "Post",
        data: {},
        async: true,
        success: function (data) {
            $(ccat).empty();
            $(ccat).append("<option value='0'>--Select Capex Catagory--</option>");
            for (var i = 0; i < data.length; i++) {
                $(ccat).append($("<option></option>").val(data[i].CapexCatagoryID)
                    .html(data[i].CapexCatagoryName));
            }
        }
    });
}
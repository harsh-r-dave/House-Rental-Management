var Hrm = Hrm || {};

Hrm.AdminHouses = function () {
    var viewModel;

    var initDataTables = function () {
        $('#house-list-table').dataTable({
            "sDom": '<"top"f>rt<"bottom"lp><"clear">',
            "columnDefs": [{
                "targets": [2],
                "orderable": false
            }]
        });
    };

    var initPage = function () {
        initDataTables();
    };

    var init = function (model) {
        console.log("Hrm.AdminHouses init()");
        viewModel = model;

        initPage();
    };

    var oPublic = {
        init: init
    }
    return oPublic;
}();
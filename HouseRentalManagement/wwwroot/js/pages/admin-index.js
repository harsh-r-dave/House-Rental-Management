var Hrm = Hrm || {};

Hrm.AdminIndex = function () {
    var viewModel;

    var initDataTables = function () {
        $('#house-list-table').dataTable();
    };

    var initPage = function () {
        initDataTables();
    };

    var init = function (model) {
        console.log("Hrm.AdminIndex init()");
        viewModel = model;

        initPage();
    };

    var oPublic = {
        init: init
    }
    return oPublic;
}();
var Hrm = Hrm || {};

Hrm.AdminManageFacility = function () {
    var viewModel;

    var initDataTables = function () {
        $('#facility-list-table').dataTable({
            "sDom": '<"top"f>rt<"bottom"lp><"clear">',
            "columnDefs": [{
                "targets": [1],
                "orderable": false
            }]
        });
    };

    var updateFacility = function (id, title) {
        $('#update-facility-form #FacilityId').val(id);
        $('#update-facility-form #FacilityTitle').val(title);
        console.log('id:' + id + ' :: title: ' + title);

        $('#update-facility-modal').modal('show');
    };

    var initPage = function () {
        initDataTables();
    };

    var init = function (model) {
        console.log("Hrm.AdminManageFacility init()");

        viewModel = model;

        initPage();
    };

    var oPublic = {
        init: init,
        updateFacility: updateFacility
    };
    return oPublic;
}();
var Hrm = Hrm || {};

Hrm.AdminAddHouse = function () {
    var viewModel;

    var initDatePickers = function () {
        $(viewModel.dateAvailableFromSelector).datepicker({
            'autoclose': true,
            startDate: 'today',
            todayBtn: true,
            todayHighlight: true,
            startView: "months"
        }).on('changeDate', function () {
            $(viewModel.dateAvailableToSelector).datepicker('setStartDate', $(viewModel.dateAvailableFromSelector).val());
        });

        $(viewModel.dateAvailableToSelector).datepicker({
            'autoclose': true,
            startDate: 'today',
            todayBtn: true,
            todayHighlight: true,
            startView: "months"
        }).on('changeDate', function () {
            $(viewModel.dateAvailableFromSelector).datepicker('setEndDate', $(viewModel.dateAvailableToSelector).val());
        });


    };

    var initPage = function () {
		initDatePickers();
    };

    var init = function (model) {
        console.log("Hrm.AdminAddHouse");

        viewModel = model;

        initPage();
    };

    var oPublic = {
        init: init
    };
    return oPublic;
}();
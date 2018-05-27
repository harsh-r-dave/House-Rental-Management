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

	var deleteHouse = function (id) {
		var link = '/Admin/DeleteHouse/' + id;
		swal({
			title: 'Are you sure?',
			text: "You won't be able to revert this!",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#337ab7',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, delete it!'
		}).then(function (result) {
			if (result.value) {
				window.location.href = link;
			}
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
		init: init,
		deleteHouse: deleteHouse
    }
    return oPublic;
}();
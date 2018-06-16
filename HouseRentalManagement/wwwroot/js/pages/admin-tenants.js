var Hrm = Hrm || {};

Hrm.AdminTenants = function () {
	var viewModel;

	var initDataTables = function () {
		$('#tenant-list-table').dataTable({
			"sDom": '<"top"f>rt<"bottom"lp><"clear">',
			"columnDefs": [{
				"targets": [-1],
				"orderable": false
			}]
		});
	};

	var deleteTenant = function (id) {
		var link = '/Admin/DeleteTenant/' + id;
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
		console.log("Hrm.AdminTenants init()");
		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		deleteTenant: deleteTenant
	}
	return oPublic;
}();
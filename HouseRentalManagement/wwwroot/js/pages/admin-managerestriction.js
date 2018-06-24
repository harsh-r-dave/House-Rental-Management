var Hrm = Hrm || {};

Hrm.AdminManageRestriction = function () {
	var viewModel;

	var initDataTables = function () {
		$('#restriction-list-table').dataTable({
			"sDom": '<"top"f>rt<"bottom"lp><"clear">',
			"columnDefs": [{
				"targets": [1],
				"orderable": false
			}]
		});
	};

	var initUpdateRestriction = function (id, title) {
		$('#update-restriction-form #RestrictionId').val(id);
		$('#update-restriction-form #RestrictionTitle').val(title);

		$('#update-restriction-modal').modal('show');
	};

	var deleteRestriction = function (id) {
		var link = '/Admin/DeleteRestriction/' + id;
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

	var initModals = function () {
		$('#add-restriction-modal').on('hidden.bs.modal', function () {
			$('#add-restriction-modal').find('.field-validation-error').html('');
			$('#add-restriction-modal').find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
		});
		$('#update-restriction-modal').on('hidden.bs.modal', function () {
			$('#update-restriction-modal').find('.field-validation-error').html('');
			$('#update-restriction-modal').find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
		});
	}

	var initPage = function () {
		initDataTables();
		initModals();
	};

	var init = function (model) {
		console.log("Hrm.AdminManageRestriction init()");

		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		initUpdateRestriction: initUpdateRestriction,
		deleteRestriction: deleteRestriction
	};
	return oPublic;
}();
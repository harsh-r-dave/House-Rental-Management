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

	var initUpdateFacility = function (id, title) {
		$('#update-facility-form #FacilityId').val(id);
		$('#update-facility-form #FacilityTitle').val(title);
		console.log('id:' + id + ' :: title: ' + title);

		$('#update-facility-modal').modal('show');
	};

	var deleteFacility = function (id) {
		var link = '/Admin/DeleteFacility/' + id;
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
		$('#add-facility-modal').on('hidden.bs.modal', function () {
			$('#add-facility-modal').find('.field-validation-error').html('');
			$('#add-facility-modal').find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
		});
		$('#update-facility-modal').on('hidden.bs.modal', function () {
			$('#update-facility-modal').find('.field-validation-error').html('');
			$('#update-facility-modal').find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
		});
	}

	var initPage = function () {
		initDataTables();
		initModals();
	};

	var init = function (model) {
		console.log("Hrm.AdminManageFacility init()");

		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		initUpdateFacility: initUpdateFacility,
		deleteFacility: deleteFacility
	};
	return oPublic;
}();
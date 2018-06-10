var Hrm = Hrm || {};

Hrm.AdminEditHouse = function () {
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

	var initDropify = function () {
		$('#house-image-uploader').dropify();
	};

	var loadAmenity = function () {
		$.ajax({
			url: "/Admin/GetHouseAmenities",
			data: {
				houseId: viewModel.houseId
			},
			method: 'get'
		}).done(function (data) {
			$('#amenity-info').html(data);
		}).fail(function (a, textError) {
			console.error(textError);
		});
	};

	var clearCheckboxes = function () {
		$('input:checkbox').removeAttr('checked');
	};
	var initClearCheckboxes = function () {
		$('#amenity-info').on('click', '#clear-checkbox', function () {
			clearCheckboxes();
		});
	};

	var uploadHouseImage = function () {
		$('#upload-image-form').on('submit', function (e) {
			e.preventDefault();
			$.ajax({
				url: "/Admin/UploadHouseImage",
				type: "POST",
				data: new FormData(this),
				contentType: false,
				cache: false,
				processData: false,
				success: function (data) {
					console.log(data);
				}
			});
		});
	};
	var initUploadHouseImage = function () {
		$('#upload-image-button').on('click', function () {
			uploadHouseImage();
		});
	};

	var initPage = function () {
		initDatePickers();
		initDropify();
		loadAmenity();
		initClearCheckboxes();

		uploadHouseImage();
	};

	var init = function (model) {
		console.log("Hrm.AdminEditHouse");

		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		loadAmenity: loadAmenity
	};
	return oPublic;
}();
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
		$('.house-image-uploader').dropify();
	};

	var initFancybox = function () {
		$('a.house-image-preview').fancybox();
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

	var loadHouseImages = function () {
		$.ajax({
			url: "/Admin/GetHouseImages",
			type: "get",
			data: {
				houseId: $('#upload-image-form #HouseId').val()
			},
			success: function (result) {
				if (result.hasOwnProperty('success') && !result.success) {
					if (result.hasOwnProperty('noImage') && result.noImage) {
						toastr.info(result.error, '', Hrm.Toastr.tipConfig);
						$('#house-image-list-partial-container').html("<div class='text-danger mt-10'>This house doesn't have any photos, please consider uploading some photos for this house.</div>");
					} else {
						toastr.error(result.error, '', Hrm.Toastr.config);
						$('#house-image-list-partial-container').html('Failed to load images');
					}
				}
				else {
					$('#house-image-list-partial-container').html(result);
					initFancybox();
				}
			}
		});
	};

	var uploadHouseImage = function () {
		$('#upload-image-form').on('submit', function (e) {
			e.preventDefault();
			$('#upload-spinner').show();
			$.ajax({
				url: "/Admin/UploadHouseImage",
				type: "POST",
				data: new FormData(this),
				contentType: false,
				cache: false,
				processData: false,
				success: function (data) {
					if (data) {
						$('.dropify-clear').trigger('click');
						$('#IsHomePageImage').prop('checked', false);
						loadHouseImages();
						toastr.success("Image has been uploaded successfully", "", Hrm.Toastr.config);						
					}
					else {
						toastr.error("Something went wrong while uploading image", '', Hrm.Toastr.config);
					}
					$('#upload-spinner').hide();
				}
			});
		});
	};
	var initUploadHouseImage = function () {
		$('#upload-image-button').on('click', function () {
			uploadHouseImage();
		});
	};

	var initHouseImageLoader = function () {
		setTimeout(function () {
			loadHouseImages();
		}, 2500);
	};


	var deleteImage = function (imageId) {
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
				$.ajax({
					url: "/Admin/DeleteHouseImage",
					method: "post",
					data: {
						imageId: imageId
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Image deleted successfully', '', Hrm.Toastr.config);
						$('#house-image-list-partial-container').html('<h4>Loading Images <i class="fa fa-spinner fa-spin"></i></h4>');
						loadHouseImages();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error("Something went wrong while deleting image", '', Hrm.Toastr.config);
				});
			}
		});
	};

	var setMainImage = function (houseId, imageId) {
		$(this).text('loading');
		$.ajax({
			url: "/Admin/SetMainHouseImage",
			method: "post",
			data: {
				houseId: houseId,
				imageId: imageId
			}
		}).done(function (result) {
			if (result.success) {
				toastr.success('Main image set successfully', '', Hrm.Toastr.config);
				$('#house-image-list-partial-container').html('<h4>Loading Images <i class="fa fa-spinner fa-spin"></i></h4>');
				loadHouseImages();
			}
			else {
				toastr.error(result.error, '', Hrm.Toastr.config);
			}
		}).fail(function (jqXHR, textStatus) {
			console.log(textStatus);
			toastr.error("Something went wrong while setting image", '', Hrm.Toastr.config);
		});
	};

	var initPage = function () {
		initDatePickers();
		initDropify();
		loadAmenity();
		initClearCheckboxes();
		initHouseImageLoader();
		uploadHouseImage();
		initFancybox();
	};

	var init = function (model) {
		console.log("Hrm.AdminEditHouse");

		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		loadAmenity: loadAmenity,
		deleteImage: deleteImage,
		setMainImage: setMainImage
	};
	return oPublic;
}();
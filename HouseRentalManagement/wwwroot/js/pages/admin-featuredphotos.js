var Hrm = Hrm || {};

Hrm.AdminFeaturedPhotos = function () {
	var viewModel;

	var initDropify = function () {
		$('.featured-photo-uploader').dropify();
	};

	var initTooltip = function () {
		$('[data-toggle="tooltip"]').tooltip()
	};

	var loadFeaturedPhotos = function () {
		$.ajax({
			url: "/Admin/ListFeaturedPhotos",
			type: "get",
			success: function (result) {
				if (result.hasOwnProperty('success') && !result.success) {
					toastr.error(result.error, '', Hrm.Toastr.config);
					$('#featured-photo-list-partial-container').html('Failed to load images');
				}
				else {
					$('#featured-photo-list-partial-container').html(result);
				}
			}
		});
	};

	var uploadFeaturedPhoto = function () {
		$('#upload-featured-photo-form').off().on('submit', function (e) {
			e.preventDefault();
			var allValid = true;
			if (!$('#Image').valid()) {
				allValid = false;
			}
			if (allValid) {
				$('#upload-spinner').show();
				$.ajax({
					url: "/Admin/UploadFeaturedPhoto",
					type: "POST",
					data: new FormData(this),
					contentType: false,
					cache: false,
					processData: false,
					success: function (data) {
						if (data) {
							$('.dropify-clear').trigger('click');
							loadFeaturedPhotos();
							toastr.success("Photo has been uploaded successfully", "", Hrm.Toastr.config);
						}
						else {
							toastr.error("Something went wrong while uploading photo", '', Hrm.Toastr.config);
						}
						$('#upload-spinner').hide();
					}
				});
			}
		});
	};
	var initUploadFeaturedPhoto = function () {
		$('#upload-photo-button').off().on('click', function () {
			uploadFeaturedPhoto();
		});
	};

	var deleteFeaturedPhoto = function (id) {
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
					url: "/Admin/DeleteFeaturedPhoto",
					method: "post",
					data: {
						imageId: id
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success("Photo deleted successfully", '', Hrm.Toastr.config);
						loadFeaturedPhotos();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error("Something went wrong while deleting photo", '', Hrm.Toastr.config);
				});
			}
		});
	};

	var initPage = function () {
		initDropify();
		initTooltip();
		initUploadFeaturedPhoto();
		loadFeaturedPhotos();
	};

	var init = function (model) {
		console.log("Hrm.AdminFeaturedPhotos init()");
		viewModel = model;
		initPage();
	}

	var oPublic = {
		init: init,
		deleteFeaturedPhoto: deleteFeaturedPhoto
	};
	return oPublic;
}();
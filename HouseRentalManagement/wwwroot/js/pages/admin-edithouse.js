var Hrm = Hrm || {};

Hrm.AdminEditHouse = function () {
	var viewModel;

	var initDatePickers = function () {
		$(viewModel.dateAvailableFromSelector).datepicker({
			'autoclose': true,
			startDate: 'today',
			todayBtn: true,
			todayHighlight: true,
			startView: "months",
			format: "M-dd-yyyy"
		}).on('changeDate', function () {
			$(viewModel.dateAvailableToSelector).datepicker('setStartDate', $(viewModel.dateAvailableFromSelector).val());
		});

		//$(viewModel.dateAvailableToSelector).datepicker({
		//	'autoclose': true,
		//	startDate: 'today',
		//	todayBtn: true,
		//	todayHighlight: true,
		//	startView: "months",
		//	format: "M/dd/yyyy"
		//}).on('changeDate', function () {
		//	$(viewModel.dateAvailableFromSelector).datepicker('setEndDate', $(viewModel.dateAvailableToSelector).val());
		//});
	};

	var initDropify = function () {
		$('.house-image-uploader').dropify();
	};

	var initFancybox = function () {
		$('a.house-image-preview').fancybox();
	};

	var initTooltip = function () {
		$('[data-toggle="tooltip"]').tooltip()
	};

	// house amenity
	var loadAmenity = function () {
		$('#amenity-tab-loader').show();
		$.ajax({
			url: "/Admin/GetHouseAmenities",
			data: {
				houseId: viewModel.houseId
			},
			method: 'get'
		}).done(function (data) {
			$('#amenity-info').html(data);
			$('#amenity-tab-loader').hide();
		}).fail(function (a, textError) {
			console.error(textError);
			$('#amenity-tab-loader').hide();
		});
	};

	var clearCheckboxes = function () {
		$('#amenity-info').find('input:checkbox').removeAttr('checked');
	};
	var initClearCheckboxes = function () {
		$('#amenity-info').off().on('click', '#clear-checkbox', function () {
			clearCheckboxes();
		});
	};
	// ^house amenity

	// house images
	var loadHouseImages = function () {
		$('#photos-tab-loader').show();
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
				$('#photos-tab-loader').hide();
			}
		});
	};

	var uploadHouseImage = function () {
		$('#upload-image-form').on('submit', function (e) {
			e.preventDefault();
			var allValid = true;
			if (!$('#Image').valid()) {
				allValid = false;
			}
			if (allValid) {
				$('#upload-spinner').show();				
				$.ajax({
					url: "/Admin/UploadHouseImage",
					type: "POST",
					data: new FormData(this),
					contentType: false,
					cache: false,
					processData: false,
					success: function (data) {
						if (data.success) {
							$('.dropify-clear').trigger('click');
							$('#IsHomePageImage').prop('checked', false);
							loadHouseImages();
							toastr.success("Image has been uploaded successfully", "", Hrm.Toastr.config);
						}
						else {
							if (data.hasOwnProperty("fileExist") && data.fileExist) {
								swal({
									title: "Filename Already Exists",
									text: "Do you want to override the file?",
									type: 'warning',
									showCancelButton: true,
									confirmButtonColor: '#337ab7',
									cancelButtonColor: '#d33',
									confirmButtonText: 'Yes, override it!'
								}).then(function (result) {
									if (result.value) {
										// set override to true if permitted
										$('#upload-image-form').find('#Override').val(true);
										// submit form again
										$('#upload-image-form').submit();
									}
								});
							}
							else {
								toastr.error("Something went wrong while uploading image", '', Hrm.Toastr.config);
							}
						}
						$('#upload-spinner').hide();
						$('#upload-image-form').find('#Override').val(false);	// set override to false once upload is done
					}
				});
			}
		});
	};
	var initUploadHouseImage = function () {
		$('#upload-image-button').off().on('click', function () {
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
	// ^house images

	// house getting around
	var loadHouseGettingAround = function () {
		$('#getting-around-tab-loader').show();
		$.ajax({
			url: "/Admin/GetHouseGettingAround",
			type: "get",
			data: {
				houseId: viewModel.houseId
			},
			success: function (result) {
				if (result.hasOwnProperty('success') && !result.success) {
					toastr.error(result.error, '', Hrm.Toastr.config);
					$('#house-getting-around-container').html('Failed to load data');
				}
				else {
					$('#house-getting-around-container').html(result);
				}
				$('#getting-around-tab-loader').hide();
			}
		});
	};
	var saveHouseGettingAround = function () {
		$('#add-getting-around-form').off().on('submit', function (e) {
			e.preventDefault();
			var allValid = true;
			if (!$('#Distance').valid()) {
				allValid = false;
			}
			if (!$('#Location').valid()) {
				allValid = false;
			}
			if (allValid) {
				$('#save-getting-around-loader').show();
				$.ajax({
					url: "/Admin/AddHouseGettingAround",
					type: "POST",
					data: new FormData(this),
					contentType: false,
					cache: false,
					processData: false,
					success: function (data) {
						if (data) {
							loadHouseGettingAround();
							toastr.success("Info has been saved successfully", "", Hrm.Toastr.config);
							$('#add-getting-around-form').trigger('reset');
						}
						else {
							toastr.error("Something went wrong while saving info", '', Hrm.Toastr.config);
						}
						$('#save-getting-around-loader').hide();
					}
				});
			}
		});
	};
	var initSaveHouseGettingAround = function () {
		$('#save-getting-around').off().on('click', function () {
			saveHouseGettingAround();
		});
	};
	var deleteHouseGettingAround = function (id) {
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
					url: "/Admin/DeleteHouseGettingAround",
					method: "post",
					data: {
						houseGettingAroundId: id
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Info deleted successfully', '', Hrm.Toastr.config);
						$('#house-getting-around-container').html('<h4>Loading Images <i class="fa fa-spinner fa-spin"></i></h4>');
						loadHouseGettingAround();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error("Something went wrong while deleting info", '', Hrm.Toastr.config);
				});
			}
		});
	};
	// ^house getting around

	// house tenants
	var loadHouseTenants = function () {
		$('#tenant-tab-loader').show();
		$.ajax({
			url: "/Admin/GetHouseTenants",
			type: "get",
			data: {
				houseId: viewModel.houseId
			},
			success: function (result) {
				if (result.hasOwnProperty('success') && !result.success) {
					if (result.hasOwnProperty('noTenants') && result.noTenants) {
						toastr.info(result.error, '', Hrm.Toastr.tipConfig);
						$('#tenant-list-container').html("<div class='text-danger mt-10'>This house doesn't have any tenants.</div>");
					} else {
						toastr.error(result.error, '', Hrm.Toastr.config);
						$('#tenant-list-container').html('Failed to load tenants');
					}
				}
				else {
					$('#tenant-list-container').html(result);
				}
				$('#tenant-tab-loader').hide();
			}
		});
	};
	var fetchTenantDropDownOptions = function () {
		$.ajax({
			url: "/Admin/GetTenantListForHouseEditPage",
			method: "post",
			data: {
				houseId: viewModel.houseId
			}
		}).done(function (result) {
			$('#add-tenant-dropdown').children('option:not(:first)').remove();
			$.each(result.list, function () {
				$('#add-tenant-dropdown').append($("<option />").val(this.tenantId).text(this.fullName));
			});
		}).fail(function (jqXHR, textStatus) {
			console.log(textStatus);
		});

	}; var fetchWaitListTenantDropDownOptions = function () {
		$.ajax({
			url: "/Admin/GetTenantWaitList",
			method: "post"
		}).done(function (result) {
			$('#add-waitlist-tenant-dropdown').children('option:not(:first)').remove();
			$.each(result.list, function () {
				$('#add-waitlist-tenant-dropdown').append($("<option />").val(this.tenantId).text(this.fullName));
			});
		}).fail(function (jqXHR, textStatus) {
			console.log(textStatus);
		});
	};
	///// tenant without waitlist
	var setTenantDropDownError = function () {
		$('#add-tenant-dropdown').css('border', '1px solid red');
		$('#add-tenant-dropdown-error').show();
	};
	var clearTenantDropDownError = function () {
		$('#add-tenant-dropdown').css('border', '');
		$('#add-tenant-dropdown-error').hide();
	};
	var initTenantDropDown = function () {
		$('#add-tenant-dropdown').on('change', function () { clearTenantDropDownError(); });
		$('#add-tenant-dropdown-error').hide();
	};
	var initAddTenantModal = function () {
		$('#add-tenant-modal').on('hide.bs.modal', function () { clearTenantDropDownError(); });
	};
	var initAddTenant = function () {
		$('#add-tenant-button').on('click', function () {
			var allValid = true;
			if ($('#add-tenant-dropdown').val() === '' || $('#add-tenant-dropdown').val() === null
				|| $('#add-tenant-dropdown').val() === undefined) {
				allValid = false;
				setTenantDropDownError();
			}
			else {
				clearTenantDropDownError();
			}
			if (allValid) {
				$('#add-tenant-loader').show();
				$.ajax({
					url: "/Admin/AddTenantToHouse",
					method: "post",
					data: {
						houseId: viewModel.houseId,
						tenantId: $('#add-tenant-dropdown').val()
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Tenant added successfully.', '', Hrm.Toastr.config);
						loadHouseTenants();
						fetchTenantDropDownOptions();
						fetchWaitListTenantDropDownOptions();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
					$('#add-tenant-modal').modal('hide');
					$('#add-tenant-loader').hide();
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error('Something went wrong while processing your request.', '', Hrm.Toastr.config);
					$('#add-tenant-loader').hide();
				});
			}
		});
	};
	///// ^tenant without waitlist
	///// wait list tenants	
	var setWaitListTenantDropDownError = function () {
		$('#add-waitlist-tenant-dropdown').css('border', '1px solid red');
		$('#add-waitlist-tenant-dropdown-error').show();
	};
	var clearWaitListTenantDropDownError = function () {
		$('#add-waitlist-tenant-dropdown').css('border', '');
		$('#add-waitlist-tenant-dropdown-error').hide();
	};
	var initWaitListTenantDropDown = function () {
		$('#add-waitlist-tenant-dropdown').on('change', function () { clearWaitListTenantDropDownError(); });
		$('#add-waitlist-tenant-dropdown-error').hide();
	};
	var initWaitListAddTenantModal = function () {
		$('#add-waitlist-tenant-modal').on('hide.bs.modal', function () { clearWaitListTenantDropDownError(); });
	};
	var initWaitListAddTenant = function () {
		$('#add-waitlist-tenant-button').on('click', function () {
			var allValid = true;
			if ($('#add-waitlist-tenant-dropdown').val() === '' || $('#add-waitlist-tenant-dropdown').val() === null
				|| $('#add-waitlist-tenant-dropdown').val() === undefined) {
				allValid = false;
				setWaitListTenantDropDownError();
			}
			else {
				clearWaitListTenantDropDownError();
			}
			if (allValid) {
				$('#add-waitlist-tenant-loader').show();
				$.ajax({
					url: "/Admin/AddTenantToHouse",
					method: "post",
					data: {
						houseId: viewModel.houseId,
						tenantId: $('#add-waitlist-tenant-dropdown').val()
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Tenant added successfully.', '', Hrm.Toastr.config);
						loadHouseTenants();
						fetchTenantDropDownOptions();
						fetchWaitListTenantDropDownOptions();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
					$('#add-waitlist-tenant-modal').modal('hide');
					$('#add-waitlist-tenant-loader').hide();
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error('Something went wrong while processing your request.', '', Hrm.Toastr.config);
					$('#add-waitlist-tenant-loader').hide();
				});
			}
		});
	};
	///// ^ wait list tenants
	var removeTenant = function (tenantId) {
		swal({
			title: 'Are you sure?',
			text: "Remove tenant from this house!",
			type: 'warning',
			showCancelButton: true,
			confirmButtonColor: '#337ab7',
			cancelButtonColor: '#d33',
			confirmButtonText: 'Yes, remove!'
		}).then(function (result) {
			if (result.value) {
				$.ajax({
					url: "/Admin/RemoveTenantFromHouse",
					method: "post",
					data: {
						tenantId: tenantId
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Tenant successfully removed from this house', '', Hrm.Toastr.config);
						loadHouseTenants();
						fetchTenantDropDownOptions();
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					toastr.error('Something went wrong while processing your request', '', Hrm.Toastr.config);
				});
			}
		});
	};
	// ^house tenants

	// house facility
	var loadFacility = function () {
		$('#facility-tab-loader').show();
		$.ajax({
			url: "/Admin/GetHouseFacilities",
			data: {
				houseId: viewModel.houseId
			},
			method: 'get'
		}).done(function (data) {
			$('#faciliy-info').html(data);
			$('#facility-tab-loader').hide();
		}).fail(function (a, textError) {
			console.error(textError);
			$('#facility-tab-loader').hide();
		});
	};

	var clearFacilityCheckboxes = function () {
		$('#faciliy-info').find('input:checkbox').removeAttr('checked');
	};
	var initClearFacilityCheckboxes = function () {
		$('#faciliy-info').off().on('click', '#clear-checkbox', function () {
			clearFacilityCheckboxes();
		});
	};
	// ^house facility

	// house restrictions
	var loadRestriction = function () {
		$('#restriction-tab-loader').show();
		$.ajax({
			url: "/Admin/GetHouseRestrictions",
			data: {
				houseId: viewModel.houseId
			},
			method: 'get'
		}).done(function (data) {
			$('#restriction-info').html(data);
			$('#restriction-tab-loader').hide();
		}).fail(function (a, textError) {
			console.error(textError);
			$('#restriction-tab-loader').hide();
		});
	};

	var clearRestrictionCheckboxes = function () {
		$('#restriction-info').find('input:checkbox').removeAttr('checked');
	};
	var initClearRestrictionCheckboxes = function () {
		$('#restriction-info').off().on('click', '#clear-checkbox', function () {
			clearRestrictionCheckboxes();
		});
	};
	// ^house restrictions

	// business processes
	var initAmenitiesProcess = function () {
		initClearCheckboxes();
		loadAmenity();
	};
	var initHouseImagesProcess = function () {
		uploadHouseImage();
		initHouseImageLoader();
	};
	var initHouseGettingAroundProcess = function () {
		loadHouseGettingAround();
		initSaveHouseGettingAround();
	};
	var initHouseTenantProcess = function () {
		loadHouseTenants();
		initTenantDropDown();
		initAddTenant();
		initAddTenantModal();
		initWaitListAddTenant();
		initWaitListTenantDropDown();
		initWaitListAddTenantModal();
	};
	var initHouseFacilityProcess = function () {
		loadFacility();
		initClearFacilityCheckboxes();
	};
	var initHouseRestrictionProcess = function () {
		loadRestriction();
		initClearRestrictionCheckboxes();
	};
	// ^business processes

	var initPage = function () {
		initDatePickers();
		initDropify();
		initFancybox();
		initTooltip();

		// business process
		initAmenitiesProcess();
		initHouseGettingAroundProcess();
		initHouseImagesProcess();
		initHouseTenantProcess();
		initHouseFacilityProcess();
		initHouseRestrictionProcess();
	};

	var init = function (model) {
		console.log("Hrm.AdminEditHouse init()");

		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init,
		loadAmenity: loadAmenity,
		deleteImage: deleteImage,
		setMainImage: setMainImage,
		deleteHouseGettingAround: deleteHouseGettingAround,
		removeTenant: removeTenant,
		loadFacility: loadFacility,
		loadRestriction: loadRestriction
	};
	return oPublic;
}();
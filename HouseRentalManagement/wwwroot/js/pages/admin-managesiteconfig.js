var Hrm = Hrm || {};

Hrm.AdminManageSiteConfig = function () {
	var viewModel;

	var updateUiForSameNumber = function () {
		if ($('#IsWhatasappNumberSameAsPhoneNumber').prop('checked') || $('#IsWhatasappNumberSameAsPhoneNumber').prop('checked') === 'true') {
			$('#WhatsappNumber').val($('#PhoneNumber').val());			
			$('#WhatsappNumber').attr('disabled', 'disabled');
		}
		else {
			$('#WhatsappNumber').val(viewModel.whatsAppNumber);			
			$('#WhatsappNumber').removeAttr('disabled');
		}
	};

	var bindEvents = function () {
		$('#IsWhatasappNumberSameAsPhoneNumber').on('click', function () {
			updateUiForSameNumber();
		});
	};

	var init = function (model) {
		console.log("Hrm.AdminManageSiteConfig init()");
		viewModel = model;

		updateUiForSameNumber();
		bindEvents();
	};

	var oPublic = {
		init: init
	}
	return oPublic;
}();
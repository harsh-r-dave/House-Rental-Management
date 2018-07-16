var Hrm = Hrm || {};

Hrm.SiteJs = function () {

	var initContactLink = function () {
		$('#contact-us-link').on('click', function () {
			if ($('#navbar-collapse-container').hasClass('in')) {
				$('#navbar-collapse-container').removeClass('in');
			}
			setTimeout(function () {
				$('#contact-us-form').find('#AddInquiryViewModel_Name').get(0).focus();
			});			
		});
	};

	var init = function () {
		console.log("Hrm.SiteJs init()");
		initContactLink();
	};

	var oPublic = {
		init: init
	}
	return oPublic;
}();
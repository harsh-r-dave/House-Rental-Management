var Hrm = Hrm || {};

Hrm.HouseIndex = function () {
	var viewModel;

	var bindEvents = function () { };

	//var initFullPage = function () {
	//    $('#main').fullpage({
	//        scrollingSpeed: 1000,
	//        autoScrolling: true,
	//        fitToSection: true,
	//        fitToSectionDelay: 2000
	//    });
	//};

	var initSmoothScroll = function () {
		$('.smooth-scroll').on('click', function () {
			$('html, body').animate({
				scrollTop: $($.attr(this, 'href')).offset().top
			}, 1500);
		});
	};

	var submitInquiry = function () {
		$('#contact-us-form').on('submit', function (e) {
			e.preventDefault();
			if ($('#contact-us-form').valid()) {
				var name = $('#AddInquiryViewModel_Name').val();
				var phone = $('#AddInquiryViewModel_PhoneNumber').val();
				var email = $('#AddInquiryViewModel_Email').val();
				var message = $('#AddInquiryViewModel_Message').val();
				$.ajax({
					url: "/Inquiry/SubmitInquiry",
					method: "post",
					data: {
						Name: name,
						PhoneNumber: phone,
						Email: email,
						Message: message
					}
				}).done(function (result) {
					if (result.success) {
						toastr.success('Your inquiry has been submitted succesfully.', '', Hrm.Toastr.config);
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
					}
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
				});
			}
		});
	};

	var init = function (model) {
		console.log("Hrm.HouseIndex init()");
		viewModel = model;

		//initFullPage();
		initSmoothScroll();
		submitInquiry();
	};

	var oPublic = {
		init: init
	}
	return oPublic;
}();
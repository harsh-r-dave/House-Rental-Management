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

	var setLoader = function () {
		$('#contact-us-loader-container #sending-message').show();
		$('#contact-us-loader-container #message-sent').hide();
		$('#contact-us-loader-container #message-failed').hide();
		$('#contact-us-form').hide();
	};
	var setSent = function () {		
		$('#contact-us-loader-container #sending-message').hide();
		$('#contact-us-loader-container #message-sent').show();
		$('#contact-us-loader-container #message-failed').hide();
		$('#contact-us-form').hide();
	};
	var setFailed = function () {
		$('#contact-us-loader-container #sending-message').hide();
		$('#contact-us-loader-container #message-sent').hide();
		$('#contact-us-loader-container #message-failed').show();
		$('#contact-us-form').hide();
	};
	var clearLoader = function () {
		$('#contact-us-loader-container #sending-message').hide();
		$('#contact-us-loader-container #message-sent').hide();
		$('#contact-us-loader-container #message-failed').hide();
		$('#contact-us-form').show();
	};	

	var submitInquiry = function () {
		$('#contact-us-form').on('submit', function (e) {
			e.preventDefault();
			if ($('#contact-us-form').valid()) {
				setLoader();
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
						setTimeout(function () {
							clearLoader();
							setSent();
						}, 750);						
					}
					else {
						toastr.error(result.error, '', Hrm.Toastr.config);
						setTimeout(function () {
							clearLoader();
							setFailed();
						}, 750);							
					}					
				}).fail(function (jqXHR, textStatus) {
					console.log(textStatus);
					setTimeout(function () {
						clearLoader();
						setFailed();
					}, 750);	
				});
			}
		});
	};

	var validatePhone = function () {
		$("#AddInquiryViewModel_PhoneNumber").keydown(function (e) {
			// Allow: backspace, delete, tab, escape, enter and .
			if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
				// Allow: Ctrl+A, Command+A
				(e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
				// Allow: home, end, left, right, down, up
				(e.keyCode >= 35 && e.keyCode <= 40)) {
				// let it happen, don't do anything
				return;
			}
			// Ensure that it is a number and stop the keypress
			if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
				e.preventDefault();
			}
		});
	};
	var init = function (model) {
		console.log("Hrm.HouseIndex init()");
		viewModel = model;

		//initFullPage();
		initSmoothScroll();
		submitInquiry();
		validatePhone();
	};

	var oPublic = {
		init: init,
		clearLoader: clearLoader
	}
	return oPublic;
}();
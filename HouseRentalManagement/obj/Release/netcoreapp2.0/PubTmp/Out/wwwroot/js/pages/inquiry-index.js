var Hrm = Hrm || {};

Hrm.InquiryIndex = function () {
	var viewModel;

	var initDatatable = function () {
		$('.inquiry-table').dataTable({
			retrieve: true,
			"sDom": '<"top"f>rt<"bottom"lp><"clear">',
			"columnDefs": [{
				"targets": [2],
				"orderable": false
			}]
		});
	};

	var loadUnreadInquiries = function () {
		$('#new-inquiries-tab-loader').show();
		$.ajax({
			url: "/Inquiry/GetUnreadInquiries",
			method: "get",
		}).done(function (result) {
			$('#new-inquiries').html(result);
			initDatatable();
			$('#new-inquiries-tab-loader').hide();
		}).fail(function () {
			toastr.error('Something went wrong while fetching new inquiries', '', Hrm.Toastr.config);
			$('#new-inquiries-tab-loader').hide();
		});
	};

	var loadReadInquiries = function () {
		$('#old-inquiries-tab-loader').show();
		$.ajax({
			url: "/Inquiry/GetReadInquiries",
			method: "get",
		}).done(function (result) {
			$('#old-inquiries').html(result);
			initDatatable();
			$('#old-inquiries-tab-loader').hide();
		}).fail(function () {
			toastr.error('Something went wrong while fetching old inquiries', '', Hrm.Toastr.config);
			$('#old-inquiries-tab-loader').hide();
		});
	};

	var loadMessage = function (id) {
	};
	
	var initPage = function () {
		loadUnreadInquiries();
		loadReadInquiries();
	};

	var init = function (model) {
		console.log("Hrm.InquiryIndex init()");

		viewModel = model;
		initPage();
	};

	var oPublic = {
		init: init,
		loadMessage: loadMessage
	};
	return oPublic;
}();
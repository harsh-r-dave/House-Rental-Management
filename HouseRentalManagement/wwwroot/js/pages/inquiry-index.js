var Hrm = Hrm || {};

Hrm.InquiryIndex = function () {
	var viewModel;
	var table; 

    var initDatatable = function () {
        $.fn.dataTable.moment('MMM DD, YYYY hh:mm A');
		table = $('.inquiry-table').DataTable({
			retrieve: true,
			"sDom": '<"top"f>rt<"bottom"lp><"clear">',
			"columnDefs": [{
				"targets": [-1, -2, -3],
				"orderable": false
			}],
			"order": [0, "desc"]
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
		$.ajax({
			url: '/Inquiry/GetMessage',
			method: 'get',
			data: {
				id: id
			}
		}).done(function (result) {
			if (result.success) {
				$('#message-modal #message-container').html(result.message);
				$('#message-modal #mark-read-button').removeData('message-id');
				$('#message-modal #mark-read-button').attr('data-message-id', id);
				if (!result.isRead) {					
					$('#message-modal #mark-read-button').removeAttr('disabled');
				}
				else {
					$('#message-modal #mark-read-button').attr('disabled', 'disabled');					
				}
				$('#message-modal').modal('show');
			}
			else {
				toastr.error(result.error, '', Hrm.Toastr.config);
			}
		}).fail(function (jqXHR, textStatus) {
			console.log(textStatus);
		});
	};

	var initMarkRead = function () {
		$('#mark-read-button').off().on('click', function () {
			var id = $(this).data('message-id');
			console.log(id);
			$.ajax({
				url: '/Inquiry/MarkRead',
				method: 'post',
				data: {
					id: id
				}
			}).done(function (result) {
				if (result.success) {
					loadUnreadInquiries();
					loadReadInquiries();
					$('#message-modal').modal('hide');
					toastr.success('Inquiry marked as read.', '', Hrm.Toastr.config);
				}
				else {
					toastr.error(result.error, '', Hrm.Toastr.config);
				}
			}).fail(function (jqXHR, textStatus) {
				console.log(textStatus);
			});
		});
	};

	var deleteInquiry = function (id) {
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
					url: '/Inquiry/DeleteInquiry',
					method: 'post',
					data: {
						id: id
					}
				}).done(function (result) {
					if (result.success) {
						var tr = $('#' + id);
						// only remove row from table to persist sort order
						//table.row(tr).remove().draw();
						loadUnreadInquiries();
						loadReadInquiries();
						toastr.success('Inquiry deleted.', '', Hrm.Toastr.config);
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

	var deleteAllUnread = function () {
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
					url: '/Inquiry/DeleteAllUnread',
					method: 'get'
				}).done(function (result) {
					if (result.success) {
						loadUnreadInquiries();
						loadReadInquiries();
						toastr.success('Inquiries deleted.', '', Hrm.Toastr.config);
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

	var deleteAllRead = function () {
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
					url: '/Inquiry/DeleteAllRead',
					method: 'get'
				}).done(function (result) {
					if (result.success) {
						loadUnreadInquiries();
						loadReadInquiries();
						toastr.success('Inquiries deleted.', '', Hrm.Toastr.config);
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

	var initPage = function () {
		loadUnreadInquiries();
		loadReadInquiries();
		initMarkRead();
	};

	var init = function (model) {
		console.log("Hrm.InquiryIndex init()");

		viewModel = model;
		initPage();
	};

	var oPublic = {
		init: init,
		loadMessage: loadMessage,
		deleteInquiry: deleteInquiry,
		refresh: initPage,
		deleteAllUnread: deleteAllUnread,
		deleteAllRead: deleteAllRead
	};
	return oPublic;
}();
var Hrm = Hrm || {};

Hrm.HouseInfo = function () {
	var viewModel;
	var directionsService = new google.maps.DirectionsService();
	var directionsDisplay = new google.maps.DirectionsRenderer({
		preserveViewport: true
	});

	var initMap = function () {
		var mapcode;
		var map;
		var address = viewModel.location;
		var target = document.getElementById('house-location');

		function initialize() {
			mapcode = new google.maps.Geocoder();
			var marker = new google.maps.Marker();
			mapcode.geocode({ 'address': address }, function (results, status) {
				if (status == google.maps.GeocoderStatus.OK) {
					//// set college marker
					//var collegeMarker = new google.maps.Marker();
					//mapcode.geocode({ 'address': "1 Georgian Dr, Barrie, ON L4M 3X9" }, function (results, status) {
					//	if (status == google.maps.GeocoderStatus.OK) {
					//		map.setCenter(results[0].geometry.location);
					//		collegeMarker.setMap(map);
					//		collegeMarker.setPosition(results[0].geometry.location);
					//		collegeMarker.setTitle("Georgian College");
					//		collegeMarker.setVisible(true);
					//	}
					//});

					// set house marker
					map.setCenter(results[0].geometry.location);
					marker.setMap(map);
					//marker.setPosition(results[0].geometry.location);
					//marker.setTitle(address);
					//marker.setVisible(true);					

					//display map when is ready
					$('#house-location').show(800);

					// draw map
					//calcRoute(address, "1 Georgian Dr, Barrie, ON L4M 3X9");
				} else {
					$('#house-location').html('<span>Location not available</span>');
					setTimeout(function () {
						$('#house-location').hide(800);
					}, 4000)
				}
			});
			var mapChoice = {
				zoom: 13,
				mapTypeId: google.maps.MapTypeId.ROADMAP
			}
			map = new google.maps.Map(target, mapChoice);
			directionsDisplay.setMap(map);
			calcRoute(address, "1 Georgian Dr, Barrie, ON L4M 3X9");
		}
		google.maps.event.addDomListener(window, 'load', initialize);
	};
	
	function calcRoute(start, end) {
		var request = {
			origin: start,
			destination: end,
			travelMode: 'TRANSIT',
			provideRouteAlternatives: true
		};
		directionsService.route(request, function (result, status) {
			console.log(result);
			if (status == 'OK') {
				directionsDisplay.setDirections(result);
			}
		});
	}

	var initFancyBox = function () {
		$('a.house-image').fancybox();
	};

	var initPage = function () {
		initMap();
		initFancyBox();
	};

	var init = function (model) {
		console.log("Hrm.HouseInfo init()");
		viewModel = model;

		initPage();
	};

	var oPublic = {
		init: init
	}
	return oPublic;
}();
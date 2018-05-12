var Hrm = Hrm || {};

Hrm.HouseInfo = function () {
    var viewModel;

    var initMap = function () {
        var mapcode;
        var map;
        var address = viewModel.location;
        var target = document.getElementById('house-location');

        function initialize() {
            mapcode = new google.maps.Geocoder();
            var marker = new google.maps.Marker();
            mapcode.geocode({ 'address': address }, function (results, status) {
                console.log(status);
                if (status == google.maps.GeocoderStatus.OK) {
                    map.setCenter(results[0].geometry.location);
                    marker.setMap(map);
                    marker.setPosition(results[0].geometry.location);
                    marker.setTitle(address);
                    marker.setVisible(true);
                } else {
                    $('#house-location').html('<span>Location not available</span>');
                    setTimeout(function () {
                        $('#house-location').hide(800);
                    }, 4000)
                }
            });
            var mapChoice = {
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            map = new google.maps.Map(target, mapChoice);
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    };

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
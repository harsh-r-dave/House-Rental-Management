var Hrm = Hrm || {};

Hrm.HouseIndex = function () {
    var viewModel;

    var bindEvents = function () { };

    var initFullPage = function () {
        $('#main').fullpage({
            scrollingSpeed: 1000,
            autoScrolling: true,
            fitToSection: true,
            fitToSectionDelay: 2000
        });
    };

    var init = function (model) {
        console.log("Hrm.HouseIndex init()");
        viewModel = model;

        initFullPage();
    };

    var oPublic = {
        init: init
    }
    return oPublic;
}();
var Hrm = Hrm || {};

Hrm.HouseInfo = function () {
	var viewModel;

	var initFancyBox = function () {
		$('a.house-image').fancybox();
		$('a.image').fancybox();
	};

	var initSlickCarousel = function () {
		$('#house-images-container').slick({
			autoplay: true,
			autoplaySpeed: 5000,
			variableWidth: true,
			centerMode: true,
			centerPadding: '60px',
			slidesToShow: 3,
			pauseOnFocus: false,
			focusOnSelect: true,
			prevArrow: '<span class="slick-custom-nav slick-left-container"><i class="fa fa-arrow-circle-left slick-custom-left"></i></span>',
			nextArrow: '<span class="slick-custom-nav slick-right-container"><i class="fa fa-arrow-circle-right slick-custom-right"></i></span>'
		});

		$('#house-images-container').on('afterChange', function (event, slick, currentSlide) {
			var slideHtml = $(slick.$slides.get(currentSlide)).html();
			var image = $('img', slideHtml);
			var imageSrc = $(image).attr('src');
			$('#main-image-anchor').attr('href', imageSrc);
			$('#main-image').attr('src', imageSrc);
		});
	};

	var initPage = function () {
		initFancyBox();
		initSlickCarousel();
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
$(document).ready(function () {

    $(".signin").click(function (e) {
        e.preventDefault();
        $("nav#signin_menu").fadeToggle(300);
        $(".signin").toggleClass("menu-open");
    });

    $("nav#signin_menu").mouseup(function () {
        return false;
    });
    $(document).mouseup(function (e) {
        if ($(e.target).parent("a.signin").length == 0) {
            $(".signin").removeClass("menu-open");
            $("nav#signin_menu").hide();
        }
    });

});
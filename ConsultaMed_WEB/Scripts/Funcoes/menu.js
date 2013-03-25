$(
    function () {

        $('.menu li').hover(
            function () {
                $('ul', this).slideDown(200);
            },
        function () {
            $('ul', this).hide();
        });
    });
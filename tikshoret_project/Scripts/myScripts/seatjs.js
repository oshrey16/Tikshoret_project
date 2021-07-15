window.addEventListener('load',
    function () {
        createseating();
    }, false);
//Note:In js the outer loop runs first then the inner loop runs completely so it goes o.l. then i.l. i.l .i.l .i.l. i.l etc and repeat

function createseating() {
    var seatingValue = [];
    for (var i = 0; i < 10; i++) {

        for (var j = 0; j < 10; j++) {
            var seatingStyle = "<div class='seat available " + i + "," + j + "'></div>";
            seatingValue.push(seatingStyle);

            if (j === 9) {

                var seatingStyle = "<div class='clearfix'></div>";
                seatingValue.push(seatingStyle);
            }
        }
    }

    $('#messagePanel').html(seatingValue);

    $(function () {
        $('.seat').on('click', function () {

            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
            } else {
                $(this).addClass("selected");
                //Add Here
                var index = $(this).attr("class").match(/\d+/g);
                var row = index[0];
                var col = index[1];
                var f = {};
                f.url = '/Home/Test';
                f.type = "POST";
                f.dataType = "json";
                f.data = JSON.stringify({ Row: row , Col: col });
                f.contentType = "application/json";
                f.success = function (response) {

                    alert("success");
                };
                f.error = function (response) {
                    alert("faild");
                    alert(response.status);
                };
                $.ajax(f);
                //TO HERE
            }

        });

        $('.seat').mouseenter(function () {
            $(this).addClass("hovering");

            $('.seat').mouseleave(function () {
                $(this).removeClass("hovering");

            });
        });
    });

};


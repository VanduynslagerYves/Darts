var view =
    {
        init: function () {

            var numberOfPlayers = $("#count").val();
            for (var i = 1; i <= numberOfPlayers; i++)
            {
                var detail = $("<a>").text("Detail")
                    .attr("href", "")
                    .attr("id","detail" + i);

                $(detail).click(function () {
                    view.getDetail(i);
                });

                $("#" + i).append(detail);
            }
            
        },
        getDetail: function (teller){
            $.get("Darts/Detail/", { id: teller },
                function (data) {
                    $("tr:first")
                        .append($("<td>").append($("<div>").html(data)))
                });
        }
    };

$(function () {
    view.init();
});
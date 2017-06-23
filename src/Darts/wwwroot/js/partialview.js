function SpelerWedstrijd(datum, puntengewonnen, puntenverloren, tegenstander)
{
    this.datum = datum;
    this.puntengewonnen = puntengewonnen;
    this.puntenverloren = puntenverloren;
    this.tegenstander = tegenstander
}

var view = {
    spelerwedstrijden = null,
    init: function () {
        $(".Detail").click(function () {
            view.getWedstrijden(this);
            return false;
        });
    },
    getWedstrijden: function (link)
    {
        $.get(link.href, function (data) {
            view.spelerwedstrijden = $.map(data, function (item) {
                return new SpelerWedstrijd(item.datum, item.puntengewonnen, item.puntenverloren, item.tegenstander)
            });
            view.toHtml();
        });
    },
    toHtml: function ()
    {
        var $tabel = $("#test")
        for (var sw in view.spelerwedstrijden)
        {
            var $row = $("<tr>");
            $row.append($("<td>").text(sw.datum));
            $row.append($("<td>").text(sw.puntengewonnen));
            $row.append($("<td>").text(sw.puntenverloren));
            $row.append($("<td>").text(sw.tegenstander));
            $tabel.append($row);
        }
    }
}
function resizeCanvas() {
    var htmlCanvas = document.getElementById('map')
    htmlCanvas.width = window.innerWidth;
    htmlCanvas.height = window.innerHeight;
}

function keyShortcuts(e) {
    // Hide the action pane
    if (e.charCode == 104 || e.charCode == 72) {
        $("#action").hide("slow");
    }
    // Show the action pane
    if (e.charCode == 115 || e.charCode == 83) {
        $("#action").show("slow");
    }
    // Change speed of the map with the + and -
    if (e.charCode == 43 || e.charCode == 45) {
        var x = (e.charCode == 43) ? 2 : -2;
        $("#speed").val(parseInt($("#speed").val()) + x)
        $("#speed").change()
    }
}

$(window).load(function () {
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas, false);
    $(document).keypress(keyShortcuts);

    var hash = window.location.hash.replace("#", "");
    if ($.isNumeric(hash)) $("#count").val(hash);

    $("#back").click(function () { Map.changePos(-1); });
    $("#forw").click(function () { Map.changePos(1); });
    $("#speed").change(Map.changeSpeed);
    $("#count").change(Map.changeCount);
    Map.init();
});

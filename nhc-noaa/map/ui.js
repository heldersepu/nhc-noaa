function resizeCanvas() {
    var htmlCanvas = document.getElementById('map')
    htmlCanvas.width = window.innerWidth;
    htmlCanvas.height = window.innerHeight;
}

function hideActions() {
    $("#actions").children().hide();
    $("#actions").animate({width: '40px', height: '40px', 'border-radius': '25px' });
}

function showActions() {
    $("#actions").animate({width: '95%', height: '200px', 'border-radius': '0px' });
    $("#actions").children().show();
}

function timeoutActions() {
    if ($("#loader").css('display') == 'none') {
        hideActions();
    }
}

function actionsClick() {
    if ($("#controls").css('display') == 'none') {
        showActions();
    }
}

function keyShortcuts(e) {
    // Hide the actions pane
    if (e.charCode == 104 || e.charCode == 72) {
        hideActions();
    }
    // Show the actions pane
    if (e.charCode == 115 || e.charCode == 83) {
        showActions();
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

    setTimeout(timeoutActions, 2000);
    var hash = window.location.hash.replace("#", "");
    if ($.isNumeric(hash)) $("#count").val(hash);

    $("#actions").click(actionsClick)
    $("#back").click(function () { Map.changePos(-1); });
    $("#forw").click(function () { Map.changePos(1); });
    $("#speed").change(Map.changeSpeed);
    $("#count").change(Map.changeCount);
    Map.init();
});

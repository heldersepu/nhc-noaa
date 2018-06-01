var map;
var activityTimeout;
var htmlCanvas = document.getElementById('map');

function resizeCanvas() {
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
    startTimeout(9000);
}

function timeoutActions() {
    if ($("#loader").css('display') == 'none') {
        hideActions();
    } else {
        startTimeout();
    }
}

function startTimeout(miliSecs) {
    miliSecs = miliSecs || 3000;
    clearTimeout(activityTimeout);
    activityTimeout = setTimeout(timeoutActions, miliSecs);
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

function moveBack() {
    startTimeout();
    map.changePos(-1);
}

function moveForw() {
    startTimeout();
    map.changePos(1);
}

function changeSpeed() {
    startTimeout();
    map.changeSpeed();
}

function changeCount() {
    startTimeout();
    map.changeCount();
}

$(window).load(function () {
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas, false);

    var hash = window.location.hash.replace("#", "");
    if ($.isNumeric(hash)) $("#count").val(hash);

    $("#actions").click(actionsClick)
    $("#back").click(moveBack);
    $("#forw").click(moveForw);
    $("#speed").change(changeSpeed);
    $("#count").change(changeCount);
    $(document).keypress(keyShortcuts);

    map = new Map(htmlCanvas);
    startTimeout();
});

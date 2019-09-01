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
    if ($(".lds-roller").css('display') === 'none') {
        hideActions();
    } else {
        $("#images").children().click(imageClick);
        startTimeout();
    }
}

function startTimeout(miliSecs) {
    if (!$.isNumeric(miliSecs)) miliSecs = 3000;
    clearTimeout(activityTimeout);
    activityTimeout = setTimeout(timeoutActions, miliSecs);
}

function actionsClick() {
    if ($("#controls").css('display') === 'none') {
        showActions();
    }
}

function keyShortcuts(e) {
    // (Space) Pauses/Restarts the map
    if (e.charCode === 32) {
        map.togglePause();
    }
    // (H) Hides the actions pane
    if (e.charCode === 104 || e.charCode === 72) {
        hideActions();
    }
    // (S) Shows the actions pane
    if (e.charCode === 115 || e.charCode === 83) {
        showActions();
        clearTimeout(activityTimeout);
    }
    // (+ and -) Changes speed of the map
    if (e.charCode === 43 || e.charCode === 45) {
        var x = e.charCode === 43 ? 2 : -2;
        $("#speed").val(parseInt($("#speed").val()) + x);
        $("#speed").change();
    }
    // (A) Goes to the Previous image
    if (e.charCode === 97 || e.charCode === 65) {
        map.changePos(-1);
    }
    // (D) Goes to the next image
    if (e.charCode === 100 || e.charCode === 68) {
        map.changePos(1);
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

function imageClick(e) {
    startTimeout();
    var i = e.target.id.replace("img", "") - 1000;
    map.setImage(i);
}

$(window).load(function () {
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas, false);

    var hash = window.location.hash.replace("#", "");
    if ($.isNumeric(hash)) $("#count").val(hash);

    $("#actions").click(actionsClick);
    $("#back").click(moveBack);
    $("#forw").click(moveForw);
    $("#speed").change(changeSpeed);
    $("#count").change(changeCount);
    $("#count").keypress(startTimeout);
    $(document).keypress(keyShortcuts);

    map = new Map(htmlCanvas);
    startTimeout();
});

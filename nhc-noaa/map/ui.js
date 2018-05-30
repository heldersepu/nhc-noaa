function resizeCanvas() {
    var htmlCanvas = document.getElementById('map')
    htmlCanvas.width = window.innerWidth;
    htmlCanvas.height = window.innerHeight;
}

function keyShortcuts(e) {
    console.log(e.charCode)
    if(e.charCode == 103) {
      // Your Code
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

function resizeCanvas() {
    var htmlCanvas = document.getElementById('map')
    htmlCanvas.width = window.innerWidth;
    htmlCanvas.height = window.innerHeight;
}

$(window).load(function () {
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas, false);

    var hash = window.location.hash.replace("#", "");
    if ($.isNumeric(hash)) $("#count").val(hash);
    
    $("#back").click(function () { Map.changePos(-1); });
    $("#forw").click(function () { Map.changePos(1); });
    $("#speed").change(Map.changeSpeed);
    $("#count").change(Map.changeCount);
    Map.init();
});

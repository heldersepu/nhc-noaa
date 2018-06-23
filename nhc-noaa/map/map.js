function Map(htmlCanvas) {

    var canvas = htmlCanvas.getContext("2d");
    var interval = setInterval(changeImage, 100);
    var count = 0;
    var images = [];
    var cdn = "http://nhc0.azureedge.net";
    changeCount();

    function loading() {
        $("#loader").show();
        $("#controls input").prop("disabled", true);
    }

    function loaded() {
        var notCached = 0;
        for (var i = 0; i < images.length; i++) {
            if (!isCached(images[i], i))
                notCached++;
            if (notCached > 1)
                break;
        }
        if (notCached > 0) {
            setTimeout(loaded, 2000);
        } else {
            $("#loader").fadeOut("slow");
            $("#controls input").prop("disabled", false);
        }
    }

    function changeCount() {
        var strCount = $("#count").val();
        var intCount = 100;
        if ($.isNumeric(strCount)) {
            history.pushState(null, null, "#" + strCount);
            intCount = parseInt(strCount);
        } else {
            $("#count").val(intCount);
        }
        getImages(intCount);
    }

    function getImages(intCount) {
        loading();
        $.ajax({
            type: "GET",
            url: "/api/Images/EastAtlantic?count=" + intCount,
            cache: false,
            success: successFunc,
            error: errorFunc
        });
    }

    function imgSrc(image, id) {
        return cdn.replace("0", id % 10) + "/api/Image?name=" + image;
    }

    function sprite(image, id) {
        var imgTag = "<img class='sprite' id='sprite" + id + "' " +
            "style='background:url(" + imgSrc(image, id) + ") -560px -1052px'><br />";
        $("#data").append(imgTag);
    }

    function stopImageLoop() {
        clearInterval(interval);
        interval = false;
    }

    function successFunc(data) {
        images = data;
        images.reverse();
        $("#data").empty();
        stopImageLoop();
        sprite(images[0], 0);
        sprite(images[images.length - 1], images.length - 1);
        addAllImages();
        count = 0;
    }

    function errorFunc(err) {
        if (err.responseText === "")
            alert("Sorry the communication failed.");
        else
            alert(err.responseText);
    }

    function changePos(pos) {
        count += pos;
        stopImageLoop();
        showImage();
    }

    function showImage() {
        if (count < 0) count = images.length - 1;
        if (count >= images.length) count = 0;
        var img = document.getElementById("img" + (count + 1000));
        canvas.drawImage(img, 0, 0);
        $(".active").removeClass("active");
        $("#img" + (count + 1000)).addClass("active");
    }

    function changeImage() {
        if (images.length > 1) {
            showImage();
            count += 1;
        }
    }

    function setImage(x) {
        stopImageLoop();
        count = x;
        showImage();
    }

    function appendImage(i) {
        var imageTag = "<img id='img" + (i + 1000) + "' >";
        $("#images").append(imageTag);
    }

    function updateImage(i) {
        var image = $("#img" + (i + 1000));
        image.attr("src", imgSrc(images[i], i));
        image.attr("title", images[i]);
        if (i + 1 === images.length) {
            changeSpeed();
            loaded();
        } else if (i === 0) {
            var img = document.getElementById("img1000");
            canvas.drawImage(img, 0, 0);
        }
    }

    function isCached(src, i) {
        var image = new Image();
        image.src = imgSrc(src, i);
        return image.complete;
    }

    function getTitles(x, y) {
        return y.attributes.title.value;
    }

    function addAllImages() {
        var imgs = $("#images img").map(getTitles);
        $("#images").empty();
        for (var i = 0; i < images.length; i++) {
            appendImage(i);
            var delay = i * 100;
            if ($.inArray(images[i], imgs) > 0 || isCached(images[i], i))
                delay = i * 8;
            setTimeout(updateImage.bind(null, i), delay);
        }
    }

    function changeSpeed() {
        stopImageLoop();
        interval = setInterval(changeImage, $("#speed").val());
    }

    function togglePause() {
        if (interval)
            stopImageLoop();
        else
            interval = setInterval(changeImage, $("#speed").val());
    }

    return {
        togglePause: togglePause,
        changeCount: changeCount,
        changeSpeed: changeSpeed,
        changePos: changePos,
        setImage: setImage
    };
}

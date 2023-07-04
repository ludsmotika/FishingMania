function previewImage(event) {
    var input = event.target;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var imagePreview = document.getElementById("imagePreview");
            imagePreview.src = e.target.result;
            imagePreview.style.display = "block";
        };

        reader.readAsDataURL(input.files[0]);
    }
}
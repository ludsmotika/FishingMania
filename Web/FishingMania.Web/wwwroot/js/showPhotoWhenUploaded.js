function previewImage(event) {
    var input = event.target;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {

            var fileInput = document.getElementById("inputFieldForImage");
            var errorMessage = document.getElementById("imageValidation");
            var previewImage = document.getElementById("imagePreview");
            var maxSize = 10000000; // 10 MB in bytes


            if (fileInput.files.length > 0) {
                var fileSize = fileInput.files[0].size;
                if (fileSize > maxSize) {
                    errorMessage.textContent = "File size exceeds the maximum allowed (10 MB).";
                    fileInput.value = '';
                    previewImage.value = '';
                    return false;
                }
                else {
                    imagePreview.src = e.target.result;
                    imagePreview.style.display = "block";
                    errorMessage.textContent = "";
                    return true;
                }
            }

            errorMessage.textContent = "";
            return true;

        };

        reader.readAsDataURL(input.files[0]);
    }
}

//#region Image Preview

// کد فعال
function showPreview(input) {
    if (input.files && input.files[0]) {

        var ImageDir = new FileReader();

        ImageDir.onload = function (e) {
            $('#imgPrev').attr('src', e.target.result);
        }

        ImageDir.readAsDataURL(input.files[0]);
    }
}


// کد دوم غیرفعال
/*
function showPreview(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();

        reader.onload = function (e) {
            document.getElementById("imgPrev").src = e.target.result;
        };

        reader.readAsDataURL(input.files[0]);
    }
}
*/

//#endregion
// کد ویرایشگر


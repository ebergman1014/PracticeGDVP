var admin = new function () {
    var self = this;
    this.displayMessage = function (msg, color) {
        $("#message").text(msg).css('color', color).show();
        setTimeout(function () { $("#message").fadeOut(); }, 2000);
    }
    this.submitDiscountForm = function(){
        $.post("", $("#storeForm").serialize(), function (data) {
            if (data.DiscountRate == $("#DiscountRate").val()) {
                self.displayMessage("Store has been updated successfully!", "green");
            }else{
                self.displayMessage("Store could not be updated.", "red");
            }
        },"json");
    }


    $(document).ready(function () {
        $("#submitDiscount").on('click', function () {
            if ($("#storeForm").valid()) {
                self.submitDiscountForm();
            }
        });
    });
}
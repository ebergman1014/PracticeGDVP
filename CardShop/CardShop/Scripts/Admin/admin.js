var admin = new function () {
    var self = this;

    this.submitDiscountForm = function(){
        $.post("",$("#discountForm").serialize(),function(){
        },"json");
    }


    $(document).ready(function () {
        $("#discountForm > input#submitDiscount").on('click', function () {
            self.submitDiscountForm();
        });
    });
}